using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class ErrorPage : SynnWebFormBase
    {
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Exception ex = (Exception)GetFromSession("ex");
                if (CurrentUser.IsAdmin)
                {
                    exTtl.InnerText = ex.Message;
                    var traceInfo = new StackTrace(ex, true);
                    var num = traceInfo.FrameCount;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < num; i++)
                    {
                        int linenumber = traceInfo.GetFrame(i).GetFileLineNumber();
                        var fileN = traceInfo.GetFrame(i).GetFileName();
                        sb.AppendLine($"{fileN} At Line - {linenumber}");
                    }
                    exmsg.InnerText = sb.ToString();
                }
            }
        }
    }
}