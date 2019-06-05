using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class WebSimplify : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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