using SynnCore.Generics;
using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebSimplify.Controls
{
    public partial class WsCalendar : System.Web.UI.UserControl
    {
        public DisplayMode Mode { get; set; }
        public bool ShowEmptyDays { get; set; }
        public bool MonthNameAsTitle { get; set; }
        public string GetDataSourceMethodName { get; set; }

        public SynnWebFormBase IPage
        {
            get
            {
                return (SynnWebFormBase)Page;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return (DateTime)IPage.GetFromSession("iDateTime_*");
            }
            set
            {
                IPage.StoreInSession("iDateTime_*", value);
            }
        }

        public DateTime EndDate
        {
            get
            {
                switch (Mode)
                {
                    case DisplayMode.Week:
                        return StartDate.EndOfWeek();
                    case DisplayMode.TwoWeeks:
                        return StartDate.AddDays(14);
                    case DisplayMode.Month:
                        return StartDate.EndOfMonth();
                }
                return StartDate;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string Title { get; set; }
        public void RefreshView()
        {
            txDisplay.Text = Title;//
            if (MonthNameAsTitle)
                txDisplay.Text = StartDate.HebrewMonthNameWithYear();

            MethodInfo m = Page.GetType().GetMethod(GetDataSourceMethodName);
            var DataSource = (List<ICalendarItem>)m.Invoke(Page, new object[] { StartDate.Date, EndDate.Date });
            List<CalendarWeekItem> lst = CalendarHelper.Generate(DataSource, StartDate, EndDate);
            gvC.DataSource = lst;
            gvC.DataBind();
        }

        protected void btnPrev_Click(object sender, ImageClickEventArgs e)
        {
            ChangeDate(-1);
            RefreshView();
        }

        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            ChangeDate(1);
            RefreshView();
        }

        private void ChangeDate(int ik)
        {
            var d = StartDate;
            switch (Mode)
            {
                case DisplayMode.Week:
                    d = d.AddDays(7 * ik);
                    break;
                case DisplayMode.TwoWeeks:
                    d = d.AddDays(7 * ik);
                    break;
                case DisplayMode.Month:
                    d = d.AddMonths(1 * ik);
                    break;
                default:
                    break;
            }
            StartDate = d;
        }

        protected void gvC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (CalendarWeekItem)e.Row.DataItem;
                ((Label)e.Row.FindControl("lblweek")).Text = string.Format("שבוע - {0}", d.StartofWeek.ToShortDateString());
                //e.Row.CssClass = "highlightRow";
                BuildText(e.Row, "lblSunaday", d, DayOfWeek.Sunday);
                BuildText(e.Row, "lblMonday", d, DayOfWeek.Monday);
                BuildText(e.Row, "lblTuesday", d, DayOfWeek.Tuesday);
                BuildText(e.Row, "lblWednsday", d, DayOfWeek.Wednesday);
                BuildText(e.Row, "lblThursday", d, DayOfWeek.Thursday);
                BuildText(e.Row, "lblFriday", d, DayOfWeek.Friday);
                BuildText(e.Row, "lblSaturday", d, DayOfWeek.Saturday);
            }
        }

        private void BuildText(GridViewRow row, string lblId, CalendarWeekItem d, DayOfWeek dw)
        {
            var lbl = ((Label)row.FindControl(lblId));
            lbl.Text = d.GetItems(dw);
        }
    }

    public enum DisplayMode
    {
        Week,
        TwoWeeks,
        Month
    }
    public static class CalendarHelper
    {

        internal static List<CalendarWeekItem> Generate(List<ICalendarItem> ds, DateTime startDate, DateTime endDate)
        {
            List<CalendarWeekItem> lst = new List<CalendarWeekItem>();
            while (startDate.StartOfWeek() < endDate.StartOfWeek())
            {
                CalendarWeekItem wi = new CalendarWeekItem(startDate.StartOfWeek().Date);
                wi.Items = ds.Where(x => x.WeekStart == startDate.StartOfWeek().Date).ToList();
                lst.Add(wi);
                startDate = startDate.AddDays(7);
            }
            return lst;
        }
    }

    public class CalendarWeekItem
    {
        public CalendarWeekItem(DateTime startofWeek)
        {
            this.StartofWeek = startofWeek;
        }

        public List<ICalendarItem> Items { get; set; }
        public DateTime StartofWeek { get; set; }

        internal string GetItems(DayOfWeek dw)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var i in Items.Where(x => x.Date.DayOfWeek == dw).OrderBy(x => x.Index).ToList())
            {
                sb.AppendLine(i.Display);
            }
            return sb.ToString();
        }
    }


    public interface ICalendarItem
    {
        DateTime Date { get; }
        string Display { get; }
        int Index { get; }
        DateTime WeekStart { get; }
    }

}