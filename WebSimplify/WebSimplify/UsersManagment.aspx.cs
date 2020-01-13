using Newtonsoft.Json.Linq;
using SynnCore.Generics;
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
    public partial class UsersManagment : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txNewFirstPassword.Value = Global.FirstLoginPass;
                RefreshGridPageGrids();
            }
        }

        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.SysUsers };
                return l;
            }
        }

        public LoggedUser EditedUser
        {
            get
            {
                return (LoggedUser)GetFromSession("edt*");
            }
            set
            {
                StoreInSession("edt*", value);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            rwGApi.Visible = EditedUser != null && EditedUser.Id > 0;
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvClientPagePermissions.ID)
                return "GetClientPagePermissions";
  
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetGroupPermissions()
        {
            List<PermissionGroup> pls = DBController.DbAuth.GetPermissionGroup();
            return pls;
        }

        public IEnumerable GetClientPagePermissions()
        {
            return Enum.GetValues(typeof(ClientPagePermissions)).OfType< ClientPagePermissions>().Where(x => !CurrentUser.CheckIsAdminBlock(x));
        }

        protected void btnAddUser_ServerClick(object sender, EventArgs e)
        {
            bool newUser = EditedUser == null;
            if (newUser)
                EditedUser = new LoggedUser();

            if (ValidateInputs(txNewUserName))
            {
                EditedUser.UserName = txNewUserName.Value;
                EditedUser.Password = txNewFirstPassword.Value;
                EditedUser.DisplayName = txDisplay.Value;
                EditedUser.AllowedClientPagePermissions = new List<ClientPagePermissions>();
                foreach (GridViewRow gvr in gvClientPagePermissions.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chk")).Checked)
                    {
                        int pageid = int.Parse(((HiddenField)gvr.FindControl("hfpid")).Value);
                        EditedUser.AllowedClientPagePermissions.Add((ClientPagePermissions)pageid);
                    }
                }
                
                if (newUser)
                    DBController.DbAuth.Add(EditedUser);
                else
                {
                    DBController.DbAuth.Update(EditedUser); // edit !!!!!!
                }
                if (ValidateInputs(txProjectId, TxClientSecret, txClientId) && !newUser)
                {
                    UserGoogleApiData apiData = GetUserGoogleApiData(EditedUser.Id);
                    apiData.installed.project_id = txProjectId.Value;
                    apiData.installed.client_secret = TxClientSecret.Value;
                    apiData.installed.client_id = txClientId.Value;
                    apiData.UserId = EditedUser.Id;
                    if (apiData.Id > 0)
                        DBController.DbGenericData.Update(apiData);
                    else
                        DBController.DbGenericData.Add(apiData);
                }
                EditedUser = null;
                SetInputs();
                AlertMessage("פעולה זו בוצעה בהצלחה");
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }

        protected void gvClientPagePermissions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (ClientPagePermissions)e.Row.DataItem;
                ((CheckBox)e.Row.FindControl("chk")).Checked =false;
                ((Label)e.Row.FindControl("LblPageName")).Text = GenericFormatter.GetEnumDescription(d);
                ((HiddenField)e.Row.FindControl("hfpid")).Value = ((int)d).ToString() ;
            }
        }

        protected void gvGroupPermissions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (PermissionGroup)e.Row.DataItem;
                ((CheckBox)e.Row.FindControl("chkGroup")).Checked = false;
                ((Label)e.Row.FindControl("LblGroupName")).Text = d.Name;
                ((HiddenField)e.Row.FindControl("hfgid")).Value = d.Id.ToString();
            }
        }

        private void RefreshGridPageGrids()
        {
            cmbusers.Items.Clear();
            List<LoggedUser> ul = DBController.DbAuth.GetUsers(new UserSearchParameters());
            AddSelectItemForCombo(cmbusers);
            foreach (var u in ul)
                cmbusers.Items.Add(new ListItem { Text = u.DisplayName, Value = u.Id.ToString() });

            RefreshGrid(gvClientPagePermissions);
        }

        protected void cmbusers_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditedUser = null;
            if (cmbusers.SelectedIndex > 0)
            {
                EditedUser = DBController.DbAuth.GetUsers(new UserSearchParameters { Id = Convert.ToInt32(cmbusers.SelectedValue) }).First() ;
            }
            SetInputs();
        }

        private void SetInputs()
        {
            txNewFirstPassword.Value = EditedUser != null ? EditedUser.Password : string.Empty;
            txNewUserName.Value = EditedUser != null ? EditedUser.UserName : string.Empty;
            txDisplay.Value = EditedUser != null ? EditedUser.DisplayName : string.Empty;
            
            foreach (GridViewRow gvr in gvClientPagePermissions.Rows)
            {
                int pageid = int.Parse(((HiddenField)gvr.FindControl("hfpid")).Value);
                ((CheckBox)gvr.FindControl("chk")).Checked = EditedUser != null && EditedUser.AllowedClientPagePermissions.Contains((ClientPagePermissions)pageid);
            }
            btnAddUser.InnerText = EditedUser != null ? "עדכן" : "הוסף";
            cmbusers.SelectedValue = EditedUser != null ? EditedUser.Id.ToString() : "-1";
            UserGoogleApiData apiData = new UserGoogleApiData();
            if (EditedUser != null)
            {
                apiData = GetUserGoogleApiData(EditedUser.Id);
            }

            txClientId.Value = EditedUser != null ? apiData.installed.client_id : string.Empty;
            TxClientSecret.Value = EditedUser != null ? apiData.installed.client_secret : string.Empty;
            txProjectId.Value = EditedUser != null ? apiData.installed.project_id : string.Empty;
        }

        private UserGoogleApiData GetUserGoogleApiData(int userId)
        {
            return DBController.DbGenericData.GetGenericData<UserGoogleApiData>(new GoogleApDataSearchParameters { UserId = userId }).FirstOrDefault() ??
                new UserGoogleApiData(); 
        }
    }
}