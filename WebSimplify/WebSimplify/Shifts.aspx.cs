using SynnCore.Generics;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSimplify.Data;
using WebSimplify.Helpers;

namespace WebSimplify
{
    public partial class Shifts : SynnWebFormBase
    {
        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.Shifts };
                return l;
            }
        }
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

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvAdd.ID)
                return DummyMethodName;
            return base.GetGridSourceMethodName(gridId);
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

                RefreshGrid(gvAdd);
            }
        }
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            var inPeriod = CurrentPeriodStart.InTwoWeekPeriod(e.Day.Date);
            if (inPeriod)
            {
                e.Cell.ToolTip = e.Day.Date.ToString("MMMM dd, yyyy");
                e.Cell.Text = GetShiftsForDate(e.Day.Date);
                if (!string.IsNullOrEmpty(e.Cell.Text))
                    e.Cell.CssClass = "shiftcellbase shiftcellactive";
                else
                    e.Cell.CssClass = "shiftcellbase shiftcellvalid";

                if (e.Day.Date == DateTime.Now.Date)
                    e.Cell.BorderColor = System.Drawing.Color.Red;
            }
            else
            {
                e.Cell.Visible = false;
                e.Cell.CssClass = "shiftcellbase";
            }
        }


        List<ShiftDayData> cm;
        protected List<ShiftDayData> CurrentMonthData
        {
            get
            {
                if (cm == null)
                    cm = DBController.DbShifts.GetShifts(new ShiftsSearchParameters { FromDate = DateTime.Now.StartOfMonth(), ToDate = DateTime.Now.EndOfMonth() });
                return cm;
            }
        }

        private string GetShiftsForDate(DateTime date)
        {
            List<ShiftDayData> currentData = CurrentMonthData.Where(x => x.Date.Date == date.Date).ToList();
            StringBuilder sb = new StringBuilder();
            if (currentData != null)
            {
                foreach (var si in currentData.OrderBy( x => Convert.ToInt32(x.DaylyShift)).ToList())
                {
                    // add shift owner name
                    //string jsEditButton = string.Format("<button class=\"btnedt\" id=\"btnedt{0}\"><i class=\"fas fa-pencil-alt\"></i></button>", si.Id.ToString());
                    string jsDeleteButton = string.Format("<button class=\"btndlt\" id=\"btndlt{0}\"><i class=\"far fa-trash-alt\"></i></button>", si.Id.ToString());
                    LoggedUser owner = DBController.DbAuth.GetUser(si.OwnerId);
                    sb.AppendFormat("{0}{1} - {2}", jsDeleteButton, owner.DisplayName, GenericFormatter.GetEnumDescription(si.DaylyShift));
                    sb.Append(HtmlStringHelper.LineBreak);
                }
            }
            return sb.ToString();
        }
        

        [WebMethod]
        [ScriptMethod()]
        public static void PerformDelete(string btnidentifier)
        {
            try
            {
                var id = Convert.ToInt32(btnidentifier.Replace("btndlt", string.Empty));
                DBController.DbShifts.Delete(id);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        protected void gvAdd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var cmbShifts = ((DropDownList)e.Row.FindControl("cmbShifts"));
                ((TextBox)e.Row.FindControl("txDate")).Text = string.Empty;

                foreach (Enum se in Enum.GetValues(typeof(ShiftTime)))
                {
                    ListItem item = new ListItem(GenericFormatter.GetEnumDescription(se), (se).ToString());
                    cmbShifts.Items.Add(item);
                }
            }
        }

        protected void btnAdd_Command(object sender, CommandEventArgs e)
        {
            var row = (sender as ImageButton).NamingContainer as GridViewRow;

            var cmbShifts = ((DropDownList)row.FindControl("cmbShifts"));
            var txDate = ((TextBox)row.FindControl("txDate"));

            if (cmbShifts.SelectedIndex > 0  && txDate.Text.NotEmpty())
            {
                ShiftDayData d = new ShiftDayData();
                d.Date = Convert.ToDateTime(txDate.Text);
                ShiftTime es;
                Enum.TryParse(cmbShifts.SelectedValue, out es);
                d.DaylyShift = es;
                d.OwnerId = CurrentUser.Id;
                d.UserGroupId = CurrentUser.AllowedSharedPermissions[0];

                DBController.DbShifts.Save(new ShiftsSearchParameters { ItemForAction = d });
                AlertMessage("פעולה זו בוצעה בהצלחה");
                RefreshGrid(gvAdd);
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }
    }
}