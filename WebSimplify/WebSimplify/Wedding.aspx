<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Wedding.aspx.cs" Inherits="WebSimplify.Wedding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader">איתור אורחי חתונה</div>
    <div class="spanel">
         <div class="spanelHeader">חיפוש</div>
        <div >
            <input type="text" name="name" id="txwedsearchkey" class="tablefilter" placeholder="הזן שם אורח לחיפוש"/>
        </div>
    </div>
     <div class="spanel">
        <div class="sgridcontainer">
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

</asp:Content>
