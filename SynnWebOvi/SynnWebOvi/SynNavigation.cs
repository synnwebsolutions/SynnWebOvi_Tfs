using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SynnWebOvi
{
    public class SynNavigation
    {
        public static class Pages
        {
            public const string Main = "Main.aspx";
            public const string Login = "Login.aspx";
            public const string ErrorPage = "Login.aspx";
            public const string UserDictionaryPage = "UserDictionaryPage.aspx";
            //public const string PageExpiredPage = "~/PageExpired.htm";
            //public const string SupportRequestSubmitPage = "~/SupportRequestSubmitPage.aspx";
        }

        public static void Goto(string url)
        {
            HttpContext.Current.Server.Transfer(url);
        }
        public static void Redirect(string page)
        {
            //string urdl = Path.Combine(HttpRuntime.AppDomainAppPath, page);
            HttpContext.Current.Response.Redirect(page);
        }
    }
}