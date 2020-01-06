using Ical.Net.CalendarComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarUtilities
{
    public class MyCalendarEvent : CalendarEvent
    {
        public string Details { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Frequency { get; set; }
        public int FrequencyCount { get; set; }

        public string SummaryText
        {
            get { return Summary; }
            set { Summary = value; }
        }

        public string LocationText
        {
            get { return Location; }
            set { Location = value; }
        }

    }
}
