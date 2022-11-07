<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="shortcut icon" href="/Library/IMG/Gestor_GloboIcone.ico" />
    <title>GE SME MFumaça</title>
    <link href="/Library/CSS/default.css" rel="stylesheet" type="text/css" />
    <link href="/Library/CSS/jScrollPane.css" rel="stylesheet" type="text/css" />
    <link href="/Library/CSS/Jquery.UI/customtheme/default.css" rel="stylesheet" type="text/css" />
    <script src="/Library/JS/jquery.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.ui.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.form.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.corner.js" type="text/javascript"></script>
    <script src="/Library/JS/ui.datepicker-pt-BR.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.jScrollPane.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.mousewheel.min.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.limit-1.2.source.js" type="text/javascript"></script>
    <script src="Library/JS/gestoreducacao.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.defaults.js" type="text/javascript"></script>
    <style type="text/css">
        .bodyPrincipal 
        { 
        	/*width: 1264px; */
        	width: 100%; 
        	height: 650px;
        	background-image: url("../Library/IMG/bkg_body.png");
            background-repeat: repeat-x;
            margin: 2px auto 0;
        }
        
        .ui-dialog .ui-dialog-title
        {
            font-weight: bold !important;
            font-size: 1.2em !important;
        }
        #divPageContainer
        {
            border-top: solid 10px #E6E5E5;
            margin: 0 auto;
            width: 1004px;
            height: 600px;
            background-color: White;
            border-left: 2px solid white;
            border-right: 2px solid white;
        }
        #divPageContainer #divHeader
        {
            overflow: hidden;
            height: 80px;
            padding-top: 5px;
            margin-left: 2px;
            margin-right: 2px;
        }
        #divPageContainer #divHeader #divUserInfo #ulUserInfo
        {
            padding-left: 5px;
            padding-top: 10px;
            width: 370px;
            margin-top: -15px;
            margin-bottom: 3px;
        }
        #divPageContainer #divHeader #divUserInfo img
        {
            float: left;
            margin-right: 10px;
            padding-right: 10px;
            width: 200px;
        }
        #divPageContainer #divHeader #divUserInfo ul li span.lblNomeUsuario
        {
            font-weight: bold;
            text-transform: uppercase;
        }
        #divPageContainer #divHeader #divIntituicaoInfo
        {
            overflow: auto;
            text-align: right;
            float: right;
        }
        #divPageContainer #divHeader #divIntituicaoInfo img
        {
            width: 55px;
            height: 55px;
            float: right;
        }
        #divPageContainer #divHeader #divIntituicaoInfo ul
        {
            display: inline-block;
            padding-right: 10px;
            padding-top: 2px;
            float: left;
        }
        #divPageContainer #divHeader #divIntituicaoInfo ul #liNomeOrgao span
        {
            font-family: arial;
            font-size: 1.1em;
            font-weight: bold;
        }
        #divPageContainer #divHeader #divIntituicaoInfo ul #liUnidadeSelecionada span
        {
            color: Black;
            font-family: Arial;
            font-size: 1.1em;
            /*font-weight: bold;*/
            text-transform: uppercase;
        }
        .divLoginInfo a { vertical-align: middle; }
        #divBarraLoginInfo
        {
            position: absolute;
            top: 75px;
            margin-left: 2px;
        }
        .divLoginInfo a { color: #009ACD !important; }
        .divLoginInfo
        {
            background-color: #E6E6FA;
            float: right;
            padding: 2px;
            width: 1000px;
        }
        .divLoginInfo ul { float: right; }
        .divLoginInfo ul li { float: left; }
        .divLoginInfo img
        {
            width: 15px;
            height: 15px;
        }
        #divPageContainer #divContent
        {
            padding: 5px 0;
            height: 525px;
            overflow: visible;
            background-color: White;
            border-left: 2px solid white;
            border-right: 2px solid white;
        }
        #divPageContainer #divFooter
        {
            background-color: #667AB3;
            color: #FFF;
            overflow: auto;
            padding: 3px;
            border-left: 2px solid white;
            border-right: 2px solid white;
            width: 994px;
        }
        #divPageContainer #divFooter ul li { float: left; }
        #divPageContainer #divFooter ul li span { color: #FFF; }
        #divSubTitle
        {
            float: left;
            left: -230px;
            left: 0px !important;
            position: relative;
            top: 15px;
            top: 1px !important;
            background-color: #E6E6FA;
            color: #20B2AA;
            height: 20px;
            width: 530px;
        }
        #ulLinksAjuda
        {
            float: left;
            left: -243px;
            top: 15px;
            position: relative;
            display: none;
        }
        #ulLinksAjuda li
        {
            float: left;
            padding: 0 5px;
        }
        #ulLinksAjuda li a
        {
            color: #F68719;
            text-transform: uppercase;
        }
        #ulLinksAjuda li a:hover { text-decoration: underline; }
        #divLoadTelaFuncionalidade
        {
            display: none;
            min-height: 535px;
            background-color: White;
        }
        .ifrmData
        {
            background-color: Transparent;
            width: 100%;
            height: 530px;            
        }
        .ifrmData html body { background-color: Transparent; }
        .divLoginInfo .withSeparator:after
        {
            content: "|";
            color: #000;
            padding: 0 5px;
        }
        #ulLinksAjuda .withSeparator:after
        {
            content: "|";
            color: #F68719;
            padding: 0 0 0 5px;
        }
        .divInline { display: inline; }
        #divPageContainer #divContent #divLoadMeusAtalhos
        {
            float: right;
            margin-top: 5px;
        }
        #divPageContainer #divContent #divLoadIndicadores { margin-top: 8px; }   
        #divPageContainer #divLoadTelaFuncionalidade #divTelaFuncionalidadesCarregando
        {
            left: 50%;
            margin-top: 32px;
            position: relative;
            top: 10px;
            display: none;
        }        
        #ulDescricao a { color: #FFFFFF; }
        #ulDescricao { float: right; }
        .imagemUsuario
        {
            border-width: 0 !important;
            height: 50px !important;
            margin-right: 0 !important;
            margin-top: 0px !important;
            padding-right: 7px !important;
            width: 38px !important;
            margin-left: -5px;
        }
        .liImagemUsuario { width: 45px; margin-top: 5px; }
        #liUltAcesso { margin-top: 3px; margin-bottom: 2px; }
        #liUltAcesso a
        {
            color: #3CB371;
            font-size: 1.0em;
        }
        #liSeparator
        {
            height: 2px;
            margin-top: 7px;
            margin-left: -5px;
        }
        .liTrocarEscola a, .liSairSistema a { color: #EE2C2C !important; }
        .liSairSistema, #liPossoAjudar { margin-left: 2px; }
        #lblLocalizacao
        {
            font-size: 1.1em;
            margin-top: 2px;
            margin-left: 2px;
        }
        #imgUltAcesso
        {
            border: none !important;
            height: 14px;
            width: 14px !important;
            margin-right: -4px !important;
            margin-top: -1px;
            padding-right: 0px !important;
        }
        #ulIntituicaoInfo li a:hover { text-decoration: underline; }
        #liServicos a, .liTrocarEscola a { color: #FF831F !important; }
        #lnkUnidadeAtual { color: black; }
        #divBreadCrumb { float: left; }
        #divServicos
        {
            margin-top: 5px;
            background-color: #FFFFFF;
            border: 1px solid #CCCCCC;
            display: none;
            padding: 5px;
            position: absolute;
            width: 190px;
        }
        #divLeft
        {
        	float:left;
        	background-color:#F5F5F5;
        	width: 10%;
        	height: 648px;
        }
        #divRight
        {
        	float:right;
        	background-color:#F5F5F5;
        	width: 10%;
        	height: 648px;
        }
        .divSessionTimeoutWarning
        {
        	position: fixed; 
        	left: 500px; 
        	top: 100px; 
        	background-color: #FDF5E6; 
        	border: 1px solid #BEBEBE;
        	padding: 10px 20px 10px 20px;
        }
        
       
        #divDialogValidacao
        {
            height: 359px;
            width: 610px;
            background-repeat:no-repeat;
            background-position:top;
            background-attachment:scroll;
            z-index: 99999;
        }
    </style>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            MostrarValidacao();
        });
        function ControleUsuarioOnline(endereco) {
            $("#divDialogValidacao").hide();
            $("#liTopoMatriculaUsuario").hide();
            $("#liTopoUnidadeUsuario").hide();
            $("#liUltAcesso").hide();
            $("#liTopoTrocarEscola").hide();
            $("#liSistemasPublicosInt").hide();
            $("#liTopoTrocarEscola").hide();
            $("#liServicos").hide();
            $("#liSistemasPublicos").hide();
            $("#liTopoTrocarEscolaPublica").hide();

            openAsIframe(endereco);
        }
        function MostrarValidacao()
        {
            var situacao = "<%=situacaoValidacao%>";
            var usuario = "<%=situacaoUsuario%>";
            var endereModuloOnline = "<%=enderecoModulo%>";
            var nomeClasse = "";
            var botao;
            var fechar = true;
            var fecharFuncao = function () { };
            var imagemSituacao;
            var alturaBotao = 0;
            ///Controle iframe
            if (endereModuloOnline != "") {
                ControleUsuarioOnline(endereModuloOnline);
            }
            else
            {
                if ((situacao != "N" && situacao != "") || (usuario != "" && usuario == "V"))
                {
                    if(situacao == "I" )
                    {
                        imagemSituacao = 'url(/Library/IMG/Home/EW_MsgTelaLogin_ChaveVencida.jpg)';
                        fechar = false;
                    }
                    else if (situacao == "A")
                    {
                        imagemSituacao = 'url(/Library/IMG/Home/EW_MsgTelaLogin_ChaveAtrasada.jpg)';
                        fechar = true;

                    }
                    else if (situacao == "V" || situacao == "R" || usuario == "V")
                    {
                        imagemSituacao = 'url(/Library/IMG/Home/EW_MsgTelaLogin_NovaVersao.jpg)';
                        fechar = true;
                    }
                }
                if (usuario == "S") {
                    $("#divDialogValidacao").css('background-image', 'url(/Library/IMG/Home/EW_MsgTelaLogin_PrimeiroAcesso.jpg)');
                    fecharFuncao = function () {
                        $("#divDialogValidacao").css('background-image', imagemSituacao);
                        mostrarAvisoValidacao(fechar, function () { });
                    };
                    if ($("#divDialogValidacao").css('background-image') != undefined && $("#divDialogValidacao").css('background-image') != "")
                        mostrarAvisoValidacao(true, fecharFuncao);
                }
                else if ((situacao != "N" && situacao != "") || (usuario != "" && usuario == "V")) {
                    $("#divDialogValidacao").css('background-image', imagemSituacao);
                    if ($("#divDialogValidacao").css('background-image') !=  undefined && $("#divDialogValidacao").css('background-image') != "")
                        mostrarAvisoValidacao(fechar, function () { });
                }
                else {
                    $("#divDialogValidacao").hide();
                }
            
            }

        }

        function mostrarAvisoValidacao(fechar, fecharFuncao) {
            $("#divDialogValidacao").dialog({
                resizable: false,
                height: 390,
                width: 610,
                title: "Aviso !!!",
                modal: true,
                autoOpen: true,
                closeOnEscape: false,
                draggable: false,
                position: { my: "center", at: "center", of: window },
                open: function (event, ui) {
                    if (fechar)
                        $(".ui-dialog-titlebar-close").show();
                    else
                        $(".ui-dialog-titlebar-close").hide();
                },
                close: fecharFuncao
            });
        }

        $(function () {
            setInterval(KeepSessionAlive, 10000);
        });

        function KeepSessionAlive() {
            $.post("/KeepSessionAlive.ashx", null, null);
        };

        //Set timeouts for when the warning message should be displayed, and what should happen when the session actually expires.
        function BodyOnLoad() {
            timeoutIDWarning = window.setTimeout('ShowSessionTimeoutWarning()', '<%=iWarningTimeoutInMilliseconds%>');
            timeoutIDExpired = window.setTimeout('ShowSessionExpiredNotification()', '<%=iSessionTimeoutInMilliseconds%>');
            //MostrarValidacao();
        }

        

        //Notify the user that his session is ABOUT to expire.  Do so by making our warning div tag visible.
        function ShowSessionTimeoutWarning() {
            var divSessionTimeoutWarning = document.getElementById('<%=divSessionTimeoutWarning.ClientID%>');            
            divSessionTimeoutWarning.style.display = 'inline';
        }

        //Notify the user that his session HAS expired.
        function ShowSessionExpiredNotification() {
            var divSessionTimeoutWarning = document.getElementById('<%=divSessionTimeoutWarning.ClientID%>');    
            window.location = '<%=sTargetURLForSessionTimeout%>';
        }

        function ResetClientSideSessionTimers() {
            var divSessionTimeoutWarning = document.getElementById('<%=divSessionTimeoutWarning.ClientID%>');
            divSessionTimeoutWarning.style.display = 'none';

            //Reset timers so we can warn the user the NEXT time the session is about to expire.
            window.clearTimeout(timeoutIDWarning);
            window.clearTimeout(timeoutIDExpired);
            timeoutIDWarning = window.setTimeout('ShowSessionTimeoutWarning()', '<%=iWarningTimeoutInMilliseconds%>');
            timeoutIDExpired = window.setTimeout('ShowSessionExpiredNotification()', '<%=iSessionTimeoutInMilliseconds%>');            
        }
    </script>
