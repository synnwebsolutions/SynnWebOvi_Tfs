using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace WebSimplify
{
    public static class ModalHelper
    {
        public static int ZIndex { get { return 3; } }
        private static string EditedItemKey = "edx_d";
        public static int ModalBackgroundOpacity { get { return 80; } }

        public static void SetHeader(this System.Web.UI.WebControls.Panel c, string header)
        {
            HtmlGenericControl div = (HtmlGenericControl)c.FindControl("editorHaeder");
            if (div.NotNull())
                div.InnerText = header;
        }

        public static void SetMessage(this System.Web.UI.WebControls.Panel c, string message)
        {
            HtmlGenericControl div = (HtmlGenericControl)c.FindControl("editorbody");
            if (div.NotNull())
                div.InnerText = message;
        }

        public static void Show(this System.Web.UI.WebControls.Panel c)
        {
            c.Visible = true;
        }

        public static void Hide(this System.Web.UI.WebControls.Panel c)
        {
            c.Visible = false;
        }

        public static void SetEditedItemId(this System.Web.UI.WebControls.Panel c, int editedItemId)
        {
           (c.Page as SynnWebFormBase).StoreInSession(EditedItemKey, editedItemId);
        }

        public static void AddModalToPage(this System.Web.UI.WebControls.Panel c)
        {
            // modal background creation
            HtmlGenericControl div = new HtmlGenericControl(HtmlTextWriterTag.Div.ToString());
            div.Attributes.Add("class", "modalbackground");
            div.Style.Add("z-index", ModalHelper.ZIndex.ToString()); // HtmlTextWriterStyle.ZIndex rendered without hypen
            div.Style.Add("opacity", string.Format(".{0}", ModalHelper.ModalBackgroundOpacity / 10));
            div.Style.Add(HtmlTextWriterStyle.Filter.ToString(), string.Format("alpha(opacity={0})", ModalHelper.ModalBackgroundOpacity));
            div.ID = string.Format("{0}_modal", c.ID);

            c.Page.Form.Style.Add(HtmlTextWriterStyle.Height.ToString(), "100%");
            c.Page.Form.Controls.Add(div);
        }
        public static int? GetEditedItemId(this System.Web.UI.WebControls.Panel c)
        {
            var ed = (c.Page as SynnWebFormBase).GetFromSession(EditedItemKey);
            if (ed.NotNull())
                return ed.ToString().ToInteger();

            return (int?)null;
        }
    }
}