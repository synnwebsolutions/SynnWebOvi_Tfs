<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Diary.aspx.cs" Inherits="WebSimplify.Diary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader">יומן</div>

    <div class="spanel">
        <div class="spanelHeader" id="monthTitle" runat="server"></div>
        <div class="spnaneltopactions">
            <button class="siconbutton left" runat="server" id="btnNextMonth" onserverclick="btnNextMonth_ServerClick"><i class="fa  fa-w-12 fa-angle-double-left fa-2x"></i></button>
            <button class="siconbutton right" runat="server" id="btnPrevMonth" onserverclick="btnPrevMonth_ServerClick"><i class="fa  fa-w-12 fa-angle-double-right fa-2x"></i></button>
        </div>
        <div class="sgridcontainer">
            <asp:GridView ID="gv" runat="server"
                OnRowDataBound="gv_RowDataBound" OnPageIndexChanging="gv_PageIndexChanging"
                AllowPaging="true" PageSize="8" CssClass="gridcalendar"
                PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false"
                PagerSettings-FirstPageText="ראשון"
                PagerSettings-NextPageText=">"
                PagerSettings-LastPageText="אחרון"
                PagerSettings-PreviousPageText="<">
                <PagerStyle CssClass="synngridpagination" />
                <Columns>
                    <asp:TemplateField HeaderText="ראשון">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="lblSunday" Visible="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="שני">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="lblMonday" Visible="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="שלישי">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="lblTuesday" Visible="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="רביעי">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="lblWendsday" Visible="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="חמישי">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="lblThursday" Visible="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="שישי">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="lblFriday" Visible="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="שבת">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="lblSaterday" Visible="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="sgridcontainer">
            <asp:GridView ID="gvMin" runat="server"
                OnRowDataBound="gvMin_RowDataBound" OnPageIndexChanging="gv_PageIndexChanging"
                AllowPaging="true" PageSize="8" CssClass="synngridstyled min-grid" ItemStyle-Width="100%" ControlStyle-Width="100%"
                PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false"
                PagerSettings-FirstPageText="ראשון"
                PagerSettings-NextPageText=">"
                PagerSettings-LastPageText="אחרון"
                PagerSettings-PreviousPageText="<">
                <PagerStyle CssClass="synngridpagination" />
                <Columns>
                    <asp:TemplateField HeaderText="תאריך">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDate" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="שם">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblName"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="תיאור">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDesc" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    </div>

</asp:Content>
