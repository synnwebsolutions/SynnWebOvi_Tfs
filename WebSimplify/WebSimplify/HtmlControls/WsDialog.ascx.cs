using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

[assembly: WebResource(@"WebSimplify.js.WsDialog.js", "application/x-javascript")]

namespace WebSimplify.HtmlControls
{
    public partial class WsDialog : System.Web.UI.UserControl
    {
        int _zIndex = 200;
        string _fadeBackgroundColor = "#333333";
        bool _useModalBackground = true;
        bool _renderPanelUnderBody = false;
        Control _originalParent;
        bool _useOriginalParentVisibility = false;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [Description("When true the panel will cause the rest of the page to be \"disabled\" and cuase modal effect"), Category("Appearance"), DefaultValue(true)]
        public bool UseModalBackground
        {
            get { return _useModalBackground; }
            set { _useModalBackground = value; }
        }
        protected override void OnPreRender(EventArgs e)
        {

            if (UseModalBackground)
            {
                Page.ClientScript.RegisterClientScriptResource(typeof(WsDialog), "WebSimplify.js.WsDialog.js");
            }
            if (this.Visible)
            {
                this.Attributes.CssStyle.Add("z-index", (ZIndex + 1).ToString());
                Page.ClientScript.RegisterClientScriptResource(typeof(WsDialog), "Bci.Html.Controls.Resource.ModalPanelDrag.js");
            }
            base.OnPreRender(e);
        }

        void PagePreRender(object sender, EventArgs e)
        {
            RenderModalBackground();
        }


        int _modalBackgroundOpacity = 40;

        public int ModalBackgroundOpacity
        {
            get { return _modalBackgroundOpacity; }
            set { _modalBackgroundOpacity = value; }
        }

        [Description("Zindex of the modal background, the panel itself will get + 1"), Category("Appearance"), DefaultValue(200)]
        public int ZIndex
        {
            get { return _zIndex; }
            set { _zIndex = value; }
        }
        protected virtual void RenderModalBackground()
        {
            if (!Visible)
                return;

            // modal background creation
            HtmlGenericControl div = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
            div.Style.Add(HtmlTextWriterStyle.Width.ToString(), "100%");
            div.Style.Add(HtmlTextWriterStyle.Height.ToString(), "100%");
            div.Style.Add(HtmlTextWriterStyle.Position.ToString(), "absolute");
            div.Style.Add(HtmlTextWriterStyle.Left.ToString(), "0px");
            div.Style.Add(HtmlTextWriterStyle.Top.ToString(), "0px");
            div.Style.Add("z-index", ZIndex.ToString()); 
            div.Style.Add("opacity", string.Format(".{0}", ModalBackgroundOpacity / 10));
            div.Style.Add(HtmlTextWriterStyle.Filter.ToString(), string.Format("alpha(opacity={0})", ModalBackgroundOpacity));
            div.Style.Add(HtmlTextWriterStyle.BackgroundColor, _fadeBackgroundColor);
            AddControlToBody(div);
            div.ID = GetModalBackgroundId();
            Page.ClientScript.RegisterStartupScript(typeof(WsDialog), ID, string.Format("disableTabControlsForModal('{0}');hideSelectBoxesForModal('{0}');", ClientID), true);
            // end modal creation
        }

        private void AddControlToBody(Control con)
        {
            Page.Form.Style.Add(HtmlTextWriterStyle.Height.ToString(), "100%");
            AddControlToBody(Page, con);
            AfterModalCreation();
        }

        protected virtual void AfterModalCreation()
        {

        }

        void PageInit(object sender, EventArgs e)
        {
            OriginalParent = this.Parent;
            AddControlToBody(this);
        }

        [Browsable(false)]
        public Control OriginalParent
        {
            get { return _originalParent; }
            set { _originalParent = value; }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (UseModalBackground)
                Page.PreRender += new EventHandler(PagePreRender);
            if (RenderPanelUnderBody)
                Page.Init += new EventHandler(PageInit);
        }


        [Description("Set to true when you want the dialog to be rendered under body (form) element, needed mainly when dialog is rendered under other div"), DefaultValue(false), Category("Appearance")]
        public bool RenderPanelUnderBody
        {
            get { return _renderPanelUnderBody; }
            set { _renderPanelUnderBody = value; }
        }
        public string GetModalBackgroundId()
        {
            return string.Format("{0}_modal", this.ID);
        }

        public static void AddControlToBody(Page page, Control c)
        {
            page.Form.Controls.Add(c);
        }
    }
}