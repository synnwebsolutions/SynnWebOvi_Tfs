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
            RefreshGrid(gvOpenTrans);
            RefreshGrid(gvMonthlyBalancesView);
            RefreshGrid(gvTemplates);
            RefreshGrid(gvAllTemplates);
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvOpenTrans.ID)
                return "GetOpenTrans";
            if (gridId == gvMonthlyBalancesView.ID)
                return "GetBalances";
            if (gridId == gvTemplates.ID)
                return DummyMethodName;
            if (gridId == gvAllTemplates.ID)
                return "GetAllTemplates";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetAllTemplates()
        {
            List<MoneyTransactionTemplate> ul = DBController.DbMoney.GetMoneyTransactionTemplates(new MonthlyMoneyTransactionSearchParameters());
            return ul;
        }

        public IEnumerable GetBalances()
        {
            List<MonthBalanceItem> ul = MoneyBalanceHelper.GetBalances(new MonthlyMoneyTransactionSearchParameters());
            if (ul.NotEmpty())
            {
                txAvgBalance.Text = ul.Where(x => !x.Active).Average(x => x.Balance).ToInteger().FormattedString();
                txLastBalance.Text = ul.Where(x => !x.Active).OrderByDescending(x => x.Month).First().Balance.FormattedString();

                SetColor(txAvgBalance);
                SetColor(txLastBalance);
            }
            return ul;
        }

        private void SetColor(TextBox tx)
        {
            var val = tx.Text.ToInteger();
            if (tx.Text.Contains('-'))
                val *= -1;
            if (val < 0)
                tx.ForeColor = System.Drawing.Color.Red;
            else
                tx.ForeColor = System.Drawing.Color.Black;
        }

        public IEnumerable GetOpenTrans()
        {
            List<MonthlyMoneyTransaction> ul = MoneyBalanceHelper.GetOpenTrans(new MonthlyMoneyTransactionSearchParameters());
            return ul;
        }

        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                return new List<ClientPagePermissions> { ClientPagePermissions.MoneyBalance };
            }
        }


        protected void gvOpenTrans_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (MonthlyMoneyTransaction)e.Row.DataItem;
                var t = DBController.DbMoney.GetTransactionTemplate(d.TemplateId);

                ((Label)e.Row.FindControl("lblMonthName")).Text = d.Month.HebrewMonthName();
                ((Label)e.Row.FindControl("lblTemplateName")).Text = t.Name;
                ((Label)e.Row.FindControl("lblTranType")).Text = t.TransactionType.GetDescription();
                var txCurrentTotal = (TextBox)e.Row.FindControl("txCurrentTotal");
                var lblAmount = ((Label)e.Row.FindControl("lblAmount"));
                txCurrentTotal.Text = lblAmount.Text = d.Amount.ToString();
                lblAmount.Visible = d.Closed;
                txCurrentTotal.Visible = !d.Closed;


                var btnCloseAction = ((ImageButton)e.Row.FindControl("btnCloseAction"));
                btnCloseAction.CommandArgument = d.Id.ToString();
            }
        }

        protected void gvOpenTrans_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOpenTrans.PageIndex = e.NewPageIndex;
            RefreshGrid(gvOpenTrans);
        }

        protected void btnCloseAction_Command(object sender, CommandEventArgs e)
        {
            MonthlyMoneyTransaction i = GetItem(e.CommandArgument);
            var row = (sender as ImageButton).NamingContainer as GridViewRow;
            var txCurrentTotal = (TextBox)row.FindControl("txCurrentTotal");
            if (txCurrentTotal.Text.NotNull())
            {
                i.Amount = txCurrentTotal.Text.ToInteger();
                i.Closed = true;
                DBController.DbMoney.Update(i);
                RefreshView();
            }
        }

        private MonthlyMoneyTransaction GetItem(object commandArgument)
        {
            var tp = new MonthlyMoneyTransactionSearchParameters();
            tp.Id = commandArgument.ToString().ToInteger();
            return DBController.DbMoney.GetTransaction(tp);
        }

        protected void gvMonthlyBalancesView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bal = (MonthBalanceItem)e.Row.DataItem;

                ((Label)e.Row.FindControl("lblMonthName")).Text = bal.Month.HebrewMonthNameWithYear();
                ((Label)e.Row.FindControl("lblMontIncomes")).Text = bal.TotalIncomes.FormattedString();
                ((Label)e.Row.FindControl("lblMonthOut")).Text = bal.TotalExpenses.FormattedString();
                ((Label)e.Row.FindControl("lblMonthBalance")).Text = bal.Balance.FormattedString();
            }
        }

        protected void gvMonthlyBalancesView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMonthlyBalancesView.PageIndex = e.NewPageIndex;
            RefreshGrid(gvMonthlyBalancesView);
        }

        protected void gvTemplates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                    ((TextBox)e.Row.FindControl("txAmount")).Text =
                    ((TextBox)e.Row.FindControl("txTempFromDate")).Text =
                    ((TextBox)e.Row.FindControl("txTempToDate")).Text =
                    ((TextBox)e.Row.FindControl("txTempName")).Text = string.Empty;


                ((CheckBox)e.Row.FindControl("chkTempAuto")).Checked = false;
                ((DropDownList)e.Row.FindControl("cmbTemtType")).SelectedIndex = 0;
            }
        }

        protected void gvTemplates_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMonthlyBalancesView.PageIndex = e.NewPageIndex;
            RefreshGrid(gvMonthlyBalancesView);
        }

        protected void btnAddTemplate_Command(object sender, CommandEventArgs e)
        {
            var row = (sender as ImageButton).NamingContainer as GridViewRow;

            var txAmount = ((TextBox)row.FindControl("txAmount")).Text;
            var txTempFromDate = ((TextBox)row.FindControl("txTempFromDate")).Text;
            var txTempToDate = ((TextBox)row.FindControl("txTempToDate")).Text;
            var txTempName = ((TextBox)row.FindControl("txTempName")).Text;


            var chkTempAuto = ((CheckBox)row.FindControl("chkTempAuto")).Checked;
            var cmbTemtType = ((DropDownList)row.FindControl("cmbTemtType")).SelectedValue;

            if (txTempName.NotEmpty() && txTempFromDate.NotEmpty() && cmbTemtType != "-1")
            {
                var i = new MoneyTransactionTemplate
                {
                    Active = true,
                    Amount = txAmount.NotEmpty() ? txAmount.ToInteger() : 0,
                    Auto = chkTempAuto,
                    FromDate = txTempFromDate.ToDateTime(),
                    Name = txTempName,
                    TransactionType = cmbTemtType == "1" ? MonthlyTransactionType.Credit : MonthlyTransactionType.Debit,
                    ToDate = txTempToDate.NotEmpty() ? txTempToDate.ToDateTime() : (DateTime?)null,
                };

                DBController.DbMoney.Add(i);
                RefreshView();
            }
        }

        protected void gvAllTemplates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bal = (MoneyTransactionTemplate)e.Row.DataItem;

                ((Label)e.Row.FindControl("lblTempNName")).Text = bal.Name;
                ((Label)e.Row.FindControl("lblTempType")).Text = bal.TransactionType.GetDescription();
                ((Label)e.Row.FindControl("lblTempAmount")).Text = bal.Amount.NotZero() ? bal.Amount.FormattedString() : string.Empty;
                ((Label)e.Row.FindControl("lblTempStartDate")).Text = bal.FromDate.IsDefault() ? string.Empty : bal.FromDate.ToShortDateString();
                ((Label)e.Row.FindControl("lblTempEndDate")).Text = !bal.ToDate.HasValue || bal.ToDate.Value.IsDefault() ? string.Empty : bal.ToDate.Value.ToShortDateString();
                ((CheckBox)e.Row.FindControl("chkteAuto")).Checked = bal.Auto;
            }
        }

        protected void gvAllTemplates_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAllTemplates.PageIndex = e.NewPageIndex;
            RefreshGrid(gvAllTemplates);
        }
    }
}