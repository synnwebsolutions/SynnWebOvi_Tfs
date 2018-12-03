<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WsCalendar.ascx.cs" Inherits="WebSimplify.Controls.WsCalendar" %>

<div class="slider row">
    <div class="col-1">
        <asp:ImageButton ID="btnPrev" runat="server" ImageUrl="../Img/right-arrow.png" OnClick="btnPrev_Click" />
    </div>
    <div class="col-10">
        <asp:Label ID="txDisplay" runat="server" Text=""></asp:Label>
    </div>
    <div class="col-1">
        <asp:ImageButton ID="btnNext" runat="server" ImageUrl="../Img/left-arrow.png" OnClick="btnNext_Click" />
    </div>
</div>

<div class="wscal" id="wCalendar" runat="server">
    <asp:GridView ID="gvC" runat="server"
        OnRowDataBound="gvC_RowDataBound"
        CssClass="wscalendargridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="שבוע" AccessibleHeaderText="lb">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblweek"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ראשון" AccessibleHeaderText="lb">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblSunaday"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="שני" AccessibleHeaderText="lb">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblMonday"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="שלישי" AccessibleHeaderText="lb">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblTuesday"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="רביעי" AccessibleHeaderText="lb">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblWednsday"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="חמישי" AccessibleHeaderText="lb">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblThursday"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="שישי" AccessibleHeaderText="lb">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblFriday"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="שבת" AccessibleHeaderText="lb">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblSaturday"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

