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
        public override GenericDataEnum GenericDataType => GenericDataEnum.CalendarBackgroundWorkerLog;

        public DateTime LastRunTime { get; set; }

        public override string GetGenericFieldValue(int i, ref bool addEmpty)
        {
            if (i == 0)
            {
                return LastRunTime.ToString();
            }
            return base.GetGenericFieldValue(i, ref addEmpty);
        }

        public override void LoadGenericFieldValue(int i, string genericFieldDbValue)
        {
            if (i == 0)
            {
                LastRunTime = genericFieldDbValue.ToDateTime();
            }
            base.LoadGenericFieldValue(i, genericFieldDbValue);
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