using System;
using System.Collections.Generic;
using System.Linq.Expressions;
public class OrderByMap<D, T> : Dictionary<D, Expression<Func<T, object>>> where D : Enum
    {
    }