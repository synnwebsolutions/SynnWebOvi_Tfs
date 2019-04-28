using HundredMilesSoftware.UltraID3Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicHelper
{
    public partial class Form1 : Form
    {
        IDatabaseProvider DbController;
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            this.ApplyTheme();
            DbController = this.InitDataProvider();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnScanFolder_Click(object sender, EventArgs e)
        {
            var dg = foldersToScanDialog.ShowDialog();
            if (dg == DialogResult.OK)
            {
                DirectoryHandler.Handle(DbController, foldersToScanDialog.SelectedPath);
            }
        }
    }
}
