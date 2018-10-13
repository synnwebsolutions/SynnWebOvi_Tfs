using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                //LoadSiteMap();
                RefreshGrid(gv);
            }
        }

    

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetData";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetData()
        {
            List<DictionaryItem> items = DBController.DbUserDictionary.PerformSearch("");
            return items;
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

        protected void gv_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            GridView g = sender as GridView;
            g.PageIndex = e.NewPageIndex;
            RefreshGrid(g);
        }
    }
}