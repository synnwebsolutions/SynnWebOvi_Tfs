using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SynnWebOvi
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

            if (string.IsNullOrEmpty(txPass.Value) || string.IsNullOrEmpty(txUname.Value))
            {
                AlertMessage("יש להזין שם משתמש וסיסמה");
                return;
            }
            if (!Validate(txUname.Value, txPass.Value))
            {
                AlertMessage("שם משתמש או סיסמה שגויים");
                return;
            }
            LoggedUser u = DBController.DbAuth.LoadUserSettings(txUname.Value, txPass.Value);
            DBController.SetUser(u);
            SynNavigation.Redirect(SynNavigation.Pages.Main);
        }

        private bool Validate(string userName, string passwword)
        {
            return DBController.DbAuth.ValidateUserCredentials(userName, passwword);
        }
    }
}