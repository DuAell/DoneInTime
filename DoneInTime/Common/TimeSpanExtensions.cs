using System;
using System.Linq;

namespace DoneInTime.Common
{
    public static class TimeSpanExtensions
    {
        static int[] weights = { 60 * 60 * 1000, 60 * 1000, 1000, 1 };

        public static TimeSpan ToTimeSpan(this string s)
        {
            return TimeSpan.FromMilliseconds(s.Split('.', ':').Zip(weights, (d, w) => Convert.ToInt64(d) * w).Sum());
        }
    }
}
