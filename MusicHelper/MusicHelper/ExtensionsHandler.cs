using SynnCore.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicHelper
{
    public static class ExtensionsHandler
    {
        public static void ApplyTheme(this Form frm)
        {
            ApplyThemeInternal(frm);
        }

        private static void ApplyThemeInternal(Control ctr)
        {
            if (ctr is Form)
                ApplyFormStyle(ctr as Form);

            if (ctr is Panel)
                ApplyPanelStyle(ctr as Panel);

            if (ctr is GroupBox)
                ApplyGroupBoxStyle(ctr as GroupBox);

            if (ctr is Button)
                ApplyButtonStyle(ctr as Button);

            if (ctr.Controls.Count > 0)
                foreach (Control ictr in ctr.Controls)
                    ApplyThemeInternal(ictr);
        }

        private static void ApplyButtonStyle(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
        }

        private static void ApplyGroupBoxStyle(GroupBox groupBox)
        {
            groupBox.BackColor = System.Drawing.Color.WhiteSmoke;
            groupBox.ForeColor = System.Drawing.Color.Black;
        }

        private static void ApplyPanelStyle(Panel panel)
        {
            panel.BackColor = System.Drawing.Color.DarkCyan;
        }

        private static void ApplyFormStyle(Form form)
        {
            form.BackColor = System.Drawing.Color.DarkGray;
        }

        public static IDatabaseProvider InitDataProvider(this Form frm)
        {
            IDatabaseProvider dbc = null;
            if (dbc == null)
            {
                string _connectionString = string.Empty;
#if DEBUG
                _connectionString = ConfigurationSettings.AppSettings["connectionString"];
#else
            _connectionString = ConfigurationSettings.AppSettings["prodConnectionString"];
#endif

                dbc = new SqlDatabaseProvider(new SynnSqlDataProvider(_connectionString));
            }
            return dbc;
        }
    }
}
