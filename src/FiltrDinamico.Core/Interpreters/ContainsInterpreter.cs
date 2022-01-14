using FiltrDinamico.Core.Models;
using System.Linq.Expressions;

namespace FiltrDinamico.Core.Interpreters
{
    internal class ContainsInterpreter<TType> : FilterTypeInterpreter<TType>
    {
        public ContainsInterpreter(FiltroItem filtroItem, Operator @operator) : base(filtroItem, @operator)
        {
        }

        internal override Expression CreateExpression(MemberExpression property, ConstantExpression constant)
        {
            var method = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });

            return Expression.Call(property, method, constant);
        }
    }
}
