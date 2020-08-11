using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumerablesApp
{
    class Program
    {
        public static IEnumerable<int> Fibonacci(int count)
        {
            int i = 1;
            int j = 1;
            int counter = 0;

            yield return i;
            yield return j;

            counter += 2;
            while(counter < count)
            {
                var current = i + j;
                yield return current;
                i = j;
                j = current;
                counter++;
            }
        }

        static void Main(string[] args)
        {
            var enumerable = Fibonacci(100);

            //var e = enumerable.GetEnumerator();

            //while(e.MoveNext())
            //{
            //    var num = e.Current;
            //    Console.WriteLine(num);
            //}

            //e.Dispose();

            foreach (var num in enumerable)
            {
                Console.WriteLine(num);
            }

            Console.ReadLine();
        }
    }
}
