using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class TestPage : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<Products> GetProducts(string field1)
        {
            System.Threading.Thread.Sleep(500);
            List<Products> allCompany = new List<Products>();
            using (JqueryDbEntities dc = new JqueryDbEntities())
            {
                allCompany = dc.Products.ToList();
            }
            return allCompany;
        }
    }
}