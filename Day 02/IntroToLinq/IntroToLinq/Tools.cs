using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IntroToLinq
{
    public static class Tools
    {
        //public static int HowManyTimes<T>(this T start, T target, Func<T, T> step)
        //    where T: IEquatable<T>
        //{
        //    var counter = 0;
        //    var current = start;

        //    while (!current.Equals(target))
        //    {
        //        counter++;
        //        current = step(current);
        //    }

        //    return counter;            
        //}

        //public static IEnumerable<K> Map<T, K>(this IEnumerable<T> source, Func<T, K> projection)
        //{
        //    foreach (var item in source)
        //    {
        //        var translated = projection(item);
        //        yield return translated;
        //    }
        //}

        //public static IEnumerable<K> TakeFirst<K>(this IEnumerable<K> source, int number)
        //{
        //    var counter = 0;

        //    foreach (var item in source)
        //    {
        //        counter++;
        //        if (counter > number) yield break;

        //        yield return item;
        //    }
        //}
    }
}
