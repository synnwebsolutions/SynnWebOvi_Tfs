<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="AppSettingsPage.aspx.cs" Inherits="WebSimplify.AppSettingsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="spageheader">הגדרות מערכת</div>

    <div class="menubuttoncontainer">
        <button class="menubutton" type="button" id="btnSaveSettings" runat="server" onserverclick="btnSaveSettings_ServerClick">שמור הגדרות</button>
    </div>


    <div class="spanel" id="dvcredit" runat="server">
        <fieldset class="row ">
            <legend>הגדרות אשראי</legend>
            <div class="row">
                <div class="col-4">
                    <label for="txCreditStartDate">תאריך תחילת תיעוד</label>

                </div>
                <div class="col-4">
                    <asp:TextBox ID="txCreditStartDate" TextMode="Date" runat="server" placeholder="dd/MM/yyyy" />
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
            <div class="row">
                <div class="col-4">
                    <label for="txCreditStartDate">הצג גרפים </label>
                </div>
                <div class="col-4">
                    <input class="sinputnumber" id="chkUseCharts" type="checkbox" runat="server" />
                </div>
            </div>
        </fieldset>
    </div>

       <div class="spanel" id="dvBalance" runat="server">
        <fieldset class="row ">
            <legend>הגדרות מעקב הכנסות \ הוצאות</legend>
            <div class="row">
                <div class="col-4">
                    <label for="txBalanceStartDate">תאריך תחילת תיעוד</label>

                </div>
                <div class="col-4">
                    <asp:TextBox ID="txBalanceStartDate" TextMode="Date" runat="server" placeholder="dd/MM/yyyy" />
                </div>
            </div>
        </fieldset>
    </div>

    <div class="spanel" id="dvWorkHours" runat="server">
        <fieldset class="row ">
            <legend>הגדרות משמרות</legend>
            <div class="row">
                <div class="col-4">
                    <label for="txCreditStartDate">אורך משמשרת </label>

                </div>
                <div class="col-4">
                    <asp:TextBox ID="txWorkHour" TextMode="Number" runat="server" placeholder="שעות" />
                </div>
                <div class="col-4">
                    <asp:TextBox ID="txWorkMinute" TextMode="Number" runat="server" placeholder="דקות" />
                </div>
            </div>
        </fieldset>
    </div>



</asp:Content>
