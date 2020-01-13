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
    public partial class HealthCarePage : SynnWebFormBase
    {
        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.HealthCare };
                return l;
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gvDiaryItems.ID)
                return "GetItems";
            return base.GetGridSourceMethodName(gridId);
        }

        public IEnumerable GetItems()
        {
            if (CurrentUser.IsAdmin) return new List<HealthLog>();

            var dbData = DBController.DbGenericData.GetGenericData< HealthLog>(new GenericDataSearchParameters { });
            return dbData.OrderByDescending(x => x.IDate).ToList();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnadddiary.Disabled = CurrentUser.IsAdmin;
                SetToggleTitle();
                AddSelectItemForCombo(cmpParents);

                var ul = DBController.DbGenericData.GetGenericData<Parent>(new GenericDataSearchParameters { });
                foreach (var u in ul)
                    cmpParents.Items.Add(new ListItem { Text = u.ParentName, Value = u.Id.ToString() });
                RefreshGrid(gvDiaryItems);
            }
        }

        protected void btnToggleState_ServerClick(object sender, EventArgs e)
        {
            ChangeView();
        }

        private void ChangeView()
        {
            dvInsert.Visible = !dvInsert.Visible;
            dvDisplay.Visible = !dvDisplay.Visible;
            SetToggleTitle();
        }

        private void SetToggleTitle()
        {
            btnToggleState.InnerText = !dvInsert.Visible ? "הוספה" : "צפייה";
        }

        protected void btnadddiary_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInputs(txadddiaryname, txadddiarydesc, txadddiarydate) && cmpParents.SelectedIndex > 0)
            {
                var c = new HealthLog
                {
                    EscorterId = CurrentUser.Id,
                    Title = txadddiaryname.Value,
                    Description = txadddiarydesc.Value,
                    IDate = Convert.ToDateTime(txadddiarydate.Value),
                    ParentId = cmpParents.SelectedValue.ToInteger()
                };
                DBController.DbGenericData.Add(c);
                AlertMessage("פעולה זו בוצעה בהצלחה");
                ClearInputs(txadddiaryname, txadddiarydesc, txadddiarydate);
                RefreshGrid(gvDiaryItems);
            }
            else
            {
                AlertMessage("אחד או יותר מהשדות ריקים");
            }
        }

        protected void gvDiaryItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var memoItem = (HealthLog)e.Row.DataItem;
                LoggedUser u = DBController.DbAuth.GetUser(memoItem.EscorterId);

                ((Label)e.Row.FindControl("lblDate")).Text = memoItem.IDate.ToString();
                ((Label)e.Row.FindControl("lblMessage")).Text = memoItem.Title;
                ((Label)e.Row.FindControl("lblId")).Text = memoItem.Description.ToString();
                ((Label)e.Row.FindControl("lblUser")).Text = u.DisplayName;
            }
        }
    }
}