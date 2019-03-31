﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="LottoRows.aspx.cs" Inherits="WebSimplify.LottoRows" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader" id="acc">הפקת לוטו</div>

    <div class="menubuttoncontainer" id="dvWorkHours" runat="server">
        <button class="menubutton" type="button" id="btnSaveTempRows" runat="server" onserverclick="btnSaveTempRows_ServerClick">שמור מספרים</button>
        <button class="menubutton" type="button" id="btnTestHistory" runat="server" onserverclick="btnTestHistory_ServerClick">בדוק בהיסטורייה</button>
    </div>

    <div class="row">

        <div class="col-12 spanel">
            <div class="spanelHeader">צור טופס</div>
            <div class="sgridcontainer">
                <asp:GridView ID="gvNewPole" runat="server"
                    OnRowDataBound="gvNewPole_RowDataBound" AllowPaging="true"
                    CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="6" OnPageIndexChanging="gvNewPole_PageIndexChanging"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText="תאריך הגרלה ">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txNewDestDate" CssClass="gridtextinput" TextMode="Date"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="מספר שורות ">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txNumOfRows" CssClass="gridtextinput" Text="14"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="עם זכייה">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="cmbStat" CssClass="gridtextinput"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" הגרל ">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnGenerate" OnCommand="btnGenerate_Command" CssClass="gridbutton" ImageUrl="Img/add.png"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>


    <div class="row">

        <div class="col-12 spanel">
            <div class="spanelHeader">הוספת הגרלה</div>
            <div class="sgridcontainer">
                <asp:GridView ID="gvTempRows" runat="server"
                    OnRowDataBound="gvTempRows_RowDataBound"
                    CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="6" OnPageIndexChanging="gvTempRows_PageIndexChanging"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="תאריך הגרלה ">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="txDestDate" CssClass="gridtextinput" TextMode="Date"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="מספר הגרלה ">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="txPoleKey" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="1">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="tx1" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="2">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="tx2" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="3">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="tx3" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="4">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="tx4" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="tx5" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="6">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="tx6" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="מספר חזק">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="txSpecial" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>

    <div class="row">
        <div class="col-12 spanel">
            <div class="spanelHeader">כל ההגרלות</div>
        </div>


        <div class=" col-3">חיפוש לפי הגרלה או תאריך</div>
        <div class="col-3">
            <input type="text" class="tablefilter" name="name" id="txPolekey" placeholder=" מספר הגרלה" runat="server" />
        </div>
        <div class="col-3">
            <asp:TextBox ID="txPoleDate" TextMode="Date" runat="server" placeholder="dd/MM/yyyy" />
        </div>
        <div class=" col-3">
            <button class="sbutton sbutton-sm" type="button" id="btnSearch" runat="server" onserverclick="btnSearch_ServerClick">חפש</button>
        </div>




        <div class="col-12 spanel">
            <div class="sgridcontainer">
                <asp:GridView ID="gvAllRows" runat="server" AllowPaging="true"
                    OnRowDataBound="gvAllRows_RowDataBound"
                    CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="14" OnPageIndexChanging="gvAllRows_PageIndexChanging"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="תאריך הגרלה ">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="atxDestDate" CssClass="gridtextinput" TextMode="Date"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="מספר הגרלה ">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="atxPoleKey" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="1">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="atx1" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="2">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="atx2" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="3">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="atx3" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="4">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="atx4" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="atx5" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="6">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="atx6" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="מספר חזק">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="atxSpecial" CssClass="gridtextinput"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="זכיות">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPoleWins"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
</asp:Content>