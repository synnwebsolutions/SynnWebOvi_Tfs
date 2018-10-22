using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using SynnWebOvi;
using SynnCore.DataAccess;
using System.Data;

namespace WebSimplify.Data
{
    [Serializable]
    public class ShiftDayData : IDbLoadable
    {
        public DateTime Date { get; set; }
        public ShiftTime DaylyShift { get; set; }
        public int Id { get;  set; }
        public int UserGroupId { get; set; }
        public int OwnerId { get;  set; }

        public ShiftDayData()
        {
        }
        public ShiftDayData(IDataReader data)
        {
            Load(data);
        }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            Date = DataAccessUtility.LoadNullable<DateTime>(reader, "Date");
            UserGroupId = DataAccessUtility.LoadInt32(reader, "UserGroupId");
            OwnerId = DataAccessUtility.LoadInt32(reader, "OwnerId");
            DaylyShift = (ShiftTime)DataAccessUtility.LoadInt32(reader, "DaylyShift");
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

        public static bool InCurrentMonth(this DateTime actualMonth, DateTime dateToCheck)
        {
            return actualMonth.Year == dateToCheck.Year && dateToCheck.Month == dateToCheck.Month;
        }
    }
}