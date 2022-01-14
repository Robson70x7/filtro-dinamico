using FiltrDinamico.Core.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace FiltrDinamico.Core.Interpreters
{
    internal class OrInterpreter<TType> : IFilterTypeInterpreter<TType>
    {
        private readonly IFilterTypeInterpreter<TType> _leftInterpreter;
        private readonly IFilterTypeInterpreter<TType> _rightInterpreter;

        public OrInterpreter(IFilterTypeInterpreter<TType> leftInterpreter, IFilterTypeInterpreter<TType> rightInterpreter)
        {
            _leftInterpreter = leftInterpreter;
            _rightInterpreter = rightInterpreter;
        }

        public Operator Operator { 
            get => _leftInterpreter.Operator;
            set { this.Operator = _leftInterpreter.Operator; }
        }

        public Expression<Func<TType, bool>> Interpret()
        {
            var leftExpression = _leftInterpreter.Interpret(); //LambaExpression Left
            var rightExpression = Expression.Invoke(_rightInterpreter.Interpret(), leftExpression.Parameters.FirstOrDefault());

            var andAlsoExpression = Expression.OrElse(leftExpression.Body, rightExpression);

            return Expression.Lambda<Func<TType, bool>>(andAlsoExpression, leftExpression.Parameters.FirstOrDefault());
        }
    }
}
