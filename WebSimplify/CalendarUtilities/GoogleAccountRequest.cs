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
        public IGoogleDataStore GoogleDataStore { get; set; }
    }
}
