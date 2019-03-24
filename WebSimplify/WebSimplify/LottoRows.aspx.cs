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
                RefreshGrid(gvNewPole);
                
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvNewPole.ID)
                return DummyMethodName;
            if (gridId == gvTempRows.ID)
                return "GetTempData";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetTempData()
        {
            dvWorkHours.Visible = TempRows != null && TempRows.Count > 0;
            return TempRows;
        }

        protected void gvNewPole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((TextBox)e.Row.FindControl("txNewDestDate")).Text =
                ((TextBox)e.Row.FindControl("txNumOfRows")).Text = string.Empty;

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
                var numOfRows = ((TextBox)row.FindControl("txNumOfRows")).Text.ToInteger();
                DateTime newDate = ((TextBox)row.FindControl("txNewDestDate")).Text.ToDateTime();
                TempRows = LottoHandler.Generate(numOfRows, newDate);

                //RefreshGrid(gvNewPole);
                RefreshGrid(gvTempRows);
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
                RefreshGrid(gvNewPole);
            }
        }
    }
}