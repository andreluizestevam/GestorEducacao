<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
	
    <link rel="shortcut icon" href="/Library/IMG/Gestor_GloboIcone.ico" />
    <title>GE SME MFumaça</title>
	
    <link href="/Library/CSS/Jquery.UI/customtheme/default.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="/Library/JS/jquery-1.4.4.min.js"></script>
    <script type="text/javascript" src="/Library/JS/jquery.tools.min.js"></script>
    <script src="/Library/JS/jquery.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.ui.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.form.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.corner.js" type="text/javascript"></script>
    <script src="/Library/JS/ui.datepicker-pt-BR.js" type="text/javascript"></script>
    <script src="Library/JS/jquery.keyboard.js" type="text/javascript"></script>
    <script src="/Library/JS/jquery.defaults.js" type="text/javascript"></script>
    <link href="Library/CSS/estrutura.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="Library/CSS/formulario.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="Library/CSS/menu1.css" rel="stylesheet" type="text/css" media="screen" />
</head>
<body class="ge">    
    <div id="divTelaFuncionalidadesCarregando" class="divTelaFuncionalidadesCarregando"
        runat="server">
        <center><img src="/Library/IMG/Gestor_Carregando.gif" alt="Carregando..." /></center>
    </div>
    <div id="totalContentWrapper">
        <div class="header">
            <div class="sideH">
                <h1>
                    <a href="#" title="PORTAL GESTOR SAÚDE - MEDICPHONE">GESTOR SAÚDE</a>
				</h1>
				
                <div class="menuPortal"> 
				</div>
				
				<!-- <h3> Inserir o nome do cliente e a cidade e uf que aparecerá na página de login acima e abaixo da logo               
				<div class="nomeCliente">   </div>
				<div class="nomeCidadeCliente">   </div>
				
				<!-- <h3> Inserir texto que aparecerá na página de login no lado esquerdo               
                </h3>
                <p> Inserir a Assinatura do autor do texto
                </p>-->
				</div>
				
            <div class="menu1">
                <ul>
                    <li class="menu1_1"><a href="https://morrodafumaca.atende.net/subportal/secretaria-de-educacao" target="_blank">WEB SITE<br />
                        <span>S M E</span></a></li>
                    <li class="menu1_2"><a class="mb" href="https://gestoreducacao.com.br/suporte/" target="_blank">acesse o<br />
                        <span>suporte</span></a></li>
                    <li class="menu1_3"><a href="https://gestoreducacao.com.br/macrofluxo/" target="_blank">o que é o<br />
                        <span>GESTOR EDUCAÇÃO?</span></a></li>
                </ul>
            </div>
			
            <form runat="server" accept-charset="iso-8859-1">
            <div id="divErroMsg" class="divErroMsg" runat="server" style="display: none; background-color: #FFE4C4;">
                <div>
                    <ul>
                        <li style="float: right; margin-bottom: 8px;"><a id="lnkCloseAviso" href="#">X</a>
                        </li>
                        <li style="clear: both; margin-bottom: 8px; margin-right: 20px;">
                            <label style="font-size: 1.2em;">
                                Inconsistência Encontrada</label>
                        </li>
                        <li style="clear: both; margin-right: 20px;">
                            <asp:Label ID="lblMensagErro" Style="color: Red;" runat="server" />
                        </li>
                        <li id="liLicenca" runat="server" style="display: none; clear: both; margin-right: 20px;
                            margin-top: 8px;">
                            <ul>
                                <li>
                                    <asp:TextBox ID="txtLicenca" runat="server" Rows="2" TextMode="MultiLine" Style="clear: both;margin-right: 0px; margin-bottom: 8px; width: 500px;">
                                    </asp:TextBox>
                                </li>
                                <li>
                                    <asp:Button ID="btnSendLicenca" class="btnSendLicenca" runat="server" Style="clear: both; margin-right: 20px;"
                                        OnClick="btnSendLicenca_Click" Text="Enviar" UseSubmitBehavior=false /></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="formulario">
                <h3>
                    faça seu acesso</h3>
                <div class="input text">
                    <label>
                        login</label>
                    <input name="name" type="text" runat="server" title="Seu login" maxlength="30" id="UserNMLOGINUSUARIO" />
                </div>
                <div class="input password">
                    <label>
                        senha</label>
                    <input type="password" runat="server" title="Sua senha" id="UserVLSENHAUSUARIO" />
                    <asp:HiddenField ID="hdfORG_CODIGO_ORGAO" runat="server" />
                </div>
                <div class="submit">
                    <a id="lnkEsqueciSenha" class="rescuePass" href="#" title="Esqueci senha">Esqueci minha
                        senha</a>
                    <asp:Button Text="Entrar" CssClass="enviar" runat="server" ID="btnEntrar" OnClick="btnEntrar_Click" />
                </div>
            </div>
            </form>
        </div>
        <div class="content">
            <div class="boxAlunos">
            </div>
		
            <div class="imagemLogo">
				<a href="#" class="logousuario" target="_blank">Cliente</a>
				
			</div>
        </div>
        <div class="footer">
		
<!--      <a href="http://www.portalgestorsaude.com.br/" class="sistema" target="_blank">Sistema</a> -->
          <a href="  " class="sistema">Sistema</a>

            <p>
                (Registro de Propriedade INPI CRPC nº BR512019002178-9)<br>
   				Licença de Uso - FME SME - CNPJ nº 10.598.648/0001-65<br> 
				*** Secretaria Municipal de Educação de Morro da Fumaça - SC - Telefone: 48 3434-6113 ***
			</p>
            <a href="https://www.gestoreducacao.com.br/" class="iCactos" target="_blank"> XXX...</a>
        </div>
		
    </div>
    <div id="divLoadEsqueciSenha" />
    <script type="text/javascript">
        $("#lnkEsqueciSenha").click(function () {
            $("#divLoadEsqueciSenha").load("/Componentes/EsqueciSenha.aspx", function () {
                $("#divLoadEsqueciSenha #frmEsqueciSenha").attr("action", "/Componentes/EsqueciSenha.aspx");
            });

            $("#divLoadEsqueciSenha").dialog({ title: "Recuperar Senha", modal: true, width: "430px", draggable: false, resizable: false, beforeclose: function () { theForm = document.getElementById("frmLogin"); } });
        });

        $("#lnkCloseAviso").click(function () {
            $("#divErroMsg").hide();
        });

        $(".btnSendLicenca").click(function () {
            $(".divTelaFuncionalidadesCarregando").show();
            $("#divErroMsg").hide();
        });

        $(".enviar").click(function () {
            $(".divTelaFuncionalidadesCarregando").show();

            /*
            $('[id=HFTeste]').val($("#UserNMLOGINUSUARIO").val());
            $('[id=HFTeste2]').val($("#UserVLSENHAUSUARIO").val());
            
            $("#UserNMLOGINUSUARIO").attr("disabled", true);
            $("#UserVLSENHAUSUARIO").attr("disabled", true);*/
        });

    </script>
</body>
</html>
