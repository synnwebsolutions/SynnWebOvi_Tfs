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
                    <asp:TemplateField HeaderText="עריכה">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="btnEdit" OnCommand="btnEdit_Command" CssClass="gridbutton" ImageUrl="Img/edit.png"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="row">
           <div class=" col-3">
        </div>
        <asp:Panel ID="panelx" runat="server" Visible="false" CssClass="editor centered col-6">
            <div class="row header" id="editorHaeder" runat="server">
            </div>
            <div class="editorbody">
                <div class="row">
                    <div class="col-6">משתמש</div>
                    <div class="col-6">
                        <asp:TextBox ID="txdUserName" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">תיאור</div>
                    <div class="col-6">
                        <asp:TextBox ID="txJobDesc" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">מצב</div>
                    <div class="col-6">
                        <asp:DropDownList ID="cmbXStatus" runat="server"></asp:DropDownList>
                    </div>
                </div>
                   <div class="row">
                    <div class="col-6">סוג</div>
                    <div class="col-6">
                        <asp:DropDownList ID="cmbJobType" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row panelbottom" id="editorBottom" runat="server">
                <div class="col-4">
                    <asp:Button ID="btnCancelJobEdit" runat="server" OnClick="btnCancelJobEdit_Click" CssClass="sbutton" Text="ביטול" />
                </div>
                <div class="col-4"></div>
                <div class="col-4">
                    <asp:Button ID="btnOkJobEdit" runat="server" OnClick="btnOkJobEdit_Click" CssClass="sbutton" Text="אישור" />
                </div>
            </div>
        </asp:Panel>
           <div class=" col-3">
        </div>
    </div>
</asp:Content>
