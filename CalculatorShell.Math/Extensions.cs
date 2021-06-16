using System;

namespace CalculatorShell.Maths
{
    public static class Extensions
    {
        public static bool IsInteger(this double d)
        {
            return
                d - Math.Floor(d) == 0
                && d < long.MaxValue
                && d > long.MinValue;
        }

        public static long ToInteger(this double d)
        {
            return Convert.ToInt64(d);
        }

        public static double ToUnixTime(this DateTime dateTime)
        {
            return dateTime
                .ToUniversalTime()
                .Subtract(new DateTime(1970, 1, 1))
                .TotalSeconds;
        }

        public static DateTime ToDateTime(this double unixTime)
        {
            DateTime baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return baseDate.AddSeconds(unixTime);
        }
    }
}
