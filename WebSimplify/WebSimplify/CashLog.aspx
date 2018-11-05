<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="CashLog.aspx.cs" Inherits="WebSimplify.CashLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="spageheader">מעקב מזומן</div>


    <div class="spanel">
        <div class="row">
            <div class=" col-4">
                <div class="sinputwlabel">
                    <label for="txp1">ממוצע לחיוב חודשי</label>
                    <asp:TextBox ID="txp1" runat="server" />
                </div>
            </div>
            <div class="col-4">
                <div class="sinputwlabel">
                    <label for="txp2">ממוצע לחיוב יומי</label>
                    <asp:TextBox ID="txp2" runat="server" />
                </div>
            </div>
            <div class=" col-4">
                <div class="sinputwlabel">
                    <label for="txp3">צפי לחודש נוכחי</label>
                    <asp:TextBox ID="txp3" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class=" col-6">
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gvCashMoneyItems" runat="server"
                    OnRowDataBound="gvCashMoneyItems_RowDataBound" AllowPaging="true"
                    CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="10" OnPageIndexChanging="gvCashMoneyItems_PageIndexChanging"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                            <asp:TemplateField HeaderText="תאריך " AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDate"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="סכום " AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txspent" CssClass="gridtextinput"></asp:TextBox>
                                <asp:Label runat="server" ID="lblspent"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="תיאור">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txIdesc" CssClass="gridtextinput"></asp:TextBox>
                                <asp:Label runat="server" ID="lblIdesc"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" הוספה ">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnAddCashItem" OnCommand="btnAddCashItem_Command" CssClass="gridbutton" ImageUrl="Img/add.png"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>


        </div>
        <div class="col-6">
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gvMonthsView" runat="server"
                    OnRowDataBound="gvMonthsView_RowDataBound" AllowPaging="true"
                    CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="10" OnPageIndexChanging="gvMonthsView_PageIndexChanging"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText=" חודש">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMonthName"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="סכום נוכחי" AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="txCurrentTotal" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" הוצאה יומית">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDaylyValue"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="צפי לסוף חודש">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMonthlyPredictions"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>


</asp:Content>
