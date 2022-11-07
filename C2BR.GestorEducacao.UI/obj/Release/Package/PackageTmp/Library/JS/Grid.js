/// <reference path="JQuery/jquery.3.2-vsdoc2.js" />
$(document).ready(function() {
    $("#ctl00_hfSelectedRow").val(-1);
    $(".rowStyle, .alternatingRowStyle").click(function() {
        $(".rowStyle, .alternatingRowStyle").removeClass("selectedRowStyle");
        $(this).addClass("selectedRowStyle");
    });
});