<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebSimplify.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader" id="acc">בית</div>

    <div class="menubuttoncontainer" id="dvWorkHours" runat="server">
        <button class="menubutton" type="button" id="btnWorkHours" runat="server" onserverclick="btnWorkHours_ServerClick">מעקב שעות עבודה</button>
    </div>

    <fieldset class="row ">
        <legend>השבוע</legend>

        <div class="col-6 marktable" id="dtDiary" runat="server">
            <fieldset class="row ">
                <legend>יומן</legend>

                <asp:Repeater ID="rpCalendar" runat="server" OnItemDataBound="rpCalendar_ItemDataBound">

                    <HeaderTemplate>
                        <table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblName" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="lblDesc" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>

                </asp:Repeater>
            </fieldset>
        </div>

        <div class="col-6 marktable" id="dtTasks" runat="server">
            <fieldset class="row ">
                <legend>משימות</legend>

                <asp:Repeater ID="rpTasks" runat="server" OnItemDataBound="rpTasks_ItemDataBound">

                    <HeaderTemplate>
                        <table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblDesc" runat="server" />
                            </td>

                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>

                </asp:Repeater>

            </fieldset>
        </div>

        <div class="col-6 marktable" id="dtShifts" runat="server">
            <fieldset class="row ">
                <legend>משמרת קרובה</legend>
                <asp:Repeater ID="rpShift" runat="server" OnItemDataBound="rpShift_ItemDataBound">

                    <HeaderTemplate>
                        <table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblName" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="lblDesc" runat="server" />
                            </td>

                        </tr>
                    </ItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>

                </asp:Repeater>
            </fieldset>
        </div>

    </fieldset>


</asp:Content>
