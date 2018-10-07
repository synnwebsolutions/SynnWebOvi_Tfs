using System;
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
                BindData();
            }
        }

        protected void btnAddDic_ServerClick(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txKey.Value) || string.IsNullOrEmpty(txVal.Value))
            //{
            //    AlertMessage("יש את כל השדות");
            //    return;
            //}

            //DBController.DbUserDictionary.Add(txKey.Value, txVal.Value);
            //ClearInputFields(new List<System.Web.UI.HtmlControls.HtmlInputControl> { txKey });
            //txVal.Value = string.Empty;
        }

        protected void btnSrc_ServerClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txSearchTxt.Text))
                return;
            string searchText = txSearchTxt.Text;
            List<DictionaryItem> items = DBController.DbUserDictionary.PerformSearch(searchText);
            gv.DataSource = items;
            gv.DataBind();
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

           BindData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }
        private void BindData()
        {
            if (string.IsNullOrEmpty(txSearchTxt.Text))
            {
                ttlResults.InnerText = "לא נמצאו התאמות";
                gv.DataSource = new List<DictionaryItem>(); ;
                gv.DataBind();
                return;
            }
            ttlResults.InnerText = string.Format("תוצאות עבור :  '{0}'", txSearchTxt.Text);
            string searchText = txSearchTxt.Text;
            List<DictionaryItem> items = DBController.DbUserDictionary.PerformSearch(searchText);
            gv.DataSource = items;
            txSearchTxt.Text = string.Empty;
            gv.DataBind();
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            BindData();
        }
    }
}