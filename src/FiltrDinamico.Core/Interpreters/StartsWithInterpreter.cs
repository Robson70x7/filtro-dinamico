using FiltrDinamico.Core.Models;
using System.Linq.Expressions;

namespace FiltrDinamico.Core.Interpreters
{
    public class StartsWithInterpreter<TType> : FilterTypeInterpreter<TType>
    {
        public StartsWithInterpreter(FiltroItem filtroItem, Operator @operator) : base(filtroItem, @operator)
        {
        }

        internal override Expression CreateExpression(MemberExpression property, ConstantExpression constant)
        {
            var method = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });
            return Expression.Call(property, method, constant);
        }
    }
}
