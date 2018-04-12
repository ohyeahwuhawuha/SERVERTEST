using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerA.Script.Base
{
    class ServerTime
    {
        static DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2017, 1, 1));

        static public long GetTime()
        {
            TimeSpan _timeSpan = DateTime.Now - startTime;
            return Convert.ToInt64(_timeSpan.TotalMilliseconds);
        }
    }
}
