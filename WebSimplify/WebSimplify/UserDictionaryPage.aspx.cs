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
    public partial class UserDictionaryPage : SynnWebFormBase
    {
        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.Dictionary };
                return l;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshGrids();
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetData";
            if (gridId == gvAdd.ID)
                return DummyMethodName;
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetData()
        {
            List<DictionaryItem> items = DBController.DbUserDictionary.PerformSearch(new DictionarySearchParameters { SearchText = txsearchkey.Value });
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

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            RefreshGrid(gv);
        }

        protected void gvAdd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((TextBox)e.Row.FindControl("txName")).Text = ((TextBox)e.Row.FindControl("txIdesc")).Text = string.Empty;
            }
        }

        protected void btnAdd_Command(object sender, CommandEventArgs e)
        {
            var row = (sender as ImageButton).NamingContainer as GridViewRow;

            var txName = ((TextBox)row.FindControl("txName")).Text;
            var txIdesc = ((TextBox)row.FindControl("txIdesc")).Text;
            if (txName.NotEmpty() && txIdesc.NotEmpty())
            {
                DBController.DbUserDictionary.Add(new DictionarySearchParameters { Key = txName, Value = txIdesc });
                AlertMessage("פעולה זו בוצעה בהצלחה");
                RefreshGrids();
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }

        private void RefreshGrids()
        {
            RefreshGrid(gv);
            RefreshGrid(gvAdd);
        }
    }
}