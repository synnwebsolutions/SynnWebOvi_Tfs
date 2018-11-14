<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="MoneyBalance.aspx.cs" Inherits="WebSimplify.MoneyBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="spageheader">מעקב הוצאות\הכנסות</div>


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
                <asp:GridView ID="gvCurrentMonthOpenTrans" runat="server"
                    OnRowDataBound="gvCurrentMonthOpenTrans_RowDataBound" AllowPaging="true"
                    CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="6" OnPageIndexChanging="gvCurrentMonthOpenTrans_PageIndexChanging"
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
                        <asp:TemplateField HeaderText="  שם ההוצאה">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTemplateName"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="סכום לעדכון" AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txCurrentTotal" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" עדכן סכום">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnAction" OnCommand="btnAction_Command" CssClass="gridbutton" ImageUrl="Img/exchange.png" AlternateText="עדכן"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>


        </div>
        <div class="col-6">
        </div>

    </div>


</asp:Content>
