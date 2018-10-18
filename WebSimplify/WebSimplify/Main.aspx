<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebSimplify.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader" id="acc">בית</div>
    <div id="srow">
        <div class="spanel">
            <div class="spanelHeader">הוספה למילון  <i class="fa fa-address-book"></i></div>
            <div>
                <input type="text" name="name" id="txadddickey" placeholder="שם להוספה"  runat="server"/>
            </div>
            <div>
                <input type="text" name="name" id="txadddicval" placeholder="ערך להוספה"  runat="server"/>
            </div>
            <button class="sbutton" type="button" id="btnadddic" runat="server" onserverclick="btnadddic_ServerClick">הוסף</button>
        </div>

        <div class="spanel">
            <div class="spanelHeader">הוספה לרשימת קניות    <i class="fa fa-credit-card"></i></div>
            <div class="col-lg-6">
                <div>כל הפריטים לבחירה</div>
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

        <div class="spanel">
            <div class="spanelHeader">הוספה ליומן   <i class="fa fa-address-card"></i></div>
            <div class="synn-textbox-with-label">
                <input type="text" name="name" id="txadddiaryname" placeholder="שם להצגה"  runat="server"/>
            </div>
            <div class="synn-textbox-with-label">
                <input type="text" name="name" id="txadddiarydesc" placeholder="פירוט" runat="server" />
            </div>
            <div class="synn-textbox-with-label">
                <input type="date" name="name" id="txadddiarydate" placeholder="תאריך"  runat="server"/>
            </div>

            <button class="sbutton sbutton-sm" type="button" id="btnadddiary" runat="server" onserverclick="btnadddiary_ServerClick">הוסף</button>
        </div>
    </div>




</asp:Content>
