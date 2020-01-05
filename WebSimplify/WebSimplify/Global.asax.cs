using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using WebSimplify;
using WebSimplify.BackGroundData;

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
        public static string SfcC = "1x2w3e4z";

        public static bool ClearDb
        {
            get { return ConfigurationManager.AppSettings["ClearDb"] == "1"; }
        }
        protected void Application_Start(object sender, EventArgs e)
        {
            Dbl = DBController.DbLog;
            MigrationHandler.Perform();
            StartBackgroundWorkers();
#if debug
            ExcelHelper.Perform(DBController);
#endif
        }

        BackgroundWorkerDispatcher backgroundWorker;
        private void StartBackgroundWorkers()
        {
            backgroundWorker = new BackgroundWorkerDispatcher();
            backgroundWorker.Workers.Add(new CalendarItemsBackgroundWorker { DBController = DBController });

            backgroundWorker.Start();
        }

        public static void PerformFirstInserts(IDatabaseProvider _DBr)
        {
            try
            {
                _DBr.DbAuth.Add(new LoggedUser
                {
                    AllowedClientPagePermissions = new List<ClientPagePermissions>(),
                    DisplayName = "Smach",
                    Password = "sm1234",
                    UserName = "smach",
                    EmailAdress = "samadela@gmail.com",
                    Preferences = new WebSimplify.Data.UserAppPreferences
                    {
                    }
                });
                _DBr.DbAuth.Add(new LoggedUser
                {
                    AllowedClientPagePermissions = new List<ClientPagePermissions>(),
                    DisplayName = "Noa",
                    Password = "ns1234",
                    UserName = "noa",
                    EmailAdress = "noae1705@gmail.com",
                    Preferences = new WebSimplify.Data.UserAppPreferences
                    {
                    }
                });

                DBController.DbGenericData.Add(new SystemMailingSettings
                {
                    EmailsGenericSubject = "מנהל היומן האוטומטי של אדלה",
                    NetworkCredentialPassword = StringCipher.Encrypt("ns120315", SfcC),
                    NetworkCredentialUserName = StringCipher.Encrypt("synnwebsolutions@gmail.com", SfcC),
                    SystemEmailAddress = StringCipher.Encrypt("synnwebsolutions@gmail.com", SfcC),
                    SystemName = "מערכת העזר של אדלה"
                });

                var smachUserId = _DBr.DbAuth.GetUsers(new UserSearchParameters { UserName = "smach", Password = "sm1234" }).FirstOrDefault().Id;
                var noaUserId = _DBr.DbAuth.GetUsers(new UserSearchParameters { UserName = "noa", Password = "ns1234" }).FirstOrDefault().Id;

                var sharingSetts = new UserMemoSharingSettings { OwnerUserId = smachUserId, UsersToShare = new List<int> { noaUserId } };
                _DBr.DbGenericData.Add(sharingSetts);

                sharingSetts = new UserMemoSharingSettings { OwnerUserId = noaUserId, UsersToShare = new List<int> { smachUserId } };
                _DBr.DbGenericData.Add(sharingSetts);
            }
            catch (Exception ex)
            {
                string cc = ex.Message;
            }
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
            if(backgroundWorker != null)
                backgroundWorker.Stop();
        }
    }
}