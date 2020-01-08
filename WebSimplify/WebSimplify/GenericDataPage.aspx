<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="GenericDataPage.aspx.cs" Inherits="WebSimplify.GenericDataPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="spageheader">נתוני טבלאות </div>

    <div class="spanel">
            <div class=" row">
                <div class="col-4">
                    <div class="spanelHeader">
                      בחירת טבלה   <i class="fa fa-address-book"></i>
                    </div>
                </div>
                <div class="col-6">
                    <div>
                        <asp:DropDownList ID="cmbTables" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbTables_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-2">
                </div>
            </div>
        </div>

       <div class="menubuttoncontainer" id="dvWorkHours" runat="server">
        <button class="menubutton" type="button" id="btnUpdate" runat="server" onserverclick="btnUpdate_ServerClick">עדכן</button>
    </div>

       <div class="spanel">
        <div class="sgridcontainer">
            <asp:GridView ID="gv" runat="server"
                OnRowDataBound="gv_RowDataBound" AllowPaging="False" OnDataBinding="gv_DataBinding"
                CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%" OnDataBound="gv_DataBound"
                AutoGenerateColumns="false" >
                <Columns>
                           <asp:TemplateField HeaderText=" ">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfpid" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                </Columns>
                </asp:GridView>
        </div>
       </div>
</asp:Content>
