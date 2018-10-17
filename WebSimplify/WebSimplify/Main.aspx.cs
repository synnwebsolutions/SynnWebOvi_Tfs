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
using WebSimplify.Data;

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
        [ScriptMethod()]
        public static void AddToDictionary(string key, string value)
        {
            DBController.DbUserDictionary.Add(new DictionarySearchParameters { Key = key, Value = value });
        }

        [WebMethod]
        [ScriptMethod()]
        public static void AddToShopList(string productName)
        {
            ShoppingData sd = DBController.DbShop.GetData();
            sd.AddToShoplist(productName);
            DBController.DbShop.Update(sd);
        }

        [WebMethod]
        [ScriptMethod()]
        public static void AddToCalendar(string title, string description, string date)
        {
            var c = new MemoItem
            {
                title = title,
                Description = description,
                Date = Convert.ToDateTime(date)
            };
            var sp = new CalendarSearchParameters { InsertItem = c };
            DBController.DbCalendar.Add(sp);
        }

    }
}