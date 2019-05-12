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
    public partial class LoginForm : Form
    {
        public IDatabaseProvider DbController { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            this.ApplyTheme();
            DbController = this.InitDataProvider();
            CenterToScreen();

        }
        string entUs = "Enter User Name...";
        string entPs = "Enter Password...";
        private void LoginForm_Load(object sender, EventArgs e)
        {
            txUsername.Text = entUs;
            txPassword.Text = entPs;

            txPassword.TextChanged += TxPassword_TextChanged;
            txUsername.TextChanged += TxPassword_TextChanged;
        }

        private void TxPassword_TextChanged(object sender, EventArgs e)
        {
            var tx = (sender as TextBox);
            if (string.IsNullOrWhiteSpace(tx.Text))
            {
                if (tx.Name == txUsername.Name)
                    tx.Text = entUs;
                else
                    tx.Text = entPs;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
#if DEBUG
            if(txUsername.Text == entUs && txPassword.Text == entPs)
            {
                txUsername.Text = "smach";
                txPassword.Text = "1x2w3e4z";
            }
#endif
            if (txUsername.NotEmpty() && txPassword.NotEmpty())
            {
                var u = new LoggedUser { Password = txPassword.Text, UserName = txUsername.Text };
                if (DbController.ValidateUser(ref u))
                {
                    GlobalAppData.SetUser(u);
                    using (var frm = new Form1())
                    {
                        this.Hide();
                        frm.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid User Name Or Password");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
