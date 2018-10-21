<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="UsersManagment.aspx.cs" Inherits="WebSimplify.UsersManagment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="spageheader" id="acc">בית</div>
    <div class="srow">
        <div class="spanel spaneltwoinrow">
            <div class="spanelHeader">הוספת משתמש  <i class="fa fa-address-book"></i></div>
            <div>
                <input type="text" name="name" id="txNewUserName" placeholder="שם משתמש" runat="server" />
            </div>
            <div>
                <input type="text" name="name" id="txNewFirstPassword" placeholder=" סיסמה ראשונית" runat="server" />
            </div>

        </div>
        <div class="spanel">
            <div class="spanelHeader">הרשאות משתמש  <i class="fa fa-address-book"></i></div>
           
            <button class="sbutton" type="button" id="btnAddUser" runat="server" onserverclick="btnAddUser_ServerClick">הוסף</button>
        </div>

        
    </div>
</asp:Content>
