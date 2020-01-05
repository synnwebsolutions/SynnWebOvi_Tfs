using Ical.Net.CalendarComponents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CalendarUtilities
{
    public class CalendarRequest
    {
        public List<string> To { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
        
        public List<MyCalendarEvent> CalendarEvents { get; set; }
        public string NetworkCredentialUserName { get;  set; }
        public string NetworkCredentialPassword { get;  set; }
    }
}
