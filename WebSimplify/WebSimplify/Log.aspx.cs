using SynnWebOvi;
using System;
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

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<LogItem> GetLogItems(string searchtext)
        {
            var lp = new LogSearchParameters() { Text = searchtext };
            List<LogItem> items = DBController.DbLog.GetLogs(lp);
            return items;
        }
    }
}