using SynnWebOvi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSimplify.Controls
{
    public partial class WsSlider : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MethodInfo m = IPage.GetType().GetMethod(GetSourceMethodName, new Type[0]);
                DataSource = (List<ListItem>)m.Invoke(Page, null);
                var i = DataSource.FirstOrDefault(x => x.Selected);
                if (i.NotNull())
                {
                    currentIndex = DataSource.IndexOf(i);

                }
                else
                    throw new SliderException();
                SetDisplay();
            }
        }

        private void SetDisplay()
        {
            if(currentIndex.Value < DataSource.Count)
                txDisplay.Text = DataSource[currentIndex.Value].Text;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        public SynnWebFormBase IPage
        {
            get
            {
                return (SynnWebFormBase)Page;
            }
        }


        public int? currentIndex
        {
            get
            {
                return (int?)IPage.GetFromSession("idx_*");
            }
            set
            {
                IPage.StoreInSession("idx_*", value);
            }
        }

        public List<ListItem> DataSource
        {
            get
            {
                return (List<ListItem>)IPage.GetFromSession("ds_*");
            }
            set
            {
                IPage.StoreInSession("ds_*", value);
            }
        }
        public bool UseSelectItem { get; set; }
        public bool UsePrevious { get; set; }
        public bool UseNext { get; set; }
        public string GetSourceMethodName { get; set; }

        public delegate void SelectionChanged(ListItem newSelection);
        public SelectionChanged SelectedChanged
        {
            get
            {
                return (SelectionChanged)IPage.GetFromSession("dls_*");
            }
            set
            {
                IPage.StoreInSession("dls_*", value);
            }
        }
        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            SetIndex(1);
            SetDisplay();
        }

        private void SetIndex(int v)
        {
            var newIdx = currentIndex + v;
            try
            {
                var nIt = DataSource.ElementAt(newIdx.Value);
                if (nIt.NotNull())
                {
                    currentIndex = currentIndex + v;
                    SelectedChanged.Invoke(nIt);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnPrev_Click(object sender, ImageClickEventArgs e)
        {
            SetIndex(-1);
            SetDisplay();
        }
    }

    public class SliderException : Exception
    {
        public override string Message
        {
            get
            {
                return "Slider - No Active Item.";
            }
        }
    }
}