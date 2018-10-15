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

        protected override void OnInit(EventArgs e)
        {
            
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
        [ScriptMethod()]
        public static void AddToDictionary(string key, string value)
        {
            DBController.DbUserDictionary.Add(key, value);
        }
        

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static CalendarMonthlyData GetCalendarItem()
        {
            var mls = new List<MemoItem>();
            mls.Add(new MemoItem { Date = new DateTime(2018,10,3), Description = "תיאור ", Header = "חיפוש עורך דין" });
            mls.Add(new MemoItem { Date = new DateTime(2018, 10, 5), Description = "תיאור", Header = "טסט לאוטו" });
            mls.Add(new MemoItem { Date = new DateTime(2018, 10, 8), Description = "תיאור", Header = "רשימת קניות" });
            mls.Add(new MemoItem { Date = new DateTime(2018, 10, 14), Description = "תיאור", Header = "לטייל עם אבא" });
            mls.Add(new MemoItem { Date = new DateTime(2018, 10, 17), Description = "תיאור", Header = "אוכל לילדים" });
            mls.Add(new MemoItem { Date = new DateTime(2018, 10, 13), Description = "תיאור", Header = "בגדים לילדים" });
            mls.Add(new MemoItem { Date = new DateTime(2018, 10, 16), Description = "תיאור", Header = "ביקור במילאנו" });
            mls.Add(new MemoItem { Date = new DateTime(2018, 10, 21), Description = "תיאור", Header = "נסיעה לאילת" });
            mls.Add(new MemoItem { Date = new DateTime(2018, 10, 24), Description = "תיאור", Header = "ביקור אצל אסף" });
            mls.Add(new MemoItem { Date = new DateTime(2018, 10, 25), Description = "תיאור", Header = "לקבוע עם רותי" });
            mls.Add(new MemoItem { Date = new DateTime(2018, 10, 27), Description = "תיאור", Header = "להתקשר לסיסי" });
            mls.Add(new MemoItem { Date = new DateTime(2018, 10, 13), Description = "תיאור", Header = "למצוא נעל" });
            mls.Add(new MemoItem { Date = new DateTime(2018, 10, 22), Description = "תיאור", Header = "לקנות מעיל" });
            return new  CalendarMonthlyData(mls);
        }

    }
}