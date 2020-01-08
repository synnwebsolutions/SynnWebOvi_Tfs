using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify
{
    public partial class GenericDataPage : SynnWebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGenericItems();
            }
        }

        private void FillGenericItems()
        {
            AddSelectItemForCombo(cmbTables);
            
            foreach (var gData in GTypes)
            {
                cmbTables.Items.Add(new System.Web.UI.WebControls.ListItem { Text = gData.Name, Value = gData.Name });
            }
        }

        public string EditedData
        {
            get
            {
                return GetFromSession("edt*g")?.ToString();
            }
            set
            {
                StoreInSession("edt*g", value);
            }
        }

        public List<Type> GTypes
        {
            get
            {
                var i = GetFromSession("gts")?.ToString();
                if (i == null)
                    GTypes = Assembly.GetAssembly(typeof(GenericData)).GetTypes().Where(t => t.IsSubclassOf(typeof(GenericData))).ToList();
                return  (List<Type>) GetFromSession("gts");
            }
            set
            {
                StoreInSession("gts", value);
            }
        }

        public int FieldsIndexes
        {
            get
            {
                var i = GetFromSession("xcfx");
                if (i == null)
                    FieldsIndexes = 0;
                return GetFromSession("xcfx").ToString().ToInteger();
            }
            set
            {
                StoreInSession("xcfx", value);
            }
        }

        protected override List<ClientPagePermissions> RequiredPermissions
        {
            get
            {
                var l = new List<ClientPagePermissions> { ClientPagePermissions.GenericDataItems };
                return l;
            }
        }

        internal override string GetGridSourceMethodName(string gridId)
        {
            if (gridId == gv.ID)
                return "GetGenericItems";
            return base.GetGridSourceMethodName(gridId);
        }

        internal override bool GetGridSourceIsDataTable(string gridId)
        {
            if (gridId == gv.ID)
                return true;
            return base.GetGridSourceIsDataTable(gridId);
        }
        public object GetGenericItems()
        {
            if (EditedData.NotEmpty())
            {
                var tp = GTypes.First(x => x.Name == EditedData);
                var data = (DBController.DbGenericData.GetGenericData(new GenericDataSearchParameters { FromType = tp }) as IList).ToDataTable();

                var att = GenericData.GetGenericDataFieldAttributes(null, tp, null);
                var props = data.Columns.OfType< DataColumn>().Select(x => x.ColumnName).ToList();
                foreach (var pinfo in props)
                {
                    var idx = data.Columns.IndexOf(pinfo);
                    var gaInfo = att.FirstOrDefault(x => x.PropertyName == pinfo);
                    if (gaInfo == null || gaInfo.DisableGridEdit)
                    {
                        data.Columns.RemoveAt(idx);
                    }
                    else
                    {
                        data.Columns[idx].ReadOnly = false;
                    }
                }
                return data;
            }
            return null;
        }

        protected void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTables.SelectedIndex > 0)
            {
                EditedData = cmbTables.SelectedValue;
            }
            else
            {
                EditedData = string.Empty;
            }
            RefreshGrid(gv);
        }

        protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv.EditIndex = e.NewEditIndex;
            RefreshGrid(gv);
        }

        protected void gv_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            var sx = sender.ToString();
        }

        protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CloseEdit();
        }

        protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gv.Rows[e.RowIndex];
            var g = row.DataItem;
            var Cols = row.Cells.Count;
            
            for (int i = 1; i <= Cols; i++)
            {
                try
                {
                    TextBox txtBox = row.Cells[i].Controls[0] as TextBox;
                    //var prop = e.GetPropertyInfo();
                    if (txtBox != null)
                    {
                        var newValue = txtBox.Text;
                    }
                }
                catch (Exception ex)
                {
                    var dd = ex.Message;
                }
            }
         

            CloseEdit();
        }

        private void CloseEdit()
        {
            gv.EditIndex = -1;
            RefreshGrid(gv);
        }
    }
}