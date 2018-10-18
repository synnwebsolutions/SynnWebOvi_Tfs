using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class Diary : SynnWebFormBase
    {

        public DateTime ActionMonth
        {
            get
            {
                return (DateTime)GetFromSession("cMonth");
            }
            set
            {
                StoreInSession("cMonth", value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActionMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                RefreshGrid(gv);
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetData";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetData()
        {
            monthTitle.InnerText = string.Format("{0} - {1}", ActionMonth.ToString("MMMM"), ActionMonth.Year.ToString());

            var sp = new CalendarSearchParameters { FromDate = ActionMonth };
            sp.ToDate = sp.FromDate.Value.AddMonths(1).AddDays(-1);
            List<MemoItem> mls = DBController.DbCalendar.Get(sp);
            var mm =  new CalendarMonthlyData(mls);
            return mm.WeeklyData.Where(x => x.Value.Count > 0).ToList();
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var row = e.Row;
                var d = (KeyValuePair<int, List<CalendarHtmlItem>>)e.Row.DataItem;
                var sunday = d.Value.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Sunday);
                if (sunday != null)
                {
                    //((TextBox)e.Row.FindControl("lblSunday")).Text = sunday.GenerateHtml();
                    row.Cells[0].Controls.Add(new LiteralControl(sunday.GenerateHtml()));
                }
                var Monday = d.Value.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Monday);
                if (Monday != null)
                {
                    //((TextBox)e.Row.FindControl("lblMonday")).Text = Monday.GenerateHtml();
                    row.Cells[1].Controls.Add(new LiteralControl(Monday.GenerateHtml()));
                }
                var Tuesday = d.Value.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Tuesday);
                if (Tuesday != null)
                {
                    //((TextBox)e.Row.FindControl("lblTuesday")).Text = Tuesday.GenerateHtml();
                    row.Cells[2].Controls.Add(new LiteralControl(Tuesday.GenerateHtml()));
                }
                var Wednesday = d.Value.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Wednesday);
                if (Wednesday != null)
                {
                    //((TextBox)e.Row.FindControl("lblWendsday")).Text = Wednesday.GenerateHtml();
                    row.Cells[3].Controls.Add(new LiteralControl(Wednesday.GenerateHtml()));
                }
                var Thursday = d.Value.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Thursday);
                if (Thursday != null)
                {
                    //((TextBox)e.Row.FindControl("lblThursday")).Text = Thursday.GenerateHtml();
                    row.Cells[4].Controls.Add(new LiteralControl(Thursday.GenerateHtml()));
                }
                var Friday = d.Value.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Friday);
                if (Friday != null)
                {
                    //((TextBox)e.Row.FindControl("lblFriday")).Text = Friday.GenerateHtml();
                    row.Cells[5].Controls.Add(new LiteralControl(Friday.GenerateHtml()));
                }
                var Saturday = d.Value.FirstOrDefault(x => x.DayOfWeek == DayOfWeek.Saturday);
                if (Saturday != null)
                {
                    //((TextBox)e.Row.FindControl("lblSaterday")).Text = Saturday.GenerateHtml();
                    row.Cells[6].Controls.Add(new LiteralControl(Saturday.GenerateHtml()));
                }
            }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            RefreshGrid(gv);
        }

        protected void btnPrevMonth_ServerClick(object sender, EventArgs e)
        {
            ActionMonth = ActionMonth.AddMonths(-1);
            RefreshGrid(gv);
        }

        protected void btnNextMonth_ServerClick(object sender, EventArgs e)
        {
            ActionMonth = ActionMonth.AddMonths(1);
            RefreshGrid(gv);
        }
    }
}