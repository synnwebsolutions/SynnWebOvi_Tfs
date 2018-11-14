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
    public partial class MoneyBalance : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshView();
            }
        }

        private void RefreshView()
        {
            RefreshGrid(gvCurrentMonthOpenTrans);
            //RefreshGrid(gvCashMoneyItems);
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvCurrentMonthOpenTrans.ID)
                return "GetCurrentMonthOpenTrans";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetCurrentMonthOpenTrans()
        {
            List<MoneyTransactionTemplate> ul = MoneyBalanceHelper.GetCurrentMonthOpenTemplates(new MonthlyMoneyTransactionSearchParameters());
            return ul;
        }

        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                return new List<ClientPagePermissions> { ClientPagePermissions.MoneyBalance };
            }
        }

        protected void gvCurrentMonthOpenTrans_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (MoneyTransactionTemplate)e.Row.DataItem;

                ((Label)e.Row.FindControl("lblMonthName")).Text = DateTime.Now.HebrewMonthName();
                ((Label)e.Row.FindControl("lblTemplateName")).Text = d.Name;
                var txCurrentTotal = (TextBox)e.Row.FindControl("txCurrentTotal");


                txCurrentTotal.Text = string.Empty;
                txCurrentTotal.Visible = d.Active;

                var btnAction = ((ImageButton)e.Row.FindControl("btnAction"));
                btnAction.CommandArgument = d.Id.ToString();
            }
        }

        protected void gvCurrentMonthOpenTrans_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnAction_Command(object sender, CommandEventArgs e)
        {

        }
    }
}