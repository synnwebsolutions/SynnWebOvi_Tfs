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
        protected override string NavIdentifier
        {
            get
            {
                return "navlog";
            }
        }
        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetData";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetData()
        {
            var lp = new LogSearchParameters() {  };
            List<LogItem> items = DBController.DbLog.GetLogs(lp).OrderByDescending(x => x.Date).ToList();
            return items;
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (LogItem)e.Row.DataItem;
                ((Label)e.Row.FindControl("lblDate")).Text = d.Date.ToString(); ;
                ((Label)e.Row.FindControl("lblMessage")).Text = d.Message.ToString();
                ((Label)e.Row.FindControl("lblTrace")).Text = d.Trace.ToString();
            }
        }
    }
}