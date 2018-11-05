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
    public partial class QuickTasks : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshGrid(gv);
            }
        }
        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.QuickTasks };
                return l;
            }
        }

        protected void btnAddQuickTask_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInputs(txTaskname, txTaskDesc))
            {
                QuickTask t = new QuickTask
                {
                    Name = txTaskname.Value,
                    Description = txTaskDesc.Value,
                    Active = true,
                    CreationDate = DateTime.Now,
                    UserGroupId = CurrentUser.Id
                };
                DBController.DbCalendar.Add(t);
                AlertMessage("פעולה זו בוצעה בהצלחה");
                ClearInputs(txTaskname, txTaskDesc);
                RefreshGrid(gv);
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }
        protected override string NavIdentifier
        {
            get
            {
                return "navtask";
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetData";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetData()
        {
            List<QuickTask> items = DBController.DbCalendar.Get(new QuickTasksSearchParameters { SearchText = txwedsearchkey.Value, Active = chkActive.Checked });
            return items;
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var d = (QuickTask)e.Row.DataItem;
                ((Label)e.Row.FindControl("lblTaskName")).Text = d.Name;
                ((Label)e.Row.FindControl("lblTaskDesc")).Text = d.Description;
                ((ImageButton)e.Row.FindControl("btnClose")).CommandArgument = d.Id.ToString();
                ((ImageButton)e.Row.FindControl("btnClose")).Visible = d.Active;
            }
        }

        protected void btnClose_Command(object sender, CommandEventArgs e)
        {
            QuickTask item = DBController.DbCalendar.Get(new QuickTasksSearchParameters { Id =  Convert.ToInt32(e.CommandArgument) }).First();
            item.Active = false;
            DBController.DbCalendar.Update(item);
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            RefreshGrid(gv);
        }

        protected void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGrid(gv);
        }
    }
}