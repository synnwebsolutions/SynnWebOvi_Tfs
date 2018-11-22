using SynnCore.Generics;
using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebSimplify.Controls
{
    public partial class WsEditor : System.Web.UI.UserControl
    {

        public int ZIndex { get { return 3; } }
        public int ModalBackgroundOpacity { get { return 80; } }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.PreRender += new EventHandler(PagePreRender);
            //Page.Init += new EventHandler(PageInit);
        }

        void PagePreRender(object sender, EventArgs e)
        {
            RenderModalBackground();
        }
        //void PageInit(object sender, EventArgs e)
        //{
        //    Page.Form.Controls.Add(this);
        //}

        private void RenderModalBackground()
        {
            if (this.Visible)
            {
                this.Attributes.CssStyle.Add("z-index", (ZIndex + 1).ToString());

                // modal background creation
                HtmlGenericControl div = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
                div.Attributes.Add("class", "modalbackground");
                div.Style.Add("z-index", ZIndex.ToString()); // HtmlTextWriterStyle.ZIndex rendered without hypen
                div.Style.Add("opacity", string.Format(".{0}", ModalBackgroundOpacity / 10));
                div.Style.Add(HtmlTextWriterStyle.Filter.ToString(), string.Format("alpha(opacity={0})", ModalBackgroundOpacity));
                div.ID = string.Format("{0}_modal", this.ID);

                Page.Form.Style.Add(HtmlTextWriterStyle.Height.ToString(), "100%");
                Page.Form.Controls.Add(div);
            }
        }


        public SynnWebFormBase IPage
        {
            get
            {
                return (SynnWebFormBase)Page;
            }
        }

    }

    public class XEditorAttribute : Attribute
    {
        public XEditorAttribute()
        {

        }

        public string Display { get; set; }
        public Type EnumType { get; set; }
        public XEditorControlType XControlType { get; set; }
    }

    public enum XEditorControlType
    {
        Text,
        TextArea,
        Enum,
        Drop,
        Check,
        Date
    }

}