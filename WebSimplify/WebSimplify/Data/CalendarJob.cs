using SynnCore.DataAccess;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public enum CalendarJobStatusEnum
    {
        [Description("ממתין")]
        Pending,
        [Description("בוטל")]
        Canceled,
        [Description("נכשל")]
        Failed,
        [Description("הסתיים")]
        Closed
    }

    public enum CalendarJobMethodEnum
    {
        GoogleAPI,
        EMail,
        DownloadICS
    }

    public class CalendarJob : GenericData
    {

        public int UserId { get; set; }
        [GenericDataField("UserIdText", "UserId")]
        public string UserIdText
        {
            get { return UserId.ToString(); }
            set { UserId = value.ToInteger(); }
        }

        public CalendarJobStatusEnum JobStatus { get; set; }
        [GenericDataField("JobStatusText", "JobStatus")]
        public string JobStatusText
        {
            get { return ((int)JobStatus).ToString(); }
            set { JobStatus = value.ToEnum<CalendarJobStatusEnum>(); }
        }

        public CalendarJobMethodEnum JobMethod { get; set; }
        [GenericDataField("JobMethodText", "JobMethod")]
        public string JobMethodText
        {
            get { return ((int)JobMethod).ToString(); }
            set { JobMethod = value.ToEnum<CalendarJobMethodEnum>(); }
        }

        public int MemoItemId { get; set; }
        [GenericDataField("MemoItemIdText", "MemoItemId")]
        public string MemoItemIdText
        {
            get { return MemoItemId.ToString(); }
            set { MemoItemId = value.ToInteger(); }
        }

        internal override string FormatedGenericValue(string valueToFormat, GenericDataFieldAttribute genericFieldInfo, IDatabaseProvider db)
        {
            if (genericFieldInfo.PropertyName == "JobStatusText")
            {
                var jobStatus = valueToFormat.ToEnum<CalendarJobStatusEnum>();
                return jobStatus.GetDescription();
            }
            if (genericFieldInfo.PropertyName == "JobMethodText")
            {
                var calendarJobMethod = valueToFormat.ToEnum<CalendarJobMethodEnum>();
                return calendarJobMethod.GetDescription();
            }
            if (genericFieldInfo.PropertyName == "UserIdText")
            {
                if (valueToFormat.IsInteger())
                {
                    var  u = db.DbAuth.GetUser(valueToFormat.ToInteger());
                    return u.DisplayName;
                }
            }
            if (genericFieldInfo.PropertyName == "MemoItemIdText")
            {
                if (valueToFormat.IsInteger())
                {
                    var u = db.DbCalendar.Get(new CalendarSearchParameters { ID = valueToFormat.ToInteger() }).FirstOrDefault();
                    return u.title;
                }
            }
            return base.FormatedGenericValue(valueToFormat, genericFieldInfo,db);
        }

        public CalendarJob(IDataReader data)
        {
            Load(data);
        }

        public CalendarJob()
        {

        }
    }
}