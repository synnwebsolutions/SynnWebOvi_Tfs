using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using SynnWebOvi;

namespace WebSimplify.Data
{

    public class UserShiftsContainer
    {
        private LoggedUser currentUser;

        public List<ShiftDayData> CrrentUserShifts { get; set; }
        public UserShiftsContainer()
        {
            
        }

        public UserShiftsContainer(LoggedUser currentUser)
        {
            this.currentUser = currentUser;
            CrrentUserShifts = new List<ShiftDayData>();
            var dt = DateTime.Now;
            var lst = new List<ShiftDayData>();
            var s1 = new ShiftDayData { Date = dt };
            s1.DaylyShifts.Add(ShiftTime.Morning);
            s1.DaylyShifts.Add(ShiftTime.Night);
            lst.Add(s1);

            var s2 = new ShiftDayData { Date = dt.AddDays(2) };
            s2.DaylyShifts.Add(ShiftTime.Night);
            lst.Add(s2);


            var s3 = new ShiftDayData { Date = dt.AddDays(3) };
            s3.DaylyShifts.Add(ShiftTime.Noon);
            lst.Add(s3);
        }
    }

    [Serializable]
    public class ShiftDayData
    {
        public DateTime Date { get; set; }
        public List<ShiftTime> DaylyShifts { get; set; }
        public ShiftDayData()
        {
            DaylyShifts = new List<ShiftTime>();
        }
    }

    public enum ShiftTime
    {
        [Description("בוקר")]
        Morning,
        [Description("ערב")]
        Noon,
        [Description("לילה")]
        Night
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static bool InTwoWeekPeriod(this DateTime periodStart , DateTime dateToCheck)
        {
            if (dateToCheck.Date >= periodStart.Date)
                return (dateToCheck.Date - periodStart.Date).TotalDays < 15;

            return false;
        }
    }
}