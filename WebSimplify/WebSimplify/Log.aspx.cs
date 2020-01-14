using log4net;
using log4net.Appender;
using log4net.Repository.Hierarchy;
using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class Log : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillEnum(cmbXStatus, typeof(CalendarJobStatusEnum));
                FillEnum(cmbJobType, typeof(CalendarJobMethodEnum));
                RefreshGrid(gv);
            }
        }
        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.SysLog };
                return l;
            }
        }
      
        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetCalendar";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetCalendar()
        {
            return DBController.DbGenericData.GetGenericData<CalendarJob>(new CalendarJobSearchParameters { }).OrderBy(x => x.CreationDate).ToList();
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var job = (CalendarJob)e.Row.DataItem;
                var memoItem = DBController.DbCalendar.Get(new CalendarSearchParameters { ID = job.MemoItemId }).First();

                LoggedUser u = job.UserId > 0 ? DBController.DbAuth.GetUser(job.UserId) : null;

                ((Label)e.Row.FindControl("lblUser")).Text = u?.DisplayName;
                ((Label)e.Row.FindControl("lblMessage")).Text = memoItem.title;
                ((Label)e.Row.FindControl("lblStatus")).Text = job.JobStatus.GetDescription();
                ((Label)e.Row.FindControl("lblAction")).Text = job.JobMethod.GetDescription();
                ((Label)e.Row.FindControl("lblUpdate")).Text = job.UpdateDate.ToString();
                ((Label)e.Row.FindControl("lblId")).Text = job.Id.ToString();
                ((ImageButton)e.Row.FindControl("btnEdit")).CommandArgument = job.Id.ToString();
                ((Label)e.Row.FindControl("lblDestDate")).Text = memoItem.Date.ToString();
                
            }
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            panelx.SetHeader("עריכה");
            var dt = (CalendarJob)DBController.DbGenericData.GetSingleGenericData(new CalendarJobSearchParameters {  Id = e.CommandArgument.ToString().ToInteger(), FromType = typeof(CalendarJob)});
            LoggedUser u = dt.UserId > 0 ? DBController.DbAuth.GetUser(dt.UserId) : null;
            var memoItem = DBController.DbCalendar.Get(new CalendarSearchParameters { ID = dt.MemoItemId }).First();

            cmbXStatus.SelectedValue = ((int)dt.JobStatus).ToString();
            cmbJobType.SelectedValue = ((int)dt.JobMethod).ToString();
            txdUserName.Text = u?.DisplayName;
            txJobDesc.Text = memoItem.Description;
            panelx.SetEditedItemId(dt.Id);
            panelx.Show();
        }

        protected void btnCancelJobEdit_Click(object sender, EventArgs e)
        {
            CloseEdit();
        }

        
        protected void btnOkJobEdit_Click(object sender, EventArgs e)
        {
            if (cmbXStatus.SelectedIndex > 0 && cmbJobType.SelectedIndex > 0)
            {
                var job = (CalendarJob)DBController.DbGenericData.GetSingleGenericData(new CalendarJobSearchParameters { Id = panelx.GetEditedItemId().Value, FromType = typeof(CalendarJob) });

                job.JobMethod = (CalendarJobMethodEnum)cmbJobType.SelectedValue.ToString().ToInteger();
                job.JobStatus = (CalendarJobStatusEnum)cmbXStatus.SelectedValue.ToString().ToInteger();
                DBController.DbGenericData.Update(job);
                panelx.Hide();
                AlertSuccess();
                CloseEdit();
            }
            else
            {
                AlertMessage(@"יש לבחור מצב ו\או סוג");
            }
        }

        private void CloseEdit()
        {
            panelx.Hide();
            RefreshGrid(gv);
        }

        protected void btnDownloadLog_ServerClick(object sender, EventArgs e)
        {
            var rootAppender = ((Hierarchy)LogManager.GetRepository())
                                            .Root.Appenders.OfType<FileAppender>()
                                            .FirstOrDefault();

            string filename = rootAppender != null ? rootAppender.File : string.Empty;
            if (File.Exists(filename))
            {
                DownloadFile("log.txt", File.ReadAllBytes(filename));
            }
        }
    }
}