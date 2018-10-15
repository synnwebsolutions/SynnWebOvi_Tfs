<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebSimplify.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- First Section -->
    <section class="" id="main" style="width: 400px;">
         <div class="sectionheader" id="acc">בית</div>
        <div id="dvaccordion">
            <h3 class="sectionsubheader">הוספה למילון  <i class="fa fa-address-book"></i></h3>
            <div class="synnbggenericsecond row">

                <div class="col-lg-12">
                    <div class="synn-textbox-with-label">
                        <label for="txadddickey">שם להוספה</label>
                        <input type="text" name="name" id="txadddickey" />
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="synn-textbox-with-label">
                        <label for="txadddicval">ערך להוספה</label>
                        <input type="text" name="name" id="txadddicval" />
                    </div>
                </div>
                <div class="col-12">
                    <button class="sbutton  sbutton-sm" type="button" id="btnadddic">הוסף</button>
                </div>
            </div>

            <h3 class="sectionsubheader">הוספה לרשימת קניות    <i class="fa fa-credit-card"></i></h3>
            <div class="synnbggenericsecond row">
            </div>

            <h3 class="sectionsubheader">הוספה ליומן   <i class="fa fa-address-card"></i></h3>
            <div class="synnbggenericsecond row">
                            <div class="col-lg-12">
                        <div class="synn-textbox-with-label">
                            <label for="txadddiaryname">שם להצגה</label>
                            <input type="text" name="name" id="txadddiaryname" />
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="synn-textbox-with-label">
                            <label for="txadddiarydesc">פירוט</label>
                            <input type="text" name="name" id="txadddiarydesc" />
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="synn-textbox-with-label">
                            <label for="txadddiarydate">תאריך</label>
                            <input type="date" name="name" id="txadddiarydate" />
                        </div>
                    </div>
                    <div class="col-12">
                        <button class="sbutton sbutton-sm" type="button" id="btnadddiary" onclick="">הוסף</button>
                    </div>
            </div>
        </div>
    </section>


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

    <section id="diary" class="">
        <div class="sectionheader">יומן</div>
        <div id="calendarmaincontainer"></div>
    </section>

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
