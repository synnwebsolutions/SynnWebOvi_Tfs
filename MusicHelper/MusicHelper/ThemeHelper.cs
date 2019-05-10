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

        private static void ApplyButtonStyle(Button btnExam)
        {
            btnExam.FlatStyle = FlatStyle.Flat;
            btnExam.BackColor = Color.Azure;
            btnExam.FlatAppearance.BorderColor = btnExam.BackColor;
            btnExam.SetBounds(btnExam.Location.X, btnExam.Location.Y, btnExam.Width, btnExam.Height);

            // Make the GraphicsPath.
            GraphicsPath polygon_path = new GraphicsPath(FillMode.Winding);
            polygon_path.AddPolygon(GetPoints(btnExam.ClientRectangle));

            // Convert the GraphicsPath into a Region.
            Region polygon_region = new Region(polygon_path);

            // Constrain the button to the region.
            btnExam.Region = polygon_region;
            btnExam.SizeChanged += BtnExam_SizeChanged;
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

    }
}
