<%@ Page Title="" Language="C#" MasterPageFile="~/WebSimplify.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WebSimplify.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="spageheader" id="acc">בית</div>
    <div class="row" id="trdic" runat="server">
        <div class=" col-2">
        </div>
        <div class="spanel col-8">
            <div class="spanelHeader mcollapsible">הוספה למילון  <i class="fa fa-address-book"></i></div>
            <div class="mcontent">
                <div>
                    <input type="text" name="name" id="txadddickey" placeholder="שם להוספה" runat="server" />
                </div>
                <div>
                    <input type="text" name="name" id="txadddicval" placeholder="ערך להוספה" runat="server" />
                </div>
                <button class="sbutton" type="button" id="btnadddic" runat="server" onserverclick="btnadddic_ServerClick">הוסף</button>
            </div>
        </div>
        <div class=" col-2">
        </div>

    </div>
       <div class="row" id="dvQtasks" runat="server">
        <div class=" col-2">
        </div>
        <div class="spanel col-8">
            <div class="spanelHeader mcollapsible">הוספה למשימות     <i class="fa fa-book-open"></i></div>
            <div class="mcontent">
                  <div class="">
                <input type="text" class="" name="name" id="txTaskname" placeholder=" שם משימה" runat="server" />
            </div>
            <div class="">
                <input type="text" class="" name="name" id="txTaskDesc" placeholder=" תיאור משימה" runat="server" />
            </div>
            <button class="sbutton sbutton-sm" type="button" id="btnAddQuickTask" runat="server" onserverclick="btnAddQuickTask_ServerClick">הוספה</button>
            </div>
        </div>
        <div class=" col-2">
        </div>
    </div>
    <div class="row" id="trshop" runat="server">
        <div class=" col-2">
        </div>
        <div class="spanel col-8">
            <div class="spanelHeader mcollapsible">הוספה לרשימת קניות    <i class="fa fa-credit-card"></i></div>
            <div class="mcontent">
                <div>
                    <input type="text" name="name" id="txShopItemToAdd" placeholder="פריט להוספה" runat="server" />
                </div>
                <button class="sbutton" type="button" id="btnAddShopItem" runat="server" onserverclick="btnAddShopItem_ServerClick">הוסף</button>
            </div>
        </div>
        <div class=" col-2">
        </div>
    </div>
    <div class="row" id="trdiary" runat="server">
        <div class=" col-2">
        </div>
        <div class="spanel col-8">
            <div class="spanelHeader mcollapsible">הוספה ליומן   <i class="fa fa-address-card"></i></div>
            <div class="mcontent">
                <div class="synn-textbox-with-label">
                    <input type="text" name="name" id="txadddiaryname" placeholder="שם להצגה" runat="server" />
                </div>
                <div class="synn-textbox-with-label">
                    <input type="text" name="name" id="txadddiarydesc" placeholder="פירוט" runat="server" />
                </div>
                <div class="synn-textbox-with-label">
                    <input type="date" name="name" id="txadddiarydate" placeholder="תאריך" runat="server" />
                </div>

                <button class="sbutton sbutton-sm" type="button" id="btnadddiary" runat="server" onserverclick="btnadddiary_ServerClick">הוסף</button>
            </div>
        </div>
        <div class=" col-2">
        </div>
    </div>




</asp:Content>
