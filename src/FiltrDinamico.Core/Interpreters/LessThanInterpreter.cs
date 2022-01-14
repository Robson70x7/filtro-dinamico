using FiltrDinamico.Core.Models;
using System.Linq.Expressions;

namespace FiltrDinamico.Core.Interpreters
{
    internal class LessThanInterpreter<TType> : FilterTypeInterpreter<TType>
    {
        public LessThanInterpreter(FiltroItem filtroItem, Operator @operator) : base(filtroItem, @operator)
        {
        }

        internal override Expression CreateExpression(MemberExpression property, ConstantExpression constant)
            => Expression.LessThan(property, constant);
    }
}
