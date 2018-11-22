<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WsEditor.ascx.cs" Inherits="WebSimplify.Controls.WsEditor" %>

<div class="row">
    <div  class="editor centered" Style="width: 60%" >

        <div class="row header" id="editorHaeder" runat="server">
        </div>
        <div id="editorBody" runat="server" class="editorbody">
        
        </div>



        <div class="row panelbottom" id="editorBottom" runat="server">
            <div class="col-4">
                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="sbutton" />
            </div>
            <div class="col-4"></div>
            <div class="col-4">
                <asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" CssClass="sbutton" />
            </div>
        </div>

    </div>

</div>
