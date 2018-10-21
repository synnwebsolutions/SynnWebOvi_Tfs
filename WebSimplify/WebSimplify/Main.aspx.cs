using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
                trdiary.Visible = CurrentUser.Allowed(ClientPagePermissions.Diary);
                trdic.Visible = CurrentUser.Allowed(ClientPagePermissions.Dictionary);
                trshop.Visible = CurrentUser.Allowed(ClientPagePermissions.Shopping);
            }
        }

        [WebMethod]
        [ScriptMethod()]
        public static void AddToDictionary(string key, string value)
        {
            
        }

        //[WebMethod]
        //[ScriptMethod()]
        //public static void AddToShopList(string productName)
        //{
        //    ShoppingData sd = DBController.DbShop.GetData();
        //    sd.AddToShoplist(productName);
        //    DBController.DbShop.Update(sd);
        //}

        protected void btnadddic_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInputs(txadddickey, txadddicval))
            {
                DBController.DbUserDictionary.Add(new DictionarySearchParameters { Key = txadddickey.Value, Value = txadddicval.Value });
                AlertMessage("פעולה זו בוצעה בהצלחה");
                ClearInputs(txadddickey, txadddicval);
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }


        protected void btnadddiary_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInputs(txadddiaryname, txadddiarydesc, txadddiarydate))
            {
                var c = new MemoItem
                {
                    title = txadddiaryname.Value,
                    Description = txadddiarydesc.Value,
                    Date = Convert.ToDateTime(txadddiarydate.Value)
                };
                var sp = new CalendarSearchParameters { InsertItem = c };
                DBController.DbCalendar.Add(sp);
                AlertMessage("פעולה זו בוצעה בהצלחה");
                ClearInputs(txadddiaryname, txadddiarydesc, txadddiarydate);
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }

        protected void btnAddShopItem_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInputs(txShopItemToAdd))
            {
                ShoppingData d = DBController.DbShop.GetData(new ShopSearchParameters());
                d.AddToShoplist(txShopItemToAdd.Value);
                DBController.DbShop.Update(new ShopSearchParameters { ItemForAction = d });
                AlertMessage("פעולה זו בוצעה בהצלחה");
                ClearInputs(txShopItemToAdd);
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }
    }
}