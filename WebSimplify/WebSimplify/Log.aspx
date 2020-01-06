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
                      <asp:TemplateField HeaderText="מזהה משימה">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblId"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="משתמש">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblUser"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="כותרת" ControlStyle-CssClass="">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblMessage"  ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="מצב ">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblStatus" CssClass=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="סוג ">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAction" CssClass=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                             <asp:TemplateField HeaderText="ת.עדכון ">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblUpdate" CssClass=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
