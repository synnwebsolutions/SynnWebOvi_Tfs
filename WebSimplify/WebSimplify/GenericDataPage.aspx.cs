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

        public DataTable GData
        {
            get
            {
                var i = GetFromSession("fddsf");
                if (i == null)
                    GData = new DataTable();
                return (DataTable)GetFromSession("fddsf");
            }
            set
            {
                StoreInSession("fddsf", value);
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
                var genericDataType = GTypes.First(x => x.Name == EditedData);
                GData = (DBController.DbGenericData.GetGenericData(new GenericDataSearchParameters { FromType = genericDataType }) as IList).ToDataTable();

                var att = GenericData.GetGenericDataFieldAttributes(null, genericDataType, null);
                var propertyInfos = GData.Columns.OfType< DataColumn>().Select(x => x.ColumnName).ToList();
                foreach (var propertyInfo in propertyInfos)
                {
                    var idx = GData.Columns.IndexOf(propertyInfo);
                    var genericFieldInfo = att.FirstOrDefault(x => x.PropertyName == propertyInfo);
                    if (genericFieldInfo == null || genericFieldInfo.DisableGridEdit)
                    {
                        GData.Columns.RemoveAt(idx);
                    }
                    else
                    {
                        var rowsToFormat = GData.Rows.Count;
                        var tmpObject = Activator.CreateInstance(genericDataType);
                        
                        for (int i = 0; i < rowsToFormat; i++)
                        {
                            var valueToFormat = GData.Rows[i][idx].ToString();
                            if (valueToFormat.NotEmpty())
                            {
                                var formatedVal = (tmpObject as GenericData).GetFormatedValue(genericDataType, valueToFormat, genericFieldInfo, DBController);
                                if (formatedVal.NotEmpty())
                                    GData.Rows[i][idx] = formatedVal;
                            }
                        }
                        GData.Columns[idx].ReadOnly = false;
                        try { GData.Columns[idx].ColumnName = genericFieldInfo.FieldName;  } catch (Exception) { }
                    }
                }
                return GData;
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

    }
}