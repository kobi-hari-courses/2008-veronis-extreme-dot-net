using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan x = new TimeSpan(0, 5, 0);

            TimeSpan x2 = TimeSpan.FromMinutes(5);

            TimeSpan x3 = TimeExtensions.Seconds(TimeExtensions.Minutes(5), 30);

            TimeSpan x4 = 5.Minutes()
                           .Seconds(30);

        }
    }
}
