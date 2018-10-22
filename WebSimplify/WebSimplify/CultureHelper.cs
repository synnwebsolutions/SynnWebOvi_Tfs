using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public static  class CultureHelper
    {
        public static string ToJewishDateString(this DateTime value, string format)
        {
            var ci = CultureInfo.CreateSpecificCulture("he-IL");
            ci.DateTimeFormat.Calendar = new HebrewCalendar();
            return value.ToString(format, ci);
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static bool InTwoWeekPeriod(this DateTime periodStart, DateTime dateToCheck)
        {
            if (dateToCheck.Date >= periodStart.Date)
                return (dateToCheck.Date - periodStart.Date).TotalDays < 15;

            return false;
        }

        public static bool InCurrentMonth(this DateTime actualMonth, DateTime dateToCheck)
        {
            return actualMonth.Year == dateToCheck.Year && dateToCheck.Month == dateToCheck.Month;
        }
    }
}