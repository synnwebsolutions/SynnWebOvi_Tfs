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

    <div class="spanel">
        <div class="sgridcontainer">
            <asp:GridView ID="gv" runat="server" EmptyDataText="No Record Found" 
                AllowPaging="true"  
                CssClass="synngridstyled " ItemStyle-Width="100%" ControlStyle-Width="100%"
                AutoGenerateColumns="true">
                <Columns> </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
