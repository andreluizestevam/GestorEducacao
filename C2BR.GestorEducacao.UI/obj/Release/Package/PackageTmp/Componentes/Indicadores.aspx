<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Indicadores.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.Indicadores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Indicadores</title>
    <style type="text/css">
        #divIndicadoresContainer { width: 210px; }
        #divIndicadoresContainer #divIndicadoresContent
        {
            height: 163px;
            border: 1px solid #E5E6E9;
            overflow-y: auto;
        }
        #divIndicadoresContainer #divIndicadoresTitle { background-color: #FF831F; }
        #divIndicadoresContainer #divIndicadoresTitle h1 { color: #FFF; }
        .withSeparatorServ
        {
            margin-left: 10px;
            margin-bottom: 4px;
        }
        #ulServicos li a
        {
            text-transform: uppercase;
            font-size: 0.9em;
            margin-left: 4px;
        }
        #ulServicos li a:hover { text-decoration: underline; }
        #ulServicos li img
        {
            margin-top: 0px;
            width: 16px;
            height: 16px;
        }
        #ulServicos { margin-top: 3px; }
    </style>
</head>
<body id="bdyIndicadores">
    <form id="frmIndicadores" runat="server">
    <div id="divIndicadoresContainer">
        <div id="divIndicadoresTitle" class="boxCornerTitle">
            <h1>
                SERVIÇOS</h1>
        </div>
        <div id="divIndicadoresContent">
            <ul id="ulServicos">
                <li id="liMeuPerfil" class="withSeparatorServ" title="Clique para visualizar Informações referentes ao perfil do usuário.">
                    <img src="/Library/IMG/Gestor_ServicosMeuPerfil.png" alt="Icone MEU PERFIL" />&nbsp;
                        <a id="lnkMeuPerfil" href="#">Meu Perfil</a>
                </li>
                <li id="liEnvioSMS" class="withSeparatorServ" title="Clique para enviar mensagem de SMS.">
                    <img src="/Library/IMG/Gestor_ServicosEnvioMsgSMS.png" alt="Icone ENVIO MENSAGEM SMS" />&nbsp;
                        <a id="lnkEnvioSms" href="#">Envio de Mensagem SMS</a>
                </li>
                <li id="liAgendConta" class="withSeparatorServ" title="Clique para visualizar a agenda de contatos.">
                    <img src="/Library/IMG/Gestor_ServicosAgendaContatos.png" alt="Icone AGENDA DE CONTATOS" />&nbsp;
                        <a id="lnkAgendaContatos" href="#">Agenda de Contatos</a>
                </li>
                <li id="liAgendAtivid" class="withSeparatorServ" title="Clique para visualizar a agenda de atividades.">
                    <img src="/Library/IMG/Gestor_ServicosAgendaAtividades.png" alt="Icone AGENDA DE ATIVIDADES" />&nbsp;
                        <a id="lnkAgendaAtividades" href="#">Agenda de Atividades</a>
                </li>
                <li id="liAlertSistem" class="withSeparatorServ" title="Clique para visualizar os informativos.">
                    <img src="/Library/IMG/Gestor_ServicosAlertasSistemicos.png" alt="Icone INFORMATIVOS" />&nbsp;
                    <a id="lnkAlertasSistemicos" href="#">Informativos</a>
                </li>
                <li id="liDownlArquiv" class="withSeparatorServ" title="Clique para visualizar e fazer downloads de arquivos disponíveis.">
                    <img src="/Library/IMG/Gestor_ServicosDownloadArquivos.png" alt="Icone DOWNLOAD DE ARQUIVOS" />&nbsp;
                    <a id="lnkDownloadArquivos" href="#">Download de Arquivos</a> 
                </li>
                <li id="liComoChegar" class="withSeparatorServ" title="Clique para visualizar a rota e o mapa de acesso ao endereço desejado.">
                    <img src="/Library/IMG/Gestor_ComoChegar.png" alt="Icone COMO CHEGAR" />&nbsp;
                    <a id="lnkComoChegar" href="/GEDUC/1000_CtrlAdminEscolar/1400_PesquisasGeoreferenciamento/1401_ComoChegar/GerarComoChegar.aspx?moduloNome=Como%20Chegar">Como chegar</a> 
                </li>
                <li id="li1" class="withSeparatorServ" title="Clique para visualizar e acessar portais WEB referentes a Educação.">
                    <img src="/Library/IMG/Gestor_AcessoFacil.png" alt="Icone ACESSO FÁCIL" />&nbsp;
                    <a id="lnkAcessoFacil" href="#">Acesso Fácil</a> 
                </li>
            </ul>
        </div>
        <div  id="divPopUpAgendaAtividades"></div>
    </div>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {

            $("[id=lnkMeuPerfil]").click(function (e) {
                $("#divLoadShowMeuPerfil").load("/Componentes/MeuPerfil.aspx", function () {
                    $("#divLoadShowMeuPerfil #frmMeuPerfil").attr("action", "/Componentes/MeuPerfil.aspx");
                });

                $("#divLoadShowMeuPerfil").dialog({ title: "Serviços - Meu Perfil", position: [420,35] , modal: true, width: "534px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
            });
            
            $("[id=lnkAlertasSistemicos]").click(function () {
                $("#divLoadShowInformativos").load("/Componentes/Informativos.aspx", function () {
                    $("#divLoadShowInformativos #frmInformativos").attr("action", "/Componentes/Informativos.aspx");
                });
                $("#divLoadShowInformativos").dialog({ title: "Serviços - Informativos", modal: true, width: "710px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
            });

            $("[id=lnkAgendaAtividades]").click(function () {
                $("#divLoadShowAgendaAtividades").load("/Componentes/AgendaAtividades.aspx", function () {
                    $("#divLoadShowAgendaAtividades #frmAgendaAtividades").attr("action", "/Componentes/AgendaAtividades.aspx");
                });
                $("#divLoadShowAgendaAtividades").dialog({ title: "Serviços - Agenda Atividades", modal: true, width: "700px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
            });

            $("[id=lnkEnvioSms]").click(function () {
                $("#divLoadShowEnvioSMS").load("/Componentes/EnvioSMS.aspx", function () {
                    $("#divLoadShowEnvioSMS #frmEnvioSMS").attr("action", "/Componentes/EnvioSMS.aspx");
                });
                $("#divLoadShowEnvioSMS").dialog({ title: "Serviços - Envio SMS", modal: true, width: "360px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
            });

            $("[id=lnkAgendaContatos]").click(function () {
                $("#divLoadShowAgendaContatos").load("/Componentes/AgendaContatos.aspx", function () {
                    $("#divLoadShowAgendaContatos #frmAgendaContatos").attr("action", "/Componentes/AgendaContatos.aspx");
                });
                $("#divLoadShowAgendaContatos").dialog({ title: "Serviços - Agenda Contatos", modal: true, width: "800px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
            });

            $("[id=lnkDownloadArquivos]").click(function () {
                $("#divLoadDownloadArquivos").load("/Componentes/DownloadArquivos.aspx", function () {
                    $("#divLoadDownloadArquivos #frmDownloadArquivos").attr("action", "/Componentes/DownloadArquivos.aspx");
                });
                $("#divLoadDownloadArquivos").dialog({ title: "Serviços - Download Arquivos", modal: true, width: "400px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
            });

            $("[id=lnkAcessoFacil]").click(function () {
                $("#divLoadShowAcessoRapido").load("/Componentes/AcessoFacil.aspx", function () {
                    $("#divLoadShowAcessoRapido #frmAcessoFacil").attr("action", "/Componentes/AcessoFacil.aspx");
                });
                $("#divLoadShowAcessoRapido").dialog({ title: "Serviços - Acesso Fácil", modal: true, width: "940px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
            });

            $('[id=lnkComoChegar]').click(function (e) {
                $('#divContent').hide();

                $('#divPageContainer #divLoadTelaFuncionalidade #divTelaFuncionalidadesCarregando').show();
                $('#ifrmData').hide();

                $('#ifrmData').attr('src', $(this).attr('href'));
                $('#divLoadTelaFuncionalidade').show();

                $('#ifrmData').load(function () {
                    $('#ifrmData').show();
                    $('#divPageContainer #divLoadTelaFuncionalidade #divTelaFuncionalidadesCarregando').hide();
                });

                $("#divBreadCrumb").load("/Componentes/BreadCrumb.aspx?moduloId=572");

                $('#divLoginInfo').hide();

                // Previne a execução do link
                e.preventDefault();
                return false;
            });
        });
    </script>
</body>
</html>