</head>
<body class="bodyPrincipal" onload="BodyOnLoad()">
 
    <div id="divDialogMessages"></div>
    <div id="divPageContainer">
        <form id="frmMain" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
        </asp:ScriptManager>
        <div>
            <%--In a real application, use a CSSClass and set these display properties in a CSS file, not inline.--%>
            <div id="divSessionTimeoutWarning" class="divSessionTimeoutWarning" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblSessionWarning" runat="server" Text="Atenção. Sua sessão vai expirar e você será redirecionado para tela de Login."></asp:Label>
                        <br />
                        <asp:Button ID="btnContinueWorking" style="margin-left: 100px; margin-top: 10px;" runat="server" Text="Continuar Trabalhando" OnClientClick="ResetClientSideSessionTimers()" OnClick="btnContinueWorking_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="divHeader" class="divHeader">
            <div id="divIntituicaoInfo" class="divIntituicaoInfo">
                <ul id="ulIntituicaoInfo" class="ulIntituicaoInfo">
                    <li id="liNomeOrgao">
                        <asp:Label runat="server" ID="lblNomeOrgao" Text=""></asp:Label>
                    </li>
                    <li id="liUnidadeSelecionada" class="liUnidadeSelecionada">
                       <a id="lnkUnidadeAtual" href="#"> <asp:Label runat="server" ID="lblUnidadeSelecionada" Text=""></asp:Label> </a></li>
                    <li>
                        <asp:Label runat="server" ID="lblCidadeUFUnidade" Text=""></asp:Label></li>
                    <li>
                        <asp:Label runat="server" ID="lblTelefoneUnidade" Text=""></asp:Label>
                    </li>                                                            
                </ul>
                <asp:Image runat="server" ID="imgOrgao" CssClass="imgOrgao" AlternateText="Brasão"
                    ImageUrl="~/Library/IMG/Gestor_BrasaoPadrao.png" />
            </div>
            <div id="divUserInfo" class="divUserInfo">                
                <ul id="ulUserInfo" class="ulUserInfo" style="float:left;">
                    <li class="liImagemUsuario">
                        <asp:Image ID="imagemUsuario" CssClass="imagemUsuario" runat="server" /></li>
                    <li id="liUserName"><span>
                        <asp:Label runat="server" ID="lblNomeUsuario" CssClass="lblNomeUsuario" Text=""></asp:Label></span>
                    </li>
                    <li id="liTopoMatriculaUsuario">
                        <asp:Label runat="server" ID="lblMatriculaUsuario" Text=""></asp:Label>
                        <span>&nbsp;-&nbsp;</span>
                        <asp:Label runat="server" ID="lblFuncaoUsuario" Text=""></asp:Label>
                    </li>
                    <li id="liTopoUnidadeUsuario">
                        <asp:Label runat="server" ID="lblUnidadeFuncUsuario" Text=""></asp:Label>
                    </li>    
                    <li id="liUltAcesso" title="Clique para visualizar os últimos acessos.">
                        <img id="imgUltAcesso" src="/Library/IMG/Gestor_CheckSucess.png" alt="Icone Últimos Acessos" />&nbsp;
                        <a href="#">Últimos Acessos</a> </li>              
                </ul>
                <ul style="width:220px; margin-left: 20px; padding-top: 9px; height: 40px; padding-right: 4px; float: left;">
                    <li><img style="width:205px;" alt="Gestor Logo" src="/Library/IMG/Logo_Portal_Educacao.png" /></li>                   
                </ul>
            </div>
            <div id="divLoginInfo" class="divLoginInfo">
                <ul>
                    <li id="li1"style="margin-left: 40px;" title="Clique para utilizar o Posso Ajudar.">
                            <a href="#">POSSO AJUDAR?</a> </li>
                    <li id="liSistemasPublicosInt"style="margin-left: 177px;" title="Clique para acessar os Sistemas.">
                        <img src="/Library/IMG/Gestor_AcessoSistemas.png" alt="Icone Sistemas" style="margin-right: -2px;" />&nbsp;
                            <a href="#" style="color:#FF831F !important;">SISTEMAS</a> </li>

                    <li id="liTopoTrocarEscola" class="liTrocarEscola" style="margin-left: 15px;" title="Clique para visualizar as Unidades.">
                        <img src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade" />&nbsp;<a
                            href="#">TROCAR UNIDADE</a> </li>
                    <li class="liSairSistema" style="margin-left: 15px;" title="Clique para encerrar sua sessão.">
                        <img src="/Library/IMG/Gestor_SairSistema.png" alt="Icone Sair" />&nbsp;<a href="javascript:window.location='/logout.aspx'">SAIR</a>
                    </li>
                </ul>
            </div>
        </div>
        <div id="divLoadTelaFuncionalidade">
            <div id="divBarraLoginInfo">
                <div id="divLoginInfo2" class="divLoginInfo" style="width:996px !important;">
                    <div id="divBreadCrumb">
                    </div>
                    <ul>                        
                        <li id="liPossoAjudar" class="withSeparator" style="margin-left: 10px;" title="Clique para utilizar o Posso Ajudar.">
                            <a id="lnkPossoAjudar" href="#">POSSO AJUDAR?</a>
                        </li>
                        <li id="liComoFazer" title="Clique para utilizar o Como Fazer.">
                            <a id="lnkComoFazer" href="#">COMO FAZER?</a>
                            <div id="divComoFazer" style="position:absolute;">
                            </div>
                        </li>
                        <li id="liServicos" style="margin-left: 15px;" title="Clique para visualizar os Serviços.">
                            <img src="/Library/IMG/Gestor_Serviços.png" alt="Icone Serviços" style="margin-right: -5px;" />&nbsp; <a href="#">
                                SERVIÇOS</a>
                            <div id="divServicos">
                                <ul id="ulServicos">
                                    <li id="liMeuPerfil" class="withSeparatorServ" title="Clique para visualizar Informações referentes ao perfil do usuário.">
                                        <img src="/Library/IMG/Gestor_ServicosMeuPerfil.png" alt="Icone MEU PERFIL" />&nbsp; <a
                                            id="lnkMeuPerfil" href="#">
                                            Meu Perfil</a> </li>
                                    <li id="liEnvioSMS" class="withSeparatorServ" title="Clique para enviar mensagem de SMS.">
                                        <img src="/Library/IMG/Gestor_ServicosEnvioMsgSMS.png" alt="Icone ENVIO MENSAGEM SMS" />&nbsp;
                                        <a id="lnkEnvioSms" href="#">Envio de Mensagem SMS</a> </li>
                                    <li id="liAgendConta" class="withSeparatorServ" title="Clique para visualizar a agenda de contatos.">
                                        <img src="/Library/IMG/Gestor_ServicosAgendaContatos.png" alt="Icone AGENDA DE CONTATOS" />&nbsp;
                                        <a id="lnkAgendaContatos" href="#">Agenda de Contatos</a> </li>
                                    <li id="liAgendAtivid" class="withSeparatorServ" title="Clique para visualizar a agenda de atividades.">
                                        <img src="/Library/IMG/Gestor_ServicosAgendaAtividades.png" alt="Icone AGENDA DE ATIVIDADES" />&nbsp;
                                        <a id="lnkAgendaAtividades" href="#">Agenda de Atividades</a> </li>
                                    <li id="liAlertSistem" class="withSeparatorServ" title="Clique para visualizar os alertas sistêmicos.">
                                        <img src="/Library/IMG/Gestor_ServicosAlertasSistemicos.png" alt="Icone ALERTAS SISTÊMICOS" />&nbsp;
                                        <a id="lnkAlertasSistemicos" href="#">Informativos</a> </li>
                                    <li id="liDownlArquiv" class="withSeparatorServ" title="Clique para visualizar os downloads de arquivos disponíveis.">
                                        <img src="/Library/IMG/Gestor_ServicosDownloadArquivos.png" alt="Icone DOWNLOAD DE ARQUIVOS" />&nbsp;
                                        <a id="lnkDownloadArquivos" href="#">Download de Arquivos</a> </li>

                                    <li id="liComoChegar" class="withSeparatorServ" title="Clique para visualizar o como chegar." runat="server">
                                        <img src="/Library/IMG/Gestor_ComoChegar.png" alt="Icone COMO CHEGAR" />&nbsp;
                                        <a id="lnkComoChegar" href="/GEDUC/1000_CtrlAdminEscolar/1400_PesquisasGeoreferenciamento/1401_ComoChegar/GerarComoChegar.aspx?moduloNome=Como%20Chegar">Como chegar</a> 
                                    </li>
                                    <li id="li3" class="withSeparatorServ" title="Clique para visualizar o acesso fácil." runat="server">
                                        <img src="/Library/IMG/Gestor_AcessoFacil.png" alt="Icone ACESSO FÁCIL" />&nbsp;
                                        <a id="lnkAcessoFacil" href="#">Acesso Fácil</a> 
                                    </li>

                                </ul>
                            </div>
                        </li>
                        <li id="liSistemasPublicos"style="margin-left: 10px;" title="Clique para acessar os Sistemas.">
                            <img src="/Library/IMG/Gestor_AcessoSistemas.png" alt="Icone Sistemas" style="margin-right: 1px;" />&nbsp;<a
                                href="#" style="color:#FF831F !important;">SISTEMAS</a> </li>

                        <li id="liTopoTrocarEscolaPublica" class="liTrocarEscola" style="margin-left: 15px;" title="Clique para visualizar as Escolas.">
                            <img src="/Library/IMG/Gestor_TrocarEscola.png" alt="Icone Trocar Unidade" />&nbsp;<a
                                href="#">TROCAR UNIDADE</a> </li>
                            <%-- <li id="liAlterarSenha" class="withSeparator" title="Clique para alterar a senha.">
                        <img src="/Library/IMG/Gestor_Senha.png" alt="Icone Alterar Senha" />&nbsp;<a href="#">Alterar
                            Senha</a> </li> --%>
                        <li class="liSairSistema" style="margin-left: 15px;" title="Clique para encerrar sua sessão.">
                            <img src="/Library/IMG/Gestor_SairSistema.png" alt="Icone Sair" />&nbsp;<a href="javascript:window.location='/logout.aspx'">SAIR</a>
                        </li>
                    </ul>
                </div>
            </div>
            <div id="divTelaFuncionalidadesCarregando">
                <img src="/Library/IMG/Gestor_Carregando.gif" alt="Carregando..." />
            </div>
            <iframe runat="server" name="ifrmData" id="ifrmData" scrolling="no" class="ifrmData" 
                frameborder="0" allowtransparency="true" ></iframe>
        </div>
        <div id="divContent">
            <%--<div id="divLoadMeusAtalhos">
            </div>--%>
            <div id="divLoadAreasConhecimento">
            </div>
            <div id="divLoadIndicadores">
            </div>
        </div>
        <div id="divFooter">
            <div id="divAccessInformation" class="divAccessInformation">
                <ul>
                    <li><span>Acesso nº:&nbsp;</span>
                        <asp:Label runat="server" ID="lblQtdeAcessoUsuario" CssClass="lblQtdeAcessoUsuario" Text="00891"></asp:Label>
                    </li>
                    <li><span>&nbsp;- Último Acesso:&nbsp;</span>
                        <asp:Label runat="server" ID="lblInforUltimoAcesso" CssClass="lblInforUltimoAcesso" Text="03/01/2010 / 22h59m"></asp:Label>
                    </li>
                    <li><span>&nbsp;/ IP&nbsp;</span>
                        <asp:Label runat="server" ID="lblIpAcesso" CssClass="lblIpAcesso" Text="200.162.50.68"></asp:Label>
                    </li>
                </ul>
                <ul id="ulDescricao">
                    <li><a href="https://www.ntibr.com.br/" target="_blank">*** Licença USO SE Icó - Direitos INPI_CRPC nº BR512019002178-9 - Contato 61 9 9880-2329</a></li>
                </ul>
            </div>
        </div>
        </form>
    </div>
    <div id="divLoadAlterarSenha" style="display: none;"></div>
    <div id="divLoadShowTrocarEscola" style="display: none;"></div>
    <div id="divLoadShowInformativos" style="display: none; height:300px !important;"></div>
    <div id="divLoadShowMeuPerfil" style="display: none; height:535px !important;"></div>
    <div id="divLoadShowEnvioSMS" style="display: none; height:257px !important;"></div>
    <div id="divLoadShowAgendaContatos" style="display: none; height:310px !important;"></div>
    <div id="divLoadShowAgendaAtividades" style="display: none;height:205px !important;"></div>
    <div id="divLoadShowUltimosAcessos" style="display: none;"></div>
    <div id="divLoadShowInformacoesEscola" style="display: none; height:305px !important;"></div>
    <div id="divLoadDownloadArquivos" style="display: none; height:260px !important;"></div>
    <div id="divLoadShowServicos" style="display: none;"></div>
    <div id="divLoadShowAcessoRapido" style="display: none; height:355px !important;"></div>
    <div id="divLoadShowSistemasPublicos" style="display: none; height:350px !important;"></div>
    <div id="divVersaoSistema" style="display: none; height:30px !important;"></div>
    <div id="divDialogValidacao"></div>    
    <script type="text/javascript">
        $.ajaxSetup({ cache: false });

        //Código responsável por capturar o acionamento de teclas chave para apresentação da versão do sistema ===========
        var pressedCtrl = false;
        var pressedShift = false;
        $(document).keyup(function (e) {
            if (e.which == 17)
                pressedCtrl = false;
        })

        $(document).keydown(function (e) {
            if (e.which == 17)
                pressedCtrl = true;

            if (e.which == 16)
                pressedShift = true;

                //Verifica se foram pressionadas as teclas CTRL + SHIFT + A, e caso tenham sido, abrem a modal com a versão
            if (e.which == 65 && pressedCtrl == true && pressedShift == true) {
                $('#divVersaoSistema').dialog({ autoopen: false, modal: true, width: 190, height: 10, resizable: false, title: "VERSÃO DO SOFTWARE",
                    open: function () { $('#divVersaoSistema').load("/Componentes/VersaoSistema.aspx"); }
                });
            }
        });
        //=================================================================================================================

        $(document).ready(function () {

            $("#divLoadAreasConhecimento").load("/Navegacao/AreasConhecimento.aspx");
            /*$("#divLoadMeusAtalhos").load("/Componentes/MeusAtalhos.aspx");*/
            $("#divLoadIndicadores").load("/Componentes/Indicadores.aspx");
            
        });

        $("[id=lnkUnidadeAtual]").click(function (e) {
            $("#divLoadShowInformacoesEscola").load("/Componentes/InformacoesEscola.aspx", function () {
                $("#divLoadShowInformacoesEscola #frmInformacoesEscola").attr("action", "/Componentes/InformacoesEscola.aspx");
            });

            $("#divLoadShowInformacoesEscola").dialog({ title: "Informações da Unidade Atual", modal: true, width: "880px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
        });

        $("#liAlterarSenha a").click(function () {
            $("#divLoadAlterarSenha").load("/Componentes/AlterarSenha.aspx");
            $("#divLoadAlterarSenha").dialog({ title: "Alterar Senha", modal: true, width: "320px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); } });            
        });

        $(".liTrocarEscola a").click(function () {
            $("#divLoadShowTrocarEscola").load("/Componentes/TrocarEscola.aspx", function () {
                $("#divLoadShowTrocarEscola #frmTrocarEscola").attr("action", "/Componentes/TrocarEscola.aspx");
            });

            $("#divLoadShowTrocarEscola").dialog({ title: "Trocar Unidade", modal: true, width: "650px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
        });

        $("#liComoFazer a").click(function () {
            $("#liComoFazer #divComoFazer").load("/Componentes/ComoFazer.aspx", function () {
                $("#liComoFazer #divComoFazer #frmComoFazer").attr("action", "/Componentes/ComoFazer.aspx");
            }).slideToggle();
        });

        $("#liSistemasPublicos a").click(function () {
            $("#divLoadShowSistemasPublicos").load("/Componentes/SistemasPublicos.aspx", function () {
                $("#divLoadShowSistemasPublicos #frmSistemasPublicos").attr("action", "/Componentes/SistemasPublicos.aspx");
            });

            $("#divLoadShowSistemasPublicos").dialog({ title: "Sistemas Públicos", modal: true, width: "940px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
        });

        $("#liSistemasPublicosInt a").click(function () {
            $("#divLoadShowSistemasPublicos").load("/Componentes/SistemasPublicos.aspx", function () {
                $("#divLoadShowSistemasPublicos #frmSistemasPublicos").attr("action", "/Componentes/SistemasPublicos.aspx");
            });

            $("#divLoadShowSistemasPublicos").dialog({ title: "Sistemas Públicos", modal: true, width: "940px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
        });

        $("#liUltAcesso a").click(function () {
            $("#divLoadShowUltimosAcessos").load("/Componentes/UltimosAcessos.aspx", function () {
                $("#divLoadShowUltimosAcessos #frmUltimosAcessos").attr("action", "/Componentes/UltimosAcessos.aspx");
            });

            $("#divLoadShowUltimosAcessos").dialog({ title: "Últimos Acessos", modal: true, width: "700px", draggable: false, resizable: false, open: function () { $("#divDashboardContent ul li").hide(); }, beforeclose: function () { $("#divDashboardContent ul li").show(); theForm = document.getElementById("frmMain"); } });
        });        

        $("#divLoginInfo2 #liServicos a").click(function () {
            $("#divLoginInfo2 #divServicos").slideToggle();
        });
        /*
        $("#lnkPossoAjudar").click(function () {
            javascript: window.open('/Componentes/PossoAjudar/');
        }); */
    </script>
</body>
</html>
