using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicHelper
{
    public static class GridExtensions
    {
        public static void HandleDataSource(this DataGridView gv, IList ds)
        {
            gv.Rows.Clear();
            if (ds.Count > 0)
            {
                FillHeaders(gv, ds[0]);
            }
        }

        private static void FillHeaders(DataGridView gv, object v)
        {
            var props = v.GetType().GetProperties();
            
            foreach (var prop in props)
            {
                var catr = prop.GetCustomAttributes(typeof(IGridInfoAttribute), true).FirstOrDefault();
                if (catr != null)
                {

                }
            }
        }
    }

    public class IGridInfoAttribute : Attribute
    {
        public IGridInfoAttribute(string header)
        {
            this.Header = header;
        }

        public string Header { get;  set; }
    }
}
