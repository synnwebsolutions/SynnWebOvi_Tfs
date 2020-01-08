using SynnWebOvi;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public IEnumerable GetGenericItems()
        {
            if (EditedData.NotEmpty())
            {
                var tp = GTypes.First(x => x.Name == EditedData);
                return DBController.DbGenericData.GetGenericData(new GenericDataSearchParameters { FromType  = tp });
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

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var g = (GenericData)e.Row.DataItem;
              
                var props = g.GetType().GetProperties();
                foreach (var pinfo in props)
                {
                 
                    var genericDataField = ((GenericDataFieldAttribute[])pinfo.GetCustomAttributes(typeof(GenericDataFieldAttribute), true)).FirstOrDefault();
                    if (genericDataField != null && !genericDataField.DisableGridEdit)
                    {
                        var val = pinfo.GetValue(g);
                        var tx = new TextBox();
                        tx.ID = $"{g.Id}-{genericDataField.FieldName}";
                        tx.Text = val?.ToString();
                        e.Row.Cells[GetColumnIndexByName(gv, genericDataField.FieldName)].Controls.Add(tx);
                    }
                }
            }
        }

        protected void gv_DataBinding(object sender, EventArgs e)
        {
            var ctrs = gv.Controls;
            gv.Columns.Clear();
            if (EditedData.NotEmpty())
            {
          
                var tp = GTypes.First(x => x.Name == EditedData);
                var props = tp.GetProperties();
                foreach (var pinfo in props)
                {
                    var genericDataField = ((GenericDataFieldAttribute[])pinfo.GetCustomAttributes(typeof(GenericDataFieldAttribute), true)).FirstOrDefault();
                    if (genericDataField != null && !genericDataField.DisableGridEdit)
                    {
                        TemplateField col = new TemplateField();
                        col.HeaderText = genericDataField.FieldName;
                        gv.Columns.Add(col);
                    }
                }
            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            if (EditedData.NotNull())
            {
                foreach (GridViewRow row in gv.Rows)
                {
                    var g = row.DataItem;
                    var id = (g as GenericData).Id;
                }
            }
        }

        protected void gv_DataBound(object sender, EventArgs e)
        {
            btnUpdate.Visible = gv.Rows.Count > 0;
        }
    }
}