<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Shopping.aspx.cs" Inherits="WebSimplify.Shopping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader">ניהול רשימת קניות</div>
    <div class="row">
        <div class=" col-1"></div>
        <div class="spanel col-10">
            <div class="row">
                <div class="spanelHeader">הוספה</div>
                <div class="col-7">
                    <div>
                        <asp:DropDownList ID="cmbItems" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class=" col-5">
                    <button class="sbutton sbutton-sm" type="button" id="btnAdd" runat="server" onserverclick="btnAdd_ServerClick">הוסף פריט</button>
                </div>
            </div>
        </div>
        <div class=" col-1"></div>
    </div>
    <div class="row">
        <div class=" col-3"></div>
        <div class="col-6">
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gv" runat="server"
                    OnRowDataBound="gv_RowDataBound" AllowPaging="true"
                    CssClass="synngridstyled tabletofilter" ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" PageSize="30" OnPageIndexChanging="gv_PageIndexChanging"
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText="הסר פריט">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkRemove" AutoPostBack="true" OnCheckedChanged="chkRemove_CheckedChanged"></asp:CheckBox>
                                <asp:HiddenField ID="hfpid" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="שם פריט">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblItemName"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="נקנה לאחרונה ">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLastValue"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class=" col-3"></div>
    </div>


</asp:Content>
