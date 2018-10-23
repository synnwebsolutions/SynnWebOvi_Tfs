<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="AppSettingsPage.aspx.cs" Inherits="WebSimplify.AppSettingsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="spageheader">הגדרות מערכת</div>
    <div class="spanel">
        <div class=" row">
            <div class="col-4">
                <div class="spanelHeader">
                    עריכת ערכת נושא   <i class="fa fa-image"></i>
                </div>
            </div>
            <div class="col-4">
                <div>
                    <asp:DropDownList ID="cmbFileTypes" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="col-4">
            </div>
        </div>
        <div class=" row">
            <div class="col-4">
                <button class="sbutton" type="button" id="btnGenerateTheme" runat="server" onserverclick="btnGenerateTheme_ServerClick">החל</button>
            </div>
            <div class="col-8">
                <textarea id="txdata" runat="server" class=""></textarea>
            </div>
        </div>
              <div class=" row">
            <div class="col-4">
                <button class="sbutton" type="button" id="btnReverse" runat="server" onserverclick="btnReverse_ServerClick">שחזר</button>
            </div>
            <div class="col-8">
            </div>
        </div>
    </div>
</asp:Content>
