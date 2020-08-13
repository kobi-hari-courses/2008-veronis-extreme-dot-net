using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunWithGenerics
{
    // Invariant
    public interface IFoo<T>
    {

    }

    public class Foo<T>: IFoo<T>
    {
        private static int _counter = 0;

        static Foo()
        {
            Console.WriteLine("Another static version of Foo is created");
        }

        public T Value { get; set; }
       
        public int OrdinalIndex { get; }

        public Foo(T value)
        {
            Value = value;
            OrdinalIndex = ++_counter;
        }
    }
}
