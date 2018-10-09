<%@ Page Title="" Language="C#" MasterPageFile="~/SynnWebOvi.Master" AutoEventWireup="true" CodeBehind="UserDictionaryPage.aspx.cs" Inherits="SynnWebOvi.UserDictionaryPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>מילון משתמש</title>
    <meta charset="UTF-8" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="synnpanel synnbggeneric">
        <div class="row">
            <div class="col-lg-8">
                <div class="synn-textbox-with-label text-dark">
                    <label for="txNewKey">ערך לחיפוש</label>
                    <asp:TextBox type="text" name="name" ID="txSearchTxt" runat="server" />
                </div>
            </div>
            <div class="col-lg-2">
                <asp:Button class="synnconfirmbutton synntextwhiteshaddow" ID="btnSrc" runat="server" OnClick="btnSrc_Click" Text="חפש" />
            </div>
            <div class="col-lg-2">
                <asp:Button class="synnconfirmbutton synntextwhiteshaddow synnbgsecondary" ID="btnClear" runat="server" OnClick="btnClear_Click" Text="נקה" />
            </div>
        </div>
    </div>
    <div class="synngridcontainer synnbggeneric">
        <asp:GridView ID="gv" runat="server" OnRowDataBound="gv_RowDataBound" AllowPaging="true" PageSize="4" CssClass="synngridstyled"
            PagerSettings-Mode="NumericFirstLast" AutoGenerateColumns="false"
            PagerSettings-FirstPageText="ראשון"
            PagerSettings-NextPageText=">"
            PagerSettings-LastPageText="אחרון"
            PagerSettings-PreviousPageText="<">
            <PagerStyle CssClass="synngridpagination" />
            <Columns>
                <asp:TemplateField HeaderText="שם">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDicName"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ערך">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDicValue"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
    <div class="synnpanel synnbggeneric">
        <div class="card text-center">
            <div class="card-header synnheader">
                הוספת ערך למילון
            </div>
            <button id="clickme">
                Click here
            </button>
        </div>
        <div class="card-body" id="divadd" style="visibility:collapse">

            <div class="">
                <div class="synn-textbox-with-label text-dark">
                    <label for="txNewKey">שם להוספה</label>
                    <asp:TextBox type="text" name="name" ID="txNewKey" runat="server" />
                </div>
            </div>
            <div class="">
                <div class="synn-textarea-with-label">
                    <label for="txNewValue">ערך להוספה</label>
                    <asp:TextBox type="text" name="name" ID="txNewValue" runat="server" TextMode="MultiLine" />
                </div>
            </div>
            <asp:Button class="synnconfirmbutton synntextwhiteshaddow" ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="הוספה" />

        </div>
        <div class="card-footer text-muted">
            2 days ago
        </div>
    </div>
    	<script>
    	    $('#clickme').on('click', function () {
    	        $('.divadd').hide();
    	        $('.divadd').toggle();
    	    });
    	    $(document).ready(function () {
    	      
			/*
			$('#btn1').click(function(){
				alert('Button Clicked!');
			});


			$('#btn1').on('click', function(){
				alert('Button Clicked!');
			});

	

			$('#btn2').on('click', function(){
				$('.para1').show();
			});
			

			$('#btn1').dblclick(function(){
				$('.para1').toggle();
			});

			$('#btn1').hover(function(){
				$('.para1').toggle();
			});

			$('#btn1').on('mouseenter', function(){
				$('.para1').toggle();
			});

			$('#btn1').on('mouseleave', function(){
				$('.para1').toggle();
			});

			$('#btn1').on('mousemove', function(){
				$('.para1').toggle();
			});
		

			$('#btn1').on('mousedown', function(){
				$('.para1').toggle();
			});

			$('#btn1').on('mouseup', function(){
				$('.para1').toggle();
			});

			$('#btn1').click(function(e){
				//alert(e.currentTarget.id);
				//alert(e.currentTarget.innerHTML);
				//alert(e.currentTarget.outerHTML);
				//alert(e.currentTarget.className);
			});

			$(document).on('mousemove', function(e){
				$('#coords').html('Coords: Y: '+e.clientY+" X: "+e.clientX);
			});

			$('input').focus(function(){
				//alert('Focus');
				$(this).css('background', 'pink');
			});

			$('input').blur(function(){
				//alert('Focus');
				$(this).css('background', 'white');
			});

			$('input').keyup(function(e){
				console.log(e.target.value);
			});
			*/

			//$('select#gender').change(function(e){
			//	alert(e.target.value);
			//});

			//$('#form').submit(function(e){
			//	e.preventDefault();
			//	var name = $('input#name').val();
			//	console.log(name);
			//});
		});
	</script>

</asp:Content>
