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
        public override GenericDataEnum GenericDataType => GenericDataEnum.CalendarJob;

        public int UserId { get; set; }
        public CalendarJobStatusEnum JobStatus { get; set; }
        public CalendarJobMethodEnum JobMethod { get; set; }
        public int MemoItemId { get; set; }

        public override string GetGenericFieldValue(int i, ref bool addEmpty)
        {
            if (i == 0)
            {
                return UserId.ToString();
            }
            if (i == 1)
            {
                return ((int)JobStatus).ToString();
            }
            if (i == 2)
            {
                return ((int)JobMethod).ToString();
            }
            if (i == 3)
            {
                return MemoItemId.ToString();
            }
            return base.GetGenericFieldValue(i, ref addEmpty);
        }

        public override void LoadGenericFieldValue(int i, string genericFieldDbValue)
        {
            if (i == 0)
            {
                UserId = genericFieldDbValue.ToInteger();
            }
            if (i == 1)
            {
                JobStatus = (CalendarJobStatusEnum)genericFieldDbValue.ToInteger();
            }
            if (i == 2)
            {
                JobMethod = (CalendarJobMethodEnum)genericFieldDbValue.ToInteger();
            }
            if (i == 3)
            {
                MemoItemId = genericFieldDbValue.ToInteger();
            }
            base.LoadGenericFieldValue(i, genericFieldDbValue);
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