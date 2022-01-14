using FiltrDinamico.Core.Extensions;
using FiltrDinamico.Core.Interpreters;
using FiltrDinamico.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FiltrDinamico.Core
{
    internal class FiltroDinamico : IFiltroDinamico
    {
        private readonly IFilterInterpreterFactory _factory;

        public FiltroDinamico(IFilterInterpreterFactory factory)
        {
            _factory = factory;
        }

        public Expression<Func<TType, bool>> FromFiltroItemList<TType>(IReadOnlyList<FiltroOperatoGrouped> filtroItems)
        {
            var dados = filtroItems
                .SelectMany(s => s.FiltroItems.Select(i => new { FiltroItem = i, s.Operator })) //Cria um objeto contendo todos os filtros e com sua respectiva função.
                .Select(filtro => _factory.Create<TType>(filtro.FiltroItem, filtro.Operator)) //Converte o objeto para a Typeconerter
                .GroupBy(s => ((FilterTypeInterpreter<TType>)s).Operator).ToList(); // Agrupo os items por tipo de operação

            //Cria a LambdaExpression individual de cada grupo de operação
            var expression = dados.Select(grupoOperator =>

                grupoOperator.Aggregate((curr, next) => 
                    curr.Operator == Operator.AND ?
                    curr.And(next) :
                    curr.Or(next))

            ).Select(s => s.Interpret()).ToArray();

            //Caso tenho mais de uma operação cria uma agregação criando uma nova expression com todas operações.
            if (expression.Count() > 1)
            {
                return expression.Aggregate((curr, next) => {
                    var unionExpression = Expression.AndAlso(curr.Body, next.Body);
                    return Expression.Lambda<Func<TType, bool>>(unionExpression, expression[0].Parameters.FirstOrDefault());
                });
            }

            return expression[0];
        }
    }
}
