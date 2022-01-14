using FiltrDinamico.Core.Models;
using System;

namespace FiltrDinamico.Core.Interpreters
{
    internal class FilterInterpreterFactory : IFilterInterpreterFactory
    {
        public IFilterTypeInterpreter<TType> Create<TType>(FiltroItem filtroItem, Operator @operator)
        {
            switch (filtroItem.FilterType)
            {
                case FilterTypeConstants.Equals:
                    return new EqualsInterpreter<TType>(filtroItem, @operator);
                case FilterTypeConstants.Contains:
                    return new ContainsInterpreter<TType>(filtroItem, @operator);
                case FilterTypeConstants.GreaterThan:
                    return new GreaterThanInterpreter<TType>(filtroItem, @operator);
                case FilterTypeConstants.LessThan:
                    return new LessThanInterpreter<TType>(filtroItem, @operator);
                case FilterTypeConstants.StartsWith:
                    return new StartsWithInterpreter<TType>(filtroItem, @operator);

                default:
                    throw new ArgumentException($"the filter type {filtroItem.FilterType} is invalid");
            }
        }
    }
}
