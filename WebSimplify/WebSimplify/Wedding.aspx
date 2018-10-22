<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Wedding.aspx.cs" Inherits="WebSimplify.Wedding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader">איתור אורחי חתונה</div>
    <div class="row">
        <div class=" col-3"></div>
        <div class="spanel col-6">
            <div class="spanelHeader">חיפוש</div>
            <div>
                <input type="text" name="name" id="txwedsearchkey" class="tablefilter" placeholder="הזן שם אורח לחיפוש" runat="server"/>
            </div>
            <button class="sbutton sbutton-sm" type="button" id="btnSearch" runat="server" onserverclick="btnSearch_ServerClick">חפש</button>
        </div>
        <div class=" col-3"></div>
    </div>
    <div class="row">
        <div class=" col-3"></div>
        <div class="col-6">
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gv" runat="server"
                    OnRowDataBound="gv_RowDataBound" AllowPaging="False"
                    CssClass="synngridstyled tabletofilter" ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText="שם האורח">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblGuestName"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="סכום ">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblGuestValue"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class=" col-3"></div>
    </div>

</asp:Content>
