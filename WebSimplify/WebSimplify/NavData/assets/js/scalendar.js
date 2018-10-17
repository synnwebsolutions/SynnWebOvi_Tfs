$(document).ready(function (){
GetCalendarData();
$('.validdate').css('background-color', '#007BFF')	
});

function GetCalendarData() {
	
	$.ajax({
		type: "POST",
		url: "Diary.aspx/GetCalendarItem",
		data: "{}",
		dataType: "Json",
		contentType: "application/json; charset=utf-8",
		success: OnCalendarSuccess,
		error: OnCalendarError
	});
}

function OnCalendarSuccess(data) {
	
	var TableContent = data.d.CalendarHtml;
	$("#calendarmaincontainer").html(TableContent);
}


function OnCalendarError(xhr, ajaxOptions, thrownError)
{
    if (xhr.status == 404)
        alert(thrownError);
    alert("GetCalendarData Error lsa" );
}