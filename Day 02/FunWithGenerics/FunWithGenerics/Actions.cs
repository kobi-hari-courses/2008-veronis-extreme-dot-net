using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    public static class Actions
    {
        public static void LogEquality<T>(T first, T second)
            where T: struct, IEquatable<T>
        {
            if (first.Equals(second))
            {
                Console.WriteLine("Equals");
            } else
            {
                Console.WriteLine("Not equals");
            }
        }

        public static T Factory<T>()
            where T: new()
        {
            return new T();
        }

        public static void CheckForNull<T>(T arg)
            where T : class
        {
            if (arg == null)
            {
                Console.WriteLine("Null");
            } else
            {
                Console.WriteLine("Not null");
            }
        }

        public static T? CreateNullableType<T>()
            where T: struct
        {
            T? val = null;
            return val;
        }
    }
}
