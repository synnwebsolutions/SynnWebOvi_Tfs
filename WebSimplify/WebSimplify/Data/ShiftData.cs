using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using SynnWebOvi;
using SynnCore.DataAccess;
using System.Data;
using SynnCore.Generics;

namespace WebSimplify.Data
{
    [Serializable]
    public class ShiftDayData : IDbLoadable, IMarkAble
    {
        public DateTime Date { get; set; }
        public ShiftTime DaylyShift { get; set; }
        public int Id { get;  set; }
        public int UserGroupId { get; set; }
        public int OwnerId { get;  set; }

        public string MarkableDescription
        {
            get
            {
                return Date.HebrewDayName();
            }
        }

        public string MarkableName
        {
            get
            {
                return GenericFormatter.GetEnumDescription(DaylyShift);
            }
        }

        public string MarkableType
        {
            get
            {
                return "המשמרת הבאה";
            }
        }

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


}