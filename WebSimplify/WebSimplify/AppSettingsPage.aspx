<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="AppSettingsPage.aspx.cs" Inherits="WebSimplify.AppSettingsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="spageheader">הגדרות מערכת</div>

    <div class="menubuttoncontainer">
        <button class="menubutton" type="button" id="btnSaveSettings" runat="server" onserverclick="btnSaveSettings_ServerClick">שמור הגדרות</button>
    </div>

     <div id="dvThemes" runat="server">
        <div class="row">
            <div class="col-12">
                <div class="sgridcontainer spanel">
                <asp:GridView ID="gvThemes" runat="server"
                    OnRowDataBound="gvThemes_RowDataBound" AllowPaging="true"
                    CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="20" OnPageIndexChanging="gvThemes_PageIndexChanging"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                            <asp:TemplateField HeaderText="ElementIdentifier " AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txElementIdentifier" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CssAttribute " AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txCssAttribute" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CssValue">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txCssValue" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" הוספה\עדכון ">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnAddCashItem" OnCommand="btnAddCashItem_Command" CssClass="gridbutton" ImageUrl="Img/add.png"></asp:ImageButton>
                                <asp:ImageButton runat="server" ID="btnUpade" OnCommand="btnUpade_Command" CssClass="gridbutton" ImageUrl="Img/exchange.png" AlternateText="עדכן"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            </div>
        </div>
    </div>


    <div class="spanel" id="dvcredit" runat="server">
        <fieldset class="row ">
            <legend>הגדרות אשראי</legend>
            <div class="row">
                <div class="col-4">
                    <label for="txCreditStartDate">תאריך תחילת תיעוד</label>

                </div>
                <div class="col-4">
                    <asp:TextBox ID="txCreditStartDate" TextMode="Date" runat="server" placeholder="dd/MM/yyyy" />
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    <label for="txCreditStartDate">יום החיוב החודשי</label>
                </div>
                <div class="col-4">
                    <input class="sinputnumber" id="txCreditDayOfMonth" placeholder="יש להזין מספר" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    <label for="txCreditStartDate">הצג גרפים </label>
                </div>
                <div class="col-4">
                    <input class="sinputnumber" id="chkUseCharts" type="checkbox" runat="server" />
                </div>
            </div>
        </fieldset>
    </div>

       <div class="spanel" id="dvBalance" runat="server">
        <fieldset class="row ">
            <legend>הגדרות מעקב הכנסות \ הוצאות</legend>
            <div class="row">
                <div class="col-4">
                    <label for="txBalanceStartDate">תאריך תחילת תיעוד</label>

                </div>
                <div class="col-4">
                    <asp:TextBox ID="txBalanceStartDate" TextMode="Date" runat="server" placeholder="dd/MM/yyyy" />
                </div>
            </div>
        </fieldset>
    </div>

    <div class="spanel" id="dvWorkHours" runat="server">
        <fieldset class="row ">
            <legend>הגדרות משמרות</legend>
            <div class="row">
                <div class="col-4">
                    <label for="txCreditStartDate">אורך משמשרת </label>

                </div>
                <div class="col-4">
                    <asp:TextBox ID="txWorkHour" TextMode="Number" runat="server" placeholder="שעות" />
                </div>
                <div class="col-4">
                    <asp:TextBox ID="txWorkMinute" TextMode="Number" runat="server" placeholder="דקות" />
                </div>
            </div>
        </fieldset>
    </div>

   
</asp:Content>
