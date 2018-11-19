<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WsSlider.ascx.cs" Inherits="WebSimplify.Controls.WsSlider"  EnableViewState="true"%>

<div class="slider row">
    <div class="col-1">
        <asp:ImageButton ID="btnPrev" runat="server" ImageUrl="../Img/right-arrow.png" OnClick="btnPrev_Click"/>
    </div>
    <div class="col-10">
        <asp:Label ID="txDisplay" runat="server" Text=""></asp:Label>
    </div>
    <div class="col-1">
        <asp:ImageButton ID="btnNext" runat="server" ImageUrl="../Img/left-arrow.png" OnClick="btnNext_Click"/>
    </div>
</div>
