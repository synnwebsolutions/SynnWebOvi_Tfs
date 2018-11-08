<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="WorkData.aspx.cs" Inherits="WebSimplify.WorkData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader">מעקב שעות עבודה</div>


    <div class="spanel">
        <div class="row">
            <div class=" col-3">
                <div class="sinputwlabel">
                    <label for="txp1">חובה יומית</label>
                    <asp:TextBox ID="txCurrentDailyRequired" runat="server" Enabled="false"/>
                </div>
            </div>
            <div class="col-3">
                <div class="sinputwlabel">
                    <label for="txp2">מאזן חודשי נוכחי</label>
                    <asp:TextBox ID="txCurrentMonthState" runat="server" />
                </div>
            </div>
             <div class=" col-3">
                <div class="sinputwlabel">
                    <label for="txp3">  תחילת משמרת</label>
                    <asp:TextBox ID="txCurrentShifStart" runat="server" />
                </div>
            </div>
            <div class=" col-3">
                <div class="sinputwlabel">
                    <label for="txp3">  זמן לסיום משמרת</label>
                    <asp:TextBox ID="txCurrentShiftLeft" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class=" col-6">
            <div class="sgridcontainer spanel">
                <asp:GridView ID="gvWorkHours" runat="server"
                    OnRowDataBound="gvWorkHours_RowDataBound" 
                    CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                    PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false" 
                    PagerSettings-FirstPageText="ראשון"
                    PagerSettings-NextPageText=">"
                    PagerSettings-LastPageText="אחרון"
                    PagerSettings-PreviousPageText="<">
                    <PagerStyle CssClass="synngridpagination" />
                    <Columns>
                            <asp:TemplateField HeaderText="תאריך " AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblDate"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="שעה " AccessibleHeaderText="lb">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txHour" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="דקות">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txMinute" CssClass="gridtextinput"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="הוספה ">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnAddWorkItem" OnCommand="btnAddWorkItem_Command" CssClass="gridbutton" ImageUrl="Img/add.png"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>



</asp:Content>
