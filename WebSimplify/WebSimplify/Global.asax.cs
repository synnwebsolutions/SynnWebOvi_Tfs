using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using WebSimplify;

namespace SynnWebOvi
{
    public class Global : System.Web.HttpApplication
    {
        public static IDatabaseProvider DBController = SynnDataProvider.DbProvider;
        public static string PassPhrase = "682";
        internal static IDbLog Dbl;

        public static string AdminUserName = "sami";
        public static string AdminPass = "1xfdahshbdjh]_7gdks";
        public static string FirstLoginPass = "1x2w3e4z";

        protected void Application_Start(object sender, EventArgs e)
        {
            Dbl = DBController.DbLog;
            MigrationHandler.Perform();
#if debug
            ExcelHelper.Perform(DBController);
#endif
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            string st = "";
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string st = "";
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                var lastExc = Server.GetLastError();
                var errcode = Dbl.AddLog(lastExc);
                StoreEx(lastExc);
            }
            catch (Exception ex)
            {
                StoreEx(ex);
            }
            SynNavigation.Goto(Pages.ErrorPage);
        }

        private static void StoreEx(Exception lastExc)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Session["ex"] = lastExc;
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}