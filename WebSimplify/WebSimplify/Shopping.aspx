<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Shopping.aspx.cs" Inherits="WebSimplify.Shopping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader">ניהול רשימת קניות</div>
    <div class="menubuttoncontainer">
        <button class="menubutton" type="button" id="btnGenerate" runat="server" onserverclick="btnGenerate_ServerClick"> הפק רשימת קניות</button>
    </div>
     <div class="row" id="trshop" runat="server">
        <div class=" col-1">
        </div>
        <div class="spanel col-10">
            <div class="spanelHeader ">הוספה לרשימת קניות    <i class="fa fa-credit-card"></i></div>
            <div class="">
                <div>
                    <input type="text" name="name" id="txShopItemToAdd" placeholder="פריט להוספה" runat="server" />
                </div>
                <button class="sbutton" type="button" id="btnAddShopItem" runat="server" onserverclick="btnAddShopItem_ServerClick">הוסף</button>
            </div>
        </div>
        <div class=" col-1">
        </div>
    </div>
  

    <div class="row" id="trAdd" runat="server">
        <div class=" col-1"></div>
        <div class="spanel col-10">
            <div class="row" >
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
                        <asp:TemplateField HeaderText="שם פריט">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblItemName"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="נקנה לאחרונה " AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblLastValue"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="הסר פריט">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnClose" OnCommand="btnClose_Command" CssClass="gridbutton" ImageUrl="Img/dlt.png"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class=" col-3"></div>
    </div>


</asp:Content>
