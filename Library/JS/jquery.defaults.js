// Configurações padrão para o jquery e seus componentes
/// <reference path="../../DevelopmentResources/jquery.3.2-vsdoc2.js" />

// Cria uma funcao para executar sempre que a chamada ajax for feita com sucesso
$.bindDefaultsAjaxSuccess = function (data, textStatus, XMLHttpRequest) {
    setTimeout("defultAjaxSuccessActions()", 1000);
};

// Cria uma funcao para executar sempre que ocorrer um erro nas chamadas ajax
$.binDefaultsAjaxError = function (xMLHttpRequest, textStatus, errorThrown) {
    //window.location = "/login.aspx";
    $("#divDialogMessages").html(xMLHttpRequest.responseText);
    $("#divDialogMessages").dialog({ title: 'Oops, Ocorreu um erro na aplicação, clique em "Enviar Relatório de Erro" para informar nossa equipe de suporte.', modal: true, width: 975, height: 450, buttons: { "Enviar Relatório de Erro": function () { $(this).dialog("close"); } } });
//        alert(xMLHttpRequest.responseText);
//        alert(textStatus);
//        alert(errorThrown);
}

$.ajaxSetup({ success: $.bindDefaultsAjaxSuccess, error: $.binDefaultsAjaxError });

function defultAjaxSuccessActions() {
    if ($.fn.corner) {
        if ($.browser.msie) {
            $('.boxRoundCorner').corner('keep');
            $('.boxCornerTitle').corner('top');
        }
        else {
            $('.boxCornerTitle').corner('top');
            $('.boxRoundCorner').corner();
        }
    }
}

$(document).ready(function () {
    defultAjaxSuccessActions();
    $(".divCalendar").datepicker();
    $("input.campoData").datepicker();
    //$("input.mascaradecimal").datepicker();
    $("input.mascaradecimal").keyup(function () {
        var val = $(this).val();

        if (isNaN(val)) {
            val = val.replace(/[^0-9\,]/g, '');
            if (val.split(',').length > 2) {
                val = val.replace(/\,+$/, "");
            }
        }

        $(this).val(val);
    });
});