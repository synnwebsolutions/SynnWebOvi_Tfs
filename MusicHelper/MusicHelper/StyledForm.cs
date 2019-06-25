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
    public partial class StyledForm : Form
    {
        public StyledForm()
        {
            InitializeComponent();
        }

        private void StyledForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                flowLayoutPanel1.Controls.Add(new XItem { txtName = i.ToString(), txtStatus = $"{i} Added !" });
            }
        }
    }
}
