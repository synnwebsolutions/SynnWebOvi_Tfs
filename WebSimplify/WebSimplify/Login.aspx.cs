using SynnCore.Security;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class Login : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override bool LoginProvider
        {
            get
            {
                return true;
            }
        }

        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            string xu = txUname.Value;
            string xp = txPass.Value;
            if (xu == Global.AdminUserName && xp == Global.AdminPass)
                CurrentUser = new LoggedUser("Smachew", 1);
            else
            {
                if (DBController.DbAuth.ValidateUserCredentials(xu, xp))
                    CurrentUser = DBController.DbAuth.LoadUserSettings(xu, xp);
                else
                {
                    AlertMessage("שם משתמש או סיסמה לא נכונים");
                    return;
                }
            }
            if (CurrentUser != null)
                SynNavigation.Goto(SynNavigation.Pages.Main);
        }
    }
}