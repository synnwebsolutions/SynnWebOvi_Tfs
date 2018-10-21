using SynnCore.Generics;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSimplify.Data;
using WebSimplify.Helpers;

namespace WebSimplify
{
    public partial class Shifts : SynnWebFormBase
    {

        public DateTime CurrentPeriodStart
        {
            get
            {
                return (DateTime)GetFromSession("CurrentPeriodStart*");
            }
            set
            {
                StoreInSession("CurrentPeriodStart*", value);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var date = DateTime.Now;
            CurrentPeriodStart = DateTime.Now.StartOfWeek(DayOfWeek.Sunday);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime dt = default(DateTime);
                dt = DateTime.Now;
                cdr.VisibleDate = dt;
                cdr.SelectedDate = dt;

                foreach (Enum se in Enum.GetValues(typeof(ShiftTime)))
                {
                    ListItem item = new ListItem(GenericFormatter.GetEnumDescription(se), (se).ToString());
                    cmbShifts.Items.Add(item);
                }
            }
        }
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            var inPeriod = CurrentPeriodStart.InTwoWeekPeriod(e.Day.Date);
            if (inPeriod)
            {
                e.Cell.Text = GetShiftsForDate(e.Day.Date);
                if(string.IsNullOrEmpty(e.Cell.Text))
                    e.Cell.CssClass = "shiftcell shiftcellactive";
                else
                    e.Cell.CssClass = "shiftcell";
            }
            else
                e.Cell.Visible = false;
        }

        private string GetShiftsForDate(DateTime date)
        {
            UserShiftsContainer currentData = DBController.DbShifts.GetShiftsData(new ShiftsSearchParameters {FromDate = date, ToDate = date });
            StringBuilder sb = new StringBuilder();
            var s = currentData.CrrentUserShifts.FirstOrDefault(x => x.Date.Date == date);
            if (s != null)
            {
                foreach (var si in s.DaylyShifts.OrderBy( x => Convert.ToInt32(x)).ToList())
                {
                    sb.Append(GenericFormatter.GetEnumDescription(si));
                    sb.Append(HtmlStringHelper.LineBreak);
                }
            }
            return sb.ToString();
        }

        protected void btnAddShift_ServerClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txadddiarydate.Value))
            {
                AlertMessage("יש להזין תאריך");
                return;
            }

            ShiftDayData d = new ShiftDayData();
            d.Date = Convert.ToDateTime(txadddiarydate.Value);
            ShiftTime es;
            Enum.TryParse(cmbShifts.SelectedValue, out es);
            d.DaylyShifts.Add(es);
            UserShiftsContainer currentData = DBController.DbShifts.GetShiftsData(new ShiftsSearchParameters());
            var existing = currentData.CrrentUserShifts.FirstOrDefault(x => x.Date.Date == d.Date.Date);
            if (existing == null)
                currentData.CrrentUserShifts.Add(d);
            else
                existing.DaylyShifts.AddRange(d.DaylyShifts);

            DBController.DbShifts.Save(new ShiftsSearchParameters { ItemForAction = currentData });
            ClearInputs(txadddiarydate);
        }
    }
}