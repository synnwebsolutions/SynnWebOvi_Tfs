using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
                    StringBuilder sb = new StringBuilder();
                    var first = true;
                    do
                    {
                        if (first) first = false;
                        else
                            sb.Append($"===== Inner Exception Info =====");
                        
                        sb = ExtractExceptionInfo(ex, sb);
                        ex = ex.InnerException;
                    }
                    while (ex != null);


                    exmsg.InnerHtml = sb.ToString();
                }
            }
        }

        private StringBuilder ExtractExceptionInfo(Exception ex, StringBuilder sb)
        {
            exTtl.InnerText = ex.Message;
            var traceInfo = new StackTrace(ex, true);
            var num = traceInfo.FrameCount;
            
            for (int i = 0; i < num; i++)
            {
                int linenumber = traceInfo.GetFrame(i).GetFileLineNumber();
                var fileN = traceInfo.GetFrame(i).GetFileName();
                var shortFile = Path.GetFileName(fileN);
                if(fileN.NotEmpty())
                sb.Append($"{shortFile ?? fileN} Line - {linenumber} {Helpers.HtmlStringHelper.LineBreak}");
            }

            return sb;
        }
    }
}