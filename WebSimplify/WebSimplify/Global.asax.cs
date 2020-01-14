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

        public static string AdminUserName = "sadmin";
        public static string AdminPass = "1xfdahshbdjh]7gdks";

        //public static string AdminUserName = "sami";
        //public static string AdminPass = "1xfdahshbdjh]_7gdks";
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

        public static void PerformMigrationInserts()
        {
            bool isInitializing = false;
            try
            {
                DBController.DbAuth.Add(new LoggedUser
                {
                    AllowedClientPagePermissions = new List<ClientPagePermissions> { ClientPagePermissions.Diary, ClientPagePermissions.QuickTasks, ClientPagePermissions.Shopping,
                    ClientPagePermissions.Wedding, ClientPagePermissions.WorkHours},
                    DisplayName = "סמאץ'",
                    Password = "sm1234",
                    UserName = "smach",
                    EmailAdress = "samadela@gmail.com",
                    Preferences = new WebSimplify.Data.UserAppPreferences
                    {
                    }
                });
                DBController.DbAuth.Add(new LoggedUser
                {
                    AllowedClientPagePermissions = new List<ClientPagePermissions> { ClientPagePermissions.Diary, ClientPagePermissions.QuickTasks, ClientPagePermissions.Shopping,
                    ClientPagePermissions.Wedding, ClientPagePermissions.WorkHours},
                    DisplayName = "נועה",
                    Password = "ns1234",
                    UserName = "noa",
                    EmailAdress = "noae1705@gmail.com",
                    Preferences = new WebSimplify.Data.UserAppPreferences
                    {
                    }
                });
                isInitializing = true;
            }
            catch (Exception ex)
            {
                string cc = ex.Message;
            }

            if (isInitializing)
            {
                AppendParentInits();
                AppendDepositAccountsInits();
                AppendMailingSettingsInits();
            }
           
        }

        private static void AppendDepositAccountsInits()
        {
            DBController.DbGenericData.Add(new Account
            {
                DepositName = "החזר לנחום ואלמו",
                AmountForPerson = 2280,
                DepositType = DepositTypeEnum.FixedAmount,
                StartDate = new DateTime(2019, 07, 01)
            });
            DBController.DbGenericData.Add(new Account
            {
                DepositName = "הוראת קבע חודשית",
                AmountForPerson = 50,
                DepositType = DepositTypeEnum.MonthlyPayment,
                StartDate = new DateTime(2019, 07, 01)
            });
        }

        private static void AppendMailingSettingsInits()
        {
            var mailingSettings = DBController.DbGenericData.GetGenericData<SystemMailingSettings>(new GenericDataSearchParameters { }).FirstOrDefault();

            if (mailingSettings == null)
            {
                DBController.DbGenericData.Add(new SystemMailingSettings
                {
                    EmailsGenericSubject = "מנהל היומן האוטומטי של אדלה",
                    NetworkCredentialPassword = StringCipher.Encrypt("ns120315"),
                    NetworkCredentialUserName = StringCipher.Encrypt("synnwebsolutions@gmail.com"),
                    SystemEmailAddress = StringCipher.Encrypt("synnwebsolutions@gmail.com"),
                    SystemName = "מערכת העזר של אדלה"
                });

                var smachUserId = DBController.DbAuth.GetUsers(new UserSearchParameters { UserName = "smach", Password = "sm1234" }).FirstOrDefault().Id;
                var noaUserId = DBController.DbAuth.GetUsers(new UserSearchParameters { UserName = "noa", Password = "ns1234" }).FirstOrDefault().Id;

                var sharingSetts = new UserMemoSharingSettings { OwnerUserId = smachUserId, UsersToShare = new List<int> { noaUserId } };
                DBController.DbGenericData.Add(sharingSetts);

                sharingSetts = new UserMemoSharingSettings { OwnerUserId = noaUserId, UsersToShare = new List<int> { smachUserId } };
                DBController.DbGenericData.Add(sharingSetts);
            }
        }

        private static void AppendParentInits()
        {
            DBController.DbGenericData.Add(new Parent
            {
                ParentName = "אמא"
            });
            DBController.DbGenericData.Add(new Parent
            {
                ParentName = "אבא"
            });

            DBController.DbAuth.Add(new LoggedUser
            {
                AllowedClientPagePermissions = new List<ClientPagePermissions> { ClientPagePermissions.Deposits },
                DisplayName = "נחום", Password = "015921182", UserName = "0549393717",
                Preferences = new WebSimplify.Data.UserAppPreferences {}
            });

            DBController.DbAuth.Add(new LoggedUser
            {
                AllowedClientPagePermissions = new List<ClientPagePermissions> { ClientPagePermissions.Deposits},
                DisplayName = "אמבט",
                Password = "307202408",
                UserName = "0525100599",
                Preferences = new WebSimplify.Data.UserAppPreferences { }
            });

            DBController.DbAuth.Add(new LoggedUser
            {
                AllowedClientPagePermissions = new List<ClientPagePermissions> { ClientPagePermissions.Deposits },
                DisplayName = "אלמו",
                Password = "303805287",
                UserName = "0523761398",
                Preferences = new WebSimplify.Data.UserAppPreferences { }
            });

            DBController.DbAuth.Add(new LoggedUser
            {
                AllowedClientPagePermissions = new List<ClientPagePermissions> { ClientPagePermissions.Deposits },
                DisplayName = "אורה",
                Password = "304243421",
                UserName = "0523593840",
                Preferences = new WebSimplify.Data.UserAppPreferences { }
            });

            DBController.DbAuth.Add(new LoggedUser
            {
                AllowedClientPagePermissions = new List<ClientPagePermissions> { ClientPagePermissions.Deposits },
                DisplayName = "לימור",
                Password = "307202440",
                UserName = "0522274258",
                Preferences = new WebSimplify.Data.UserAppPreferences { }
            });

            DBController.DbAuth.Add(new LoggedUser
            {
                AllowedClientPagePermissions = new List<ClientPagePermissions> { ClientPagePermissions.Deposits },
                DisplayName = "תמר",
                Password = "307202507",
                UserName = "0526064242",
                Preferences = new WebSimplify.Data.UserAppPreferences { }
            });

            DBController.DbAuth.Add(new LoggedUser
            {
                AllowedClientPagePermissions = new List<ClientPagePermissions> { ClientPagePermissions.Deposits },
                DisplayName = "טל",
                Password = "307202515",
                UserName = "0508494361", 
                Preferences = new WebSimplify.Data.UserAppPreferences { }
            });

            DBController.DbAuth.Add(new LoggedUser
            {
                AllowedClientPagePermissions = new List<ClientPagePermissions> { ClientPagePermissions.Deposits },
                DisplayName = "רחל",
                Password = "307202572",
                UserName = "0509363013",
                Preferences = new WebSimplify.Data.UserAppPreferences { }
            });

            DBController.DbAuth.Add(new LoggedUser
            {
                AllowedClientPagePermissions = new List<ClientPagePermissions> { ClientPagePermissions.Deposits },
                DisplayName = "אברהם",
                Password = "307202598",
                UserName = "0523691178",
                Preferences = new WebSimplify.Data.UserAppPreferences { }
            });
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