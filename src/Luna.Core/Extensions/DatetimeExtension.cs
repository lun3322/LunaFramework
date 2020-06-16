using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Extensions
{
    public static class DatetimeExtension
    {
        public static bool Between(this DateTime @this, DateTime minValue, DateTime maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        public static DateTime EndOfDay(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day).AddDays(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }

        public static DateTime EndOfMonth(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, 1).AddMonths(1).Subtract(new TimeSpan(0, 0, 0, 0, 1));
        }

        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startDayOfWeek = DayOfWeek.Sunday)
        {
            var end = dt;
            var endDayOfWeek = startDayOfWeek - 1;
            if (endDayOfWeek < 0)
            {
                endDayOfWeek = DayOfWeek.Saturday;
            }

            if (end.DayOfWeek != endDayOfWeek)
            {
                end = endDayOfWeek < end.DayOfWeek ? end.AddDays(7 - (end.DayOfWeek - endDayOfWeek)) : end.AddDays(endDayOfWeek - end.DayOfWeek);
            }

            return new DateTime(end.Year, end.Month, end.Day, 23, 59, 59, 999);
        }
    }
}