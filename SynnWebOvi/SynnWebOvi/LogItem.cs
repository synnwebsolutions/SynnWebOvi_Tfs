using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Services.Description;

namespace SynnWebOvi
{
    public class LogItem : IDbLoadable
    {
        public LogItem()
        {

        }
        public LogItem(IDataReader data)
        {
            Load(data);
        }

        public DateTime Date { get;  set; }
        public int Id { get; set; }
        public string Trace { get;  set; }
        public string Message { get; set; }

        public void Load(IDataReader reader)
        {
            Id = DataAccessUtility.LoadInt32(reader, "Id");
            Date = DataAccessUtility.LoadNullable<DateTime>(reader, "Date");
            Trace = DataAccessUtility.LoadNullable<string>(reader, "Trace");
            Message = DataAccessUtility.LoadNullable<string>(reader, "Message");
        }
    }
}