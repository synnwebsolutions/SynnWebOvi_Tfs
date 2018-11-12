using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebSimplify.Data;

namespace WebSimplify
{
    public partial class Main : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtDiary.Visible = CurrentUser.Allowed(ClientPagePermissions.Diary);
                dtShifts.Visible = CurrentUser.Allowed(ClientPagePermissions.Shifts);
                dtTasks.Visible = CurrentUser.Allowed(ClientPagePermissions.QuickTasks);
                dvWorkHours.Visible = CurrentUser.Allowed(ClientPagePermissions.WorkHours);
                FillData();
            }
        }

        private void FillData()
        {
            var startOfWeek = DateTime.Now.StartOfWeek();
            if (CurrentUser.Allowed(ClientPagePermissions.Diary))
            {
                rpCalendar.DataSource =  DBController.DbCalendar.Get(new CalendarSearchParameters { FromDate = startOfWeek, ToDate = DateTime.Now.EndOfWeek() }).OrderBy(x => x.Date)
                    .ToList(); 
                rpCalendar.DataBind();
            }
            if (CurrentUser.Allowed(ClientPagePermissions.Shifts))
            {
                var nextShift = DBController.DbShifts.GetShifts(new ShiftsSearchParameters { FromDate = DateTime.Now, ToDate = DateTime.Now.EndOfWeek() }).OrderBy(x => x.Date)
                    .Take(1).ToList();
                rpShift.DataSource = nextShift;
                rpShift.DataBind();
            }
            if (CurrentUser.Allowed( ClientPagePermissions.QuickTasks))
            {
                List<QuickTask> items = DBController.DbCalendar.Get(new QuickTasksSearchParameters {Active = true});
                rpTasks.DataSource = items;
                rpTasks.DataBind();
            }
        }
        

        protected void rpTasks_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                QuickTask im = (QuickTask)e.Item.DataItem;
                //Label lblName = (Label)e.Item.FindControl("lblName");
                Label lblDesc = (Label)e.Item.FindControl("lblDesc");

                //lblName.Text = im.CreationDate.HebrewDayName();
                lblDesc.Text = string.Format("{0} -  {1}", im.MarkableName, im.MarkableDescription);
            }
        }

        protected void rpCalendar_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MemoItem im = (MemoItem)e.Item.DataItem;
                Label lblName = (Label)e.Item.FindControl("lblName");
                Label lblDesc = (Label)e.Item.FindControl("lblDesc");

                lblName.Text = im.Date.HebrewDayName();
                lblDesc.Text = string.Format("{0} -  {1}", im.MarkableName, im.MarkableDescription);
            }
        }

        protected void rpShift_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ShiftDayData im = (ShiftDayData)e.Item.DataItem;
                Label lblName = (Label)e.Item.FindControl("lblName");
                Label lblDesc = (Label)e.Item.FindControl("lblDesc");

                lblName.Text = im.Date.HebrewDayName();
                lblDesc.Text = im.MarkableName;
            }
        }

        protected void btnWorkHours_ServerClick(object sender, EventArgs e)
        {
            SynNavigation.Redirect("WorkData.aspx");
        }

        protected void btnDevTasks_ServerClick(object sender, EventArgs e)
        {
            SynNavigation.Redirect("DevTasks.aspx");
        }
    }
    
}