<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="UserDictionaryPage.aspx.cs" Inherits="WebSimplify.UserDictionaryPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="spageheader">מילון מונחים</div>
        <div class="row" id="trdic" runat="server">
        <div class=" col-3">
        </div>
        <div class=" col-6">
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gvAdd" runat="server"
                    OnRowDataBound="gvAdd_RowDataBound" CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" >
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText="שם " AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txName" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="תיאור">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txIdesc" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" הוספה ">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnAdd" OnCommand="btnAdd_Command" CssClass="gridbutton" ImageUrl="Img/add.png"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>


        </div>
        <div class=" col-3">
        </div>

    </div>
    <div class="row">
        <div class=" col-3"></div>
        <div class="spanel col-6">
            <div class="spanelHeader">חיפוש</div>
            <div class="">
                <input type="text" class="tablefilter" name="name" id="txsearchkey" placeholder="ערך לחיפוש" runat="server"/>
            </div>
            <button class="sbutton sbutton-sm" type="button" id="btnSearch" runat="server" onserverclick="btnSearch_ServerClick">חפש</button>
        </div>
        <div class=" col-3"></div>
    </div>
    <div class="row">
        <div class=" col-3"></div>
        <div class="col-6">
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gv" runat="server" OnRowDataBound="gv_RowDataBound" CssClass="synngridstyled tabletofilter"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText="שם">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDicName"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ערך">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDicValue"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class=" col-3"></div>
    </div>
</asp:Content>
