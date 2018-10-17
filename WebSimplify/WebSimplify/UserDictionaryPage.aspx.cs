using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class UserDictionaryPage : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<DictionaryItem> GetDictionaryItems(string searchtext)
        {
            List<DictionaryItem> items = DBController.DbUserDictionary.PerformSearch(new DictionarySearchParameters() { SearchText = searchtext });
            return items;
        }

    }
}