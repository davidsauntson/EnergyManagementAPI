/*----------------------------------------------------------
DAVID SAUNTSON

ENERGY MANAGER WEB APPLICATION
HANDWRITTEN
JAVASCRIPT FILE (.js)

Custom Scripts for Tariff Pages
----------------------------------------------------------*/

function showBandEditors(checkBox) {
    if (checkBox.checked) {
        $("#singleRate").slideUp(1000);
        $("#bandedRate").fadeIn(1000);
    }
    else {
        $("#bandedRate").fadeOut(1000);
        $("#singleRate").slideDown(1000);
        $("#upper1").val(0);
        
    };
}

function showBand(bandNo) {
    var id = "#rowBand" + bandNo;
    $(id).fadeIn(1000);
}

function showToolTip(x) {
    var id = "#" + x;
    $(id).fadeIn(300);
}

function hideToolTip(x) {
    var id = "#" + x;
    $(id).fadeOut(500);
}

$(function () {
    $("#upper1").change(function () {

        var newValue = 1 + parseInt($("#upper1").val(), 10);
        $("#lower2").attr("readonly", false);
        $("#lower2").val(newValue);
        $("#lower2").attr("readonly", true);
    });

    $("#upper2").change(function () {
        var newValue = 1 + parseInt($("#upper2").val(), 10);
        $("#lower3").attr("readonly", false);
        $("#lower3").val(newValue);
        $("#lower3").attr("readonly", true);
    });

    $("#rateHolder").change(function () {
        var value = parseFloat($("#rateHolder").val(), 10);
        $("#rate1").val(value);
    });

    $("#rate1").change(function () {
        var value = parseFloat($("#rate1").val(), 10);
        $("#rateHolder").val(value);
    });
});
