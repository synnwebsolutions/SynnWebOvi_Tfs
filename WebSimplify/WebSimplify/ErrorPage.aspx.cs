using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    exmsg.InnerText = ex.StackTrace;
                }
            }
        }
    }
}