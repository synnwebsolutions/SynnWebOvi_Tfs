﻿using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSimplify.Controls;

namespace WebSimplify
{
    public partial class DevTasks : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dvTaks.Visible = CurrentUser.Allowed(ClientPagePermissions.SysDev);
                FillEnum(cmbXStatus, typeof(DevTaskStatus));

                RefreshView();
            }
        }
       
        private void RefreshView()
        {
            RefreshGrid(gv);
            RefreshGrid(gvAdd);
        }
        protected override List<Panel> ModalPanels
        {
            get
            {
                return new List<Panel> { panelx};
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetCurrentShopItems";
            if (gridId == gvAdd.ID)
                return DummyMethodName;
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetCurrentShopItems()
        {
            List<DevTaskItem> ul = DBController.DbAuth.Get(new DevTaskItemSearchParameters { Active = true });
            return ul.Where(x => x.Status != DevTaskStatus.Finalized).OrderBy(x => (int)x.Status).ToList();
        }

        protected void gvAdd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((TextBox)e.Row.FindControl("txtaskname")).Text = ((TextBox)e.Row.FindControl("txtaskdesc")).Text = string.Empty;
            }
        }

        protected void btnAdd_Command(object sender, CommandEventArgs e)
        {
            var row = (sender as ImageButton).NamingContainer as GridViewRow;

            var txtaskname = ((TextBox)row.FindControl("txtaskname")).Text;
            var txtaskdesc = ((TextBox)row.FindControl("txtaskdesc")).Text;
            if (txtaskname.NotEmpty())
            {
                var d = new DevTaskItem
                {
                     Name = txtaskname,
                     Description = txtaskdesc, 
                     Status = DevTaskStatus.Open
                };
                DBController.DbAuth.Add(d);
                AlertMessage(" פנייתך התקבלה בהצלחה - הנושא יטופל בהקדם  ");
                RefreshView();
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }

        protected void btnClose_Command(object sender, CommandEventArgs e)
        {
            AlertMessage("לא ניתן למחוק משימות - יש לשנות סטטוס ל 'בוטל'");
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (DevTaskItem)e.Row.DataItem;
                ((Label)e.Row.FindControl("lblItemName")).Text = d.Name;
                ((Label)e.Row.FindControl("lbltaskdesc")).Text = d.Description;
                ((Label)e.Row.FindControl("lblStatus")).Text = d.Status.GetDescription();
                ((ImageButton)e.Row.FindControl("btnClose")).CommandArgument = d.Id.ToString();
                ((ImageButton)e.Row.FindControl("btnEdit")).CommandArgument = d.Id.ToString();
            }
        }

        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            RefreshGrid(gv);
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            panelx.SetHeader("עריכה");
            var dt = DBController.DbAuth.Get(new DevTaskItemSearchParameters { Id = e.CommandArgument.ToString().ToInteger() }).First();
            cmbXStatus.SelectedValue = ((int)dt.Status).ToString();
            txdTaskName.Text = dt.Name;
            txXtaskDesc.Text = dt.Description;
            panelx.SetEditedItemId(dt.Id);
            panelx.Show();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            panelx.Hide();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            var d = new DevTaskItem
            {
                Description = txXtaskDesc.Text,
                Name = txdTaskName.Text,
                Status = (DevTaskStatus)cmbXStatus.SelectedValue.ToString().ToInteger(),
                Id = panelx.GetEditedItemId().Value
            };
            DBController.DbAuth.Update(d);
            panelx.Hide();
            AlertSuccess();
            RefreshView();
        }
    }
}