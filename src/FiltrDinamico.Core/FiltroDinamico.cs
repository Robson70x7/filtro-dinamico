using FiltrDinamico.Core.Extensions;
using FiltrDinamico.Core.Interpreters;
using FiltrDinamico.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FiltrDinamico.Core
{
    public class FiltroDinamico : IFiltroDinamico
    {
        private readonly IFilterInterpreterFactory _factory;

        public FiltroDinamico(IFilterInterpreterFactory factory)
        {
            _factory = factory;
        }

        public Expression<Func<TType, bool>> FromFiltroItemList<TType>(IReadOnlyList<FiltroOperatoGrouped> filtroItems)
        {
            var dados = filtroItems
                .SelectMany(s => s.FiltroItems.Select(i => new { FiltroItem = i, s.Operator }))
                .Select(filtro => _factory.Create<TType>(filtro.FiltroItem, filtro.Operator))
                .GroupBy(s => ((FilterTypeInterpreter<TType>)s)._operator).ToList();

            var expression = dados.Select(grupoOperator =>

                grupoOperator.Aggregate((curr, next) =>
                {
                    var interpreter = (FilterTypeInterpreter<TType>)curr;

                    return interpreter._operator == Operator.AND ?
                    interpreter.And(next) :
                    interpreter.Or(next);
                })
            ).Select(s => s.Interpret()).ToArray();

            if (expression.Count() > 1)
            {
                var unionExpression = Expression.AndAlso(expression[0].Body, expression[1].Body);

                return Expression.Lambda<Func<TType, bool>>(unionExpression, expression[0].Parameters.FirstOrDefault());
            }

            return expression[0];


        }
    }
}
