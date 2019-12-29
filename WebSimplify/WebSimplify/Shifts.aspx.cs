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
using WebSimplify.Controls;
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

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvAdd.ID)
                return DummyMethodName;
            return base.GetGridSourceMethodName(gridId);
        }

        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //WsCalendar.StartDate = DateTime.Now.StartOfWeek();
                RefreshView();
            }
        }

        public List<XCalendarItem> GetCalendarItems(DateTime? StartDate = null, DateTime? EndDate = null)
        {
            var d = new List<XCalendarItem>();
            var dbData = DBController.DbShifts.GetShifts(new ShiftsSearchParameters
            {
                FromDate = StartDate.HasValue ? StartDate.Value.Date : DateTime.Now.StartOfMonth().Date,
                ToDate = EndDate.HasValue ? EndDate.Value.Date : DateTime.Now.EndOfMonth().Date
            }).Select(x => x as ICalendarItem).ToList();
            foreach (var dbitem in dbData)
                d.Add(new XCalendarItem { Date = dbitem.Date, Text = dbitem.Display });
            return d;
        }

        //[WebMethod]
        //[ScriptMethod()]
        //public static void PerformDelete(string btnidentifier)
        //{
        //    try
        //    {
        //        var id = Convert.ToInt32(btnidentifier.Replace("btndlt", string.Empty));
        //        DBController.DbShifts.Delete(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.Message;
        //    }
        //}

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

            if (txDate.Text.NotEmpty())
            {
                ShiftDayData d = new ShiftDayData();
                d.Date = Convert.ToDateTime(txDate.Text);
                ShiftTime es;
                Enum.TryParse(cmbShifts.SelectedValue, out es);
                d.DaylyShift = es;
                d.OwnerId = CurrentUser.Id;


                var exsistingShifts = DBController.DbShifts.GetShifts(new ShiftsSearchParameters { IDate = d.Date, DaylyShiftTime = d.DaylyShift });
                if (exsistingShifts.IsEmptyOrNull())
                {
                    DBController.DbShifts.Save(new ShiftsSearchParameters { ItemForAction = d });
                    AlertMessage("פעולה זו בוצעה בהצלחה");
                }
                RefreshView();
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }

        private void RefreshView()
        {
            RefreshGrid(gvAdd);
            //WsCalendar.RefreshView();
        }
    }
}