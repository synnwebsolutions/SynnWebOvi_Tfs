using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSimplify.Data;

namespace WebSimplify
{
    public partial class CashLog : SynnWebFormBase
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
            List<CashMonthlyData> ul = DBController.DbMoney.Get(new CashSearchParameters { });
            if (ul.NotEmpty())
            {
                var inactiveItems = ul.Where(x => !x.Active).ToList();
                if (inactiveItems.NotEmpty())
                {
                    var avg = inactiveItems.Average(x => x.TotalSpent);
                    txp1.Text = avg.ToInteger().FormattedString();
                    txp2.Text = inactiveItems.Average(x => x.DaylyValue.ToInteger()).ToInteger().FormattedString();
                }

                var current = DBController.DbMoney.Get(new CashSearchParameters { Month = DateTime.Now }).FirstOrDefault();
                if (current != null)
                    txp3.Text = current.MonthlyPrediction.ToInteger().FormattedString();
            }
            RefreshGrid(gvMonthsView);
            RefreshGrid(gvCashMoneyItems);
        }

        protected override string NavIdentifier
        {
            get
            {
                return "navcash";
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvMonthsView.ID)
                return "GetCashMonthItems";
            if (gridId == gvCashMoneyItems.ID)
                return "GetCashMoneyItems";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetCashMonthItems()
        {
            List<CashMonthlyData> ul = DBController.DbMoney.Get(new CashSearchParameters { });
            return ul.OrderByDescending(x => x.Date).ToList();
        }

        public IEnumerable GetCashMoneyItems()
        {
            List<CashMoneyItem> ul = new List<CashMoneyItem> { new CashMoneyItem { DummyItem = true, Date = DateTime.Now.EndOfMonth() } };
            ul.AddRange( DBController.DbMoney.GetCashItems(new CashSearchParameters { Month = DateTime.Now }));
            return ul.OrderByDescending(x => x.Date).ToList();
        }

        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.CashLog };
                return l;
            }
        }

        protected void gvMonthsView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (CashMonthlyData)e.Row.DataItem;
                ((Label)e.Row.FindControl("lblMonthName")).Text = d.Date.HebrewMonthName();
                ((Label)e.Row.FindControl("lblDaylyValue")).Text = d.DaylyValue;
                ((Label)e.Row.FindControl("lblMonthlyPredictions")).Text = d.MonthlyPrediction;
                var txCurrentTotal = (Label)e.Row.FindControl("txCurrentTotal");
                txCurrentTotal.Text = d.TotalSpent.FormattedString();
                txCurrentTotal.Enabled = d.Active;

                //var btnAction = ((ImageButton)e.Row.FindControl("btnAction"));
                //var btnCloseMonth = ((ImageButton)e.Row.FindControl("btnCloseMonth"));
                //btnAction.CommandArgument = btnCloseMonth.CommandArgument = d.Id.ToString();
                //btnAction.Visible = btnCloseMonth.Visible = d.Active;
            }
        }

        protected void gvMonthsView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMonthsView.PageIndex = e.NewPageIndex;
            RefreshGrid(gvMonthsView);
        }

        protected void gvCashMoneyItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (CashMoneyItem)e.Row.DataItem;

                var btnAddCashItem = ((ImageButton)e.Row.FindControl("btnAddCashItem"));

                var lblDate = ((Label)e.Row.FindControl("lblDate"));
                var lblspent = ((Label)e.Row.FindControl("lblspent"));
                var txIdesc = ((TextBox)e.Row.FindControl("txIdesc"));
                var lblIdesc = ((Label)e.Row.FindControl("lblIdesc"));
                var txspent = ((TextBox)e.Row.FindControl("txspent"));

                btnAddCashItem.Visible = txIdesc.Visible = txspent.Visible = d.DummyItem;
                lblspent.Visible = lblIdesc.Visible = !d.DummyItem;

                lblspent.Text = txspent.Text = d.TotalSpent.FormattedString();
                lblIdesc.Text = txIdesc.Text = d.Description;

                btnAddCashItem.CommandArgument = d.Id.ToString();
                lblDate.Text = d.DummyItem ? string.Empty : d.Date.ToShortDateString();
            }
        }

        protected void gvCashMoneyItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCashMoneyItems.PageIndex = e.NewPageIndex;
            RefreshGrid(gvCashMoneyItems);
        }

        protected void btnAddCashItem_Command(object sender, CommandEventArgs e)
        {
            var row = (sender as ImageButton).NamingContainer as GridViewRow;

            var spent = ((TextBox)row.FindControl("txspent")).Text;
            var Idesc = ((TextBox)row.FindControl("txIdesc")).Text;
            if (spent.NotEmpty() && Idesc.NotEmpty())
            {
                var i = new CashMoneyItem
                {
                    Date = DateTime.Now,
                    Description = Idesc,
                    TotalSpent = spent.ToInteger(),
                    UserGroupId = CurrentUser.AllowedSharedPermissions[0]
                };
                DBController.DbMoney.Add(i);
                var current = DBController.DbMoney.Get(new CashSearchParameters { Month = DateTime.Now }).FirstOrDefault();
                current.TotalSpent += i.TotalSpent;
                DBController.DbMoney.Update(current);
                RefreshView();
            }
        }
    }
}