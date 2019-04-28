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

            var p = @"D:\SmachData\2Pac - Letter to my unborn child.mp3";
            var fi = TagLib.File.Create(p);
            MusicItem i = new MusicItem(fi);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
