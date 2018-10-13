$(document).ready(function (){
GetCalendarData();
$('.validdate').css('background-color', '#007BFF')	
});

function GetCalendarData() {
	
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
	$("#calendarmaincontainer").html(TableContent);
}


function OnCalendarError(data) {
alert("GetCalendarData Error");
}