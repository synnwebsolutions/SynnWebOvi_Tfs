<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Diary.aspx.cs" Inherits="WebSimplify.Diary" %>

<%@ Register Src="~/Controls/WsCalendar.ascx" TagPrefix="uc1" TagName="WsCalendar" %>
<%@ Register Src="~/Controls/xCalendar.ascx" TagPrefix="uc1" TagName="xCalendar" %>


<%--<%@ Register Src="~/Controls/WsSlider.ascx" TagPrefix="uc1" TagName="WsSlider" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader">יומן</div>
     <div class="menubuttoncontainer">
        <button class="menubutton" type="button" id="btnSendCalenadr" runat="server" onserverclick="btnSendCalenadr_ServerClick">שלח יומן</button>
         <button class="menubutton" type="button" id="btnDownloadCal" runat="server" onserverclick="btnDownloadCal_ServerClick">הורד יומן</button>
    </div>

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
      
        <div class="row">
            <div class="col-12">
                <uc1:xCalendar runat="server" ID="xCalendar" DisplayMode="Month" GetDataSourceMethodName="GetCalendarItems" ShowSelector="true"/>
            </div>
        </div>
    </div>


</asp:Content>
