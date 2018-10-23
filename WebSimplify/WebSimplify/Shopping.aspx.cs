using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class Shopping : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshView();
            }
        }

        protected override string NavIdentifier
        {
            get
            {
                return "navshop";
            }
        }
        private void RefreshView()
        {
            List<ShopItem> ul = DBController.DbShop.Get(new ShopSearchParameters());
            cmbItems.Items.Clear();
            AddSelectItemForCombo(cmbItems);
            foreach (var u in ul)
                cmbItems.Items.Add(new ListItem { Text = u.Name, Value = u.Id.ToString() });
            RefreshGrid(gv);
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetCurrentShopItems";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetCurrentShopItems()
        {
            List<ShopItem> ul = DBController.DbShop.Get(new ShopSearchParameters { Active = true });
            return ul;
        }
        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (ShopItem)e.Row.DataItem;
                ((CheckBox)e.Row.FindControl("chkRemove")).Checked = true;
                ((Label)e.Row.FindControl("lblItemName")).Text = d.Name;
                ((Label)e.Row.FindControl("lblLastValue")).Text = d.LastBought.HasValue ? d.LastBought.Value.ToShortDateString() : string.Empty;
                ((HiddenField)e.Row.FindControl("hfpid")).Value = d.Id.ToString();
            }
        }

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            if(cmbItems.SelectedIndex > 0)
                DBController.DbShop.ActivateShopItem(new ShopSearchParameters { IdToActivate = Convert.ToInt32(cmbItems.SelectedValue) });

            RefreshView();
        }

        protected void chkRemove_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gv.Rows)
            {
                if (!((CheckBox)gvr.FindControl("chkRemove")).Checked)
                {
                    int idToDeactivate = int.Parse(((HiddenField)gvr.FindControl("hfpid")).Value);
                    DBController.DbShop.DeActivateShopItem(new ShopSearchParameters { IdToDeactivate = idToDeactivate });
                    break;
                }
            }
            RefreshView();
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            RefreshGrid(gv);
        }
    }
}