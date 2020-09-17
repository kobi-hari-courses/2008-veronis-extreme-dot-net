using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGC
{
    public static class SomeTask
    {
        private static int _counter = 0;
        private static AsyncMutex _mutex = new AsyncMutex();

        public static async Task DoSomething(string id)
        {
            using (await _mutex.Lock())
            {
                Console.WriteLine("Starting to do something " + id);
                Console.WriteLine("Counter = " + _counter);

                var readValue = _counter;

                await Task.Delay(5000);

                _counter = readValue + 1;

                Console.WriteLine("Completed to do something " + id);
                Console.WriteLine("Counter = " + _counter);
            }
        }
    }
}
