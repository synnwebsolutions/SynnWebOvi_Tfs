using SynnCore.DataAccess;
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
            set { UserId = value.ToInteger(); }
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


        public CalendarJob(IDataReader data)
        {
            Load(data);
        }

        public CalendarJob()
        {

        }
    }
}