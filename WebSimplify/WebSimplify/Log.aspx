<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="WebSimplify.Log" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader">יומן מערכת</div>
    <div class="spanel">
        <div>
            <input type="text" name="name" id="txlogsearchkey" placeholder="Search Text"  class="tablefilter" />
        </div>
    </div>
    <div class="spanel">
        <div class="sgridcontainer">
            <asp:GridView ID="gv" runat="server"
                OnRowDataBound="gv_RowDataBound" AllowPaging="False"
                CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                AutoGenerateColumns="false" >
                <Columns>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDate"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Message" ControlStyle-CssClass="textverrysamll">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblMessage"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Trace ">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTrace" CssClass="textverrysamll"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
