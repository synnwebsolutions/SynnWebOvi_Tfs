using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicHelper
{
    public static class ThemeHelper
    {
        private static UserTheme CurrentTheme = GlobalAppData.UserTheme;
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

            if (ctr is SynnMPlayer)
                ApplyMusicPlayerStyle(ctr as SynnMPlayer);

            if (ctr is CheckedListBox)
                ApplyCheckedListBoxStyle(ctr as CheckedListBox);

            if (ctr.Controls.Count > 0)
                foreach (Control ictr in ctr.Controls)
                    ApplyThemeInternal(ictr);
        }

        private static void ApplyMusicPlayerStyle(SynnMPlayer synnMPlayer)
        {
            
        }

        private static void ApplyCheckedListBoxStyle(CheckedListBox c)
        {
            c.BackColor = CurrentTheme.CheckedListBoxBackColor; //Color.OrangeRed;
            c.SelectionMode = SelectionMode.One;
        }

        private static void ApplyButtonStyle(Button btnExam)
        {
            HandleRole(btnExam);
            btnExam.FlatStyle = FlatStyle.Flat;
            btnExam.FlatAppearance.BorderColor = btnExam.BackColor;
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
                if (ctr.Tag.ToString() == "ext")
                {
                    ctr.BackColor = Color.Black;
                    ctr.ForeColor = Color.White;
                }
                if (ctr.Tag.ToString() == "login")
                {
                    if (ctr is Button)
                    {
                        var btn = ctr as Button;
                        btn.Paint += Btn_Paint;
                    }
                }
            }
            else
            {
                ctr.BackColor = Color.DarkGray;
                ctr.ForeColor = Color.WhiteSmoke;
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
            groupBox.BackColor = System.Drawing.Color.FromArgb(225, 85, 10);
            groupBox.ForeColor = System.Drawing.Color.WhiteSmoke;
        }

        private static void ApplyPanelStyle(Panel panel)
        {
            panel.BackColor = System.Drawing.Color.OrangeRed;
        }

        private static void ApplyFormStyle(Form form)
        {
            form.BackColor = System.Drawing.Color.DarkGray;
        }

    }

    [Serializable]
    public class UserTheme
    {
        public Color CheckedListBoxBackColor { get;  set; }
        public bool UseShapedButtons { get;  set; }
    }
}
