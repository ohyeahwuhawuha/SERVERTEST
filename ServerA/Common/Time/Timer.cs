using System;

namespace Common.Time
{
    public static class Timer
    {
        static DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2017, 1, 1));

        static public long GetTime()
        {
            TimeSpan _timeSpan = DateTime.Now - startTime;
            return Convert.ToInt64(_timeSpan.TotalMilliseconds);
        }        
    }
}
