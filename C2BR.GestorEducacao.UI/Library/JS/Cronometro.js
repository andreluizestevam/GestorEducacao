//$(document).ready(function () {
//    //O Cronometro deve ser atualizado a cada postback
//    //Cronometro parado!
//    if ($("#hidTimerId").val() == "0") {
//        hora = $("#hidHoras").val();
//        minuto = $("#hidMinutos").val();
//        segundo = $("#hidSegundos").val();
//        $("#lblHora").text((hora < 10 ? "0" + hora : hora) + ":" + (minuto < 10 ? "0" + minuto : minuto) + ":" + (segundo < 10 ? "0" + segundo : segundo));
//        $('#divCronometro').show();
//    } //Cronometro rodando!
//    else if ($("#hidTimerId").val() != "1" && $("#hidHoras").val() != "" && $("#hidMinutos").val() != "" && $("#hidSegundos").val() != "") {
//        hora = $("#hidHoras").val();
//        minuto = $("#hidMinutos").val();
//        segundo = $("#hidSegundos").val();
//        IniciarCronometro();
//    }
//});

//var timerID = 0;
//var segundo = 0;
//var minuto = 0;
//var hora = 0;

//function IniciarCronometro() {
//    $('#divCronometro').show();
//    Cronometro();
//}

//function PararCronometro() {
//    clearTimeout(timerID);
//    $("#hidTimerId").val("-1");
//}

//function ZerarCronometro() {
//    $("#hidTimerId").val("");
//    $("#hidHoras").val("");
//    $("#hidMinutos").val("");
//    $("#hidSegundos").val("");
//    $('#divCronometro').hide();
//}

//function Cronometro() {
//    if (segundo <= 58)
//        segundo++;
//    else if (segundo == 59 && minuto <= 58) {
//        segundo = 0;
//        minuto++;
//    }
//    else if (minuto == 59) {
//        segundo = 0;
//        minuto = 0;
//        hora++;
//    }
//    else if (hora == 23)
//        hora = 0;

//    $("#hidHoras").val(hora);
//    $("#hidMinutos").val(minuto);
//    $("#hidSegundos").val(segundo);

//    $("#lblHora").text((hora < 10 ? "0" + hora : hora) + ":" + (minuto < 10 ? "0" + minuto : minuto) + ":" + (segundo < 10 ? "0" + segundo : segundo));

//    timerID = setTimeout("Cronometro();", 1000)
//    
//    $("#hidTimerId").val(timerID);
//} 

var intervalo;

function IniciarCronometro() {
    $('#divCronometro').show();
    Cronometro();
    var timerID = 0; 
    var s = 0;
    var m = 0;
    var h = 0;
    intervalo = window.setInterval(function () {
        if (s == 60) { m++; s = 0; }
        if (m == 60) { h++; s = 0; m = 0; }
        if (h < 10) document.getElementById("hora").innerHTML = "0" + h + "h"; else document.getElementById("hora").innerHTML = h + "h";
        if (s < 10) document.getElementById("segundo").innerHTML = "0" + s + "s"; else document.getElementById("segundo").innerHTML = s + "s";
        if (m < 10) document.getElementById("minuto").innerHTML = "0" + m + "m"; else document.getElementById("minuto").innerHTML = m + "m";
        s + 1; 
    },1000);
}
var timerID = 0;
function PararCronometro() {
    window.clearInterval(intervalo);
    clearTimeout(timerID);
    $("#hidTimerId").val("1");
}



function ZerarCronometro() {

    $("#hidTimerId").val("1");
    $("#hidHoras").val("0");
    $("#hidMinutos").val("0");
    $("#hidSegundos").val("0");
    $('#divCronometro').hide();
}
window.onload = tempo;