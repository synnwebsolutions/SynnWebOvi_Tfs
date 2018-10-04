using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SynnWebOvi
{
    public partial class Main : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddToDict_ServerClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txDicKey.Value) || string.IsNullOrEmpty(txDicVal.Value))
            {
                AlertMessage("יש את כל השדות");
                return;
            }

            DBController.DbUserDictionary.Add(txDicKey.Value, txDicVal.Value);
            ClearInputFields(new List<System.Web.UI.HtmlControls.HtmlInputControl> {txDicKey });
            txDicVal.Value = string.Empty;

        }

        protected void btnSearchDic_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnClearDic_ServerClick(object sender, EventArgs e)
        {

        }
    }
}