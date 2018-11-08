using SynnCore.DataAccess;
using SynnCore.Generics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
  
    public class WorkHoursData : IDbLoadable
    {
        public DateTime Month { get; set; }
        public WorkTime CurrentMonthTotal { get; set; }

        public WorkTime CurrentShiftStart { get; set; }
        public WorkTime CurrentShiftEnd { get; set; }

        public int Id { get; set; }
        public int UserGroupId { get; set; }
        public bool Active { get; set; }

        public WorkHoursData()
        {

        }

        public WorkHoursData(IDataReader data)
        {
            Load(data);
        }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            UserGroupId = DataAccessUtility.LoadInt32(reader, "UserGroupId");
            Month = DataAccessUtility.LoadNullable<DateTime>(reader, "Month");
            Active = DataAccessUtility.LoadNullable<bool>(reader, "Active");
            var currentMonthTotal =  DataAccessUtility.LoadNullable<string>(reader, "CurrentMonthTotal");
            if (currentMonthTotal.IsEmpty())
                CurrentMonthTotal = new WorkTime();
            else
                CurrentMonthTotal = XmlHelper.CreateFromXml<WorkTime>(currentMonthTotal);

            var cfs = DataAccessUtility.LoadNullable<string>(reader, "CurrentShiftStart");
            if (cfs.IsEmpty())
                CurrentShiftStart = new WorkTime();
            else
                CurrentShiftStart = XmlHelper.CreateFromXml<WorkTime>(cfs);

            var cfe = DataAccessUtility.LoadNullable<string>(reader, "CurrentShiftEnd");
            if (cfe.IsEmpty())
                CurrentShiftEnd = new WorkTime();
            else
                CurrentShiftEnd = XmlHelper.CreateFromXml<WorkTime>(cfe);
        }

        internal string CurrentShiftTimeLeft(WorkTime required)
        {
            if (CurrentShiftStart.NotFilled())
            {
                CurrentShiftStart = new WorkTime();
                return CurrentShiftStart.ToTimeDisplay();
            }
            var cPoint = new WorkTime { Hour = DateTime.Now.Hour, Minute = DateTime.Now.Minute };
            WorkTime soFar = cPoint.Reduce(CurrentShiftStart);

            TimeSpan requiredSpan = new TimeSpan(required.Hour, required.Minute, 0);
            TimeSpan predEnd = new TimeSpan(CurrentShiftStart.Hour, CurrentShiftStart.Minute, 0);
            predEnd = predEnd + requiredSpan;

            return  required.Reduce(soFar).ToTimeDisplay() + " | " + new WorkTime { Hour = predEnd.Hours, Minute = predEnd.Minutes }.ToTimeDisplay();
        }

        internal void HandleShift(int hour, int minute, WorkTime required)
        {
            if (CurrentShiftStart.NotFilled())
            {
                CurrentShiftStart.Hour = hour;
                CurrentShiftStart.Minute = minute;
            }
            else
            {
                // close shift

                TimeSpan currentShiftEndAt = new TimeSpan(hour, minute, 0); // 17:23
                TimeSpan currentShiftStartAt = new TimeSpan(CurrentShiftStart.Hour, CurrentShiftStart.Minute, 0); // 08:24

                TimeSpan requiredSpan = new TimeSpan(required.Hour, required.Minute, 0);
                TimeSpan currentShiftBalance = currentShiftEndAt - currentShiftStartAt;

                TimeSpan currentDayTotalBalance = currentShiftBalance - requiredSpan;

                TimeSpan currentMonthTotalTotalSpan = new TimeSpan(CurrentMonthTotal.Hour, CurrentMonthTotal.Minute, 0);
                currentMonthTotalTotalSpan += currentDayTotalBalance;
                CurrentMonthTotal = new WorkTime { Hour = currentMonthTotalTotalSpan.Hours, Minute = currentMonthTotalTotalSpan.Minutes };
                CurrentShiftStart = new WorkTime();
            }
        }
    }
 
    [Serializable]
    public class WorkTime
    {
        public int Hour { get; set; }
        public int Minute { get; set; }

        internal WorkTime Append(int hour, int minute)
        {
            var w = new WorkTime { Hour = this.Hour, Minute = this.Minute };

            w.Hour += hour;
            w.Minute += minute;
            w.Check();
            return w;
        }

        internal bool NotFilled()
        {
            return Hour == 0 && Minute == 0;
        }

        internal WorkTime Reduce(WorkTime c)
        {
            var w = new WorkTime { Hour = this.Hour, Minute = this.Minute };

            w.Hour -= c.Hour;
            w.Minute -= c.Minute;
            w.Check();
            return w;
        }

        public void Check()
        {
            if (Minute > 59)
            {
                var hoursFromMinutes = Minute / 60;
                var minutesFromMinutes = Minute % 60;

                Hour += hoursFromMinutes;
                Minute = minutesFromMinutes;
            }
            if (Minute < 0)
            {
                var ires = 60 + Minute;
                Minute = ires;
                Hour -= 1;
            }
            if (Hour < 0)
            {
                Minute *= -1;
            }
        }
    }
}