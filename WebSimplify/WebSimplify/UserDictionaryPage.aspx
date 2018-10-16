<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="UserDictionaryPage.aspx.cs" Inherits="WebSimplify.UserDictionaryPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <section id="dictionary" class="">
        <div class="sectionheader">מילון</div>
        <div class="synnbggenericsecond row">
            <div class="col-lg-8">
                <div class="synn-textbox-with-label">
                    <label for="txsearchkey">ערך לחיפוש</label>
                    <input type="text" name="name" id="txsearchkey" />
                </div>
            </div>
            <div class="col-lg-2">
                <button class="sbutton sbutton-lg" type="button" id="btnSrc" onclick="GetDictionaryData()">חפש</button>
            </div>
            <div class="col-lg-2">
                <button class="sbutton ssecondary sbutton-lg" type="button" id="btnClr" onclick="ClearDictionaryData()">נקה</button>
            </div>
        </div>

        <div class="synnbggenericsecond row" id="gridcontainer">
            <div class="col-lg-12">
                <div id="DictionaryDataPanel"></div>
            </div>
        </div>



    </section>
    <!-- Second Section -->

</asp:Content>
