<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="LottoPoles.aspx.cs" Inherits="WebSimplify.LottoPoles" %>

<%@ Register Src="~/Controls/LottoPoleSelector.ascx" TagPrefix="uc1" TagName="LottoPoleSelector" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


  <%--  <div class="row">

        <div class="col-12 spanel">
            <div class="spanelHeader">הוספת הגרלה</div>
            <div class="sgridcontainer">
                <asp:GridView ID="gvNewPole" runat="server"
                    OnRowDataBound="gvTemplates_RowDataBound" AllowPaging="true"
                    CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="6" OnPageIndexChanging="gvTemplates_PageIndexChanging"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText="תאריך הגרלה ">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txDestDate" CssClass="gridtextinput" TextMode="Date"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="מספר הגרלה ">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txPoleKey" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="1">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="tx1" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="2">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="tx2" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="3">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="tx3" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="4">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="tx4" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="tx5" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="6">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="tx6" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="מספר חזק">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txSpecial" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" הוספה ">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnAddPole" OnCommand="btnAddPole_Command" CssClass="gridbutton" ImageUrl="Img/add.png"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>--%>

    <div class="row">

        <div class="col-12 spanel">
            <uc1:LottoPoleSelector runat="server" id="LottoPoleSelector" SaveDataMethodName="AddPole"/>
        </div>
    </div>

    <div class="row">
        <%--<div class=" col-3"></div>--%>
        <div class="col-12">
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gv" runat="server"
                    OnRowDataBound="gv_RowDataBound"
                    CssClass="synngridstyled tabletofilter" ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="10" AllowPaging="true"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText=" מספר הגרלה ">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPoleKey"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="תאריך הגרלה ">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPoleDate"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="מספר שורות">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPoleRows"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="זכיות">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPoleWins"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" הפק שורות שוב ">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnReGenerate" OnCommand="btnReGenerate_Command" CssClass="gridbutton" ImageUrl="Img/play-button.png" AlternateText="עדכן"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" עדכן ">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnUpdate" OnCommand="btnUpdate_Command" CssClass="gridbutton" ImageUrl="Img/exchange.png" AlternateText="עדכן"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <%--<div class=" col-3"></div>--%>
    </div>


     <div class="row">
           <div class=" col-3">
        </div>
        <asp:Panel ID="panelx" runat="server" Visible="false" CssClass="editor centered col-6">
            <div class="row header" id="editorHaeder" runat="server">
            </div>
            <div class="editorbody">
                <div class="row">
                    <div class="col-6">תאריך יעד</div>
                    <div class="col-6">
                        <asp:TextBox ID="txPoleDate" TextMode="Date" runat="server" placeholder="dd/MM/yyyy" />
                    </div>
                </div>
            </div>
            <div class="row panelbottom" id="editorBottom" runat="server">
                <div class="col-4">
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="sbutton" Text="ביטול" />
                </div>
                <div class="col-4"></div>
                <div class="col-4">
                    <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" CssClass="sbutton" Text="אישור" />
                </div>
            </div>
        </asp:Panel>
           <div class=" col-3">
        </div>
    </div>

</asp:Content>
