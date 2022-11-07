<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvioSMS.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.EnvioSMS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Envio SMS</title>
    <script src="/Library/JS/jquery.maskedinput-1.2.2.min.js" type="text/javascript"></script>
    <style type="text/css">        
        #divEnvioSMSContent  
        {
            margin:auto;
            width:340px;
        }        
        #divEnvioSMSContainer #divAlterarSenhaFormButtons
        {
            margin-top: 5px;
        }
        #divEnvioSMSContainer #divRodapeSMS
        {
            margin-top: 20px;
            float: right;
        }
        #divEnvioSMSContainer #imgLogoGestor
        {
            width: 120px;
            height: 30px;
        }
        .successMessageSMS 
        { 
            background: url(/Library/IMG/Gestor_ImgGood.png) no-repeat scroll center 10px;                             
            font-size: 15px;
            font-weight: bold;
            margin: 25% auto 13% auto;
            padding: 35px 10px 10px;
            text-align: center;
            width: 220px;
        }
        .ErrorMessageSMS
        {
            background: url(/Library/IMG/Gestor_ImgError.png) no-repeat scroll center 10px;
            font-size: 15px;
            font-weight: bold;
            margin: 25% auto 13% auto;
            padding: 35px 10px 10px;
            text-align: center;
            width: 220px;   
        }
        #divLoadingSMS  
        {
        	margin: 100px 70px auto 120px; 
            width: 90px;
        }
        .txtMsg{ width: 335px; height: 27px; margin-top: 4px;} 
        .liEmissor { clear: both;}
        .liDestinatarios, .litxtDestinatarios { clear: both; margin-top: 2px;}
        .liTelefoneSMS { clear: none; margin-top: 2px; margin-left: 5px;}
        .liTpContatoSMS { clear:both; margin-top: 2px;}
        .liUnidadeSMS { width:245px; margin-top: 2px; clear:none; margin-left: 5px;}
        .liMsg { clear: both; margin-top: 0px; }        
        #ulEnvioSMSForm li
        {
        	float: left;
        }
        #lblDe, #lblPara, #lblTitMsg
        {
        	color: Black;
        	font-weight: bold;
        }
        #txtEmissor { width: 210px; }
        .liBtnEnviar { clear: both; margin-left: 90px; margin-top: 5px;}
        .liPara { clear: both; }
        .ddlDestinatarios
        {
        	width: 250px;
        }
        .ddlUnidadeEscolarSMS
        {
        	width: 243px;
        }
        #liSucessMsg
        { 
        	width: 210px;
        	margin-top: 5px;
            color: #DF6B0D;
            font-weight: bold;
        }
        #spaCtCarac { color: #FF6347; }
        .vsEnvioSMS { margin-top: -15px; color:#B22222; clear: both; margin-left: -90px; width: 265px; }
        .btnEnviarSMS{ margin-left: 186px; margin-top: -20px; }
        #liHelpTxtSMS
        {
            margin-top: 10px;
            width: 210px;
            color: #DF6B0D;
            font-weight: bold;
            clear: both;
        }
        .pAcesso
        {
        	font-size: 1.1em;
        	color: #4169E1;
        }
        .pFechar
        {
        	font-size: 0.9em;
        	color: #FF6347;
        	margin-top: 2px;
        }
        .ddlTpContatoSMS
        {
        	width: 90px;
        }
        .campoTelefoneSMS
        {
        	width: 80px;
        }
    </style>
</head>
<body>
    <div id="divEnvioSMSContainer">
        <form id="frmEnvioSMS" runat="server">
        <div id="divEnvioSMSContent" clientidmode="Static" runat="server" style="display:none;">
            <ul id="ulEnvioSMSForm">
                <li>
                    <label id="lblDe">De</label>
                </li>                
                <li class="liEmissor">
                    <label>Emissor</label>                                
                    <asp:TextBox runat="server" Enabled="false" ID="txtEmissor" />
                </li>
                <li class="liPara">
                    <label id="lblPara">Para</label>                                
                </li>
                <li id="liTpContato" class="liTpContatoSMS">        
                    <label>
                        Tipo do Contato</label>                                
                    <asp:DropDownList ID="ddlTpContato" CssClass="ddlTpContatoSMS" runat="server" 
                        ToolTip="Selecione o Tipo de Contato" AutoPostBack="True" onselectedindexchanged="ddlTpContato_SelectedIndexChanged">
                    </asp:DropDownList>    
                                    
                </li>
                <li class="liUnidadeSMS">
                    <label>Unidade</label> 
                    <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolarSMS" runat="server" 
                    AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
                    </asp:DropDownList>                                                
                </li>
                <li id="liDestinatarios" class="liDestinatarios" runat="server">        
                    <label>
                        Nome Destinatário</label>                                
                    <asp:DropDownList ID="ddlDestinatarios" CssClass="ddlDestinatarios" runat="server" 
                        ToolTip="Selecione o Destinatário" AutoPostBack="True" ValidationGroup="vgEnvioSMS"
                        onselectedindexchanged="ddlFuncionarios_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlFuncionarios" runat="server" ControlToValidate="ddlDestinatarios" ErrorMessage="Campo Funcionário obrigatório" ValidationGroup="vgEnvioSMS" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li id="litxtDestinatarios" class="litxtDestinatarios" style="display:none;" runat="server">
                    <label for="txtTelefone">Nome Destinatário</label>                                
                    <asp:TextBox ID="txtNomeDestinatario" CssClass="ddlDestinatarios" ValidationGroup="vgEnvioSMS" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNomeDestinatario" ErrorMessage="Campo Nome Destinatario é Obrigatório" ValidationGroup="vgEnvioSMS" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liTelefoneSMS">
                    <label for="txtTelefone">Telefone</label>                                
                    <asp:TextBox ID="txtTelefone" CssClass="campoTelefoneSMS" ValidationGroup="vgEnvioSMS" Enabled="False" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtTelefone" runat="server" ControlToValidate="txtTelefone" ErrorMessage="Campo Telefone Obrigatório" ValidationGroup="vgEnvioSMS" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liMsg">
                    <label for="txtMsg" id="lblTitMsg" style="width:55px; float: left;">Mensagem</label><span style="display:inline; color: Black;">(Máximo de 110 caracteres)</span>                                
                    <asp:TextBox ID="txtMsg" CssClass="txtMsg" ToolTip="Digite a mensagem a ser enviada." MaxLength="110" runat="server" ValidationGroup="vgEnvioSMS" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtMsg" runat="server" ControlToValidate="txtMsg" ErrorMessage="*** Campo Mensagem Obrigatório ***" ValidationGroup="vgEnvioSMS" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liBtnEnviar">
                    <span style="margin-left: -89px;"> Faltam </span><span id="spaCtCarac"></span><span id="descspaCtCarac"> caracteres.</span>
                    <asp:Button CssClass="btnEnviarSMS" Text="Enviar Msg" runat="server" ID="btnEnviarSMS" ValidationGroup="vgEnvioSMS" onclick="btnSalvar_Click" />
                    <asp:ValidationSummary ID="vsEnvioSMS" runat="server" CssClass="vsEnvioSMS" ValidationGroup="vgEnvioSMS" />
                </li>
                <li class="liHelpTxtSMS" id="liHelpTxtSMS" runat="server">                        
                    <p id="pAcesso" class="pAcesso">
                        Digite a mensagem e tecle em ENVIAR para encaminhá-la ao destinatário.</p>
                    <p id="pFechar" class="pFechar">
                        Clique no X para fechar a janela.</p>                                 
                </li>
            </ul>
            <div id="divRodapeSMS">
                <img id="imgLogoGestor" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
            </div>
        </div>
        <div id="divSuccessoMessage" runat="server" class="successMessageSMS">
            <asp:Label ID="lblMsg" runat="server" Visible="false" />
            <asp:Label style="color:#B22222 !important; display:block; " Visible="false" ID="lblMsgAviso" runat="server" />
        </div>
        <div id="divErrorMessage" runat="server" class="ErrorMessageSMS">
            <asp:Label ID="lblError" runat="server" />
            <asp:Label style="color:#B22222 !important; display:block; " ID="lblErrorAviso" runat="server" />
        </div>
        <div id="divLoadingSMS" clientidmode="Static" runat="server">
            <img alt="icone carregando" title="Aguarde enquanto a página carrega." src="/Library/IMG/Gestor_Carregando.gif" />
        </div>        
        </form>
    </div>    

    <script type="text/javascript">
        // Sobrescreve o metodo do asp.Net de PostBack
        function __doPostBack(eventTarget, eventArgument) {
            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                theForm.__EVENTTARGET.value = eventTarget;
                theForm.__EVENTARGUMENT.value = eventArgument;

                if (eventTarget == 'ddlUnidade' || eventTarget == 'ddlDestinatarios' || eventTarget == 'ddlTpContato') {

                    var options = {
                        target: '#divLoadShowEnvioSMS', // target element(s) to be updated with server response 
                        url: '/Componentes/EnvioSMS.aspx'
                        //beforeSubmit: showRequest,  // pre-submit callback 
                        //success: showResponse,  // post-submit callback 
                        //error: requestError
                        // other available options: 
                        //url:       url         // override for form's 'action' attribute 
                        //type:      type        // 'get' or 'post', override for form's 'method' attribute 
                        //dataType:  null        // 'xml', 'script', or 'json' (expected server response type) 
                        //clearForm: true        // clear all form fields after successful submit 
                        //resetForm: true        // reset the form after successful submit 

                        // $.ajax options can be used here too, for example: 
                        //timeout:   3000 
                    };

                    $(theForm).ajaxSubmit(options);
                }
                else {
                    theForm.submit();
                }
            }
        }

        $('#divEnvioSMSContainer #btnSalvar').click(function () {
            $('#divEnvioSMSContainer #divEnvioSMSContent').hide();
            $('#divLoadingSMS').show();
        });

        $(document).ready(function () {
            $('#divEnvioSMSContainer #frmEnvioSMS').ajaxForm({ target: '#divLoadShowEnvioSMS', url: '/Componentes/EnvioSMS.aspx' });

            if ($('.campoTelefoneSMS').val().length <= 10) {
                $('.campoTelefoneSMS').mask("(99)9?999-99999");
            } else {
                $('.campoTelefoneSMS').mask("(99)9?9999-9999");                
            }

            $('.txtMsg').limit('110', '#spaCtCarac');

            $('.txtMsg').limit('110');

            if ($('#divEnvioSMSContainer #divErrorMessage').is(":visible") == false) {
                if (($('#divEnvioSMSContainer .successMessageSMS span').length > 0)()) {
                    $('#divEnvioSMSContainer #divSuccessoMessage').show();
                    $('#divEnvioSMSContainer #divEnvioSMSContent').hide();
                }
                else {
                    $('#divEnvioSMSContainer #divSuccessoMessage').hide();
                    $('#divEnvioSMSContainer #divEnvioSMSContent').show();
                }
            }

            $('#divLoadingSMS').hide();
        });
    </script>
</body>
</html>
