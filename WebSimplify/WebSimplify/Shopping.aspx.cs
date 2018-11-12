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
        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.Shopping };
                return l;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gv.Columns[1].Visible = false;
                RefreshView();
            }
        }

        public bool GMode
        {
            get
            {
                var g = (bool?)GetFromSession("gMode");
                return g.HasValue && g.Value;
            }
            set
            {
                StoreInSession("gMode", value);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            trshop.Visible = !GMode;
            btnGenerate.InnerText = GMode ?  "סגור" : "הפק רשימת קניות";
        }
        
        private void RefreshView()
        {
            RefreshGrid(gv);
            RefreshGrid(gvAdd);
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetCurrentShopItems";
            if (gridId == gvAdd.ID)
                return DummyMethodName;
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
                ((Label)e.Row.FindControl("lblItemName")).Text = d.Name;
                ((Label)e.Row.FindControl("lblLastValue")).Text = d.LastBought.HasValue ? d.LastBought.Value.ToShortDateString() : string.Empty;
                ((ImageButton)e.Row.FindControl("btnClose")).CommandArgument = d.Id.ToString();
            }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            RefreshGrid(gv);
        }

        protected void btnClose_Command(object sender, CommandEventArgs e)
        {
            DBController.DbShop.DeActivateShopItem(new ShopSearchParameters { IdToDeactivate = Convert.ToInt32(e.CommandArgument) });
            RefreshGrid(gv);
        }

        protected void btnGenerate_ServerClick(object sender, EventArgs e)
        {
            GMode = !GMode;
            RefreshGrid(gv);
        }

        protected void gvAdd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((TextBox)e.Row.FindControl("txProductName")).Text =  string.Empty;
            }
        }

        protected void btnAdd_Command(object sender, CommandEventArgs e)
        {
            var row = (sender as ImageButton).NamingContainer as GridViewRow;

            var txProductName = ((TextBox)row.FindControl("txProductName")).Text;
            if (txProductName.NotEmpty())
            {
                ShopItem ul = DBController.DbShop.Get(new ShopSearchParameters { ItemName = txProductName }).FirstOrDefault();
                if (ul == null)
                {
                    ShopItem n = new ShopItem
                    {
                        Name = txProductName
                    };
                    DBController.DbShop.AddNewShopItem(n);
                }
                ul = DBController.DbShop.Get(new ShopSearchParameters { ItemName = txProductName }).FirstOrDefault();
                DBController.DbShop.ActivateShopItem(new ShopSearchParameters { IdToActivate = ul.Id });
                AlertMessage("פעולה זו בוצעה בהצלחה");
                RefreshView();
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }
    }
}