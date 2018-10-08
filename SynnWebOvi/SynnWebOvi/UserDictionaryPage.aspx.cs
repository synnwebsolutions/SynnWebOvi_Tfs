using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SynnWebOvi
{
    public partial class UserDictionaryPage : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSiteMap();
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
            if (string.IsNullOrEmpty(txSearchTxt.Text))
                return new List<DictionaryItem>();
            List<DictionaryItem> items = DBController.DbUserDictionary.PerformSearch(txSearchTxt.Text);
            return items;
        }

        private void LoadSiteMap()
        {
            //var lnk = Master.FindControl("lnk1");
            //lnk.Visible = false;
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

        protected void btnSrc_Click(object sender, EventArgs e)
        {
            RefreshGrid(gv);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txNewKey.Text) || string.IsNullOrEmpty(txNewValue.Text))
            {
                AlertMessage("יש להזין את כל השדות");
                return;
            }

            DBController.DbUserDictionary.Add(txNewKey.Text, txNewValue.Text);
            txNewKey.Text = txNewValue.Text = string.Empty;
            ShowUserNotification("פעולה בוצעה בהצלחה");
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txSearchTxt.Text = string.Empty;
            RefreshGrid(gv);
        }
    }
}