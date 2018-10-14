using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebSimplify
{
    public class CalendarHtmlItem
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int WeekNumber { get; set; }
        public bool IsCurrent { get; set; }

        public DateTime Date { get; set; }

        public List<MemoItem> mItems { get; set; }

        public string DateString { get { return Date.ToShortDateString(); } }

        public DayOfWeek DayOfWeek { get;  set; }

        public CalendarHtmlItem(DateTime d, Dictionary<int, List<CalendarHtmlItem>> wd, List<MemoItem> memos)
        {
            Date = d;
            IsCurrent = Date.Date == DateTime.Now.Date;
            WeekNumber = d.Day / 7;
            DayOfWeek = d.DayOfWeek;
            mItems = memos.Where(x => x.Date.Date == d.Date).ToList();
            wd[WeekNumber].Add(this);
        }

        internal string GenerateHtml()
        {
            var sb = new StringBuilder("<td class='calendarcellcontainer'>");
            sb.Append("<table class='calendardaydata '>");
            sb.AppendFormat("<tr><td class='calendarinnercell calendardayidf'>{0}</td></tr>", Date.Day);

            sb.Append("<tr>");
            string rowFormat = "<tr><td class='calendarinnercell calendardayinfo'><ul class='a'>{0}</ul></td></tr>";
            string ls = string.Empty;
            foreach (var item in mItems)
                ls += string.Format("<li>{0}</li>", item.Header);
            sb.AppendFormat(rowFormat,ls);

            sb.Append("</table>");
            sb.Append("</td>");
            return sb.ToString();
        }
    }

    public class CalendarMonthlyData
    {
        public string CalendarHtml { get; set; }
        Dictionary<int, List<CalendarHtmlItem>> WeeklyData;

        public CalendarMonthlyData(List<MemoItem> mls)
        {            
            WeeklyData = new Dictionary<int, List<CalendarHtmlItem>>();
            for (int i = 0; i < 6; i++)
                WeeklyData[i] = new List<CalendarHtmlItem>();
            GenerateHtml(mls);
        }

        private void GenerateHtml(List<MemoItem> mls)
        {
            var now = DateTime.Now;
            
            int days = DateTime.DaysInMonth(now.Year, now.Month);
            var dDay = new DateTime(now.Year, now.Month, 1);
            for (int i = 0; i < days; i++)
            {
                CalendarHtmlItem c = new CalendarHtmlItem(dDay, WeeklyData, mls);
                dDay = dDay.AddDays(1);
            }

            StringBuilder sb = new StringBuilder();
            AppendHeaders(sb);
            foreach (var week in WeeklyData)
            {
                
                if (week.Value != null && week.Value.Count > 0)
                {
                    sb.Append("<tr class='calendarrow'>");
                    for (int i = 0; i < 7; i++)
                    {
                        var vDay = week.Value.FirstOrDefault(x => x.DayOfWeek == (DayOfWeek)i);
                        if (vDay != null)
                        {
                            string dayHtml = vDay.GenerateHtml();
                            sb.Append(dayHtml);
                        }
                        else
                            sb.AppendFormat("<td class='calendarcell calendarinactivecell'>{0}</td>", "");// new DateTime(now.Year, now.Month, i).ToShortDateString());
                    }
                    sb.Append("</tr>");
                }
               
            }
            sb.Append("</table>");
            CalendarHtml = sb.ToString();
        }

        private void AppendHeaders(StringBuilder sb)
        {
            sb.Append("<table class='calendartable'><tr>");
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