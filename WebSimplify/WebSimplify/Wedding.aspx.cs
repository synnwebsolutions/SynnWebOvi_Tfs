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
    public partial class Wedding : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<WeddingGuest> GetWeddingItems(string guesttext)
        {
            List<WeddingGuest> items = DBController.DbWedd.GetGuests(guesttext);
            return items;
        }
    }
}