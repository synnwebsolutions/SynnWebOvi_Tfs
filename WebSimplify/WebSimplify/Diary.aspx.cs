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
using WebSimplify.Controls;
using CalendarUtilities;

namespace WebSimplify
{
    public partial class Diary : SynnWebFormBase
    {
        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.Diary };
                return l;
            }
        }

        public IEnumerable GetCalendarItems()
        {
            var dbData =  DBController.DbCalendar.Get(new CalendarSearchParameters { FromDate = DateTime.Now.Date });
            return dbData.OrderBy(x => x.Date).ToList();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (dvDisplay.Visible)
            {
                RefreshGrid(gvDiaryItems);
                gvDiaryItems.Columns[gvDiaryItems.Columns.Count - 1].Visible = CurrentUser.IsAdmin;
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvDiaryItems.ID)
                return "GetCalendarItems";
            return base.GetGridSourceMethodName(gridId);
        }

        protected void btnadddiary_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInputs(txadddiaryname, txadddiarydesc, txadddiarydate, txadddiaryHour))
            {
                var c = new MemoItem
                {
                    UserId = CurrentUser.Id,
                    CreationDate = DateTime.Now,
                    title = txadddiaryname.Value,
                    Description = txadddiarydesc.Value,
                    Date = Convert.ToDateTime(txadddiarydate.Value).AddHours(txadddiaryHour.GetHours()?? 0).AddMinutes(txadddiaryHour.GetMinutes() ?? 0),
                    RepeatEvery = (RepeatEvery)cmbRepeatEvery.SelectedValue.ToInteger(),
                    Shared = cmbShareVals.SelectedValue.ToInteger() == (int)MemoSharingEnum.YES
                };
                DBController.DbCalendar.Add(c);
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
                FillEnum(cmbRepeatEvery, typeof(RepeatEvery), false);
                FillEnum(cmbShareVals, typeof(MemoSharingEnum), false);
                btnadddiary.Disabled = CurrentUser.IsAdmin;
                SetToggleTitle();
            }
        }

        private void SetToggleTitle()
        {
            btnToggleState.InnerText = !dvInsert.Visible ? "הוספה" : "צפייה";
        }

        protected void gvDiaryItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //var d = (LogItem)e.Row.DataItem;
                var memoItem = (MemoItem)e.Row.DataItem;
                var job = DBController.DbGenericData.GetGenericData<CalendarJob>(new CalendarJobSearchParameters { MemoId = memoItem.Id }).FirstOrDefault();
                LoggedUser u = DBController.DbAuth.GetUser(memoItem.UserId);

                ((Label)e.Row.FindControl("lblDate")).Text = memoItem.Date.ToString();
                ((Label)e.Row.FindControl("lblMessage")).Text = memoItem.title;
                ((Label)e.Row.FindControl("lblStatus")).Text =job != null ?(job.JobStatus == CalendarJobStatusEnum.Closed ? "נרשם בהצלחה" : job?.JobStatus.GetDescription()) : string.Empty;
                ((Label)e.Row.FindControl("lblAction")).Text = job?.JobMethod.GetDescription();
                ((Label)e.Row.FindControl("lblUpdate")).Text = job?.UpdateDate.ToString();
                ((Label)e.Row.FindControl("lblId")).Text = memoItem.Description.ToString();
                ((Label)e.Row.FindControl("lblUser")).Text = u.DisplayName;
            }
        }

        protected void btnToggleState_ServerClick(object sender, EventArgs e)
        {
            ChangeView();
        }

        private void ChangeView()
        {
            dvInsert.Visible = !dvInsert.Visible;
            dvDisplay.Visible = !dvDisplay.Visible;
            SetToggleTitle();
        }
    }
}