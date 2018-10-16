<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="WebSimplify.Log" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
    <section id="sys" class="">
        <div class="sectionheader">מערכת</div>
        <div class="synnbggenericsecond row">
            <div class="col-lg-8">
                <div class="synn-textbox-with-label">
                    <label for="txlogsearchkey">ערך לחיפוש</label>
                    <input type="text" name="name" id="txlogsearchkey" />
                </div>
            </div>
            <div class="col-lg-2">
                <button class="sbutton sbutton-lg" type="button" id="btnlogSrc" onclick="GetLogData()">חפש</button>
            </div>
            <div class="col-lg-2">
                <button class="sbutton ssecondary sbutton-lg" type="button" id="btnlogClr" onclick="ClearLogData()">נקה</button>
            </div>
        </div>

        <div class="synnbggenericsecond row" id="loggridcontainer">
            <div class="col-lg-12">
                <div id="logDataPanel"></div>
            </div>
        </div>


    </section>


</asp:Content>
