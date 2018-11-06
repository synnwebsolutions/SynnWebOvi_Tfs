using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSimplify.Data;

namespace WebSimplify
{
    public partial class CreditStats : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshView();
            }
        }

        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.CreditData };
                return l;
            }
        }
        protected override string NavIdentifier
        {
            get
            {
                return "navcredit";
            }
        }

        private void RefreshView()
        {
            List<CreditCardMonthlyData> ul = DBController.DbMoney.Get(new CreditSearchParameters { });
            if (ul.NotEmpty())
            {
                var inactiveItems = ul.Where(x => !x.Active).ToList();
                if (inactiveItems.NotEmpty())
                {
                    var avg = inactiveItems.Average(x => x.TotalSpent);
                    txp1.Text = avg.ToInteger().FormattedString();
                    txp2.Text = inactiveItems.Average(x => x.DaylyValue.ToInteger()).ToInteger().FormattedString();
                }

                var current = DBController.DbMoney.Get(new CreditSearchParameters { Month = DateTime.Now }).FirstOrDefault();
                if (current != null)
                    txp3.Text = current.MonthlyPrediction.ToInteger().FormattedString();
            }
            RefreshGrid(gv);
        }
        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetCreditItems";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetCreditItems()
        {
            List<CreditCardMonthlyData> ul = DBController.DbMoney.Get(new CreditSearchParameters { });
            return ul.OrderByDescending(x => x.Date).ToList();
        }
        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (CreditCardMonthlyData)e.Row.DataItem;
                ((Label)e.Row.FindControl("lblMonthName")).Text = d.Date.HebrewMonthName();
                ((Label)e.Row.FindControl("lblDaylyValue")).Text = d.DaylyValue;
                ((Label)e.Row.FindControl("lblMonthlyPredictions")).Text = d.Active ? d.MonthlyPrediction : d.TotalSpent.FormattedString();
                var txCurrentTotal = (TextBox)e.Row.FindControl("txCurrentTotal");
                txCurrentTotal.Text = d.TotalSpent.FormattedString(); 
                txCurrentTotal.Visible = d.Active;

                var btnAction = ((ImageButton)e.Row.FindControl("btnAction"));
                var btnCloseMonth = ((ImageButton)e.Row.FindControl("btnCloseMonth"));
                btnAction.CommandArgument = btnCloseMonth.CommandArgument = d.Id.ToString();
                btnAction.Visible = btnCloseMonth.Visible = d.Active;
            }
        }

        protected void btnAction_Command(object sender, CommandEventArgs e)
        {
            CreditCardMonthlyData i = GetItem(e.CommandArgument);
            var row = (sender as ImageButton).NamingContainer as GridViewRow;
            var txCurrentTotal = (TextBox)row.FindControl("txCurrentTotal");
            var nVal = Convert.ToInt32(Convert.ToDecimal(txCurrentTotal.Text));
            if (i.Active)
            {
                i.TotalSpent = nVal;
                DBController.DbMoney.Update(i);
                RefreshGrid(gv);
            }
        }

        private CreditCardMonthlyData GetItem(object commandArgument)
        {
            List<CreditCardMonthlyData> ul = DBController.DbMoney.Get(new CreditSearchParameters { Id = Convert.ToInt32(commandArgument) });
            return ul.FirstOrDefault();
        }

        protected void btnCloseMonth_Command(object sender, CommandEventArgs e)
        {
            CreditCardMonthlyData i = GetItem(e.CommandArgument);
            i.Active = false;
            DBController.DbMoney.Update(i);
            RefreshView();
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            RefreshGrid(gv);
        }

        [WebMethod]
        [ScriptMethod()]
        public static JsonChartData GetChartData(string chartid)
        {
            var da = new JsonChartData();
            var lst = new List<JsonLineCahrtItem>();
            lst.Add(new JsonLineCahrtItem { Label = "t1", Y = 10 });
            lst.Add(new JsonLineCahrtItem { Label = "t2", Y = 55 });
            lst.Add(new JsonLineCahrtItem { Label = "e3", Y = 24 });
            lst.Add(new JsonLineCahrtItem { Label = "r4", Y = 33 });
            da.ChartData =  new JavaScriptSerializer().Serialize(lst);
            da.ChartTitle = "simple Title";
            return da;
        }
    }
}