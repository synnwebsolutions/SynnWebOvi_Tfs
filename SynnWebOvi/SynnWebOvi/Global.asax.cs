using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SynnWebOvi
{
    public class Global : System.Web.HttpApplication
    {
        internal IDatabaseProvider DBController = SynnDataProvider.DbProvider;
        internal IDbLog Dbl;
        protected void Application_Start(object sender, EventArgs e)
        {
            Dbl = DBController.DbLog;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var lastExc =  Server.GetLastError();
            var errcode = Dbl.AddLog(lastExc);
            var url = string.Format("{0}?errcode={1}", SynNavigation.Pages.ErrorPage, errcode);
            SynNavigation.Goto(url);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}