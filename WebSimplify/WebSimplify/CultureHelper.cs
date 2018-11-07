using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public static  class CultureHelper
    {
        public static CultureInfo HebrewCulture =  CultureInfo.CreateSpecificCulture("he-IL");
        public static string ToJewishDateString(this DateTime value, string format)
        {
            HebrewCulture.DateTimeFormat.Calendar = new HebrewCalendar();
            return value.ToString(format, HebrewCulture);
        }


    }

    public static class ExtensionsHelper
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime StartOfWeek(this DateTime dt)
        {
            while (dt.DayOfWeek != DayOfWeek.Sunday)
            {
                dt = dt.AddDays(-1);
            }
            return dt;
        }

        public static DateTime EndOfWeek(this DateTime dt)
        {
            while (dt.DayOfWeek != DayOfWeek.Saturday)
            {
                dt = dt.AddDays(1);
            }
            return dt;
        }

        public static DateTime EndOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year,dt.Month,1).AddMonths(1).AddDays(-1);
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

        public static string HebrewMonthName(this DateTime cMonth)
        {
            return cMonth.ToString("MMMM", CultureHelper.HebrewCulture);
        }

        public static string HebrewMonthNameWithYear(this DateTime cMonth)
        {
            return cMonth.ToString("MMMM", CultureHelper.HebrewCulture) + " " + cMonth.Year.ToString();
        }

        public static string HebrewDayName(this DateTime cMonth)
        {
            return CultureHelper.HebrewCulture.DateTimeFormat.DayNames[(int)cMonth.DayOfWeek];
        }

        public static string ToInputFormat(this DateTime cMonth)
        {
            return cMonth.ToString("yyyy-MM-dd");
        }

        public static bool Empty(this DateTime cMonth)
        {
            return cMonth == null || cMonth == DateTime.MinValue;
        }

        public static bool NotEmpty(this DateTime cMonth)
        {
            return cMonth != null && cMonth != DateTime.MinValue;
        }

        public static int NumberOfDays(this DateTime cMonth)
        {
            var tmpm = new DateTime(cMonth.Year, cMonth.Month, 1);
            var tmpmEnd = tmpm.AddMonths(1).AddDays(-1);
            var span = tmpmEnd - tmpm;
            return Convert.ToInt32(span.TotalDays);
        }

        public static string FormattedString(this int integer)
        {
            return string.Format("{0:n0}", integer);
        }

        public static int ToInteger(this double d)
        {
            return Convert.ToInt32(d);
        }

        public static int ToInteger(this string d)
        {
            var dd = string.Empty;
            foreach (var c in d.Where(x => x.IsInteger()))
                dd += c;
            return Convert.ToInt32(dd);
        }

        public static bool IsInteger(this char a)
        {
            return a >= '0' && a <= '9';
        }

        public static int ToInteger(this decimal d)
        {
            return Convert.ToInt32(d);
        }

        public static bool IsEmpty(this IEnumerable d)
        {
            if (d == null)
                return true;
            bool any = true;
            foreach (var e in d)
            {
                any = false;
                break;
            }
            return any;
        }

        public static bool IsEmpty(this string d)
        {
            return string.IsNullOrEmpty(d);
        }

        public static bool NotEmpty(this string d)
        {
            return !string.IsNullOrEmpty(d);
        }

        public static DateTime ToDateTime(this string d) 
        {
            return Convert.ToDateTime(d);
        }

        
        public static bool NotEmpty(this IEnumerable d)
        {
            if (d == null)
                return false;
            bool any = false;
            foreach (var e in d)
            {
                any = true;
                break;
            }
            return any;
        }

        public static bool NotNull(this object d)
        {
            return d != null;
        }

    }
}