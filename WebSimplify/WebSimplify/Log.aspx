<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="WebSimplify.Log" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader">יומן מערכת</div>
    <div class="spanel">
        <div>
            <input type="text" name="name" id="txlogsearchkey" placeholder="Search Text"/>
        </div>
    <button class="sbutton sbutton-lg" type="button" id="btnlogSrc" onclick="GetLogData()">חפש</button>
    <button class="sbutton ssecondary sbutton-lg" type="button" id="btnlogClr" onclick="ClearLogData()">נקה</button>
    </div>

</asp:Content>
