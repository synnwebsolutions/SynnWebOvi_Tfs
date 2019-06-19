using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XmusicCore
{ 
    public static class ThemeHelper
    {
        private static UserTheme CurrentTheme = GlobalAppData.UserTheme;
        public static void ApplyTheme(this Control frm)
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

            if (ctr is SynnMPlayer)
                ApplyMusicPlayerStyle(ctr as SynnMPlayer);

            if (ctr is CheckedListBox)
                ApplyCheckedListBoxStyle(ctr as CheckedListBox);

            HandleRole(ctr);

            if (ctr.Controls.Count > 0)
                foreach (Control ictr in ctr.Controls)
                    ApplyThemeInternal(ictr);
        }

        private static void ApplyMusicPlayerStyle(SynnMPlayer synnMPlayer)
        {
            
        }

        private static void ApplyCheckedListBoxStyle(CheckedListBox c)
        {
            //c.BackColor = CurrentTheme.CheckedListBoxBackColor; //Color.OrangeRed;
            c.SelectionMode = SelectionMode.One;
        }

        private static void ApplyButtonStyle(Button btnExam)
        {
            btnExam.FlatStyle = FlatStyle.Flat;
            if (btnExam.BackColor != Color.Transparent)
                btnExam.FlatAppearance.BorderColor = btnExam.BackColor;
            else
                btnExam.FlatAppearance.BorderColor = Color.White;

            btnExam.SetBounds(btnExam.Location.X, btnExam.Location.Y, btnExam.Width, btnExam.Height);

            if (GlobalAppData.UserTheme.UseShapedButtons)
            {
                // Make the GraphicsPath.
                GraphicsPath polygon_path = new GraphicsPath(FillMode.Winding);
                polygon_path.AddPolygon(GetPoints(btnExam.ClientRectangle));

                // Convert the GraphicsPath into a Region.
                Region polygon_region = new Region(polygon_path);

                // Constrain the button to the region.
                btnExam.Region = polygon_region;
                btnExam.SizeChanged += BtnExam_SizeChanged;
            }

        }

        private static void HandleRole(Control ctr)
        {
            if (ctr.Tag != null)
            {
                var ct = ctr.Tag.ToString();

                if (ct == ThemeTag.Exit)
                {
                    ctr.BackColor = CurrentTheme.Exit;
                    //ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.Grid)
                {
                    ctr.BackColor = CurrentTheme.Grid;
                    //ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.MainBg)
                {
                    ctr.BackColor = CurrentTheme.MainBg;
                    //ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.MediaPlayer)
                {
                    ctr.BackColor = CurrentTheme.MediaPlayer;
                    //ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.Search)
                {
                    ctr.BackColor = CurrentTheme.Search;
                    if(ctr is Button)
                        ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.SearchClear)
                {
                    ctr.BackColor = CurrentTheme.SearchClear;
                    //ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.SearchDo)
                {
                    ctr.BackColor = CurrentTheme.SearchDo;
                    //ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.SideBar)
                {
                    ctr.BackColor = CurrentTheme.SideBar;
                    //ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.SyncAll)
                {
                    ctr.BackColor = CurrentTheme.SyncAll;
                    //ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.Youtube)
                {
                    ctr.BackColor = CurrentTheme.YouTube;
                    //ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.USB)
                {
                    ctr.BackColor = CurrentTheme.USB;
                    //ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.Playlist)
                {
                    ctr.BackColor = CurrentTheme.Playlist;
                    //ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.MusicPlayerAction)
                {
                    ctr.BackColor = CurrentTheme.MusicPlayerAction;
                    //ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.MusicPlayerMain)
                {
                    ctr.BackColor = CurrentTheme.MusicPlayerMain;
                    //ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.YoutubeContainer)
                {
                    ctr.BackColor = CurrentTheme.YoutubeContainer;
                    ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.MainBgLogin)
                {
                    ctr.BackColor = CurrentTheme.MainBgLogin;
                    //ctr.ForeColor = Color.White;
                }
                if (ct == ThemeTag.LoginAction)
                {
                    ctr.BackColor = CurrentTheme.LoginAction;
                    ctr.ForeColor = Color.White;
                }
                if (ct == "login")
                {
                    //if (ctr is Button)
                    //{
                    //    var btn = ctr as Button;
                    //    btn.Paint += Btn_Paint;
                    //}
                }
            }
            else
            {
                //ctr.BackColor = CurrentTheme.DefaultColor;
                //ctr.ForeColor = Color.WhiteSmoke;
            }
        }

        private static void Btn_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;

            ControlPaint.DrawBorder(e.Graphics, btn.ClientRectangle,
                                    Color.Red, 1, ButtonBorderStyle.Solid,
                                    Color.Red, 1, ButtonBorderStyle.Solid,
                                    Color.Red, 1, ButtonBorderStyle.Solid,
                                    Color.Red, 1, ButtonBorderStyle.Solid
                                    );
        }

        private static void BtnExam_SizeChanged(object sender, EventArgs e)
        {
            UpdateRegion(sender as Button);
        }

        private static void UpdateRegion(Button btnExam)
        {
            GraphicsPath polygon_path = new GraphicsPath(FillMode.Winding);
            polygon_path.AddPolygon(GetPoints(btnExam.ClientRectangle));
            btnExam.Region = new Region(polygon_path);
  
        }

        public static Point[] GetPoints(Rectangle container)
        {
            Point[] points = new Point[6];
            int half = container.Height / 2;
            int quart = container.Width / 10;
            points[0] = new Point(container.Left + quart, container.Top); // top left
            points[1] = new Point(container.Right - quart, container.Top); // right top
            points[2] = new Point(container.Right, container.Top + half); // right middle
            points[3] = new Point(container.Right - quart, container.Bottom); // right bottom
            points[4] = new Point(container.Left + quart, container.Bottom); // bottom left
            points[5] = new Point(container.Left, container.Top + half); // left middle
            return points;
        }

        private static void ApplyGroupBoxStyle(GroupBox groupBox)
        {
            //groupBox.BackColor = System.Drawing.Color.FromArgb(225, 85, 10);
            groupBox.ForeColor = System.Drawing.Color.WhiteSmoke;
        }

        private static void ApplyPanelStyle(Panel panel)
        {
            //panel.BackColor = System.Drawing.Color.OrangeRed;
        }

        private static void ApplyFormStyle(Form form)
        {
            //form.BackColor = System.Drawing.Color.Black;
        }

    }

}
