using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using System.Collections;

namespace WebSimplify
{
    public partial class LottoPoles : SynnWebFormBase
    {
        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.Lotto };
                return l;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshGrid(gv);
                //RefreshGrid(gvNewPole);
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetData";
            //if (gridId == gvNewPole.ID)
            //    return DummyMethodName;
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetData()
        {
            var items = DBController.DbLotto.Get(new LottoPolesSearchParameters { });
            return items;
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (LottoPole)e.Row.DataItem;
                List<LottoRow> rowPoles = GetPoleRows(d);

                ((Label)e.Row.FindControl("lblPoleId")).Text = d.Id.ToString();
                ((Label)e.Row.FindControl("lblPoleKey")).Text = d.PoleKey;
                ((Label)e.Row.FindControl("lblPoleDate")).Text = d.PoleActionDate.ToShortDateString();
                ((Label)e.Row.FindControl("lblPoleWins")).Text = d.WinsText;
                ((Label)e.Row.FindControl("lblPoleNums")).Text = string.Format("({0}) [{1}]",string.Join(",", d.GetNumbers().Select(x => x.ToString()).ToList()),d.SpecialNumber);
                ((Label)e.Row.FindControl("lblPoleRows")).Text = rowPoles.Count.ToString();
                var btnUpdate = ((ImageButton)e.Row.FindControl("btnUpdate"));
                var btnReGenerate = ((ImageButton)e.Row.FindControl("btnReGenerate"));
                btnUpdate.CommandArgument = btnReGenerate.CommandArgument = d.Id.ToString();
            }
        }

        protected void gvTemplates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((TextBox)e.Row.FindControl("txDestDate")).Text =
                    ((TextBox)e.Row.FindControl("txPoleKey")).Text =
                    ((TextBox)e.Row.FindControl("txSpecial")).Text =
                    ((TextBox)e.Row.FindControl("tx1")).Text =
                    ((TextBox)e.Row.FindControl("tx2")).Text =
                    ((TextBox)e.Row.FindControl("tx3")).Text =
                ((TextBox)e.Row.FindControl("tx4")).Text =
                ((TextBox)e.Row.FindControl("tx5")).Text =
                ((TextBox)e.Row.FindControl("tx6")).Text = string.Empty;

            }
        }

        protected void gvTemplates_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
         
        }
        protected void btnAddPole_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var row = (sender as ImageButton).NamingContainer as GridViewRow;

                var i = new LottoPole
                {
                    PoleActionDate = ((TextBox)row.FindControl("txDestDate")).Text.ToDateTime(),
                    SpecialNumber = ((TextBox)row.FindControl("txSpecial")).Text.ToInteger(),
                    PoleKey = ((TextBox)row.FindControl("txPoleKey")).Text,
                    N1 = ((TextBox)row.FindControl("tx1")).Text.ToInteger(),
                    N2 = ((TextBox)row.FindControl("tx2")).Text.ToInteger(),
                    N3 = ((TextBox)row.FindControl("tx3")).Text.ToInteger(),
                    N4 = ((TextBox)row.FindControl("tx4")).Text.ToInteger(),
                    N5 = ((TextBox)row.FindControl("tx5")).Text.ToInteger(),
                    N6 = ((TextBox)row.FindControl("tx6")).Text.ToInteger()
                };

                AddPole(i);
            }
            catch
            {
                AlertMessage("פעולה לא בוצעה בהצלחה");
            }
        }

        public void AddPole(LottoPole i)
        {
            DBController.DbLotto.AddLottoPole(i);
            LottoHandler.FindMatches(DBController, i);
            //RefreshGrid(gvNewPole);
            RefreshGrid(gv);
        }

        protected void btnUpdate_Command(object sender, CommandEventArgs e)
        {
            LottoPole cp = DBController.DbLotto.Get(new LottoPolesSearchParameters { Id = e.CommandArgument.ToString().ToInteger() }).FirstOrDefault();
            LottoHandler.FindMatches(DBController, cp);
            RefreshGrid(gv);
        }

        protected void btnReGenerate_Command(object sender, CommandEventArgs e)
        {
            panelx.SetEditedItemId(e.CommandArgument.ToString().ToInteger());
            panelx.Visible = true;
        }

        private  List<LottoRow> GetPoleRows(LottoPole cp)
        {
            return DBController.DbLotto.Get(new LottoRowsSearchParameters { PoleActionDate = cp.PoleActionDate, PoleKey = cp.PoleKey });
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            HideEditor();
        }

        private void HideEditor()
        {
            panelx.Visible = false;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (txPoleDate.Text.NotEmpty())
            {
                var poleId = panelx.GetEditedItemId();
                if (poleId.HasValue)
                {
                    var allPoles = DBController.DbLotto.Get(new LottoPolesSearchParameters { });
                    var maxpoleNum = allPoles.Select(x => x.PoleKey).Where(x => x.IsInteger()).Select(x => x.ToInteger()).OrderByDescending(x => x).First();
                    LottoPole cp = DBController.DbLotto.Get(new LottoPolesSearchParameters { Id = poleId }).FirstOrDefault();
                    List<LottoRow> rowPoles = GetPoleRows(cp);
                    foreach (var row in rowPoles)
                    {
                        row.PoleDestinationDate = txPoleDate.Text.ToDateTime().Date;
                        row.PoleKey = string.Empty;
                        DBController.DbLotto.AddLottoRow(row);
                    }
                    ShowUserNotification("פעולה בוצעה בהצלחה");
                }
            }
            HideEditor();
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            RefreshGrid(gv);
        }
    }
}