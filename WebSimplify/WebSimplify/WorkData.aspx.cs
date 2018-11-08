using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class WorkData : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 RefreshGrid(gvWorkHours);
                if (!CurrentUser.Preferences.DailyRequiredWorkHours.NotNull())
                {
                    AlertMessage("יש להזין אורך משמרת - במסך הגדרות משתמש");
                }
            }
        }

        public WorkHoursData CurrentData
        {
            get
            {
                
                var cData = GetFromSession("crw*");
                if(cData == null)
                {
                    List<WorkHoursData> ul = DBController.DbShifts.GetWorkHoursData(new WorkHoursSearchParameters { Month = DateTime.Now });
                    StoreInSession("crw*", ul.FirstOrDefault());
                }
                return (WorkHoursData)GetFromSession("crw*");
            }
        }

        protected override bool HasNavLink
        {
            get
            {
                return false;
            }
        }
        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvWorkHours.ID)
                return "GetWorkHoursData";
            return base.GetGridSourceMethodName(gridId);
        }

        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.WorkHours };
                return l;
            }
        }

        public IEnumerable GetWorkHoursData()
        {
            List<WorkHoursData> ul = DBController.DbShifts.GetWorkHoursData(new WorkHoursSearchParameters { Month = DateTime.Now });
            return ul;
        }

        protected void gvWorkHours_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (WorkHoursData)e.Row.DataItem;
                ((Label)e.Row.FindControl("lblDate")).Text = DateTime.Now.HebrewDayAndMonth();
                if (!CurrentUser.IsAdmin)
                {

                    txCurrentDailyRequired.Text = CurrentUser.Preferences.DailyRequiredWorkHours.NotNull() ? CurrentUser.Preferences.DailyRequiredWorkHours.ToTimeDisplay() : string.Empty;
                    txCurrentMonthState.Text = d.CurrentMonthTotal.ToTimeDisplay();
                    if (d.CurrentMonthTotal.Hour < 0 || (d.CurrentMonthTotal.Hour == 0 && d.CurrentMonthTotal.Minute < 0))
                        txCurrentMonthState.ForeColor = System.Drawing.Color.Red;
                    else
                        txCurrentMonthState.ForeColor = System.Drawing.Color.Black;

                    txCurrentShifStart.Text = d.CurrentShiftStart.NotFilled() ? string.Empty : d.CurrentShiftStart.ToTimeDisplay();
                    txCurrentShiftLeft.Text = d.CurrentShiftTimeLeft(CurrentUser.Preferences.DailyRequiredWorkHours);
                }
            }
        }

        protected void btnAddWorkItem_Command(object sender, CommandEventArgs e)
        {
            var row = (sender as ImageButton).NamingContainer as GridViewRow;

            var txHour = ((TextBox)row.FindControl("txHour")).Text;
            var txMinute = ((TextBox)row.FindControl("txMinute")).Text;
            if (txMinute.IsEmpty() || txHour.IsEmpty())
            {
                return;
            }
            CurrentData.HandleShift(txHour.ToInteger(), txMinute.ToInteger(), CurrentUser.Preferences.DailyRequiredWorkHours);
            DBController.DbShifts.UpdateWorkMonthlyData(CurrentData);
            RefreshGrid(gvWorkHours);
        }
    }
}