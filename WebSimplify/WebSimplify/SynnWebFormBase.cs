using SynnCore.Controls;
using SynnCore.Generics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebSimplify;
using WebSimplify.Data;

namespace SynnWebOvi
{
    public class SynnWebFormBase : System.Web.UI.Page
    {

        internal static IDatabaseProvider DBController = SynnDataProvider.DbProvider;
        internal const string DecimalFormat = "#.#";
        public LoggedUser CurrentUser
        {
            get
            {
                return (LoggedUser)GetFromSession("ssUser_*");
            }
            set
            {
                StoreInSession("ssUser_*", value);
            }
        }

        protected override void InitializeCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("he-IL");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            base.InitializeCulture();
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
        protected virtual string NavIdentifier { get;}
        protected virtual List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                return new List<ClientPagePermissions>();
            }
        }

        protected virtual bool HasNavLink
        {
            get
            {
                return true;
            }
        }
        
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

        protected void ClearInputs(params HtmlControl[] p)
        {
            foreach (var input in p)
            {
                if (input is HtmlInputGenericControl)
                {
                    (input as HtmlInputGenericControl).Value = string.Empty;
                }
                else if (input is HtmlInputText)
                {
                    (input as HtmlInputText).Value = string.Empty;
                }
                else if (input is HtmlTextArea)
                {
                    (input as HtmlTextArea).Value = string.Empty;
                }
            }
        }

        protected bool ValidateInputs(params HtmlControl[] p)
        {
            foreach (var input in p)
            {
                if (input is HtmlInputText)
                    if (string.IsNullOrEmpty((input as HtmlInputText).Value))
                        return false;
            }
            return true;
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!LoginProvider && CurrentUser == null)
                SynNavigation.Goto(Pages.Login);
            if (!IsPostBack)
            {
                if (RequiredPermissions.NotEmpty())
                    Title = GenericFormatter.GetEnumDescription(RequiredPermissions[0]);
                else
                    Title = "מסך הבית";
                if (!LoginProvider)
                {
                    Master.FindControl("navdiary").Visible = CurrentUser.Allowed(ClientPagePermissions.Diary);
                    Master.FindControl("navdic").Visible = CurrentUser.Allowed(ClientPagePermissions.Dictionary);
                    Master.FindControl("navshifts").Visible = CurrentUser.Allowed(ClientPagePermissions.Shifts);
                    Master.FindControl("navshop").Visible = CurrentUser.Allowed(ClientPagePermissions.Shopping);
                    Master.FindControl("navwed").Visible = CurrentUser.Allowed(ClientPagePermissions.Wedding);
                    Master.FindControl("navtask").Visible = CurrentUser.Allowed(ClientPagePermissions.QuickTasks);
                    Master.FindControl("navcredit").Visible = CurrentUser.Allowed(ClientPagePermissions.CreditData);
                    Master.FindControl("navcash").Visible = CurrentUser.Allowed(ClientPagePermissions.CashLog);

                    //Master.FindControl("navsys").Visible = CurrentUser.IsAdmin;
                    Master.FindControl("navusers").Visible = CurrentUser.Allowed(ClientPagePermissions.SysAdmin) || CurrentUser.IsAdmin;
                    Master.FindControl("navlog").Visible = CurrentUser.Allowed(ClientPagePermissions.SysAdmin) || CurrentUser.IsAdmin;

                    if(HasNavLink)
                        ((HtmlAnchor)Master.FindControl(NavIdentifier)).Attributes.Add("class", "active");
                }

                foreach (ClientPagePermissions en in RequiredPermissions)
                {
                    if (!CurrentUser.Allowed(en))
                        SynNavigation.Goto(Pages.Main);
                }
            }
        }

        

        public void AlertMessage(string message)
        {
            string scriptmessage = string.Format("alert(\"{0}\");", message);
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", scriptmessage, true);
        }


        protected void AddSelectItemForCombo(DropDownList cmb)
        {
            cmb.Items.Add(new System.Web.UI.WebControls.ListItem { Text = "בחר", Value = "-1" , Selected = true});
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

        public void RefreshGrid(GridView gv)
        {
            string methodName = GetGridSourceMethodName(gv.ID);
            MethodInfo m = Page.GetType().GetMethod(methodName, new Type[0]);
            gv.DataSource = (IEnumerable)m.Invoke(Page, null);
            gv.DataBind();            
        }
        

        internal virtual string GetGridSourceMethodName(string gridId)
        {
            throw new NotImplementedException();
        }

        public void ShowUserNotification(string messageText)
        {
                    ClientScript.RegisterStartupScript(GetType(), "jNotify", string.Format(@"
        jNotify(
            '{0}',
            {{
                ShowOverlay: {1},
                autoHide: true,
                TimeShown: {3},
                HorizontalPosition: 'right',
                VerticalPosition: 'bottom',
                LongTrip: 40,
                MinWidth: {2}
            }}
          );", messageText.Replace("\n", "<br>"), "false", "200", "2500"),
             true);
        }

        [WebMethod]
        [ScriptMethod()]
        public static void Navigate(string destinationpage)
        {
            string url = string.Empty;

            SynNavigation.Goto(url);
        }
    }

}