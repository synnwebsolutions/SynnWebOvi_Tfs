using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynnCore.Forms
{
    public static class GridStyleHelper
    {
        public static void ApplyGridStyle(DataGridView dgv, object sender = null)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (sender != null && sender is IGridCustom)
            {

            }
        }

        public static void ApplyGridStyle(DataGridView dgv, IGridCustom sender)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (sender != null && sender is IGridCustom)
            {
                List<GridColumnDataAttribute> data = new List<GridColumnDataAttribute>();

                MemberInfo[] myMembers = sender.GetColumnsInfoType(dgv.Name).GetMembers();

                for (int i = 0; i < myMembers.Length; i++)
                {
                    Object[] myAttributes = myMembers[i].GetCustomAttributes(typeof(GridColumnDataAttribute), true);
                    if (myAttributes.Length > 0)
                    {
                        data.Add(myAttributes[0] as GridColumnDataAttribute);
                    }
                }

                foreach (var item in data)
                {
                    var col = dgv.Columns[item.ColumnName];
                    col.Visible = item.Visible;
                    col.HeaderText = item.NewHaederText;
                    col.DisplayIndex = item.Index;
                }
            }
        }
    }

    public class GridColumnDataAttribute : Attribute
    {
        public string ColumnName { get; set; }
        public string NewHaederText { get; set; }
        public bool Visible { get; set; }
        public int Index { get;  set; }

        public GridColumnDataAttribute()
        {
            Visible = true;
        }
    }
}
