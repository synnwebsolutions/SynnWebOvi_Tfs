using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CalendarUtilities
{
    public class CalendarMailRequest
    {
        public List<string> To { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
        public ICredentialsByHost NetworkCredential { get; set; }
    }
}
