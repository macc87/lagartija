using System;
using System.Diagnostics;

namespace Utilities.Extensions
{

    ///<summary>
    /// DateTime util class 
    ///</summary>
    public static class DateTimeExtension
    {
        const string UsCentralTime = "Central Standard Time";
        const string UsEasternTime = "Eastern Standard Time";
        const string UsPacificTime = "Pacific Standard Time";

        public static DateTime ConvertToUsEasternTime(this DateTime value)
        {
            DateTime result = DateTime.MinValue;
            try
            {
                TimeZoneInfo destinationZone = TimeZoneInfo.FindSystemTimeZoneById(UsEasternTime);
                result = TimeZoneInfo.ConvertTime(value, destinationZone);
            }
            catch (TimeZoneNotFoundException)
            {
                Trace.WriteLine("Unable to find the {0} zone in the registry.",
                                  UsEasternTime);
            }
            catch (InvalidTimeZoneException)
            {
                Trace.WriteLine("Registry data on the {0} zone has been corrupted.",
                                  UsEasternTime);
            }
            return result;
        }
        public static string ConvertToStringWithFormat(this DateTime? dateTime, string format)
        {
            if (dateTime == null) return null;
            var result = (DateTime)dateTime;
            return result.ToString(format);
        }
        public static DateTime ConvertToUsPacificTime(this DateTime value)
        {
            DateTime result = DateTime.MinValue;
            try
            {
                TimeZoneInfo destinationZone = TimeZoneInfo.FindSystemTimeZoneById(UsPacificTime);
                result = TimeZoneInfo.ConvertTime(value, destinationZone);
            }
            catch (TimeZoneNotFoundException)
            {
                Trace.WriteLine("Unable to find the {0} zone in the registry.",
                                  UsPacificTime);
            }
            catch (InvalidTimeZoneException)
            {
                Trace.WriteLine("Registry data on the {0} zone has been corrupted.",
                                  UsPacificTime);
            }
            return result;
        }


        public static DateTime ConvertToUsCentralTime(this DateTime value)
        {
            DateTime result = DateTime.MinValue;
            try
            {
                TimeZoneInfo destinationZone = TimeZoneInfo.FindSystemTimeZoneById(UsCentralTime);
                result = TimeZoneInfo.ConvertTime(value, destinationZone);
            }
            catch (TimeZoneNotFoundException)
            {
                Trace.WriteLine("Unable to find the {0} zone in the registry.",
                                  UsCentralTime);
            }
            catch (InvalidTimeZoneException)
            {
                Trace.WriteLine("Registry data on the {0} zone has been corrupted.",
                                  UsCentralTime);
            }
            return result;
        }

        private static int DateValue(this DateTime dt)
        {
            return dt.Year * 372 + (dt.Month - 1) * 31 + dt.Day - 1;
        }

        public static int YearsBetween(this DateTime dt, DateTime dt2)
        {
            return dt.MonthsBetween(dt2) / 12;
        }
        public static int YearsBetween(this DateTime dt, DateTime dt2, bool includeLastDay)
        {
            return dt.MonthsBetween(dt2, includeLastDay) / 12;
        }
        public static int YearsBetween(this DateTime dt, DateTime dt2, bool includeLastDay, out int excessMonths)
        {
            int months = dt.MonthsBetween(dt2, includeLastDay);
            excessMonths = months % 12;
            return months / 12;
        }
        public static int MonthsBetween(this DateTime dt, DateTime dt2)
        {
            int months = (dt2.DateValue() - dt.DateValue()) / 31;
            return Math.Abs(months);
        }
        public static int MonthsBetween(this DateTime dt, DateTime dt2, bool includeLastDay)
        {
            if (!includeLastDay) return dt.MonthsBetween(dt2);
            int days;
            if (dt2 >= dt)
                days = dt2.AddDays(1).DateValue() - dt.DateValue();
            else
                days = dt.AddDays(1).DateValue() - dt2.DateValue();
            return days / 31;
        }
        public static int WeeksBetween(this DateTime dt, DateTime dt2)
        {
            return dt.DaysBetween(dt2) / 7;
        }
        public static int WeeksBetween(this DateTime dt, DateTime dt2, bool includeLastDay)
        {
            return dt.DaysBetween(dt2, includeLastDay) / 7;
        }
        public static int WeeksBetween(this DateTime dt, DateTime dt2, bool includeLastDay, out int excessDays)
        {
            int days = dt.DaysBetween(dt2, includeLastDay);
            excessDays = days % 7;
            return days / 7;
        }
        public static int DaysBetween(this DateTime dt, DateTime dt2)
        {
            return (dt2.Date - dt.Date).Duration().Days;
        }
        public static int DaysBetween(this DateTime dt, DateTime dt2, bool includeLastDay)
        {
            int days = dt.DaysBetween(dt2);
            if (!includeLastDay) return days;
            return days + 1;
        }
    }

}
