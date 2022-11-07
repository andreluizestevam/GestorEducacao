<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="GerarComoChegar.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1400_PesquisasGeoreferenciamento.F1401_ComoChegar.GerarComoChegar" Title="Untitled Page" %>
<%@ Register assembly="Artem.GoogleMap" namespace="Artem.Web.UI.Controls" tagprefix="artem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { margin: -10px auto auto !important; }
        .ulDados li { width: 100%; }
        #divClear label { width: 290px; }    
       .limpa { clear:both; }
        *                   {margin: 0px;}
        .textBox            {width: 200px; margin-bottom: 3px !important; margin-left: 3px;}
        .dropDownList       {width: 200px; margin-top: -5px; margin-left: 3px;}
        .rblOpcao           {clear:both; display:inherit;}
        .roteiro            {width:300px;}
        .mapa               {width:650px !important; padding-left:5px; height: 295px !important;}
        .divPageContainer   {width: 985px;margin: 0 auto;}
        .divHeader          {background: transparent url(../../../../Library/IMG/Gestor_BkHeader.gif) repeat-x;height: 68px;border-left: 1px solid #DDDDDD;border-right: 1px solid #DDDDDD;padding-top: 25px;}
        .divContent         {padding: 5px;}        
        .divRightBar        {background: transparent url(../../../../Library/IMG/Gestor_BarBk.gif) repeat-y scroll top right;}
        #divClear input[type="radio"] {float:left;}
        #divClear label     {float:left;}
        #divClear           {width:950px;}
        #divSombraRodape    {height: 5px;background: transparent url(Library/IMG/Gestor_VerticalShadown.png) repeat-x scroll 0 0;width: 100%;margin-top:-1px;}
        #divCantoComSombra  {float:left;height:18px;margin-left:-10px;margin-top:-3px;background: transparent url(../../../../Library/IMG/Gestor_RoundShadownCorner.png) repeat-x scroll 0 0;width: 31px;position: absolute;}    
        .ulEndPart{ width:100%; } 
        .ulEndPart li { float: left; width: 265px;}
        #Table1, #tbInterna { border:0px !important; }
        #spaCabec { font-size: 1.3em; color: Orange;}
        #lblTitRoteiro  { font-size: 1.3em; font-weight:bold; margin-top:6px;}         
        #lblTitMapChegar { font-size: 1.3em; font-weight:bold;margin-left: 5px;margin-top:6px;}
        #tbInterna td { height: 15px !important;}
        #route { height: 285px; overflow-y: auto; }
        #divBarraPadraoContent{display:none;}
        #divBarraComoChegar { position:absolute; margin-left: 750px; margin-top:-25px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
        #divBarraComoChegar ul { display: inline; float: left; margin-left: 10px; }
        #divBarraComoChegar ul li { display: inline; margin-left: -2px; }
        #divBarraComoChegar ul li img { width: 19px; height: 19px; }
        #route table { margin: 0px !important; }
    </style>
<!--[if IE]>
<style type="text/css">
       #divBarraComoChegar { width: 238px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
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
            <div id="divContent" class="divContent divRightBar">
            <div id="tbCabecalho">
                <table id="Table1" cellpadding="0" cellspacing="0" style="width:98%;">
                    <tr>
                        <td style="text-align:left;" colspan="3">
                            <span id="spaCabec">Para saber como CHEGAR, escolha uma das opções abaixo e informe o Endereço de Origem (A) e Destino (B)</span>
                        </td>
                    </tr>
                    <tr style="padding-top: 2px !important;">
                        <td colspan="2">
                            <table id="tbInterna" style="width:100%; padding-top: 5px !important; padding-left:19px;">
                                <tr>
                                    <td colspan="3">
                                        <div id="divClear" class="divClear">
                                            <asp:RadioButtonList ID="rblOpcao"
                                                                 runat="server" AutoPostBack="true"
                                                                 RepeatDirection="Horizontal" 
                                                                 RepeatLayout="Flow"
                                                                 Width="100%"
                                                                 CssClass="rblOpcao" 
                                                onselectedindexchanged="rblOpcao_SelectedIndexChanged">
                                                <asp:ListItem Selected="true" Value="1">Pesquisa Livre</asp:ListItem>
                                                <asp:ListItem Value="2">Até o meu Posto de Saúde</asp:ListItem>
                                                <asp:ListItem Value="3">Entre os Órgãos da Administração</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <ul class="ulEndPart">
                                            <li style="margin-left: 10px;"> <label class="lblObrigatorio" style="float:left;"> A - Origem</label> <asp:TextBox ID="ttxOrigem1" runat="server" CssClass="textBox"></asp:TextBox> </li>
                                            <li style="margin-left: 55px;"> <label class="lblObrigatorio" style="float:left;margin-left: -10px"> A - Origem </label> <asp:TextBox ID="txtOrigem2" runat="server" CssClass="textBox"></asp:TextBox> </li>
                                            <li style="margin-left: 55px;"> <label class="lblObrigatorio" style="float:left;margin-left: -20px"> A - Origem </label> <asp:DropDownList ID="ddlColaborador" runat="server" CssClass="dropDownList"> </asp:DropDownList></li>                                            
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <ul class="ulEndPart">
                                            <li style="margin-left: 10px;"> <label class="lblObrigatorio" style="float:left;margin-right: 1px;"> B - Destino</label> <asp:TextBox ID="txtDestino" runat="server" CssClass="textBox"></asp:TextBox> </li>
                                            <li style="margin-left: 55px;"> <label class="lblObrigatorio" style="float:left;margin-right: 1px;margin-left: -10px"> B - Destino </label> <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="dropDownList"> </asp:DropDownList> </li>
                                            <li style="margin-left: 55px;"> <label class="lblObrigatorio" style="float:left;margin-right: 1px;margin-left: -20px"> B - Destino </label> <asp:DropDownList ID="ddlAluno" runat="server" CssClass="dropDownList"> </asp:DropDownList></li>
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: right; color:Orange;">
                                        <label style="margin-bottom: -4px; font-size: 1.1em;"> Escolha Como Chegar...</label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="background-color: #E6E6FA;">         
                        <td colspan="3">
                            <ul class="ulEndPart">
                                <li style="margin-left: 10px; text-align: center;"> <label id="lblTitRoteiro"> ROTEIRO</label></li>
                                <li style="width: 550px; text-align: center;"> <label id="lblTitMapChegar"> MAPA DE COMO CHEGAR</label> 
                                </li>
                                <li style="width: 120px; float: right; margin-top:2px; margin-right: 0px;"> 
                                    <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="De carro"
                                        ImageUrl="../../../../Library/IMG/Gestor_BtnCarroCinza.JPG" onclick="ImageButton1_Click" />
                                    <asp:ImageButton ID="ImageButton2" runat="server" ToolTip="Transporte público"
                                        ImageUrl="../../../../Library/IMG/Gestor_BtnConducaoCinza.JPG" onclick="ImageButton2_Click" />
                                    <asp:ImageButton ID="ImageButton3" runat="server" ToolTip="A pé"
                                        ImageUrl="../../../../Library/IMG/Gestor_BtnPedestreCinza.JPG" onclick="ImageButton3_Click" />
                                </li>
                            </ul>
                        </td>  
                    </tr>
                </table>
            </div>
            <div class="divContent">
                <table id="tbMap" runat="server" cellpadding="0" cellspacing="0" style="width:98%;">
                    <tr>
                       <td style="vertical-align:top;" class="roteiro">
                            <div id="route"></div>
                        </td>
                        <td class="mapa">
                            <artem:GoogleMap ID="GMapa" CssClass="mapa" runat="server">
                            </artem:GoogleMap>-
                            <br />
                        </td>
                    </tr>
                </table>
            </div>            
        </div>
        </li>
    </ul>
</asp:Content>
