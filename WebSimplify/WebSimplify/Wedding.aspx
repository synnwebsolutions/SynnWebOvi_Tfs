<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Wedding.aspx.cs" Inherits="WebSimplify.Wedding" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <!-- Third Section -->
    <section id="weddinglog" class="">
        <div class="sectionheader">איתור אורחי חתונה</div>
        <div class="synnbggenericsecond row">
            <div class="col-lg-8">
                <div class="synn-textbox-with-label">
                    <label for="txwedsearchkey">ערך לחיפוש</label>
                    <input type="text" name="name" id="txwedsearchkey" />
                </div>
            </div>
            <div class="col-lg-2">
                <button class="sbutton  sbutton-lg" type="button" id="btnweddSrc" onclick="GetWeddingData()">חפש</button>
            </div>
            <div class="col-lg-2">
                <button class="sbutton ssecondary  sbutton-lg" type="button" id="btnweddClr" onclick="ClearWeddingData()">נקה</button>
            </div>
        </div>

        <div class="synnbggenericsecond row" id="weddgridcontainer">
            <div class="col-lg-12">
                <div id="WeddingDataPanel"></div>
            </div>
        </div>

    </section>

</asp:Content>
