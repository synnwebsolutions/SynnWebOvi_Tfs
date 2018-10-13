$(document).ready(function ()
{
	$('.container').css('width', '100%').css('margin','auto').addClass('synnbggeneric');
	$('#siteNavbar').css('background-color', '#007BFF').css('width', '80%').css('margin','auto').addClass('synnbggradient')
	.css('top', '15px');
	$('#siteNavbar .container').css('background', 'transparent');
	$('.nav-link').css('font-family', 'Impact');
	$('section').css('min-height','100vh').css('width', '100%').css('margin','20px').addClass('synnbggeneric');
	$('.row').css('padding-top', '30px').css('width', '80%').css('margin','auto');
	$('.gridfooterrow a').css('color', '#E24242');	
	function GetCalendarData() {
	
	alert("GetCalendarData Start");
	$.ajax({
		type: "POST",
		url: "Main.aspx/GetCalendarItem",
		data: "{}",
		dataType: "json",
		contentType: "application/json; charset=utf-8",
		success: OnCalendarSuccess,
		error: OnCalendarError
	});
}

function OnCalendarSuccess(data) {
	
	var TableContent = data.d.CalendarHtml;
	alert(TableContent);
	$("#calendarmaincontainer").html(TableContent);
}


function OnCalendarError(data) {
alert("GetCalendarData Error");
}
});