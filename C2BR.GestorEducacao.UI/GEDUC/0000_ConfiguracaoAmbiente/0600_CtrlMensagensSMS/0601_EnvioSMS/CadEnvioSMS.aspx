<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="CadEnvioSMS.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0600_CtrlMensagensSMS.F0601_EnvioSMS.CadEnvioSMS" Title="Untitled Page" %>
<%@ Register assembly="Artem.GoogleMap" namespace="Artem.Web.UI.Controls" tagprefix="artem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">      
    <style type="text/css">      
        .ulDados { width: 350px;margin-top: 30px !important;}  
        
        /*--> CSS LIs */
        .liEmissor { clear: both;}
        .liDestinatarios { clear: both; margin-top: 2px;}
        .liTelefoneSMS { clear: none; margin-top: 2px; margin-left: 5px;}
        .liTpContatoSMS { clear:both; margin-top: 2px;}
        .liUnidadeSMS { width:245px; margin-top: 2px; clear:none; margin-left: 5px;}
        .liMsg { clear: both; margin-top: 0px; }  
        .liBtnEnviar { clear: both; margin-left: 84px; margin-top: 5px;}
        .liPara { clear: both; }
        #liHelpTxtSMS
        {
            margin-top: 10px;
            width: 210px;
            color: #DF6B0D;
            font-weight: bold;
            clear: both;
        }
        #divBarraComoChegar ul li { display: inline; margin-left: -2px; }
        
        /*--> CSS DADOS */
        .txtMsg{ width: 340px; height: 27px; margin-top: 4px;} 
        #lblDe, #lblPara, #lblTitMsg
        {
        	color: Black;
        	font-weight: bold;
        }
        .txtEmissor { width: 210px; }        
        .ddlDestinatarios { width: 250px;  }
        .ddlUnidadeEscolarSMS { width: 243px; }
        #spaCtCarac { color: #FF6347; }
        .vsEnvioSMS { margin-top: -15px; color:#B22222; clear: both; margin-left: -90px; }
        .btnEnviarSMS{ margin-left: 198px; margin-top: -20px; }        
        .pAcesso
        {
        	font-size: 1.1em;
        	color: #4169E1;
        }
        .ddlTpContatoSMS { width: 90px; }
        .campoTelefoneSMS { width: 80px; }
        #divBarraPadraoContent{display:none;}
        #divBarraComoChegar { position:absolute; margin-left: 770px; margin-top:-30px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
        #divBarraComoChegar ul { display: inline; float: left; margin-left: 10px; }        
        #divBarraComoChegar ul li img { width: 19px; height: 19px; }  
        #helpMessages {display:none;}
        .helpMessages
        {
            margin-top: -5px;
            font-size: 11px !important;
        }
        
</style>
<!--[if IE]>
<style type="text/css">
       #divBarraComoChegar { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="helpMessagesFC" class="helpMessages">
        <div id="divMensagGenerica" class="divMensagGenerica">
            <span ID="lblMsgGenric" style="margin-top: -3px;">Digite a mensagem e tecle em ENVIAR para encaminhá-la ao destinatário.</span>
        </div>
        <div id="divMensagCamposObrig" class="divMensagGenerica" style="margin-top: 2px;">
            <span>Campos com preenchimento obrigatório ( <span style="color:Red;">*</span> )</span>
        </div>
    </div>
    <div id="div1" class="bar" > 
            <div id="divBarraComoChegar" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
            <ul id="ulNavegacao" style="width: 39px;">
                <li id="btnVoltarPainel">
                    <a href="javascript:parent.showHomePage()">
                        <img title="Clique para voltar ao Painel Inicial." 
                                alt="Icone Voltar ao Painel Inicial." 
                                src="/BarrasFerramentas/Icones/VoltarPainelGestor.png" />
                    </a>
                </li>
                <li id="btnVoltar">
                    <a href="javascript:BackToHome();">
                        <img title="Clique para voltar a Pagina Anterior."
                                alt="Icone Voltar a Pagina Anterior." 
                                src="/BarrasFerramentas/Icones/VoltarPaginaAnterior.png" />
                    </a>
                </li>
            </ul>
            <ul id="ulEditarNovo" style="width: 39px;">
                <li id="btnEditar">
                    <img title="Abre o registro atualmente selecionado em modo de edicao." alt="Icone Editar." src="/BarrasFerramentas/Icones/EditarRegistro_Desabilitado.png" />
                </li>
                <li id="btnNovo">
                    <img title="Abre o formulario para Criar um Novo Registro."
                            alt="Icone de Criar Novo Registro." 
                            src="/BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
                </li>
            </ul>
            <ul id="ulGravar">
                <li>
                    <img title="Grava o registro atual na base de dados."
                            alt="Icone de Gravar o Registro." 
                            src="/BarrasFerramentas/Icones/Gravar_Desabilitado.png" />
                </li>
            </ul>
            <ul id="ulExcluirCancelar" style="width: 39px;">
                <li id="btnExcluir">
                    <img title="Exclui o Registro atual selecionado."
                            alt="Icone de Excluir o Registro." 
                            src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
                </li>
                <li id="btnCancelar">
                    <a href='<%= Request.Url.AbsoluteUri %>'>
                        <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                                alt="Icone de Cancelar Operacao Atual." 
                                src="/BarrasFerramentas/Icones/Cancelar.png" />
                    </a>
                </li>
            </ul>
            <ul id="ulAcoes" style="width: 39px;">
                <li  id="btnPesquisar">
                    <img title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado."
                            alt="Icone de Pesquisa." 
                            src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
                </li>
                <li id="liImprimir">
                    <img title="Formula um relatório a partir dos parâmetros especificados e o exibe em Modo de Impressão." 
                            alt="Icone de Impressao." 
                            src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />                    
                </li>
            </ul>
        </div>
    </div>
    <ul id="ulDados" class="ulDados"> 
        <li>
            <label id="lblDe">De</label>
        </li>                
        <li class="liEmissor">
            <label>Emissor</label>                                
            <asp:TextBox runat="server" Enabled="false" ID="txtEmissor" CssClass="txtEmissor" />
        </li>
        <li class="liPara">
            <label id="lblPara">Para</label>                                
        </li>
        <li id="liTpContato" class="liTpContatoSMS">        
            <label>
                Tipo do Contato</label>                                
            <asp:DropDownList ID="ddlTpContato" CssClass="ddlTpContatoSMS" runat="server" 
                ToolTip="Selecione o Tipo de Contato" AutoPostBack="True" onselectedindexchanged="ddlTpContato_SelectedIndexChanged">
                <asp:ListItem Value="F" Selected="true">Funcionário</asp:ListItem>
                <asp:ListItem Value="P">Professor</asp:ListItem>
                <asp:ListItem Value="A">Aluno</asp:ListItem>
                <asp:ListItem Value="R">Responsável</asp:ListItem>
            </asp:DropDownList>                    
        </li>
        <li class="liUnidadeSMS">
            <label>Unidade</label> 
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolarSMS" runat="server" 
            AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>                                                
        </li>
        <li id="liDestinatarios" class="liDestinatarios">        
            <label>
                Nome Destinatário</label>                                
            <asp:DropDownList ID="ddlDestinatarios" CssClass="ddlDestinatarios" runat="server" 
                ToolTip="Selecione o Destinatário" AutoPostBack="True" ValidationGroup="vgEnvioSMS"
                onselectedindexchanged="ddlFuncionarios_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlFuncionarios" runat="server" ControlToValidate="ddlDestinatarios" ErrorMessage="Campo Funcionário obrigatório" ValidationGroup="vgEnvioSMS" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liTelefoneSMS">
            <label for="txtTelefone">Telefone</label>                                
            <asp:TextBox ID="txtTelefone" CssClass="campoTelefoneSMS" ValidationGroup="vgEnvioSMS" Enabled="False" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtTelefone" runat="server" ControlToValidate="txtTelefone" ErrorMessage="Campo Telefone Obrigatório" ValidationGroup="vgEnvioSMS" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liMsg">
            <label for="txtMsgInt" id="lblTitMsg" class="lblObrigatorio" style="width:55px; float: left;">Mensagem</label><span style="display:inline; color: Black; margin-left: 5px;">(Máximo de 110 caracteres)</span>                                
            <asp:TextBox ID="txtMsgInt" ClientIDMode="Static" MaxLength="110" CssClass="txtMsg" ToolTip="Digite a mensagem a ser enviada." runat="server" ValidationGroup="vgEnvioSMS" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtMsg" runat="server" ControlToValidate="txtMsgInt" ErrorMessage="*** Campo Mensagem Obrigatório ***" ValidationGroup="vgEnvioSMS" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liBtnEnviar">
            <%-- <span style="margin-left: -89px;"> Faltam </span><span runat="server" clientidmode="Static" id="spaCtCaracInt"></span><span id="descspaCtCarac"> caracteres.</span>--%>
            <asp:Button CssClass="btnEnviarSMS" Text="Enviar Msg" runat="server" ID="btnEnviarSMS" ValidationGroup="vgEnvioSMS" onclick="btnSalvar_Click" />
        </li>
    </ul>
<script type="text/javascript">
    $(document).ready(function () {
        $(".campoTelefoneSMS").mask("(99) 9999-9999");

        $('#txtMsgInt').limit('110', '#spaCtCaracInt');

        $('#txtMsgInt').limit('110');
    });
</script>    
</asp:Content>
