using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebSimplify
{
    public class CalendarBackgroundWorkerLog : GenericData
    {
        public DateTime LastRunTime { get; set; }

        [GenericDataField("LastRunTimeText", "LastRunTime")]
        public string LastRunTimeText
        {
            get { return LastRunTime.ToString(); }
            set { LastRunTime = value.ToDateTime(); }
        }

        public CalendarBackgroundWorkerLog(IDataReader data)
        {
            Load(data);
        }

        public CalendarBackgroundWorkerLog()
        {

        }
    }

}