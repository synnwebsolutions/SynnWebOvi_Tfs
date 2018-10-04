using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace SynnCore.Forms
{
    public class FormHelper
    {
        public static void SetGridDataSource(IEnumerable lst, DataGridView dgv)
        {

            if (lst != null && (lst as IList).Count > 0)
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.DataSource = lst;
                object o = (lst as IList)[0];
                var props = o.GetType().GetProperties();
                foreach (PropertyInfo pi in props)
                {
                    string pName = pi.Name;
                    var attr = (GridFieldAttribute)pi.GetCustomAttributes(typeof(GridFieldAttribute)).ToList().FirstOrDefault();
                    if (attr != null)
                    {
                        var column = dgv.Columns[pi.Name];
                        column.Visible = true;
                        column.HeaderText = attr.DisplayName;
                        column.DisplayIndex = attr.Index;
                        SetColumnGenericStyle(column);
                        if (!string.IsNullOrEmpty(attr.DisplayFormat))
                        {
                            column.DefaultCellStyle.Format = attr.DisplayFormat;
                        }
                    }
                    else
                    {
                        dgv.Columns[pi.Name].Visible = false;
                    }
                }
            }
            SetGridStyle(dgv);
        }

        private static void SetGridStyle(DataGridView dgv)
        {
            DataGridViewCellStyle headerstyle = dgv.ColumnHeadersDefaultCellStyle;

            headerstyle.Font = new Font(FontFamily.Families[10], 11.8F, FontStyle.Bold);
            headerstyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            headerstyle.BackColor = Color.Black;
            headerstyle.ForeColor = Color.Orange;

            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;

            dgv.DefaultCellStyle.SelectionBackColor = Color.Gold;
        }

        private static void SetColumnGenericStyle(DataGridViewColumn column)
        {
            var rowStyle = column.DefaultCellStyle;

            rowStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            rowStyle.Font = new Font(FontFamily.GenericSansSerif, 10.0F);
            rowStyle.BackColor = Color.Black;
            rowStyle.ForeColor = Color.WhiteSmoke;
        }

    }

    public class GridFieldAttribute : Attribute
    {

        public GridFieldAttribute(string displayname, int index, bool editable = false, string displayFormat = null)
        {
            DisplayName = displayname;
            Editable = editable;
            Index = index;
            DisplayFormat = displayFormat;
        }

        public string DisplayName { get; private set; }
        public bool Editable { get; private set; }
        public int Index { get; private set; }
        public string DisplayFormat { get; set; }
    }


}
