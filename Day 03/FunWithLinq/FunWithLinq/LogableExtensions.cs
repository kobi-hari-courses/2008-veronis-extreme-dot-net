using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FunWithLinq
{
    public static class LogableExtensions
    {
        public static ILogable<K> Select<T, K>(this ILogable<T> source, Expression<Func<T, K>> expr)
        {
            return new Logable<K>(source.Lines, "Select" + expr.ToString());
        }

        public static ILogable<T> Where<T>(this ILogable<T> source, Expression<Func<T, bool>> predicate)
        {
            return new Logable<T>(source.Lines, "Where" + predicate.ToString());
        }
    }
}
