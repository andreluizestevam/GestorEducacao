<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoGenericas.Master" AutoEventWireup="true" CodeBehind="AtualLancAtiviPortal.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0500_SuporteOperacionalGE._0510_AtualLancAtiviPortal.AtualLancAtiviPortal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 610px; margin-top: 15px; }
        
        /*--> CSS LIs */
        #divBarraExportarBasePortal ul li { display: inline; margin-left: -2px; }
        .liCabecalho { background-color:#FFFFE0; width: 600px;text-align:center;padding:4px 0;margin-bottom: 1px; }
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        #divBarraExportarBasePortal { position:absolute; margin-left: 750px; margin-top:-40px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
        #divBarraExportarBasePortal ul { display: inline; float: left; margin-left: 10px; }        
        #divBarraExportarBasePortal ul li img { width: 19px; height: 19px; }
        .chkSelecionarTodos label { display: inline; margin-left: -2px; }
        .divGrid
        {
            /*height: 360px;*/
            width: 600px;
            overflow-y: auto;
            margin-top: 3px;            
        }
        .emptyDataRowStyle { background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important; }
        .lblCabecalho { text-transform:uppercase; font-weight:bold; padding-top: 3px; }
        .grdBusca th { background-color: #8B8989; }
        .lblAtuaBDPorRelac { font-size: 1.3em; color: #FF8C00; }
        .liLnkAtualBD { margin-top: 15px; margin-left: 200px; border: 1px solid #CCCCCC; padding: 3px; float: left !important; }
        .divValidationSummary {
            cursor: move;
            display: none;
            left: 0;
            padding: 10px;
            position: absolute;
            top: 35px;
            width: 210px;
        }
        .divValidationSummary .divButtons { text-align: right; }
        .divValidationSummary #divValidationSummaryContent { margin-bottom: 10px; }
        .divValidationSummary .vsValidation { margin-left: 10px; }
        .ui-corner-all { border-radius: 4px 4px 4px 4px; }
        .divValidationSummary li {
            border-bottom: 1px solid #CFCFCF;
            color: #666666;
            line-height: 11px;
            list-style-type: circle;
            padding-bottom: 2px;
        }
        .divTelaExportacaoCarregando
        {
            left: 50%;
            margin-top: 32px;
            position: relative;
            top: 10px;
            display: none;
        }
        
    </style>
<!--[if IE]>
<style type="text/css">
       #divBarraExportarBasePortal { width: 238px; }
</style>
<![endif]-->
    
<script src="/Library/JS/Grid.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="div1" class="bar"> 
            <div id="divBarraExportarBasePortal" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
            <asp:HiddenField ID="hidsh1" Value="" runat="server" />
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
                <li id="btnEditar" style="float:left;">
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
    <ul class="ulDados">     
        <li id="liLnkAtualBD" runat="server" class="liLnkAtualBD">
            <asp:LinkButton ID="lnkAtualBP" runat="server" OnClick="lnkAtualBP_Click" CssClass="lnkAtualBP" ToolTip="Atualizar Lançamento de atividades a partir do Portal de Relacionamento">
                <img id="imgIconAtualBP" width="20" height="20" src='/Library/IMG/Gestor_DBRestore.png' alt="Atualizar Lançamento de atividades a partir do Portal de Relacionamento" />
                <asp:Label runat="server" ID="lblAtuaBDPorRelac" CssClass="lblAtuaBDPorRelac" Text="Atualizar Lançamento de atividades do Professor"></asp:Label>
            </asp:LinkButton>
        </li>
        <li class="liClear">
            <div id="divTelaExportacaoCarregando" class="divTelaExportacaoCarregando" runat="server">
                <img src="/Library/IMG/Gestor_Carregando.gif" alt="Carregando..." />
            </div>
        </li>
        <div id="divValidationSummary" style="display:none;" class="divValidationSummary ui-state-error ui-corner-all">
            <div id="divButtons" class="divButtons">
                <a id="lnkClose" class="lnkClose" title="Fechar" href="#">
                    [x]</a>
            </div>
            <div id="divValidationSummaryContent">
                <span style="float: left; margin-right: 0.3em;" class="ui-icon ui-icon-alert"></span>
                <strong>Inconsistências Encontradas:</strong>
            </div>
            <asp:ValidationSummary ID="vsValidation" CssClass="vsValidation" runat="server" />
            <asp:Label runat="server" ID="lblMensagemErro" CssClass="errorMessage"></asp:Label>
            <asp:Label runat="server" ID="lblMensagemSucesso" CssClass="successMessage"></asp:Label>
        </div>                
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".lnkClose").click(function (event) {
                $(".divValidationSummary").attr("style", "display: none;");
            });
            $(".lnkAtualBP").click(function (event) {
                $(".liLnkAtualBD").attr("style", "display: none;");
                $(".divTelaExportacaoCarregando").attr("style", "display: block;");
            });
        });   
    </script>
</asp:Content>
