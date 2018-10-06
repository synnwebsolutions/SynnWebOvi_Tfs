using SynnCore.Controls;
using SynnCore.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SynnWebOvi
{
    public class SynnWebFormBase : System.Web.UI.Page
    {

        internal IDatabaseProvider DBController = SynnDataProvider.DbProvider;

        public LoggedUser CurrentUser
        {
            get
            {
                return (LoggedUser)GetFromSession("ssUser_*");
            }
            set
            {
                StoreInSession("ssUser_*",value);
            }
        }

        internal void FillEnum(DropDownList cmb, Type type, bool addSelectValue = true)
        {
            AddSelectItemForCombo(cmb);
            foreach (Enum item in Enum.GetValues(type))
            {
                string description = GenericFormatter.GetEnumDescription(item);
                cmb.Items.Add(new System.Web.UI.WebControls.ListItem { Text = description, Value = Convert.ToInt32(item).ToString() });
            }
        }

        internal void ClearInputFields(List<HtmlInputControl> ctrs)
        {
            foreach (HtmlInputControl ctr in ctrs)
            {
                ctr.Value = string.Empty;
            }
        }

        //public bool UserAllowed(LoggedUser user)
        //{
        //    //foreach (PermissionsEnum en in RequiredPermissions)
        //    //{
        //    //    if (!user.Allowed(en))
        //    //        return false;
        //    //}
        //    return true;
        //}

        //protected virtual PermissionsEnumList RequiredPermissions
        //{
        //    get
        //    {
        //        return InternalRequiredPermissions;
        //    }
        //}

        protected virtual bool LoginProvider
        {
            get
            {
                return false;
            }
        }

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    StringBuilder sbOut = new StringBuilder();
        //    StringWriter swOut = new StringWriter(sbOut);
        //    HtmlTextWriter htwOut = new HtmlTextWriter(swOut);
        //    base.Render(htwOut);
        //    CurrentHtml = sbOut.ToString();
        //    writer.Write(CurrentHtml);
        //}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsPostBack)
            {
                //if (HideReturnButton)
                //{
                //    //Master.FindControl("btnBack").Visible = false;
                //}
                if (!LoginProvider)
                {
                    //Master.FindControl("btnWed").Visible = false;
                }
                //else
                //{
                //    Master.FindControl("btnTasks").Visible = CurrentUser.Logged;
                //    Master.FindControl("btnSOut").Visible = CurrentUser.Logged;
                //    Master.FindControl("btnLogs").Visible = CurrentUser.Logged && (CurrentUser.IsAdmin || CurrentUser.IsManager);
                //    Master.FindControl("btnUsers").Visible = CurrentUser.Logged && (CurrentUser.IsAdmin || CurrentUser.IsManager);
                //    Master.FindControl("btnLoginAs").Visible = CurrentUser.Logged && CurrentUser.IsAdmin;
                //}
            }
        }

        public void AlertMessage(string message)
        {
            string scriptmessage = string.Format("alert(\"{0}\");", message);
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", scriptmessage, true);
        }


        protected void AddSelectItemForCombo(DropDownList cmb)
        {
            cmb.Items.Add(new System.Web.UI.WebControls.ListItem { Text = "בחר", Value = "-1" });
        }


        public void SetComboValue(HtmlSelect cmb, string valueToFind)
        {
            var li = cmb.Items.OfType<System.Web.UI.WebControls.ListItem>().Where(x => x.Value == valueToFind).FirstOrDefault();
            cmb.SelectedIndex = cmb.Items.IndexOf(li);
        }

        public void StoreInSession(string key, object obj)
        {
            Session[key] = obj;
        }
        
        public object GetFromSession(string key)
        {
            if (Session[key] != null)
                return Session[key];
            return null;
        }

        public void NotifySucces(string msg = null)
        {
            if (msg != null)
                AlertMessage(msg);
            else
                AlertMessage("הפעולה בוצעה בהצלחה");
        }
    }

}