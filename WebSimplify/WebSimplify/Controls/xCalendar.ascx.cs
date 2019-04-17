using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify.Controls
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

        public SynnWebFormBase IPage
        {
            get
            {
                return (SynnWebFormBase)Page;
            }
        }

        public string PageKey
        {
            get
            {
                var tp = IPage.GetType();
                return string.Format("{0}_",  tp.Name) ;
            }
        }

        public DateTime? IDate
        {
            get
            {
                if (IPage.GetFromSession(PageKey + "iDateTime_*") != null)
                    return (DateTime?)IPage.GetFromSession(PageKey + "iDateTime_*");
                return null;
            }
            set
            {
                IPage.StoreInSession(PageKey + "iDateTime_*", value);
            }
        }

        public string GetDataSourceMethodName { get; set; }
        public string cssMainContainerClass { get; set; }
        public string cssMainHeadersClass { get; set; }
        public string cssDayItemContainerClass { get; set; }
        public string cssToDayItemClass { get; set; }
        public string cssDayItemHeaderClass { get; set; }
        public string cssDayItemClass { get; set; }
        public bool ShowSelector { get; set; }

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
            DateTime? endDate = null;
            Table tb = new Table();
            tb.CssClass = cssMainContainerClass ?? xCalendarConsts.cssMainContainerClass;
            GenerateWeekHeaders(tb);
            if (!IDate.HasValue)
                IDate = DateTime.Now;

            if (DisplayMode == xCalendarMode.Month)
                IDate = IDate.Value.StartOfTheMonth();
            else
            {
                ShowSelector = false;
                IDate = IDate.Value.StartOfWeek();
                if (DisplayMode == xCalendarMode.TwoWeek)
                    endDate = IDate.Value.AddDays(14);
                else
                    endDate = IDate.Value.AddDays(7);
            }
            int startMonth = IDate.Value.Month;
            //int numberOfWeeks = IDate.Value.NumberOfWeeksInMonth();
            var tmpDate = IDate;
            MethodInfo m = Page.GetType().GetMethod(GetDataSourceMethodName);
            var Items = (List<XCalendarItem>)m.Invoke(Page, new object[] { IDate, IDate.Value.EndOfMonth() });

            while (tmpDate.Value.IsSameMonth(startMonth))
            {
                if ((!endDate.HasValue || tmpDate.Value.Date < endDate.Value.Date))
                {
                    TableRow weekRow = new TableRow();
                    for (int j = 0; j < 7; j++)
                    {
                        while (j < (int)tmpDate.Value.DayOfWeek)
                        {
                            weekRow.AddWeekDay(new Table()); // irelevant date
                            j++;
                        }
                        if (tmpDate.Value.IsSameMonth(startMonth))
                        {
                            var xd = new xCalendarDay(tmpDate.Value);
                            xd.DateInfos = Items.Where(x => x.Date == tmpDate.Value.Date).ToList();
                            var innerTable = xd.GetDay();
                            innerTable.SetStyles(cssDayItemHeaderClass ?? xCalendarConsts.cssDayItemHeaderClass, cssDayItemClass ?? xCalendarConsts.cssDayItemClass);
                            innerTable.CssClass = cssDayItemContainerClass ?? xCalendarConsts.cssDayItemContainerClass;
                            if (tmpDate.Value.IsToday())
                                innerTable.CssClass = string.Format("{0} {1}", innerTable.CssClass, cssToDayItemClass ?? xCalendarConsts.cssToDayItemClass);

                            weekRow.AddWeekDay(innerTable);
                        }
                        tmpDate = tmpDate.Value.AddDays(1);
                    }
                    tb.Controls.Add(weekRow);
                }
                else
                    tmpDate = tmpDate.Value.AddDays(1);
            }
            SetMonthName();
            tbl.Controls.Add(tb);
        }

        private void GenerateWeekHeaders(Table tb)
        {
            TableRow r = new TableRow();
            for (int i = 0; i < 7; i++)
            {
                TableHeaderCell hc = new TableHeaderCell();
                hc.Text = ((DayOfWeek)i).HebrewDay();
                hc.CssClass = cssMainHeadersClass ?? xCalendarConsts.cssMainHeadersClass;
                r.Cells.Add(hc);
            }
            tb.Rows.Add(r);
        }

        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            ChangeDate(1);
        }

        protected void btnPrev_Click(object sender, ImageClickEventArgs e)
        {
            ChangeDate(-1);
        }

        private void ChangeDate(int ik)
        {
            var d = IDate ?? DateTime.Now;
            switch (DisplayMode)
            {
                case xCalendarMode.Week:
                    d = d.AddDays(7 * ik);
                    break;
                case xCalendarMode.TwoWeek:
                    d = d.AddDays(7 * ik);
                    break;
                case xCalendarMode.Month:
                    d = d.AddMonths(1 * ik);
                    break;
                default:
                    break;
            }
            IDate = d;
            SetMonthName();
        }

        private void SetMonthName()
        {
            selector.Visible = ShowSelector;
            txDisplay.Text = IDate.Value.HebrewMonthNameWithYear();
        }
    }

    public enum xCalendarMode
    {
        Week = 1,
        TwoWeek = 2,
        Month = 4
    }
}