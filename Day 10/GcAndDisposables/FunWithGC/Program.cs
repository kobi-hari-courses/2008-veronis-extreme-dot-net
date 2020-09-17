using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGC
{

    #region Pair
    public class Pair
    {
        private int _x;
        private int _y;
        public Pair Next { get; set; }

        public Pair(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            return $"Pair ({_x},{_y})";
        }

        ~Pair()
        {
            Console.WriteLine($"Destructing Pair with the following data: x = {_x}, y = {_y}");
        }

    }
    #endregion


    class Program
    {
        static void Main(string[] args)
        {
            RunInParallel();
            Console.ReadLine();
        }

        #region Task Mutex Example

        public static async void RunInParallel()
        {
            var t1 = SomeTask.DoSomething("1");
            var t2 = SomeTask.DoSomething("2");
            var t3 = CountToTen();


            await Task.WhenAll(t1, t2, t3);
        }

        public static async Task CountToTen()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("I = " + i);
                await Task.Delay(1000);
            }
        }

        #endregion


        #region Disposables fun

        public static void DisposableExample()
        {
            using (var repository = new Repository())
            {
                foreach (var item in repository.GetData())
                {
                    Console.WriteLine(item);
                }
            }
        }

        public static void BadDisposableExample()
        {
            var repository = new Repository();
            foreach (var item in repository.GetData())
            {
                Console.WriteLine(item);
            }
        }

        public static void BadDisposableExample2()
        {
            var repository = new Repository();
            foreach (var item in repository.GetData())
            {
                Console.WriteLine(item);
            }

            repository.Dispose();
            var data = repository.GetData();
        }

        public static void DisposableActionExample()
        {
            using (Disposables.Call(() => Console.WriteLine("I am Done")))
            {
                foreach (var item in Enumerable.Range(1, 1000000))
                {
                    if (item % 50 == 0)
                    {
                        Console.WriteLine("Found a proper number: " + item);
                        return;
                    }
                }
            }
        }

        public static void FirstDisposableFactoryExample()
        {
            var factory = new RepositoryFactory();
            using (var token = factory.GetRepository())
            {
                var data = token.Value.GetData();
            }
        }

        public static void WriteSomeColorsToConsole()
        {
            using (ChangeColor(ConsoleColor.Yellow))
            {
                Console.WriteLine("This message is yellow");

                using (ChangeColor(ConsoleColor.Red))
                {
                    Console.WriteLine("This is red");
                }

                Console.WriteLine("This message is yellow");
            }
        }

        public static IDisposable ChangeColor(ConsoleColor color)
        {
            var current = Console.ForegroundColor;
            Console.ForegroundColor = color;

            return Disposables.Call(() =>
            {
                Console.ForegroundColor = current;
            });
        }

        #endregion

        #region Gc Examples

        static Pair StaticPair { get; set; }

        static WeakReference WeakPair { get; set; }

        public static void DoSomething()
        {
            Pair main = new Pair(1, 2);
            Pair p1 = new Pair(3, 4);
            Pair p2 = new Pair(5, 6);
            p1.Next = p2;
            StaticPair = main;
            WeakPair = new WeakReference(StaticPair);
            //GC.Collect();
        }

        static void GcExample()
        {
            DoSomething();
            GC.Collect();
            Console.WriteLine("After first garbage collection");
            Console.ReadLine();
            StaticPair = null;
            GC.Collect();
            Console.WriteLine("After second garbage collection");
            Console.ReadLine();
        }

        #endregion


    }
}
