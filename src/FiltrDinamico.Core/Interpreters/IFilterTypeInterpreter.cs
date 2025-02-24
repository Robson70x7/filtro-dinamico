﻿using FiltrDinamico.Core.Models;
using System;
using System.Linq.Expressions;

namespace FiltrDinamico.Core.Interpreters
{
    public interface IFilterTypeInterpreter<TType>
    {
        Expression<Func<TType, bool>> Interpret();
        Operator Operator { get; set; }
    }
}
