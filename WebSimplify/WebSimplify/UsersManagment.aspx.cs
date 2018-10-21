﻿using SynnCore.Generics;
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

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvClientPagePermissions.ID)
                return "GetClientPagePermissions";
            if (gridId == gvGroupPermissions.ID)
                return "GetGroupPermissions";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetGroupPermissions()
        {
            List<PermissionGroup> pls = DBController.DbAuth.GetPermissionGroup();
            return pls;
        }

        public IEnumerable GetClientPagePermissions()
        {
            return Enum.GetValues(typeof(ClientPagePermissions));
        }

        protected void btnAddUser_ServerClick(object sender, EventArgs e)
        {

            if (ValidateInputs(txNewUserName))
            {
                LoggedUser u = new LoggedUser();
                u.UserName = txNewUserName.Value;
                u.Password = txNewFirstPassword.Value;
               
                foreach (GridViewRow gvr in gvGroupPermissions.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkGroup")).Checked)
                    {
                        int grouppId = int.Parse(((HiddenField)gvr.FindControl("hfgid")).Value);
                        u.AllowedSharedPermissions.Add(grouppId);
                        
                    }
                }
                foreach (GridViewRow gvr in gvClientPagePermissions.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chk")).Checked)
                    {
                        int pageid = int.Parse(((HiddenField)gvr.FindControl("hfpid")).Value);
                        u.AllowedClientPagePermissions.Add((ClientPagePermissions)pageid);
                    }
                }
                if (u.AllowedSharedPermissions.Count > 1)
                {
                    AlertMessage("יותר מדי קבוצות הרשאה");
                    return;
                }
                DBController.DbAuth.Add(u);
                SetInputs(null);
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

        protected void abtnAddGroup_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInputs(txNewGroupName))
            {
                PermissionGroup g = new PermissionGroup {Name = txNewGroupName.Value };
                DBController.DbAuth.Add(g);
                AlertMessage("פעולה זו בוצעה בהצלחה");
                ClearInputs(txNewGroupName);
                RefreshGridPageGrids();
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }

        private void RefreshGridPageGrids()
        {
            cmbusers.Items.Clear();
            List<LoggedUser> ul = DBController.DbAuth.GetUsers(new UserSearchParameters());
            AddSelectItemForCombo(cmbusers);
            foreach (var u in ul)
                cmbusers.Items.Add(new ListItem { Text = u.UserName, Value = u.Id.ToString() });

            RefreshGrid(gvClientPagePermissions);
            RefreshGrid(gvGroupPermissions);
        }

        protected void cmbusers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoggedUser u = null;
            if (cmbusers.SelectedIndex > 0)
            {
                u = DBController.DbAuth.GetUsers(new UserSearchParameters { Id = Convert.ToInt32(cmbusers.SelectedValue) }).First() ;
            }
            SetInputs(u);
        }

        private void SetInputs(LoggedUser user)
        {
            txNewFirstPassword.Value = user != null ? user.Password : string.Empty;
            txNewUserName.Value = user != null ? user.UserName : string.Empty;
            foreach (GridViewRow gvr in gvGroupPermissions.Rows)
            {
                int grouppId = int.Parse(((HiddenField)gvr.FindControl("hfgid")).Value);
                ((CheckBox)gvr.FindControl("chkGroup")).Checked = user != null && user.AllowedSharedPermissions.Contains(grouppId);
            }
            foreach (GridViewRow gvr in gvClientPagePermissions.Rows)
            {
                int pageid = int.Parse(((HiddenField)gvr.FindControl("hfpid")).Value);
                ((CheckBox)gvr.FindControl("chk")).Checked = user != null && user.AllowedClientPagePermissions.Contains((ClientPagePermissions)pageid);
            }
        }
    }
}