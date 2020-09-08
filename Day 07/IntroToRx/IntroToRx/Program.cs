using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace IntroToRx
{
    class Program
    {
        private static event EventHandler<long> _myPrivateEvent;

        public static event EventHandler<long> OnNumber
        {
            add
            {
                _myPrivateEvent += value;
            }
            remove
            {
                _myPrivateEvent -= value;
            }
        }

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

        public static IObservable<long> CreateTimerObservable()
        {
            return Observable.Timer(TimeSpan.FromSeconds(3));
        }

        public static IObservable<long> CreateContinousTimerObservable()
        {
            return Observable.Timer(TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(2));
        }

        public static IObservable<long> CreateFromSingleItem()
        {
            return Observable.Return<long>(42);
        }

        public static IObservable<long> CreateFromEnumerable()
        {
            var list = new List<long> { 42, 20, 100, 50 };
            return list.ToObservable();
        }

        public static IObservable<long> CreateFromGenerate()
        {
            return Observable.Generate<(int i1, int i2), long>((i1: 1, i2: 1),
                pair => pair.i2 < 100,
                pair => (i1: pair.i2, i2: pair.i1 + pair.i2),
                pair => (long)pair.i2
                );
        }

        public static IObservable<long> CreateFromEvent()
        {
            return Observable.FromEventPattern<long>(
                h => Program.OnNumber += h,
                h => Program.OnNumber -= h
                )
                .Select(ep => ep.EventArgs);
        }

        public static IObservable<long> CreateCustomObservable()
        {
            return Observable.Create<long>(observer =>
            {
                observer.OnNext(42);
                Task.Delay(2000).ContinueWith(t => observer.OnNext(60));
                Task.Delay(5000).ContinueWith(t => observer.OnNext(100));
                Task.Delay(8000).ContinueWith(t => observer.OnNext(200));
                Task.Delay(10000).ContinueWith(t => observer.OnCompleted());
                return Disposable.Empty;
            });
        }

        public static IObservable<long> CreateCustomSubject()
        {
            var res = new Subject<long>();
            res.OnNext(42);
            Task.Delay(2000).ContinueWith(t => res.OnNext(60));
            Task.Delay(5000).ContinueWith(t => res.OnNext(100));
            Task.Delay(8000).ContinueWith(t => res.OnNext(200));
            Task.Delay(10000).ContinueWith(t => res.OnCompleted());

            return res;
        }

        public static IObservable<long> CreateCustomBehaviorSubject()
        {
            var res = new BehaviorSubject<long>(42);
            Task.Delay(2000).ContinueWith(t => res.OnNext(60));
            Task.Delay(5000).ContinueWith(t => res.OnNext(100));
            Task.Delay(8000).ContinueWith(t => res.OnNext(200));
            Task.Delay(10000).ContinueWith(t => res.OnCompleted());

            return res;
        }

        public static IObservable<long> CreateCustomReplaySubject()
        {
            var res = new ReplaySubject<long>();
            res.OnNext(42);
            Task.Delay(2000).ContinueWith(t => res.OnNext(60));
            Task.Delay(5000).ContinueWith(t => res.OnNext(100));
            Task.Delay(8000).ContinueWith(t => res.OnNext(200));
            Task.Delay(10000).ContinueWith(t => res.OnCompleted());

            return res;
        }


        static async Task Main(string[] args)
        {
            var observable = CreateTimerObservable();
            var res = await observable;

            var res2 = await observable;
        }

        static async Task Main1(string[] args)
        {
            var sw = Stopwatch.StartNew();

            IObserver<long> observer1 = CreateObserver(1, ConsoleColor.White, sw);
            IObserver<long> observer2 = CreateObserver(2, ConsoleColor.Red, sw);

            IObservable<long> observable = CreateFromEvent();

            Program._myPrivateEvent?.Invoke(null, 42);

            using (observable.Subscribe(observer1))
            {
                Program._myPrivateEvent?.Invoke(null, 60);

                await Task.Delay(3500);

                Program._myPrivateEvent?.Invoke(null, 80);


                using (observable.Subscribe(observer2))
                {
                    Program._myPrivateEvent?.Invoke(null, 100);

                    await Task.Delay(2000);

                    Program._myPrivateEvent?.Invoke(null, 200);

                    Console.ReadLine();
                }
            }

        }
    }
}
