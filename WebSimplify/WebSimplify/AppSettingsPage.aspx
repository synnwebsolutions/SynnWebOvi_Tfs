<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="AppSettingsPage.aspx.cs" Inherits="WebSimplify.AppSettingsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="spageheader">הגדרות מערכת</div>

    <div class="menubuttoncontainer">
        <button class="menubutton" type="button" id="btnSaveSettings" runat="server" onserverclick="btnSaveSettings_ServerClick">  שמור הגדרות</button>
    </div>


    <div class="spanel" id="dvcredit" runat="server">
        <div class="spanelHeader">הגדרות אשראי</div>
        <div class="row">
            <div class="col-4">
                <label for="txCreditStartDate">תאריך תחילת תיעוד</label>

            </div>
            <div class="col-4">
                <asp:TextBox  id="txCreditStartDate" TextMode="Date" runat="server" placeholder="dd/MM/yyyy" />
            </div>
        </div>
        <div class="row">
            <div class="col-4">
                <label for="txCreditStartDate">יום החיוב החודשי</label>
            </div>
            <div class="col-4">
                <input class="sinputnumber" id="txCreditDayOfMonth" placeholder="יש להזין מספר" runat="server" />
            </div>
        </div>

    </div>



</asp:Content>
