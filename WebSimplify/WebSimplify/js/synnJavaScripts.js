$(document).ready(function ()
{
    ArrangeTableFilters(); // finds grid inner tbody and sets the class
    ArrangeASPcalendar();

    $(".tablefilter").on("keyup", function () {
        var value = $(this).val().toLowerCase();
            $(".datatofilter tr").filter(function () {
                {
                    var rowText = $(this).text().toLowerCase();
                    $(this).toggle(rowText.lenght > 0 && rowText.indexOf(value) > -1);
                }
            });
    });

});
function ArrangeTableFilters() {
    $(".tabletofilter").find("tbody").addClass('datatofilter');
}

function ArrangeASPcalendar()
{
    var headersTable = $(".aspcalendar").find("tbody").find("tbody");
    headersTable.css('background-color', '#9797FB');
    headersTable.find('a').parent().addClass("aspcalendarheaderactions");
}