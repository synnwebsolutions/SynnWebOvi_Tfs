<%@ Page Title="" Language="C#" MasterPageFile="~/SynnWebOvi.Master" AutoEventWireup="true" CodeBehind="Diary.aspx.cs" Inherits="SynnWebOvi.Diary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/jquery-1.12.4.js"></script>
    <script src="//code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container syncontainer">
        <div = class="row">
        <div id="datepicker"></div>
        <script>
            $("#datepicker").datepicker();
        </script>
            </div>
    </div>
</asp:Content>
