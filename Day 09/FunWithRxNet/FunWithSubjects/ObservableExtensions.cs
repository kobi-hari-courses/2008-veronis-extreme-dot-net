using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithSubjects
{
    public static class ObservableExtensions
    {
        public static IDisposable SubscribeConsole<T>(this IObservable<T> source, string prefix)
        {
            return source
                .Subscribe(
                val => Console.WriteLine($"{prefix} Next: {val}"),
                err => Console.WriteLine($"{prefix} Error: {err.Message}"),
                () => Console.WriteLine($"{prefix} Completed"));
                
        }
    }
}
