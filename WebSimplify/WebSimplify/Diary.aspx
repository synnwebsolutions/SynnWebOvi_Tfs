<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Diary.aspx.cs" Inherits="WebSimplify.Diary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader">יומן</div>

    <div class="row">
        <div class="col-3"></div>
        <div class="spanel spanelmin col-6">
            <div class="spanelHeader">הוספה ליומן    <i class="fa fa-address-card"></i></div>

            <div>
                <input type="date" name="name" id="txadddiarydate" placeholder="תאריך" runat="server" />
            </div>
            <div>
                <input type="text" name="name" id="txadddiaryname" placeholder=" שם" runat="server" />
            </div>
            <div>
                <input type="text" name="name" id="txadddiarydesc" placeholder=" תיאור" runat="server" />
            </div>
            <button class="sbutton sbutton-sm" type="button" id="btnadddiary" runat="server" onserverclick="btnadddiary_ServerClick">הוסף</button>
        </div>
        <div class="col-3"></div>
    </div>
    <div class="spanel">
        <asp:Calendar ID="cdr" runat="server" FirstDayOfWeek="Sunday" Width="100%" OnDayRender="cdr_DayRender" OnVisibleMonthChanged="cdr_VisibleMonthChanged" CssClass="aspcalendar"></asp:Calendar>
    </div>


</asp:Content>
