using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class WebSimplify : System.Web.UI.MasterPage
    {
        public string CurrentUserName
        {
            get;
            set;
        }

        public bool IsAdmin { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            
           
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            SetInfoBar();
        }

        private void SetInfoBar()
        {
            try
            {
                dvVrs.InnerText = string.Format(dvVrs.InnerText, Assembly.GetExecutingAssembly().GetName().Version.ToString());
                dvurs.InnerText = string.Format(dvurs.InnerText, CurrentUserName);
                if (!IsAdmin)
                {
                    navBarR.Attributes["class"] = navBarR.Attributes["class"].ToString() + " gradient-bg";
                }
                //infoBar.Attributes["class"] = infoBar.Attributes["class"].ToString() + " gradient-bg";
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnMessageBoxOk_Click(object sender, EventArgs e)
        {
            messageBoxx.Hide();
        }

        protected void nvLogout_ServerClick(object sender, EventArgs e)
        {
            HttpContext.Current.Session[SynNavigation.SignOutKey] = 1;
            SynNavigation.Goto(Pages.Login);
        }
    }
}