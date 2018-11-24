using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSimplify.Data;

namespace WebSimplify
{
    public partial class AppSettingsPage : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dvcredit.Visible = CurrentUser.Allowed(ClientPagePermissions.CreditData);
                dvWorkHours.Visible = CurrentUser.Allowed(ClientPagePermissions.WorkHours);
                dvBalance.Visible = CurrentUser.Allowed(ClientPagePermissions.MoneyBalance);
                dvThemes.Visible = CurrentUser.IsAdmin;
                
                RefreshGrid(gvThemes);
                FillSettings();
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvThemes.ID)
                return "GetThemes";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetThemes()
        {
            if (dvThemes.Visible)
            {
                List<ThemeScript> tls = new List<ThemeScript> { new ThemeScript { IsDummy = true } };
                tls.AddRange(DBController.DbLog.GetThemes(new ThemeSearchParameters { }));
                return tls;
            }
            return null;
        }

        private void FillSettings()
        {
            if (!CurrentUser.IsAdmin)
            {
                if (CurrentUser.Allowed(ClientPagePermissions.CreditData))
                {
                    var p = CurrentUser.Preferences;
                    txCreditDayOfMonth.Value = p.CreditCardPaymentDay.ToString();
                    txCreditStartDate.Text = p.CreditLogStartDate != DateTime.MinValue ?  p.CreditLogStartDate.ToInputFormat() : DateTime.Now.ToInputFormat();
                    chkUseCharts.Checked = p.UseCharts;
                }

                if (CurrentUser.Allowed(ClientPagePermissions.WorkHours))
                {
                    var p = CurrentUser.Preferences;
                    txWorkHour.Text = p.DailyRequiredWorkHours.NotNull() ? p.DailyRequiredWorkHours.Hour.ToString() : string.Empty;
                    txWorkMinute.Text = p.DailyRequiredWorkHours.NotNull() ? p.DailyRequiredWorkHours.Minute.ToString() : string.Empty;
                }
                if (CurrentUser.Allowed(ClientPagePermissions.MoneyBalance))
                {
                    var p = CurrentUser.Preferences;
                    txBalanceStartDate.Text = !p.BalanceLogStartDate.IsDefault() ? p.BalanceLogStartDate.ToString() : string.Empty;
                }
            }
        }

        protected override string DefaultNavItem
        {
            get
            {
                return "navsys";
            }
        }


        protected void btnReverse_ServerClick(object sender, EventArgs e)
        {
            UiManager.ReverseStyle();
            AlertMessage("פעולה זו בוצעה בהצלחה");
        }

        protected void btnSaveSettings_ServerClick(object sender, EventArgs e)
        {
            if (!CurrentUser.IsAdmin)
            {
                try
                {
                    if (CurrentUser.Allowed(ClientPagePermissions.CreditData))
                    {
                        var p = CurrentUser.Preferences;
                        p.CreditCardPaymentDay = txCreditDayOfMonth.Value.ToInteger();
                        p.CreditLogStartDate = txCreditStartDate.Text.ToDateTime();
                        p.UseCharts = chkUseCharts.Checked;
                    }

                    if (CurrentUser.Allowed(ClientPagePermissions.WorkHours))
                    {
                        var p = CurrentUser.Preferences;
                        if (!p.DailyRequiredWorkHours.NotNull())
                            p.DailyRequiredWorkHours = new WorkTime();
                        p.DailyRequiredWorkHours.Hour = txWorkHour.Text.ToInteger();
                        p.DailyRequiredWorkHours.Minute = txWorkMinute.Text.ToInteger();
                    }

                    if (CurrentUser.Allowed(ClientPagePermissions.MoneyBalance))
                    {
                        var p = CurrentUser.Preferences;
                        p.BalanceLogStartDate = txBalanceStartDate.Text.ToDateTime();
                    }

                    DBController.DbAuth.UpdatePreferences(CurrentUser);
                }
                catch (Exception ex)
                {
                    AlertMessage("אחד או יותר מהנתונים שהוזנו אינו תקין");
                    return;
                }
            }
        }

        protected void btnAddCashItem_Command(object sender, CommandEventArgs e)
        {
            ThemeScript i = new ThemeScript();
            var row = (sender as ImageButton).NamingContainer as GridViewRow;
            i.ElementIdentifier = ((TextBox)row.FindControl("txElementIdentifier")).Text;
            i.CssAttribute = ((TextBox)row.FindControl("txCssAttribute")).Text;
            i.CssValue = ((TextBox)row.FindControl("txCssValue")).Text;
            DBController.DbLog.Add(i);
            RefreshGrid(gvThemes);
        }

        protected void gvThemes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (ThemeScript)e.Row.DataItem;
                ((TextBox)e.Row.FindControl("txElementIdentifier")).Text = d.ElementIdentifier;
                ((TextBox)e.Row.FindControl("txCssAttribute")).Text = d.CssAttribute;
                ((TextBox)e.Row.FindControl("txCssValue")).Text = d.CssValue;

                var btnAddCashItem = ((ImageButton)e.Row.FindControl("btnAddCashItem"));
                var btnUpade = ((ImageButton)e.Row.FindControl("btnUpade"));
                btnAddCashItem.Visible = d.IsDummy;
                btnUpade.Visible = !d.IsDummy;

                if (!d.IsDummy)
                    btnUpade.CommandArgument = d.Id.ToString();
            }
        }

        protected void gvThemes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvThemes.PageIndex = e.NewPageIndex;
            RefreshGrid(gvThemes);
        }

        protected void btnUpade_Command(object sender, CommandEventArgs e)
        {
            ThemeScript i = GetItem(e.CommandArgument);
            var row = (sender as ImageButton).NamingContainer as GridViewRow;
            i.ElementIdentifier = ((TextBox)row.FindControl("txElementIdentifier")).Text;
            i.CssAttribute = ((TextBox)row.FindControl("txCssAttribute")).Text;
            i.CssValue = ((TextBox)row.FindControl("txCssValue")).Text;
            DBController.DbLog.Update(i);
            RefreshGrid(gvThemes);
        }

        private ThemeScript GetItem(object ca)
        {
            return DBController.DbLog.GetThemes(new ThemeSearchParameters { Id = ca.ToString().ToInteger() }).FirstOrDefault(); 
        }

        [WebMethod]
        public static List<object> GetThemeData()
        {
            List<object> chartData = new List<object>();

            List<ThemeScript> scripyts = DBController.DbLog.GetThemes(new ThemeSearchParameters { });

            foreach (var item in scripyts)
                chartData.Add(new object[] { "", item.ScriptText });

            return chartData;
        }
    }
}