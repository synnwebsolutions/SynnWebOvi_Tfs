using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SynnWebOvi
{
    public partial class SynnWebOvi : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkDictionary_ServerClick(object sender, EventArgs e)
        {
            Nav(SynNavigation.Pages.UserDictionaryPage);
        }

        private void Nav(string i_page)
        {
            SynNavigation.Goto(i_page);
        }

        protected void lnkDiary_ServerClick(object sender, EventArgs e)
        {
            Nav(SynNavigation.Pages.Diary);
        }

        protected void lnkWedding_ServerClick(object sender, EventArgs e)
        {
            Nav(SynNavigation.Pages.Wedding);
        }

        protected void lnkSystemPreferences_ServerClick(object sender, EventArgs e)
        {
            Nav(SynNavigation.Pages.AppSettingsPage);
        }

        protected void lnkLog_ServerClick(object sender, EventArgs e)
        {
            Nav(SynNavigation.Pages.Log);
        }

        protected void lnkHome_ServerClick(object sender, EventArgs e)
        {
            Nav(SynNavigation.Pages.Main);
        }
    }
}