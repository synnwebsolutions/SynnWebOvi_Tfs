using SynnCore.Security;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSimplify.Data;

namespace WebSimplify
{
    public partial class Login : SynnWebFormBase
    {
        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            var res = GetFromSession(SynNavigation.SignOutKey);
            if (res != null)
            {
                CurrentUser = null;
                StoreInSession(SynNavigation.SignOutKey, null);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(UserAlias))
                {
                    try
                    {
                        string msg = $"Performin Direct Acces For Alias : {UserAlias}";
                        DBController.DbLog.AddLog(msg);
                        CurrentUser = DBController.DbAuth.LoadUserSettings(UserAlias);
                        StartUpManager.PerformUserStartUp(CurrentUser);
                    }
                    catch 
                    {
                        
                    }
                    SynNavigation.Goto(Pages.Login);
                }
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (CurrentUser != null)
            {
                if (!CurrentUser.IsAdmin)
                {
                    Logger.Instance.Info($"{CurrentUser.DisplayName} Last Logged at : {DateTime.Now}");
                }
                SynNavigation.Goto(Pages.Main);
            }
        }


        protected override bool LoginProvider
        {
            get
            {
                return true;
            }
        }

        public  string UserAlias
        {
            get
            {
                var ux = string.Empty;
                if (Request.QueryString["ux"] != null)
                {
                    ux = Request.QueryString["ux"].ToString();
                }
                return ux;
            }
        }

        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            string xu = txUname.Value;
            string xp = txPass.Value;
#if DEBUG
            if (string.IsNullOrEmpty(txUname.Value))
            {
                //xu = "avr";
                //xp = "avr";
                xu = Global.AdminUserName;
                xp = Global.AdminPass;
            }
#endif
            if (xu == Global.AdminUserName && xp == Global.AdminPass)
                CurrentUser = new LoggedUser("Smachew", 0) {  DisplayName = "Administrator"};
            else
            {
                if (DBController.DbAuth.ValidateUserCredentials(xu, xp))
                {
                    CurrentUser = DBController.DbAuth.LoadUserSettings(xu, xp);
                    CurrentUser.Preferences = DBController.DbGenericData.GetGenericData<UserAppPreferences>(new GenericDataSearchParameters { }).FirstOrDefault();
                    if (CurrentUser.Preferences == null)
                    {
                        CurrentUser.Preferences = new UserAppPreferences
                        {
                            UserId = CurrentUser.Id,
                            CreationDate = DateTime.Now,
                            UpdateDate = DateTime.Now
                        };
                        DBController.DbGenericData.Add(CurrentUser.Preferences);
                    }
                    StartUpManager.PerformUserStartUp(CurrentUser);
                }
                else
                {

                    AlertMessage("שם משתמש או סיסמה לא נכונים");
                    return;
                }
            }
    
        }
    }
}