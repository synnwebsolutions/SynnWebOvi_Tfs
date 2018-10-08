using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SynnWebOvi
{
    public partial class Log : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RefreshGrid(gv);
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetData";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetData()
        {
            LogSearchParameters lsp = new LogSearchParameters();
            lsp.Text = txSearchTxt.Text;
            List<LogItem> items = DBController.DbLog.GetLogs(lsp);
            return items;
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (LogItem)e.Row.DataItem;
                ((Label)e.Row.FindControl("lblDate")).Text = d.Date.ToString();
                ((Label)e.Row.FindControl("lblMessage")).Text = d.Message;
                ((Label)e.Row.FindControl("lblTrace")).Text = d.Trace;
            }
        }

        protected void btnSrc_Click(object sender, EventArgs e)
        {
            RefreshGrid(gv);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txSearchTxt.Text = string.Empty;
            RefreshGrid(gv);
        }
    }
}