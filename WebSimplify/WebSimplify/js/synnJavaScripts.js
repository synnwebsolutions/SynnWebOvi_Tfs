$(document).ready(function ()
{
    ArrangeTableFilters(); // finds grid inner tbody and sets the class
    ArrangeASPcalendar();
    HandleDeleteShiftButtons();
    HandleChartCredits();
});
function ArrangeTableFilters() {
    $(".tabletofilter").find("tbody").addClass('datatofilter');

    $(".tablefilter").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $(".datatofilter tr").filter(function () {
            {
                var rowText = $(this).text().toLowerCase();
                $(this).toggle(rowText.length > 0 && rowText.indexOf(value) > -1);
            }
        });
    });
}

function ArrangeASPcalendar()
{
    var headersTable = $(".aspcalendar").find("tbody").find("tbody");
    headersTable.css('background-color', '#9797FB');
    headersTable.find('a').parent().addClass("aspcalendarheaderactions");
}

function HandleDeleteShiftButtons() {
    $(".btndlt").click(function (e)
    {
        var val = e.target.id;
  
        $.ajax({
            type: "POST",
            url: "Shifts.aspx/PerformDelete",
            data: "{'btnidentifier': '" + val + "'}",
            dataType: 'Json',
            contentType: "application/Json; charset=utf-8",
            success: OnAddSuccess,
            error: OnAddError
        });
    });
}
function OnAddSuccess(data) {

    alert("פעולה בוצעה בהצלחה");
}

function OnAddError(xhr, ajaxOptions, thrownError) {
    var err = eval("(" + xhr.responseText + ")");
    alert(err.Message);
    //alert("תקלה - פעולה לא בוצעה בהצלחה", "הוספה");
}

function HandleChartCredits()
{
    alert("HandleChartCredits");
    $.ajax({
        type: "POST",
        url: "CreditStats.aspx/GetChartData",
        data: "{'chartid': 'chartCredit'}",
        dataType: 'Json',
        contentType: "application/Json; charset=utf-8",
        success: OnChartSuccess,
        error: OnChartError
    });
}

function OnChartSuccess(data) {
    alert(data.data);
    var chart = new CanvasJS.Chart("chartCredit", {
        animationEnabled: true,
        theme: "light2",//light1
        title: {
            text: data.ChartTitle
        },
        data: [
        {
            // Change type to "bar", "splineArea", "area", "spline", "pie",etc.
            type: "line",
            dataPoints: data.ChartData
        }
        ]
    });
    chart.render();
}