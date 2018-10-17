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
                <div class="col-lg-6">
                    <h2>כל הפריטים לבחירה</h2>
                    <select name="langOpt3[]" multiple id="ddlShopItems">
                        <option value="C++">C++</option>
                        <option value="C#">C#</option>
                        <option value="Java">Java</option>
                        <option value="Objective-C">Objective-C</option>
                        <option value="JavaScript">JavaScript</option>
                        <option value="Perl">Perl</option>
                        <option value="PHP">PHP</option>
                        <option value="Ruby on Rails">Ruby on Rails</option>
                        <option value="Android">Android</option>
                        <option value="iOS">iOS</option>
                        <option value="HTML">HTML</option>
                        <option value="XML">XML</option>
                    </select>
                </div>
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
                    <button class="sbutton sbutton-sm" type="button" id="btnadddiary" >הוסף</button>
                </div>
            </div>
        </div>
    </section>


  

</asp:Content>
