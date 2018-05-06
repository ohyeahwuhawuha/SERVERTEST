using System;

namespace Common.Time
{
    public static class Timer
    {
        static DateTime referenceTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2017, 1, 1));

        static long startTime;

        static public long GetTime()
        {
            TimeSpan _timeSpan = DateTime.Now - referenceTime;
            return Convert.ToInt64(_timeSpan.TotalMilliseconds);
        }

        static public void SetStartTime()
        {
            startTime = GetTime();
        }

        static public long GetStartTime()
        {
            return startTime;
        }
    }
}
