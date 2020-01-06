using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class WebSimplify : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                //dvVrs.InnerText = $"| V. :  { Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0,5)} |";
                dvVrs.InnerText = string.Format(dvVrs.InnerText, Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 5));
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnMessageBoxOk_Click(object sender, EventArgs e)
        {
            messageBoxx.Hide();
        }

        protected void nvLogout_ServerClick(object sender, EventArgs e)
        {
            HttpContext.Current.Session[SynNavigation.SignOutKey] = 1;
            SynNavigation.Goto(Pages.Login);
        }
    }
}