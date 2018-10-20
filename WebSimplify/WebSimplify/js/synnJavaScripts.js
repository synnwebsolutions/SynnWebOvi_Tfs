$(document).ready(function ()
{
    $(".tabletofilter").find("tbody").addClass('datatofilter');
    ClearTableData();
    //ArrangeRows();
    $(".tablefilter").on("keyup", function () {
        var value = $(this).val().toLowerCase();
            $(".datatofilter tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
    });

});
function ClearTableData() {
    $(".datatofilter tr").filter(function () {
        $(this).toggle("false")
    });
}

function ArrangeRows()
{
    var totalwidth = $(".srow").width();
    var elementsCount = $(".srow").children(".spanel").length;
    var singleItemWidth = parseInt(totalwidth) / parseInt(elementsCount);

    $(".srow").children(".spanel").each(function () {
        $(this).css('width', singleItemWidth);
    });
   
}