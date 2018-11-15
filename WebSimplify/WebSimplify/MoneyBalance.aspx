<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="MoneyBalance.aspx.cs" Inherits="WebSimplify.MoneyBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="spageheader">מעקב הוצאות\הכנסות</div>


    <div class="spanel">
        <div class="row">
            <div class=" col-4">
                <div class="sinputwlabel">
                    <label for="txp1">מאזן ממוצע  </label>
                    <asp:TextBox ID="txAvgBalance" runat="server" />
                </div>
            </div>
            <div class="col-4">
                <div class="sinputwlabel">
                    <label for="txp2">  מאזן אחרון</label>
                    <asp:TextBox ID="txLastBalance" runat="server" />
                </div>
            </div>
            <div class=" col-4">
                <div class="sinputwlabel">
                    <label for="txp3">יתרה לחודש נוכחי</label>
                    <asp:TextBox ID="txCurrentMonthBal" runat="server" />
                </div>
            </div>
        </div>
    </div>


    <div class="row">
        <div class=" col-6 spanel">
            <div class="spanelHeader">תנועות פתוחות</div>
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gvOpenTrans" runat="server"
                    OnRowDataBound="gvOpenTrans_RowDataBound" AllowPaging="true"
                    CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="6" OnPageIndexChanging="gvOpenTrans_PageIndexChanging"
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
                        <asp:TemplateField HeaderText=" סוג">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTranType" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="סכום לעדכון">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txCurrentTotal" CssClass="gridtextinput"></asp:TextBox>
                                <asp:Label runat="server" ID="lblAmount" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" סגור תנועה">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnCloseAction" OnCommand="btnCloseAction_Command" CssClass="gridbutton" ImageUrl="Img/locked.png" AlternateText="סגור"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="col-6 spanel">
            <div class="spanelHeader">מאזנים</div>
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gvMonthlyBalancesView" runat="server"
                    OnRowDataBound="gvMonthlyBalancesView_RowDataBound" AllowPaging="true"
                    CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="10" OnPageIndexChanging="gvMonthlyBalancesView_PageIndexChanging"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText=" חודש">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMonthName" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" הכנסות">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMontIncomes" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" הוצאות ">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMonthOut" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="  מאזן סופי">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblMonthBalance" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-2">
        </div>
        <div class=" col-8 spanel">
            <div class="spanelHeader"> הוספת תנועה קבועה</div>
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gvTemplates" runat="server"
                    OnRowDataBound="gvTemplates_RowDataBound" AllowPaging="true"
                    CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="6" OnPageIndexChanging="gvTemplates_PageIndexChanging"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText=" שם">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txTempName" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="סוג ">
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="cmbTemtType" CssClass="gridtextinput">
                                    <asp:ListItem Enabled="true" Text=" בחר סוג" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="זכות" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="חובה" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" סכום">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txAmount" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="תאריך התחלה ">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txTempFromDate" CssClass="gridtextinput" TextMode="Date"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="תאריך סיום ">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txTempToDate" CssClass="gridtextinput" TextMode="Date"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" אוטומטי">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkTempAuto" CssClass="gridtextinput"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" הוספה ">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnAddTemplate" OnCommand="btnAddTemplate_Command" CssClass="gridbutton" ImageUrl="Img/add.png"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="col-2">
        </div>

    </div>

        <div class="row">
        <div class="col-2">
        </div>
        <div class=" col-8 spanel">
            <div class="spanelHeader">תנועות קבועות</div>
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gvAllTemplates" runat="server"
                    OnRowDataBound="gvAllTemplates_RowDataBound" AllowPaging="true"
                    CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="6" OnPageIndexChanging="gvAllTemplates_PageIndexChanging"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText=" שם">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTempNName" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="סוג ">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTempType" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" סכום">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTempAmount" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="תאריך התחלה ">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTempStartDate" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="תאריך סיום ">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblTempEndDate" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" אוטומטי">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkteAuto" Enabled="false" CssClass="gridtextinput"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="col-2">
        </div>

    </div>
</asp:Content>
