// 
// Biblioteca de métodos do Portal Educação
//

function LogOut() {
    var url = self.location.href;
    window.location = url.replace("Default", "Logout");
}

function showHomePage() {    
    //$('.liAreaConhecimentoItemAlternating, .liAreaConhecimentoItem').removeClass('moduloSelected');
    $('#divCarregaModalidades, #divLoadTelaFuncionalidade').hide();

    // Recarrega os componentes
    $("#divLoadAreasConhecimento").load("/Navegacao/AreasConhecimento.aspx");
    $("#divIndicadores").load("/Componentes/Indicadores.aspx");
    //$("#divLoadMeusAtalhos").load("/Componentes/MeusAtalhos.aspx");
    
    $('#divCarregaModalidades').load($("#ulAreasConhecimento li").children(":first").attr('href'));

    //$('#divLoadMeusAtalhos').hide();
    $('#divDashboardContent').remove();
    ///////

    $('#divContent, #divLoginInfo').show();
}

function openAsIframe(url) {
    $('#divContent').hide();

    $('#divPageContainer #divLoadTelaFuncionalidade #divTelaFuncionalidadesCarregando').show();
    $('#ifrmData').hide();

    $('#ifrmData').attr('src', url);
    $('#divLoadTelaFuncionalidade').show();

    $('#ifrmData').load(function () {
        $('#ifrmData').show();
        $('#divPageContainer #divLoadTelaFuncionalidade #divTelaFuncionalidadesCarregando').hide();
    });

    // Previne a execução do link
    //e.preventDefault();
    return false;
}
    function avisoGeralSistema(mensagem) {
        $('#avisoGeralSistema').dialog({
            resizable: false,
            title: "Aviso importante do sistema!!!",
            modal: true,
            autoOpen: true,
            closeOnEscape: false,
            draggable: false,
            position: { my: "center", at: "center", of: window },
            buttons: {
                "Entendido": function () {
                    $(this).dialog("close");
                }
            },
            open: function (event, ui) {
                $(".ui-dialog-titlebar-close").hide();
                $("'#avisoGeralSistema p:first").html(mensagem);
            }
        });
    }

