using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IntroToLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            //var i = 10;
            //var count = i.HowManyTimes(20, n => n + 1);

            //var countString = "Hello".HowManyTimes("Hellooooooo", t => t + 'o');

            //Console.WriteLine(countString);
            //Console.ReadLine();

            var items = RandomNumbers(100)
                    .Zip(RandomNumbers(200), (i, j) => (i, j))
                    .Take(40)
                    .OrderBy(pair => pair.i)
                    .ThenBy(pair => pair.j);


            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("--------------------------------------------");

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

        }


        static IEnumerable<int> RandomNumbers(int seed)
        {
            var rand = new Random(seed);

            while(true)
            {
                yield return rand.Next(1, 100);
            }
        }
    }
}
