<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="UserDictionaryPage.aspx.cs" Inherits="WebSimplify.UserDictionaryPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="spageheader">מילון מונחים</div>
    <div class="spanel">
        <div class="spanelHeader">חיפוש</div>
        <div class="">
            <input type="text" name="name" id="txsearchkey" placeholder="ערך לחיפוש" />
        </div>
        <button class="sbutton " type="button" id="btnSrc">חפש</button>
    </div>
    <div class="spanel">
        <div class="sgridcontainer">
            <asp:GridView ID="gv" runat="server" OnRowDataBound="gv_RowDataBound" AllowPaging="true" PageSize="4" CssClass="synngridstyled"
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
</asp:Content>
