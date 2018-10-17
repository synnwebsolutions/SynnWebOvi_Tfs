using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SynnWebOvi;

namespace WebSimplify.Data
{
    public class AccountData : System.Web.UI.Page
    {
        static string key = "ssUser_*";

        private static AccountData instance;
        public AccountData()
        {
            
        }

        internal static LoggedUser GetCurrentUser()
        {
            LoggedUser u = null;
            if (instance == null)
                instance = new AccountData();
            if (instance.Session[key] != null)
                u =  (LoggedUser)instance.Session[key];
            instance = null;
            return u;
        }
    }
}