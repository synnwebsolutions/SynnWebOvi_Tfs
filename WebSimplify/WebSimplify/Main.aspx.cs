using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class Main : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<DictionaryItem> GetDictionaryItems(string searchtext)
        {
            List<DictionaryItem> items = DBController.DbUserDictionary.PerformSearch(searchtext);
            return items;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<LogItem> GetLogItems(string searchtext)
        {
            var lp = new LogSearchParameters() { Text = searchtext };
            List<LogItem> items = DBController.DbLog.GetLogs(lp);
            return items;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<WeddingGuest> GetWeddingItems(string guesttext)
        {
            List<WeddingGuest> items = DBController.DbWedd.GetGuests(guesttext);
            return items;
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static CalendarMonthlyData GetCalendarItem()
        {
            return new  CalendarMonthlyData();
        }

    }
}