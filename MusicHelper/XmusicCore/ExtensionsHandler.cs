using SynnCore.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XmusicCore
{
    public static class ExtensionsHandler
    {
        public static string GetConnectionString(this object frm)
        {
            return GetConnectionString();
        }

        private static string GetConnectionString()
        {
#if DEBUG
            return GlobalAppData.Configs.TestConnectionString;
#else
           return GlobalAppData.Configs.ProdConnectionString;
#endif
        }

        public static IDatabaseProvider InitDataProvider(this object frm)
        {
            IDatabaseProvider dbc = null;
            if (dbc == null)
            {
                string _connectionString = GetConnectionString();
                dbc = new SqlDatabaseProvider(new SynnSqlDataProvider(_connectionString));
            }
            return dbc;
        }


        public static void RefreshGrid(this DataGridView Grid, IList ds) 
        {
            Grid.DataSource = null;

            if (ds != null && ds.Count > 0)
            {
                Grid.RowPostPaint += Grid_RowPostPaint;
                Grid.DataSource = ds;
                Grid.Dock = DockStyle.Fill;
                Grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                Grid.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
                Grid.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
                Grid.RowHeadersVisible = false;
                Grid.RightToLeft = RightToLeft.Yes;
                CustomizeGridAfterDataBound(Grid, ds[0].GetType());
                //if (GridRefreshed != null)
                //    GridRefreshed(this, new XGridRefreshedEventArgs(ds));

            }
        }

        private static void Grid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            (sender as DataGridView).Rows[e.RowIndex].Cells[rowIdxCol].Value = (e.RowIndex + 1).ToString();
        }

        private static void CustomizeGridAfterDataBound(DataGridView Grid, Type GridDataSourceType)
        {
            PropertyInfo[] props = GridDataSourceType.GetProperties();
            var ginfos = new List<GridInfoAttribute>();
            foreach (PropertyInfo p in props)
            {
                var gInfo = (GridInfoAttribute)Attribute.GetCustomAttributes(p, typeof(GridInfoAttribute), false).FirstOrDefault();
                if (gInfo != null)
                    ginfos.Add(gInfo);
                else
                    HideColumn(Grid,p.Name);
                //HandleColumn(gInfo, p.Name);
            }
            foreach (var g in ginfos.OrderBy(x => x.DisplayIndex).ToList())
            {
                HandleColumn(Grid, g, g.PropertName);
            }
            AddIndexColumn(Grid);
            //foreach (DataGridViewButtonColumn extraButton in GetExtraButtons())
            //{
            //    if (Grid.Columns[extraButton.Name] == null)
            //        Grid.Columns.Add(extraButton);
            //}
        }

        private static void AddIndexColumn(DataGridView grid)
        {
            if (!grid.Columns.OfType<DataGridViewColumn>().Any(x => x.Name == rowIdxCol))
            {
                DataGridViewColumn column = new DataGridViewTextBoxColumn();
                column.ValueType = typeof(string);
                column.Name = rowIdxCol;
                //column.DataPropertyName = gInfo.PropertName;
                column.HeaderText = rowIdxCol;
                column.ReadOnly = true;
                column.Visible = true;
                column.Width = 10;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                grid.Columns.Add(column);
            }
        }

        public static bool NotEmpty(this IList lst)
        {
            return lst != null && lst.Count > 0;
        }

        private static string rowIdxCol = "#";
        private static void HandleColumn(DataGridView Grid, GridInfoAttribute gInfo, string defaultName)
        {
            DataGridViewColumn column = Grid.Columns[defaultName];
            if (column != null)
                Grid.Columns.Remove(column);
            if (gInfo != null)
            {
                if (gInfo.IsHyperLink.HasValue && gInfo.IsHyperLink.Value)
                {
                    column = new DataGridViewLinkColumn
                    {
                        UseColumnTextForLinkValue = false,
                        ActiveLinkColor = Color.White,
                        LinkBehavior = LinkBehavior.SystemDefault,
                        LinkColor = Color.Blue,
                        VisitedLinkColor = Color.YellowGreen,
                        ValueType = gInfo.ValueType,
                        Name = gInfo.ColumnName,
                        DataPropertyName = gInfo.PropertName,
                        HeaderText = gInfo.ColumnName,
                        ReadOnly = gInfo.Readonly,
                        Visible = gInfo.Visible
                    };
                }
                else
                {
                    Type valueType = gInfo.ValueType;
                    if (valueType.IsEnum)
                    {
                        column = new DataGridViewComboBoxColumn();
                        ((DataGridViewComboBoxColumn)column).DataSource = Enum.GetValues(valueType);
                    }
                    else if (valueType.Equals(typeof(bool)))
                        column = new DataGridViewCheckBoxColumn();
                    else
                    {
                        column = new DataGridViewTextBoxColumn();
                        if (gInfo.RightToLeft)
                        {
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                        }
                    }

                    column.ValueType = valueType;
                    column.Name = gInfo.ColumnName;
                    column.DataPropertyName = gInfo.PropertName;
                    column.HeaderText = gInfo.ColumnName;
                    column.ReadOnly = gInfo.Readonly;
                    column.Visible = gInfo.Visible;
                }
            }
            else
            {
                column = new DataGridViewTextBoxColumn();
                column.Visible = false;
                column.Name = defaultName;
                column.DataPropertyName = defaultName;
            }
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Grid.Columns.Add(column);
        }

        private static void HideColumn(DataGridView Grid, string defaultName)
        {
            DataGridViewColumn column = Grid.Columns[defaultName];
            Grid.Columns.Remove(column);
            column = new DataGridViewTextBoxColumn();
            column.Visible = false;
            column.Name = defaultName;
            column.DataPropertyName = defaultName;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid.Columns.Add(column);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class GridInfoAttribute : Attribute
    {
        internal Type ValueType;
        public bool Visible { get; set; }
        public bool RightToLeft { get; set; }
        public string ColumnName { get; internal set; }
        public string PropertName { get; internal set; }

        public bool Readonly { get; internal set; }

        public bool? IsHyperLink { get; internal set; }
        public string DisplayText { get; internal set; }

        public int DisplayIndex { get; set; }

        public GridInfoAttribute(int displayIndex, string propertName, string columnName, bool isReadonly = true, bool visible = false, bool rightToLeft = false, Type valueType = null)
        {
            PropertName = propertName;
            ColumnName = columnName;
            Readonly = isReadonly;
            Visible = visible;
            ValueType = valueType ?? typeof(string);
            RightToLeft = rightToLeft;
            DisplayIndex = displayIndex;
        }

        public GridInfoAttribute(bool islink, string propertName, string columnName, string diasplayText)
        {
            IsHyperLink = islink;
            PropertName = propertName;
            ColumnName = columnName;
            Visible = true;
            DisplayText = diasplayText;
        }
    }



}
