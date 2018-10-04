using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynnCore.Forms
{
    public static class FormStyleHelper
    {
        private static FormStyleSettings XAppSettings;
        public static string ActionTab = "act";
        public static string SearchTab = "src";
        public static string DataContainerTab = "dt";
        public static string FullDataContainerTab = "fdt";
        public static string ExitButton = "btx";
        private static Dictionary<Type, Form> Forms = null;

        public static int ExitButtonHeight = 40;
        public static int ExitButtonWidth = 266;
        private static Font DefaultFont = new Font("Arial", 10, FontStyle.Bold | FontStyle.Italic);

        public static void Init(FormStyleSettings stylesettings)
        {
            XAppSettings = stylesettings;
        }

        public static void ApplyTheme(Form form)
        {
            //form.Size = GetFormSize();
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            if (XAppSettings == null)
            {
                throw new Exception("Form Style Settings not Initiated - Please Invoke Init Method !");
            }

            try
            { form.BackColor = XAppSettings.MainTheme; }
            catch
            { form.BackColor = Color.LightSteelBlue; }

            HandleControls(form.Controls, form);
            if (form is ICustomable)
                SetCustomableForm(form as ICustomable);

            if (!string.IsNullOrEmpty(XAppSettings.IconPath))
            {
                form.Icon = new Icon(XAppSettings.IconPath);
                form.ShowIcon = true;
            }
        }

        private static void SetCustomableForm(ICustomable customable)
        {
            customable.PerformCustomization();
        }


        private static void HandleControls(Control.ControlCollection controls, Form form = null)
        {
            if (controls != null)
            {
                foreach (Control ctr in controls)
                {
                    if (ctr.Tag != null && !string.IsNullOrEmpty(ctr.Tag.ToString()) && form != null)
                        HandleSizeAndLocation(ctr, form);
                    if (ctr is Button)
                    {
                        (ctr as Button).FlatStyle = FlatStyle.Flat;
                    }
                    if (ctr is TextBox)
                    {
                        (ctr as TextBox).RightToLeft = RightToLeft.Yes;
                    }
                    if (ctr is DataGridView)
                    {

                        GridStyleHelper.ApplyGridStyle(ctr as DataGridView, form);
                    }
                    if (XAppSettings.UseGradientColor)
                        AddGradient(ctr);
                    ctr.Font = DefaultFont;// new Font("Arial", 10, FontStyle.Bold | FontStyle.Italic);
                    HandleControls(ctr.Controls);
                }
            }
        }

        private static void AddGradient(Control ctr)
        {
            if (ctr is GroupBox || ctr is Panel)
            {
                ctr.Paint += Pn_Paint;
            }
        }
        private static void Pn_Paint(object sender, PaintEventArgs e)
        {
            Control pn = sender as Control;
            Point startPoint = new Point(0, 0);
            Point endPoint = new Point(pn.Width, pn.Height);

            LinearGradientBrush lgb = new LinearGradientBrush(startPoint, endPoint, XAppSettings.MainTheme, Color.Black);
            Graphics g = e.Graphics;
            g.FillRectangle(lgb, 0, 0, pn.Width, pn.Height);
        }



        private static void HandleSizeAndLocation(Control ctr, Form form)
        {

            int searchHeight = 60;

            if (ctr.Tag.ToString() == ActionTab)
            {
                int x = 2 * XAppSettings.DefaultDelimeter + form.Width / 2;
                int y = 2 * XAppSettings.DefaultDelimeter + searchHeight;
                ctr.Location = new Point(x, y);
            }
            if (ctr.Tag.ToString() == SearchTab)
            {
                ctr.Location = new Point(XAppSettings.DefaultDelimeter, XAppSettings.DefaultDelimeter);
            }
            if (ctr.Tag.ToString() == DataContainerTab)
            {
                int x = XAppSettings.DefaultDelimeter;
                int y = 2 * XAppSettings.DefaultDelimeter + searchHeight;
                ctr.Location = new Point(x, y);
                ctr.Width = form.Width / 2;
            }
            if (ctr.Tag.ToString() == FullDataContainerTab)
            {
                int x = XAppSettings.DefaultDelimeter;
                int y = XAppSettings.DefaultDelimeter;
                ctr.Location = new Point(x, y);
                ctr.Width = form.Width - 2 * XAppSettings.DefaultDelimeter;
                ctr.Height = form.Height - 3 * XAppSettings.DefaultDelimeter - ExitButtonHeight;
            }
            if (ctr.Tag.ToString() == ExitButton)
            {
                LocateExitButton(form, ctr as Button);
            }
        }
        public static void LocateExitButton(Form form, Button button)
        {
            int x = form.Width - XAppSettings.DefaultDelimeter - ExitButtonWidth;
            int y = form.Height - XAppSettings.DefaultDelimeter - ExitButtonHeight;
            button.Location = new Point(x, y);
        }
    }

    public class FormStyleSettings
    {
        public FormStyleSettings()
        {

        }

        public int DefaultDelimeter { get;  set; }
        public string IconPath { get;  set; }
        public Color MainTheme { get;  set; }
        public bool RandomColorChange { get;  set; }
        public bool UseGradientColor { get;  set; }
    }
}
