using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SynnWebOvi
{
    public class SynNavigation
    {
  

        public static void Goto(string url)
        {
            HttpContext.Current.Response.Redirect(url);
        }
        public static void Redirect(string page)
        {
            //string urdl = Path.Combine(HttpRuntime.AppDomainAppPath, page);
            HttpContext.Current.Response.Redirect(page);
        }
    }

    public static class Pages
    {
        public const string Main = "Main.aspx";
        public const string Login = "Login.aspx";
        public const string ErrorPage = "ErrorPage.aspx";
        public const string UserDictionaryPage = "UserDictionaryPage.aspx";

        public static string Log = "Log.aspx";
        public static string AppSettingsPage = "AppSettingsPage.aspx";
        public static string Wedding = "Wedding.aspx";
        public static string Diary = "Diary.aspx";
        public static string Shopping = "Shopping.aspx";
        public static string Shifts = "Shifts.aspx";
    }
}