using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebSimplify
{
    public class CalendarItem
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int WeekNumber { get; set; }
        public bool IsCurrent { get; set; }

        public DateTime Date { get; set; }

        public string DateString { get { return Date.ToString(); } }

        public DayOfWeek DayOfWeek { get;  set; }

        public CalendarItem(DateTime d, Dictionary<int, List<CalendarItem>> wd)
        {
            Date = d;
            IsCurrent = Date.Date == DateTime.Now.Date;
            WeekNumber = d.Day / 7;
            DayOfWeek = d.DayOfWeek;
            wd[WeekNumber].Add(this);
        }
    }

    public class CalendarMonthlyData
    {
        public string CalendarHtml { get; set; }
        Dictionary<int, List<CalendarItem>> WeeklyData;

        public CalendarMonthlyData()
        {
            WeeklyData = new Dictionary<int, List<CalendarItem>>();
            for (int i = 0; i < 6; i++)
                WeeklyData[i] = new List<CalendarItem>();
            GenerateHtml();
        }

        private void GenerateHtml()
        {
            var now = DateTime.Now;
            
            int days = DateTime.DaysInMonth(now.Year, now.Month);
            var dDay = new DateTime(now.Year, now.Month, 1);
            for (int i = 0; i < days; i++)
            {
                CalendarItem c = new CalendarItem(dDay, WeeklyData);
                dDay = dDay.AddDays(1);
            }

            StringBuilder sb = new StringBuilder();
            AppendHeaders(sb);
            foreach (var week in WeeklyData)
            {
                sb.Append("<tr class='calendarrow'>");
                for (int i = 0; i < 7; i++)
                {
                    var vDay = week.Value.FirstOrDefault(x => x.DayOfWeek == (DayOfWeek)i);
                    if (vDay != null)
                        sb.AppendFormat("<td class='validdate'>{0}</td>", vDay.DateString);
                    else
                        sb.AppendFormat("<td class='invaliddate'>{0}</td>", "");// new DateTime(now.Year, now.Month, i).ToShortDateString());
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            CalendarHtml = sb.ToString();
        }

        private void AppendHeaders(StringBuilder sb)
        {
            sb.Append("<table class='sgridstyled'><tr>");
            sb.Append("<th>ראשון</th>");
            sb.Append("<th>שני</th>");
            sb.Append("<th>שלישי</th>");
            sb.Append("<th>רביעי</th>");
            sb.Append("<th>חמישי</th>");
            sb.Append("<th>שישי</th>");
            sb.Append("<th>שבת</th>");
            sb.Append("</tr>");
        }
    }
}