function GetLogData() {
	
	var val =$("#txlogsearchkey").val();
	if(val == '')
	{
		DisplayNoneLogs();
		return;
	}
	$.ajax({
		type: "POST",
		url: "Main.aspx/GetLogItems",
		data: "{'searchtext': '" + val + "'}",
		dataType: "json",
		contentType: "application/json; charset=utf-8",
		success: OnSuccess,
		error: OnError
	});
}
function OnSuccess(data) {
	var TableContent = "<table class='sgridstyled'>" +
							"<tr>" +
								"<th>#</th>" +							
								"<th>תאריך</th>" +
								"<th>הודעה</th>" +
							"</tr>";
							
	if(data.d.length == 0)
	{
		DisplayNoneLogs();
		return;
	}		
	for (var i = 1; i <= data.d.length; i++) {
		TableContent += "<tr class='gridrow'>" +
								"<td>" + i + "</td>" +
								"<td>" + data.d[i-1].DateString + "</td>" +
								"<td>" + data.d[i-1].Message + "</td>" +
							"</tr>";
	}
	TableContent += "</table>";

	$("#logDataPanel").html(TableContent);
}
function OnError(data) {
alert("GetDictionaryItems Error");
}

function ClearLogData() {
	$("#txlogsearchkey").val("");
	$("#logDataPanel").html("");
}

function DisplayNoneLogs()
{
	$("#logDataPanel").html("<div class='salert'>לא נמצאו תוצאות !</div>");
}