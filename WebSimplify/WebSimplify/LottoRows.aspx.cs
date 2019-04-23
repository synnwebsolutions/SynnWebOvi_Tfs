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
    public partial class LottoRows : SynnWebFormBase
    {

        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.LottoRows };
                return l;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshGrids();
                FillEnum(cmbFromWin, typeof(LottoWin));
            }
        }

        private void RefreshGrids()
        {
            RefreshGrid(gvNewPole);
            RefreshGrid(gvAllRows);
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvNewPole.ID)
                return DummyMethodName;
            if (gridId == gvTempRows.ID)
                return "GetTempData";
            if (gridId == gvAllRows.ID)
                return "GetAllData";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetTempData()
        {
            dvWorkHours.Visible = TempRows != null && TempRows.Count > 0;
            return TempRows;
        }

        public IEnumerable GetAllData()
        {
            if (!TempRows.IsEmpty())
                return null;

            List<LottoRow> rows = DBController.DbLotto.Get(new LottoRowsSearchParameters { PoleActionDate = string.IsNullOrEmpty(txPoleDate.Text) ? (DateTime?)null : txPoleDate.Text.ToDateTime()
                , PoleKey = txPolekey.Value });
            if (cmbFromWin.SelectedIndex > 0)
            {
                rows = rows.Where(x => x.Wins.NotEmpty() && x.Wins.Any(w => (int)w >= cmbFromWin.SelectedValue.ToInteger())).ToList();
            }
            return rows.OrderByDescending(x => x.PoleDestinationDate).ToList();
        }

        protected void gvNewPole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((TextBox)e.Row.FindControl("txNewDestDate")).Text =
                ((TextBox)e.Row.FindControl("txNumOfRows")).Text = string.Empty;
                ((CheckBox)e.Row.FindControl("cmbStat")).Checked = false;
                //var cmb = ((DropDownList)e.Row.FindControl("cmbStat"));
                //cmb.Items.Clear();
                //FillEnum(cmb, typeof(LottoStatItem));
            }
        }

        protected void gvNewPole_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnGenerate_Command(object sender, CommandEventArgs e)
        {
            try
            {
                
                var row = (sender as ImageButton).NamingContainer as GridViewRow;
                var txx = ((TextBox)row.FindControl("txNumOfRows"));
                //var cmbStat = ((DropDownList)row.FindControl("cmbStat"));
                var cmbStat = ((CheckBox)row.FindControl("cmbStat"));
                var numOfRows = txx.Text.IsEmpty() ? 14 :  txx.Text.ToInteger();
                DateTime newDate = ((TextBox)row.FindControl("txNewDestDate")).Text.ToDateTime();

                if (cmbStat.Checked)
                {
                    int cnt = 0;
                    LottoWin maxWin = LottoWin.None;
                    do
                    {
                        LottoPole maxPole = null;
                        TempRows = LottoHandler.Generate(numOfRows, newDate);
                        GetMaxWinForTempRows(ref maxWin, ref maxPole);
                        cnt++;
                    }
                    while (maxWin < LottoWin.Six);
                    string msg = "הסתיים בהצלחה לאחר לאחר נסיונות : " + cnt.ToString();
                    DBController.DbLog.AddLog(msg);
                    AlertMessage(msg);
                }
                else
                    TempRows = LottoHandler.Generate(numOfRows, newDate);

                RefreshGrid(gvTempRows);
                RefreshGrids();
            }
            catch (Exception ex)
            {
                AlertMessage("פעולה לא בוצעה בהצלחה");
            }
        }

        public List<LottoRow> TempRows
        {
            get
            {
                return (List<LottoRow>)GetFromSession("tmpodsf_*");
            }
            set
            {
                StoreInSession("tmpodsf_*", value);
            }
        }

        protected void gvTempRows_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (LottoRow)e.Row.DataItem;

                ((Label)e.Row.FindControl("txDestDate")).Text = d.PoleDestinationDate.ToShortDateString();
                ((Label)e.Row.FindControl("txPoleKey")).Text = d.PoleKey;
                ((Label)e.Row.FindControl("tx1")).Text = d.N1.ToString();
                ((Label)e.Row.FindControl("tx2")).Text = d.N2.ToString();
                ((Label)e.Row.FindControl("tx3")).Text = d.N3.ToString();
                ((Label)e.Row.FindControl("tx4")).Text = d.N4.ToString();
                ((Label)e.Row.FindControl("tx5")).Text = d.N5.ToString();
                ((Label)e.Row.FindControl("tx6")).Text = d.N6.ToString();
                ((Label)e.Row.FindControl("txSpecial")).Text = d.SpecialNumber.ToString();
                
            }
        }

        protected void gvTempRows_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTempRows.PageIndex = e.NewPageIndex;
            RefreshGrid(gvTempRows);
        }

        protected void btnSaveTempRows_ServerClick(object sender, EventArgs e)
        {
            if (!TempRows.IsEmpty())
            {
                foreach (var trow in TempRows)
                    DBController.DbLotto.AddLottoRow(trow);

                TempRows = null;
                RefreshGrid(gvTempRows);
                RefreshGrids();
            }
        }

        protected void btnTestHistory_ServerClick(object sender, EventArgs e)
        {
            if (!TempRows.IsEmpty())
            {
                LottoWin maxWin = LottoWin.None;
                LottoPole maxPole = null;
                GetMaxWinForTempRows(ref maxWin, ref maxPole);
                string msg = string.Format("max win is {0} at [{1}]  {2}", maxWin.GedDescription(), maxPole.PoleKey, maxPole.PoleActionDate.ToShortDateString());
                AlertMessage(msg);
            }
        }

        private void GetMaxWinForTempRows(ref LottoWin maxWin, ref LottoPole maxPole)
        {
            var poles = DBController.DbLotto.Get(new LottoPolesSearchParameters { }).OrderByDescending(x => x.PoleActionDate);
            foreach (var pole in poles)
            {
                foreach (var trow in TempRows)
                {
                    LottoWin cwin = LottoHandler.TestMatch(pole, trow);
                    if (cwin > maxWin)
                    {
                        maxWin = cwin;
                        maxPole = pole;
                    }
                    if (maxWin == LottoWin.JackPot)
                        break;
                }
                if (maxWin == LottoWin.JackPot)
                    break;
            }
        }

        protected void gvAllRows_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAllRows.PageIndex = e.NewPageIndex;
            RefreshGrid(gvAllRows);
        }

        protected void gvAllRows_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (LottoRow)e.Row.DataItem;

                ((Label)e.Row.FindControl("atxDestDate")).Text = d.PoleDestinationDate.ToShortDateString();
                ((Label)e.Row.FindControl("atxPoleKey")).Text = d.PoleKey;
                ((Label)e.Row.FindControl("atx1")).Text = d.N1.ToString();
                ((Label)e.Row.FindControl("atx2")).Text = d.N2.ToString();
                ((Label)e.Row.FindControl("atx3")).Text = d.N3.ToString();
                ((Label)e.Row.FindControl("atx4")).Text = d.N4.ToString();
                ((Label)e.Row.FindControl("atx5")).Text = d.N5.ToString();
                ((Label)e.Row.FindControl("atx6")).Text = d.N6.ToString();
                ((Label)e.Row.FindControl("atxSpecial")).Text = d.SpecialNumber.ToString();
                ((Label)e.Row.FindControl("lblPoleWins")).Text = d.WinsText;
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            RefreshGrid(gvAllRows);
        }
    }
}