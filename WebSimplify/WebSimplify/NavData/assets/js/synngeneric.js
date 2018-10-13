/* wedding */
function GetWeddingData() {
	
	var val =$("#txwedsearchkey").val();
	if(val == '')
	{
		DisplayNoneWedd();
		return;
	}

	$.ajax({
		type: "POST",
		url: "Main.aspx/GetWeddingItems",
		data: "{'guesttext': '" + val + "'}",
		dataType: "json",
		contentType: "application/json; charset=utf-8",
		success: OnWeddSuccess,
		error: OnWeddError
	});
}
function OnWeddSuccess(data) {
	
	
	var TableContent = "<table class='sgridstyled'>" +
							"<tr>" +
								"<th>#</th>" +							
								"<th>שם</th>" +
								"<th>סכום</th>" +
							"</tr>";
		
		
	if(data.d.length == 0)
	{
		DisplayNoneWedd();
		return;
	}	

	for (var i = 1; i <= data.d.length; i++) {
		TableContent += "<tr class='gridrow'>" +
								"<td>" + i + "</td>" +
								"<td>" + data.d[i-1].Name + "</td>" +
								"<td>" + data.d[i-1].Amount + "</td>" +
							"</tr>";
	}
	TableContent += "</table>";

	$("#WeddingDataPanel").html(TableContent);
}
function OnWeddError(data) {
alert("GetWeddingData Error");
}

function ClearWeddingData() {
	$("#txwedsearchkey").val("");
	$("#WeddingDataPanel").html("");
}

function DisplayNoneWedd()
{
	$("#WeddingDataPanel").html("<div class='salert'>לא נמצאו תוצאות !</div>");
}

/* End Wedding */


/* Start Log */
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
		success: OnLogSuccess,
		error: OnLogError
	});
}
function OnLogSuccess(data) {
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
function OnLogError(data) {
alert("GetLogData Error");
}

function ClearLogData() {
	$("#txlogsearchkey").val("");
	$("#logDataPanel").html("");
}

function DisplayNoneLogs()
{
	$("#logDataPanel").html("<div class='salert'>לא נמצאו תוצאות !</div>");
}
/* End Log */

/* Start Dictionary */
function GetDictionaryData() {
	
	var val =$("#txsearchkey").val();
	if(val == '')
	{
		DisplayNoneDics();
		return;
	}
	$.ajax({
		type: "POST",
		url: "Main.aspx/GetDictionaryItems",
		data: "{'searchtext': '" + val + "'}",
		dataType: "json",
		contentType: "application/json; charset=utf-8",
		success: OnDicSuccess,
		error: OnDicError
	});
}
function OnDicSuccess(data) {
	var TableContent = "<table class='sgridstyled'>" +
							"<tr>" +
								"<th>#</th>" +							
								"<th>שם</th>" +
								"<th>ערך</th>" +
							"</tr>";
							
	if(data.d.length == 0)
	{
		DisplayNoneDics();
		return;
	}		
	for (var i = 1; i <= data.d.length; i++) {
		TableContent += "<tr class='gridrow'>" +
								"<td>" + i + "</td>" +
								"<td>" + data.d[i-1].DictionaryKey + "</td>" +
								"<td>" + data.d[i-1].DictionaryValue + "</td>" +
							"</tr>";
	}
	TableContent += "</table>";

	$("#DictionaryDataPanel").html(TableContent);
}
function OnDicError(data) {
alert("GetDictionaryItems Error");
}

function ClearDictionaryData() {
	$("#txsearchkey").val("");
	$("#DictionaryDataPanel").html("");
}

function DisplayNoneDics()
{
	$("#DictionaryDataPanel").html("<div class='salert'>לא נמצאו תוצאות !</div>");
}
/* End Dictionary */