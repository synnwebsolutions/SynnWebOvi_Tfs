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
    public partial class DepositsPage : SynnWebFormBase
    {
        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.Deposits };
                return l;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshView();
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvMyDeposit.ID)
                return "GetMyDeposits";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetMyDeposits()
        {
            var ul = DBController.DbGenericData.GetGenericData<UserDeposit>(new UserDepositSearchParameters { UserId = CurrentUser.Id });
            return ul.OrderByDescending(x => x.IDate).ToList();
        }

        private void RefreshView()
        {
            var ul = GetMyDeposits().OfType<UserDeposit>().ToList();
            if (!CurrentUser.IsAdmin)
            {
                var myDeposits = DBController.DbGenericData.GetGenericData<UserDeposit>(new GenericDataSearchParameters { });
                var accounts = DBController.DbGenericData.GetGenericData<Account>(new GenericDataSearchParameters { });

                var myBalance = 0;
                foreach (var acc in accounts)
                {
                    myBalance += acc.GetMyBalnace(myDeposits, DBController);
                }
                txpSumm.Text = ul.Sum(x => x.Amount).FormattedString();
                txpBalance.Text = myBalance.FormattedString();

                
                txpUpToDate.Text = myDeposits.NotEmpty() ? myDeposits.Max(x => x.IDate).ToShortDateString() : DateTime.Now.ToShortDateString();
            }
            RefreshGrid(gvMyDeposit);
        }

        protected void gvMyDeposit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (UserDeposit)e.Row.DataItem;
                var account = (Account)DBController.DbGenericData.GetSingleGenericData(new GenericDataSearchParameters { Id = d.AccountId, FromType = typeof(Account) });

                ((Label)e.Row.FindControl("lblMessage")).Text = account.DepositName;
                ((Label)e.Row.FindControl("lblId")).Text = d.Amount.FormattedString();
                ((Label)e.Row.FindControl("lblDate")).Text = d.IDate.ToShortDateString();
            }
        }
    }
}