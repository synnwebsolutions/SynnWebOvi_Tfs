using SynnWebOvi;
using System;
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

        }
    }
}