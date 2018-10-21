$(document).ready(function ()
{
    ArrangeTableFilters();
    ClearTableData();
    ArrangeASPcalendar();

    $(".tablefilter").on("keyup", function () {
        var value = $(this).val().toLowerCase();
            $(".datatofilter tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
    });

});
function ArrangeTableFilters() {
    $(".tabletofilter").find("tbody").addClass('datatofilter');
}
function ClearTableData() {
    $(".datatofilter tr").filter(function () {
        $(this).toggle("false")
    });
}

function ArrangeASPcalendar()
{
    var headersTable = $(".aspcalendar").find("tbody").find("tbody");
    headersTable.css('background-color', '#9797FB');
    headersTable.find('a').parent().addClass("aspcalendarheaderactions");
}