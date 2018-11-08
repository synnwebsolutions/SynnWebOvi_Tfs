using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                FillSettings();
            }
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
            }
        }

        protected override string NavIdentifier
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

                    DBController.DbAuth.UpdatePreferences(CurrentUser);
                }
                catch (Exception ex)
                {
                    AlertMessage("אחד או יותר מהנתונים שהוזנו אינו תקין");
                    return;
                }
            }
        }
    }
}