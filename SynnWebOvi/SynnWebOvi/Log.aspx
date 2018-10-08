<%@ Page Title="" Language="C#" MasterPageFile="~/SynnWebOvi.Master" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="SynnWebOvi.Log" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="synnpanel synnbggeneric">
        <div class="row">
            <div class="col-lg-8">
                <div class="synn-textbox-with-label text-dark">
                    <label for="txNewKey">ערך לחיפוש</label>
                    <asp:TextBox type="text" name="name" ID="txSearchTxt" runat="server" />
                </div>
            </div>
            <div class="col-lg-2">
                 <asp:Button class="synnconfirmbutton synntextwhiteshaddow" ID="btnSrc" runat="server" OnClick="btnSrc_Click" Text="חפש" />
            </div>
               <div class="col-lg-2">
                 <asp:Button class="synnconfirmbutton synntextwhiteshaddow synnbgsecondary" ID="btnClear" runat="server" OnClick="btnClear_Click" Text="נקה" />
            </div>
        </div>
    </div>
    <div class="synngridcontainer synnbggeneric">
        <asp:GridView ID="gv" runat="server" OnRowDataBound="gv_RowDataBound" AllowPaging="true" PageSize="10" CssClass="synngridstyled"
            PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false"
            PagerSettings-FirstPageText="ראשון"
            PagerSettings-NextPageText=">"
            PagerSettings-LastPageText="אחרון"
            PagerSettings-PreviousPageText="<">
            <PagerStyle CssClass="synngridpagination" />
            <Columns>
                <asp:TemplateField HeaderText="I Date">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDate"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Message">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblMessage"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Trace" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblTrace"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
