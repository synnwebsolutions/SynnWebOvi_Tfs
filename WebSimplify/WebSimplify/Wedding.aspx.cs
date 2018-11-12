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
    public partial class Wedding : SynnWebFormBase
    {
        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.Wedding };
                return l;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            //if (string.IsNullOrEmpty(txwedsearchkey.Value))
            //    return new List<WeddingGuest>();
            List<WeddingGuest> items = DBController.DbWedd.GetGuests(new WeddSearchParameters { SearchText = txwedsearchkey.Value });
            return items;
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (WeddingGuest)e.Row.DataItem;
                ((Label)e.Row.FindControl("lblGuestName")).Text = d.Name;
                ((Label)e.Row.FindControl("lblGuestValue")).Text = d.Amount.ToString();
            }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            RefreshGrid(gv);
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            RefreshGrid(gv);
        }
    }
}