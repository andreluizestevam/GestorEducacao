<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoGenericas.Master" AutoEventWireup="true"
  CodeBehind="IDEMEC_Local.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F9000_CtrlResultados.F9210_IndicesEducacaoExterno.F9211_IDE_MEC_Local.IDEMEC_Local" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <title>Indicadores Demográficos e Educacionais</title>
  <link href="Library/indicadores.css" rel="stylesheet" type="text/css" />
  <style type="text/css">
    .ulDados { width: 100%; }
    
    /*--> CSS LIs */
    .liClear { clear: both; }
    .liUf
    {
      width: 40px;
      float: left !important;
      margin-right: 10px;
    }
    .liCidade
    {
      width: 300px;
      float: left !important;
    }
    
    /*--> CSS DADOS */
    #helpMessages { display:none; }
    .barra_governo_box { display: none; }
    #divBarraComoChegar { position:absolute; margin-left: 770px; margin-top:-75px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
    #divBarraComoChegar ul { display: inline; float: left; margin-left: 10px; }
    #divBarraComoChegar ul li { display: inline; margin-left: -2px; }
    #divBarraComoChegar ul li img { width: 19px; height: 19px; }   
    
    </style>
    <img src="Library/logo.gif" />
<!--[if IE]>
<style type="text/css">
       #divBarraComoChegar { width: 238px; margin-left: 760px; }
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
                <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                        alt="Icone de Cancelar Operacao Atual." 
                        src="/BarrasFerramentas/Icones/Cancelar_Desabilitado.png" />
            </li>
        </ul>
        <ul id="ulAcoes" style="width: 39px;">
            <li  id="btnPesquisar">
                    <img title="Volta ao formulário de pesquisa."
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
    <li id="liUF" clientidmode="Static" style="display:none;"  class="liUf" runat="server">
      <label for="ddlUF" style="width: 20px;" class="lblObrigatorio" title="Unidade Federativa">
        UF</label>
      <asp:DropDownList ID="ddlUF" CssClass="liUf" runat="server" 
        ToolTip="Selecione a UF" AutoPostBack="True" 
        onselectedindexchanged="ddlUF_SelectedIndexChanged">
        <asp:ListItem Value='12'>AC</asp:ListItem>
        <asp:ListItem Value='27'>AL</asp:ListItem>
        <asp:ListItem Value='16'>AP</asp:ListItem>
        <asp:ListItem Value='13'>AM</asp:ListItem>
        <asp:ListItem Value='29'>BA</asp:ListItem>
        <asp:ListItem Value='23'>CE</asp:ListItem>
        <asp:ListItem Value='53'>DF</asp:ListItem>
        <asp:ListItem Value='32'>ES</asp:ListItem>
        <asp:ListItem Value='52'>GO</asp:ListItem>
        <asp:ListItem Value='21'>MA</asp:ListItem>
        <asp:ListItem Value='51'>MT</asp:ListItem>
        <asp:ListItem Value='50'>MS</asp:ListItem>
        <asp:ListItem Value='31'>MG</asp:ListItem>
        <asp:ListItem Value='15'>PA</asp:ListItem>
        <asp:ListItem Value='25'>PB</asp:ListItem>
        <asp:ListItem Value='41'>PR</asp:ListItem>
        <asp:ListItem Value='26'>PE</asp:ListItem>
        <asp:ListItem Value='22'>PI</asp:ListItem>
        <asp:ListItem Value='33'>RJ</asp:ListItem>
        <asp:ListItem Value='24'>RN</asp:ListItem>
        <asp:ListItem Value='43'>RS</asp:ListItem>
        <asp:ListItem Value='11'>RO</asp:ListItem>
        <asp:ListItem Value='14'>RR</asp:ListItem>
        <asp:ListItem Value='42'>SC</asp:ListItem>
        <asp:ListItem Value='35'>SP</asp:ListItem>
        <asp:ListItem Value='28'>SE</asp:ListItem>
        <asp:ListItem Value='17'>TO</asp:ListItem>
      </asp:DropDownList>
    </li> 
    <li id="liCidade" runat="server" style="display:none;" clientidmode="Static"  class="liCidade">
      <label for="ddlCidade" class="lblObrigatorio" title="Município">
        Cidade</label>
      <asp:DropDownList ID="ddlCidade" CssClass="liCidade" runat="server" ToolTip="Selecione a Cidade">
      </asp:DropDownList>
    </li>
    <li>
        <div id="Indicadores">
            <asp:Literal ID="LiteralHtml" runat="server"></asp:Literal>
        </div>
    </li>
    <br class="liClear" />
    <br />
  </ul>
  <br class="liClear" />
  <br />
  <br />
  
  <br class="liClear" />
</asp:Content>
