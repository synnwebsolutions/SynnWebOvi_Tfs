<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="xCalendar.ascx.cs" Inherits="WebSimplify.Controls.xCalendar" %>

<style>
    .dayitem {
        padding: 10%;
        list-style-type: square;
        margin: auto;
        background-color: #808080;
    }

    .dayitemcontainer {
        padding: 10%;
        border: none;
        background-color: #2B2B2D; 
        width: 100%;
    }

    .dayitemheader {
        background-color: black;
        padding: 5%;
        color: white;
    }

    .maincontainer {
        width: 100%;
        background-color: black;
    }

    .mainheaders {
        /*background-color:lightgrey;*/
        color: lightgray;
        width: 14.3%;
        text-align: center;
    }

    .today {
        color: lightgray;
        background-color: tomato;
    }
</style>

<div class="slider row" id="selector" runat="server">
    <div class="col-1">
        <asp:ImageButton ID="btnPrev" runat="server" ImageUrl="../Img/right-arrow.png" OnClick="btnPrev_Click" />
    </div>
    <div class="col-10">
        <asp:Label ID="txDisplay" runat="server" Text=""></asp:Label>
    </div>
    <div class="col-1">
        <asp:ImageButton ID="btnNext" runat="server" ImageUrl="../Img/left-arrow.png" OnClick="btnNext_Click" />
    </div>
</div>
<div class="row">
    <div id="tbl" runat="server" class="col-12">
    </div>
</div>
