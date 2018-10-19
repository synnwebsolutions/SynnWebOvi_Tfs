$(document).ready(function ()
{
    $(".tabletofilter").find("tbody").addClass('datatofilter');
    ClearTableData();

    $(".tablefilter").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        if (value == null || value == '')
            ClearTableData();
        else {
            $(".datatofilter tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
        }
    });

});
function ClearTableData() {
    $(".datatofilter tr").filter(function () {
        $(this).toggle("false")
    });
}