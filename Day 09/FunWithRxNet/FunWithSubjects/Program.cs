using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace FunWithSubjects
{
    class Program
    {
        public static void Main(string[] args)
        {
            LowerOrderSelection();
            Console.ReadLine();
        }

        public static void LowerOrderSelection()
        {
            var o2 = Observable.Interval(TimeSpan.FromSeconds(1))
                .GroupBy(i => i % 4);

            var oe2 = Enumerable.Range(0, 4)
                .Select(i => Observable
                                .Interval(TimeSpan.FromSeconds(4))
                                .Delay(TimeSpan.FromSeconds(i))
                                .Select(val => i * 4 + val));

            var switched = o2.Switch();
            var amb = oe2.Amb();

            switched.SubscribeConsole("Switch");
            amb.SubscribeConsole("Amb");

        }

        static void Chaining()
        {
            var ob1 = Observable.Interval(TimeSpan.FromSeconds(1))
                .Take(5);
            var ob2 = Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(i => -i)
                .Take(5);

            var concat = Observable.Concat(ob1, ob2);
            //concat.SubscribeConsole("Concat");

            var repeat = ob1.Repeat(3);
            repeat.SubscribeConsole("Repeat");
        }

        static void Combining()
        {
            var rnd = new Random();

            var ob1 = Observable
                .Interval(TimeSpan.FromSeconds(2))
                .Select(i => rnd.Next(100))
                .Publish()
                .RefCount();

            var ob2 = Observable
                .Interval(TimeSpan.FromSeconds(3));

            var zipped = Observable.Zip(ob1, ob2, (r, i) => $"{i}, {r}");
            var combined = Observable.CombineLatest(ob1, ob2, (r, i) => $"{i}, {r}");
            var latestFrom = ob1.WithLatestFrom(ob2, (r, i) => $"{i}, {r}");

            ob1.SubscribeConsole("Random");
            ob2.SubscribeConsole("Sequential");
            latestFrom.SubscribeConsole("With Latest From");

        }

        static void HigherOrder()
        {
            //var o2 = Observable.Interval(TimeSpan.FromSeconds(1))
            //          .Select(number => Observable.Interval(TimeSpan.FromSeconds(1)).Select(val => number * 10 + val));

            var interval = Observable.Interval(TimeSpan.FromSeconds(0.2));
            var o2 = interval.GroupBy(i => i % 2);
            
            var o1 = o2.Merge();

            o1.SubscribeConsole("O1");
        }

        static void Aggregation()
        {
            var ob1 = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Skip(1)
                .Take(5);

            var agg = ob1.Aggregate(1L, (i, j) => i * j);
            agg.SubscribeConsole("Scan");
            ob1.SubscribeConsole("Interval");


        }

        static void HotMergeExample()
        {
            var ob1 = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Take(10);


            var ob2 = Observable
                .Interval(TimeSpan.FromSeconds(2))
                .Take(10)
                .Select(i => i * 10);

            //var subject = new Subject<long>();
            //ob1.Subscribe(subject);
            //ob2.Subscribe(subject);

            Observable
                .Merge(ob1, ob2)
                .Publish()
                .RefCount()
                .SubscribeConsole("Merge");
            Console.ReadLine();
        }

        static async Task PublishExample()
        {
            var onInterval = Observable
                .Timer(TimeSpan.FromSeconds(0.5), TimeSpan.FromSeconds(1))
                .Select(val => val * 2)
                .Take(5)
                .Publish(42)
                .RefCount();

            Console.WriteLine("Here we go!");

            await Task.Delay(3000);


            onInterval.SubscribeConsole("onIterval - 1");

            await Task.Delay(2000);

            onInterval.SubscribeConsole("onInterval - 2");

            await Task.Delay(6000);

            onInterval.SubscribeConsole("onInterval - 3");


            Console.ReadLine();

        }
    }
}
