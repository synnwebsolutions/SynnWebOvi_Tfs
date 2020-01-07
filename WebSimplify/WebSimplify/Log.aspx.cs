using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
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
                //var d = (LogItem)e.Row.DataItem;
                var job = (CalendarJob)e.Row.DataItem;
                var memoItem = DBController.DbCalendar.Get(new CalendarSearchParameters { ID = job.MemoItemId }).First();
                LoggedUser u = DBController.DbAuth.GetUser(job.UserId);

                ((Label)e.Row.FindControl("lblUser")).Text = u.DisplayName;
                ((Label)e.Row.FindControl("lblMessage")).Text = memoItem.title;
                ((Label)e.Row.FindControl("lblStatus")).Text = job.JobStatus.GetDescription();
                ((Label)e.Row.FindControl("lblAction")).Text = job.JobMethod.GetDescription();
                ((Label)e.Row.FindControl("lblUpdate")).Text = job.UpdateDate.ToString();
                ((Label)e.Row.FindControl("lblId")).Text = job.Id.ToString();
            }
        }
    }
}