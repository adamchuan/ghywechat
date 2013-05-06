using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Time
    {
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            //DateTime1<DateTime2
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts.TotalMilliseconds.ToString() + "ms"; //201.12ms
        }
    }
}
