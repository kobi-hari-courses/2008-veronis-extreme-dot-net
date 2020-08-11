using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExMethods
{
    public static class TimeExtensions
    {
        public static TimeSpan Minutes(this int i)
        {
            return TimeSpan.FromMinutes(i);
        }

        public static TimeSpan Hours(this int i)
        {
            return TimeSpan.FromHours(i);
        }

        public static TimeSpan Seconds(this int i)
        {
            return TimeSpan.FromSeconds(i);
        }

        public static TimeSpan Minutes(this TimeSpan source, int minutes)
        {
            return source.Add(minutes.Minutes());
        }

        public static TimeSpan Hours(this TimeSpan source, int hours)
        {
            return source.Add(hours.Hours());
        }

        public static TimeSpan Seconds(this TimeSpan source, int seconds)
        {
            return source.Add(seconds.Seconds());
        }

    }
}
