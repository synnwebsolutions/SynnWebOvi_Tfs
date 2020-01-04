using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarUtilities
{
    public class GoogleAccountRequest
    {
        public MyCalendarEvent CalendarEvent { get; set; }
        public string CredentialsJsonString { get; set; }
        public string DbConnectionString { get; set; }
        public string DbTableName { get; set; }
    }
}
