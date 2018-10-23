<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="WebSimplify.ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="spageheader" id="acc">אוופס......</div>
    <div class="row">
        <div class=" col-2">
        </div>
        <div class="spanel col-8">
            <h2>כנראה שיש תקלה במערכת - יטופל בהקדם - צ'אוו !</h2>
        
        </div>
        <div class=" col-2">
        </div>

    </div>
      <div class="row">
        <div class=" col-2">
        </div>
        <div class="spanel col-8">
            <h3 id="exTtl" runat="server"></h3>
            <div id="exmsg" runat="server"></div>
        </div>
        <div class=" col-2">
        </div>

    </div>
</asp:Content>
