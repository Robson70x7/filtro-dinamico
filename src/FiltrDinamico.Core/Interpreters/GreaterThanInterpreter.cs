using FiltrDinamico.Core.Models;
using System.Linq.Expressions;

namespace FiltrDinamico.Core.Interpreters
{
    public class GreaterThanInterpreter<TType> : FilterTypeInterpreter<TType>
    {
        public GreaterThanInterpreter(FiltroItem filtroItem, Operator @operator) : base(filtroItem, @operator)
        {
        }

        internal override Expression CreateExpression(MemberExpression property, ConstantExpression constant) 
            => Expression.GreaterThan(property, constant);
    }
}
