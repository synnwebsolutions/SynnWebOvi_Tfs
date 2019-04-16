using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace xWebControls.Calendar
{
    public partial class xCalendar : System.Web.UI.UserControl
    {
        public xCalendarMode DisplayMode { get; set; }
        //private List<XCalendarItem> Items
        //{
        //    set
        //    {
        //        HttpContext.Current.Session["idxxs"] = value;
        //    }
        //    get
        //    {
        //        if(HttpContext.Current.Session["idxxs"] != null)
        //            return (List<XCalendarItem>)HttpContext.Current.Session["idxxs"];
        //        return new List<XCalendarItem>();
        //    }
        //}
        public bool ShowDayHeader { get; set; }
        public DateTime? IDate { get; set; }

        public string GetDataSourceMethodName { get; set; }
        public string cssMainContainerClass { get; set; }
        public string cssMainHeadersClass { get; set; }
        public string cssDayItemContainerClass { get; set; }
        public string cssToDayItemClass { get; set; }
        public string cssDayItemHeaderClass { get; set; }
        public string cssDayItemClass { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //FillCalendar();
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            FillCalendar();
        }

        public void RefreshView()
        {
            FillCalendar();
        }

        private void FillCalendar()
        {
            tbl.Controls.Clear();
            Table tb = new Table();
            tb.CssClass = cssMainContainerClass ?? Consts.cssMainContainerClass;
            GenerateWeekHeaders(tb);
            DateTime d = IDate.HasValue ? IDate.Value : DateTime.Now.StartOfWeek();
            int startMonth = d.Month;
            int numberOfWeeks = (int)DisplayMode;

            MethodInfo m = Page.GetType().GetMethod(GetDataSourceMethodName);
            var Items = (List<XCalendarItem>)m.Invoke(Page, new object[] { });

            for (int i = 0; i < numberOfWeeks; i++) 
            {
                TableRow weekRow = new TableRow();
                for (int j = 0; j < 7; j++)
                {
                    while (j < (int)d.DayOfWeek)
                    {
                        weekRow.AddWeekDay(new Table()); // irelevant date
                        j++;
                    }
                    if (d.IsSameMonth(startMonth))
                    {
                        var xd = new xCalendarDay(d);
                        xd.DateInfos = Items.Where(x => x.Date == d.Date).ToList();
                        var innerTable = xd.GetDay();
                        innerTable.SetStyles(cssDayItemHeaderClass ?? Consts.cssDayItemHeaderClass, cssDayItemClass ?? Consts.cssDayItemClass);
                        innerTable.CssClass = cssDayItemContainerClass ?? Consts.cssDayItemContainerClass;
                        if (d.IsToday())
                            innerTable.CssClass = string.Format("{0} {1}", innerTable.CssClass, cssToDayItemClass ?? Consts.cssToDayItemClass);

                        weekRow.AddWeekDay(innerTable);
                    }
                    d = d.AddDays(1);
                }
                tb.Controls.Add(weekRow);
            }
            tbl.Controls.Add(tb);
        }

        private void GenerateWeekHeaders(Table tb)
        {
            TableRow r = new TableRow();
            for (int i = 0; i < 7; i++)
            {
                TableHeaderCell hc = new TableHeaderCell();
                hc.Text = ((DayOfWeek)i).HebrewDay();
                hc.CssClass = cssMainHeadersClass ?? Consts.cssMainHeadersClass;
                r.Cells.Add(hc);
            }
            tb.Rows.Add(r);
        }
    }

    public enum xCalendarMode
    {
        Week = 1,
        TwoWeek = 2,
        Month = 4
    }
}