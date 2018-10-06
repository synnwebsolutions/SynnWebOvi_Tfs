using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SynnWebOvi
{
    public partial class Main : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //BindData();
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (DictionaryItem)e.Row.DataItem;
                ((Label)e.Row.FindControl("lblDicName")).Text = d.DictionaryKey;
                ((Label)e.Row.FindControl("lblDicValue")).Text = d.DictionaryValue;
            }
        }

        protected void btnAddToDict_ServerClick(object sender, EventArgs e)
        {
        

        }

        protected void btnSearchDic_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnClearDic_ServerClick(object sender, EventArgs e)
        {

        }

      
    }
}