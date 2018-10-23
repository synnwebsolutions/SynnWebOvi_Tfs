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
                AddSelectItemForCombo(cmbFileTypes);
                foreach (XuiFile u in Enum.GetValues(typeof(XuiFile)))
                    cmbFileTypes.Items.Add(new ListItem { Text = u.ToString(), Value = ((int)u).ToString() });
            }
        }

        protected override string NavIdentifier
        {
            get
            {
                return "navsys";
            }
        }

        protected void btnGenerateTheme_ServerClick(object sender, EventArgs e)
        {
            if (cmbFileTypes.SelectedIndex > 0 &&  ValidateInputs(txdata))
            {
                XuiFile xi = (XuiFile)Convert.ToInt32(cmbFileTypes.SelectedValue);
                UiManager.Apply(xi, txdata.Value);

                AlertMessage("פעולה זו בוצעה בהצלחה");
                ClearInputs(txdata);
                
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }

        protected void btnReverse_ServerClick(object sender, EventArgs e)
        {
            UiManager.ReverseStyle();
            AlertMessage("פעולה זו בוצעה בהצלחה");
        }
    }
}