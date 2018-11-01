using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSimplify.Helpers;

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

        protected override string NavIdentifier
        {
            get
            {
                return "navdiary";
            }
        }

        protected void btnadddiary_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInputs(txadddiaryname, txadddiarydesc, txadddiarydate))
            {
                var c = new MemoItem
                {
                    title = txadddiaryname.Value,
                    Description = txadddiarydesc.Value,
                    Date = Convert.ToDateTime(txadddiarydate.Value)
                };
                var sp = new CalendarSearchParameters { InsertItem = c };
                DBController.DbCalendar.Add(sp);
                AlertMessage("פעולה זו בוצעה בהצלחה");
                ClearInputs(txadddiaryname, txadddiarydesc, txadddiarydate);
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActionMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                cdr.VisibleDate = ActionMonth;
                cdr.SelectedDate = DateTime.Now;
            }
        }

        protected void cdr_DayRender(object sender, DayRenderEventArgs e)
        {
            if (ActionMonth.Year == e.Day.Date.Year && ActionMonth.Month == e.Day.Date.Month)
            {
                bool hasVal = false;
                DateTime Today = DateTime.Today;

                var HebCal = new HebrewCalendar();
                //int curYear = HebCal.GetYear(Today);    //current numeric hebrew year
                //int curMonth = HebCal.GetMonth(Today);

                e.Cell.ToolTip = e.Day.Date.ToString("MMMM dd, yyyy");
                e.Cell.Text = GetDiaryForDate(e.Day.Date, ref hasVal);
                e.Cell.CssClass = "shiftcell ";
                if (hasVal)
                    e.Cell.CssClass += "shiftcellactive";
                else
                    e.Cell.CssClass += "shiftcellvalid";
            }
            else
                e.Cell.Text = string.Empty;
        }

        private string GetDiaryForDate(DateTime date, ref bool hasval)
        {
            List<MemoItem> currentData = DBController.DbCalendar.Get(new CalendarSearchParameters { FromDate = date, ToDate = date.AddHours(23).AddMinutes(59) });
            StringBuilder sb = new StringBuilder();
            sb.Append(date.Day.ToString() + HtmlStringHelper.LineBreak);
            if (currentData != null)
            {
                foreach (var si in currentData)
                {
                    sb.AppendFormat("{0} - {1}", si.title, si.Description);
                    sb.Append(HtmlStringHelper.LineBreak);
                    hasval = true;
                }
            }
            return sb.ToString();
        }

        protected void cdr_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            ActionMonth = new DateTime(e.NewDate.Year, e.NewDate.Month, 1);
        }
    }
}