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
                if (!string.IsNullOrEmpty(e.Cell.Text))
                    e.Cell.CssClass = "shiftcellbase shiftcellactive";
                else
                    e.Cell.CssClass = "shiftcellbase shiftcellvalid";
            }
            else
            {
                e.Cell.Visible = false;
                e.Cell.CssClass = "shiftcellbase";
            }
        }

        private string GetShiftsForDate(DateTime date)
        {
            List<ShiftDayData> currentData = DBController.DbShifts.GetShifts(new ShiftsSearchParameters {FromDate = date, ToDate = date.AddHours(23).AddMinutes(59) });
            StringBuilder sb = new StringBuilder();
            if (currentData != null)
            {
                foreach (var si in currentData.OrderBy( x => Convert.ToInt32(x.DaylyShift)).ToList())
                {
                    // add shift owner name
                    LoggedUser owner = DBController.DbAuth.GetUser(si.OwnerId);
                    sb.AppendFormat("{0} - {1}",owner.DisplayName, GenericFormatter.GetEnumDescription(si.DaylyShift));
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
            d.DaylyShift = es;
            d.OwnerId = CurrentUser.Id;
            d.UserGroupId = CurrentUser.AllowedSharedPermissions[0];

            DBController.DbShifts.Save(new ShiftsSearchParameters { ItemForAction = d });
            ClearInputs(txadddiarydate);
        }
    }
}