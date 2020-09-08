using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx
{
    class Program
    {
        public static IObserver<long> CreateObserver(int id, ConsoleColor color, Stopwatch sw)
        {
            return Observer.Create<long>(
                onNext: val => {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{sw.ElapsedMilliseconds} Observer {id}, Next: {val}");
                    },
                onCompleted: () => {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{sw.ElapsedMilliseconds} Observer {id}, Completed:");
                },
                onError: err => {
                    Console.ForegroundColor = color;
                    Console.WriteLine($"{sw.ElapsedMilliseconds} Observer {id}, Error: {err}");
                }
                );
        }

        public static IObservable<long> CreateIntervalObservable()
        {
            return Observable.Interval(TimeSpan.FromSeconds(1));
        }

        static async Task Main(string[] args)
        {
            var sw = Stopwatch.StartNew();

            IObserver<long> observer1 = CreateObserver(1, ConsoleColor.White, sw);
            IObserver<long> observer2 = CreateObserver(2, ConsoleColor.Red, sw);

            IObservable<long> observable = CreateIntervalObservable();

            observable.Subscribe(observer1);

            await Task.Delay(3500);

            observable.Subscribe(observer2);


            Console.ReadLine();
        }
    }
}
