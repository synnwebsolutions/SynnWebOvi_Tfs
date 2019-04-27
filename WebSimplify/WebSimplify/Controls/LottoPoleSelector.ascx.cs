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
    public partial class LottoPoleSelector : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string SaveDataMethodName { get; set; }
        public PoleSelectorMode SelectorMode { get; set; }
        public SynnWebFormBase IPage
        {
            get
            {
                return (SynnWebFormBase)Page;
            }
        }

        public List<int> IPole
        {
            get
            {
                if (IPage.GetFromSession("iDatsdsaf_*") == null)
                    IPage.StoreInSession("iDatsdsaf_*", new List<int>());
                return (List<int>)IPage.GetFromSession("iDatsdsaf_*");
            }
            set
            {
                IPage.StoreInSession("iDatsdsaf_*", value);
            }
        }

        public int SpecialNumber
        {
            get
            {
                if (IPage.GetFromSession("sfdf*") == null)
                    IPage.StoreInSession("sfdf*", 0);
                return (int)IPage.GetFromSession("sfdf*");
            }
            set
            {
                IPage.StoreInSession("sfdf*", value);
            }
        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            gvLottoPoleNumbers.Visible = SelectorMode == PoleSelectorMode.Grid;
            rSingleSelector.Visible = SelectorMode == PoleSelectorMode.SingleInput;
            FillGrid();
        }

        private void FillGrid()
        {
            if (SelectorMode == PoleSelectorMode.Grid)
            {
                var data = new List<LottoSelectorItem>();
                for (int i = 1; i <= 7; i++)
                    data.Add(new LottoSelectorItem(i));
                gvLottoPoleNumbers.DataSource = data;
                gvLottoPoleNumbers.DataBind();
            }
            else
            {

            }
        }

        protected void btnSp_Command(object sender, CommandEventArgs e)
        {
            HandleNumberSelection(sender as ImageButton, e.CommandArgument.ToString().ToInteger(), true);
        }
        const string activeSelectionClass = "ltactive";
        private void HandleNumberSelection(ImageButton sender, int num, bool specialNumber = false)
        {
            if (specialNumber)
            {
                if (SpecialNumber == num)// unselect
                    SpecialNumber = 0;
                else
                    SpecialNumber = num;
            }
            else
            {
                if (IPole.Contains(num))
                    IPole.Remove(num);
                else if (IPole.Count < 6)
                    IPole.Add(num);
            }
        }

        protected void btnI1_Command(object sender, CommandEventArgs e)
        {
            HandleNumberSelection(sender as ImageButton, e.CommandArgument.ToString().ToInteger());
        }

        protected void gvLottoPoleNumbers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var DummyItem = (LottoSelectorItem)e.Row.DataItem;
                var btnSp = ((ImageButton)e.Row.FindControl("btnSp"));
                SetButtonCommandArguments(DummyItem.RowIdentifier, btnSp,true);


                int btnIdx = 1;
                foreach (var num in DummyItem.GetNumbers())
                {
                    string btnKey = string.Format("btnI{0}", btnIdx);
                    var btnI = ((ImageButton)e.Row.FindControl(btnKey));
                    SetButtonCommandArguments(num, btnI);
                    btnIdx++;
                }
            }
        }

        private void SetButtonCommandArguments(int argumentKey, ImageButton btnI, bool isSpecialNumber = false)
        {
            if (argumentKey > 0)
            {
                if (isSpecialNumber && SpecialNumber == argumentKey)
                    btnI.CssClass = string.Format("{0} {1}", btnI.CssClass, activeSelectionClass);
                else if(IPole.Contains(argumentKey))
                    btnI.CssClass = string.Format("{0} {1}", btnI.CssClass, activeSelectionClass);

                btnI.AlternateText = argumentKey.ToString();
                btnI.CommandArgument = argumentKey.ToString();
            }
            else
                btnI.Visible = false;
        }

        public class LottoSelectorItem
        {
            public LottoSelectorItem(int rowIdentifier)
            {
                RowIdentifier = rowIdentifier;
                Data = new Dictionary<int, List<int>>();
                Data.Add(1, new List<int> {1,2,3,4,5,6 });
                Data.Add(2, new List<int> { 7,8,9,10,11,12});
                Data.Add(3, new List<int> {13,14,15,16,17,18 });
                Data.Add(4, new List<int> { 19,20,21,22,23,24 });
                Data.Add(5, new List<int> { 25,26,27,28,29,30});
                Data.Add(6, new List<int> {31,32,33,34,35,36 });
                Data.Add(7, new List<int> { 37,-1, -1, -1, -1, -1 });
            }

            public Dictionary<int, List<int>> Data { get; private set; }
            public int RowIdentifier { get; private set; }

            public List<int> GetNumbers()
            {
                return Data[RowIdentifier];
            }
        }
       

        protected void btnGenerate_Click1(object sender, EventArgs e)
        {
            if (SelectorMode == PoleSelectorMode.SingleInput && txSingleTextNums.Value.NotEmpty() && txSingleTextSpeciaslNum.Value.NotEmpty())
            {
                var inums = txSingleTextNums.Value.Split(' ').Where(x => x.Length > 0).ToList();
                if (inums.Count == 6)
                {
                    IPole = inums.Select(x => x.ToInteger()).ToList();
                    SpecialNumber = txSingleTextSpeciaslNum.Value.ToInteger();
                }
            }
            if (SpecialNumber.InRangeNoBorders(0,8) && IPole.Count == 6  && IPole.InRangeNoBorders(0, 38) && txPoleKey.Value.NotEmpty() && txPoleDate.Value.NotEmpty())
            {
                MethodInfo m = Page.GetType().GetMethod(SaveDataMethodName);
                IPole = IPole.OrderBy(x => x).ToList();
                LottoPole i = new LottoPole();
                i.PoleActionDate = txPoleDate.Value.ToDateTime();
                i.PoleKey = txPoleKey.Value;
                i.SpecialNumber = SpecialNumber;
                i.N1 = IPole[0];
                i.N2 = IPole[1];
                i.N3 = IPole[2];
                i.N4 = IPole[3];
                i.N5 = IPole[4];
                i.N5 = IPole[5];
                m.Invoke(Page, new object[] { i });
                btnClear_Click1(sender, null);
            }
            else
            {
                IPage.AlertMessage("אחד או יותר מהפרמטרים חסרים");
            }
        }

        protected void btnClear_Click1(object sender, EventArgs e)
        {
            txPoleDate.Value = string.Empty;
            txPoleKey.Value = string.Empty;
            txSingleTextNums.Value = txSingleTextSpeciaslNum.Value = string.Empty;
            SpecialNumber = 0;
            IPole = new List<int>();
        }
    }

    public enum PoleSelectorMode
    {
        Grid,
        SingleInput
    }
}