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

        private List<Account> GetAccounts()
        {
            return DBController.DbGenericData.GetGenericData<Account>(new GenericDataSearchParameters { });
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Logger.Instance.Info($"{CurrentUser.UserName} DepositsPage Last Enter at : {DateTime.Now}");
                RefreshView();
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvMyDeposit.ID)
                return "GetMyDeposits";
            if (gridId == gvUsersDeposits.ID)
                return "GetUserss";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetUserss()
        {
            var ul = DBController.DbAuth.GetUsers(new UserSearchParameters());
            return ul.Where(x => x.Allowed(ClientPagePermissions.Deposits)).OrderBy(x => x.DisplayName).ToList();
        }

        public IEnumerable GetMyDeposits()
        {
            var ul = DBController.DbGenericData.GetGenericData<UserDeposit>(new UserDepositSearchParameters { UserId = CurrentUser.IsAdmin  ? (int?)null : CurrentUser.Id });
            return ul.OrderByDescending(x => x.IDate).ToList();
        }


        private void RefreshView()
        {
            var ul = GetMyDeposits().OfType<UserDeposit>().ToList();
            if (!CurrentUser.IsAdmin)
            {
                var myDeposits = DBController.DbGenericData.GetGenericData<UserDeposit>(new GenericDataSearchParameters { });
                var accounts = GetAccounts();

                var required = accounts.Sum(x => x.GetTotalToPay());
                var myPayments = ul.Sum(x => x.Amount);

                txpSumm.Text = myPayments.FormattedString();
                txpBalance.Text = (required - myPayments).FormattedString();
                txRequired.Text = required.FormattedString();

                txpUpToDate.Text = myDeposits.NotEmpty() ? myDeposits.Max(x => x.IDate).ToShortDateString() : DateTime.Now.ToShortDateString();
            }
            RefreshGrid(gvMyDeposit);
            RefreshGrid(gvUsersDeposits);
        }

        protected void gvMyDeposit_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (UserDeposit)e.Row.DataItem;
                Account account = null;
                LoggedUser u = null;
                if (d.UserId > 0)
                    u = DBController.DbAuth.GetUser(d.UserId);
                if(d.AccountId > 0)
                    account = (Account)DBController.DbGenericData.GetSingleGenericData(new GenericDataSearchParameters { Id = d.AccountId, FromType = typeof(Account) });

                ((Label)e.Row.FindControl("lblMessage")).Text = d.PaymentReferenceNumber;
                ((Label)e.Row.FindControl("lblId")).Text = d.Amount.FormattedString();
                ((Label)e.Row.FindControl("lblDate")).Text = d.IDate.ToShortDateString();
                ((Label)e.Row.FindControl("lblexec")).Text = u?.DisplayName;
                
            }
        }

        protected void gvUsersDeposits_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var user = (LoggedUser)e.Row.DataItem;
                var payments = DBController.DbGenericData.GetGenericData<UserDeposit>(new UserDepositSearchParameters { UserId = user.Id });
                var accounts = GetAccounts();

                var required = accounts.Sum(x => x.GetTotalToPay());
                var userPayments = payments.Sum(x => x.Amount);
 

                ((Label)e.Row.FindControl("lblTotal")).Text = userPayments.FormattedString();
                ((Label)e.Row.FindControl("lblBal")).Text = (required - userPayments).FormattedString();
                ((Label)e.Row.FindControl("lblDatev")).Text = payments.NotEmpty() ?payments.Max(x => x.IDate).ToShortDateString() : string.Empty;
                ((Label)e.Row.FindControl("lblexecv")).Text = user?.DisplayName;

            }
        }

        protected void gvMyDeposit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMyDeposit.PageIndex = e.NewPageIndex;
            RefreshGrid(gvMyDeposit);
        }
    }
}