using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmusicCore;

namespace XTempoPlayer
{
    public partial class XSongItem : UserControl
    {
        public XSongItem()
        {
            InitializeComponent();
        }

        private void XSongItem_Load(object sender, EventArgs e)
        {

        }

        public Image ProfilePic
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }

        internal void Init(MusicItem musicItem)
        {
            labelName.Text = musicItem.Artist;
            labelStatus.Text = musicItem.Title;

            //TagLib.IPicture pic = f.Tag.Pictures[0];
            //MemoryStream ms = new MemoryStream(pic.Data.Data);
            //ms.Seek(0, SeekOrigin.Begin);

            //// ImageSource for System.Windows.Controls.Image
            //BitmapImage bitmap = new BitmapImage();
            //bitmap.BeginInit();
            //bitmap.StreamSource = ms;
            //bitmap.EndInit();

            //// Create a System.Windows.Controls.Image control
            //System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            //ProfilePic.Source = bitmap;
        }
    }
}
