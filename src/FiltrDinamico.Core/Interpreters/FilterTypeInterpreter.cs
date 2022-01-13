﻿using FiltrDinamico.Core.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FiltrDinamico.Core.Interpreters
{
    public abstract class FilterTypeInterpreter<TType> : IFilterTypeInterpreter<TType>
    {
        public FiltroItem _filtroItem;
        public Operator _operator;

        public FilterTypeInterpreter(FiltroItem filtroItem, Operator @operator)
        {
            _filtroItem = filtroItem;
            _operator = @operator;
        }

        public Expression<Func<TType, bool>> Interpret()
        {
            var dynamicType = typeof(TType);
            var parameter = Expression.Parameter(dynamicType, dynamicType.Name.First().ToString());
            var property = Expression.Property(parameter, _filtroItem.Property);
            var propertyInfo = (PropertyInfo)property.Member;
            var value = Convert.ChangeType(_filtroItem.Value.ToString(), propertyInfo.PropertyType);
            var constant = Expression.Constant(value);
            var expression = CreateExpression(property, constant);

            return Expression.Lambda<Func<TType, bool>>(expression, parameter);
        }

        internal abstract Expression CreateExpression(MemberExpression property, ConstantExpression constant);
    }
}
