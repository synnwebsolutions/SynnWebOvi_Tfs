<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="DevTasks.aspx.cs" Inherits="WebSimplify.DevTasks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader">משימות פיתוח</div>

    <div class="row" id="trshop" runat="server">
        <div class=" col-3">
        </div>
        <div class="spanel col-6">
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gvAdd" runat="server"
                    OnRowDataBound="gvAdd_RowDataBound" CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                        <asp:TemplateField HeaderText="שם משימה " AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtaskname" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" תיאור המשימה " AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtaskdesc" CssClass="gridtextinput"></asp:TextBox>
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

    <div class="row" id="dvTaks" runat="server">
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
                        <asp:TemplateField HeaderText="שם משימה ">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblItemName"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" תיאור המשימה " AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbltaskdesc"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="  סטטוס " AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblStatus"></asp:Label>
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
