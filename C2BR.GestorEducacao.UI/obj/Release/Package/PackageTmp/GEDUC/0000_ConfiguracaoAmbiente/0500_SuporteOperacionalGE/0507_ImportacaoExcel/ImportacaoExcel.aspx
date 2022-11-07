<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoGenericas.Master"
    AutoEventWireup="true" CodeBehind="ImportacaoExcel.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0507_ImportacaoExcel.ImportacaoExcel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 1000px; margin-top: 15px; }
        
        /*--> CSS LIs */
        #divBarraExportarBasePortal ul li { display: inline; margin-left: -2px; }
        .liCabecalho { background-color:#FFFFE0; width: 990px;text-align:center;padding:4px 0;margin-bottom: 1px; }
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        #divBarraExportarBasePortal { position:absolute; margin-left: 750px; margin-top:-65px; border: 1px solid #CCC; overflow: auto; width: 230px; padding: 3px 0; }
        #divBarraExportarBasePortal ul { display: inline; float: left; margin-left: 10px; }        
        #divBarraExportarBasePortal ul li img { width: 19px; height: 19px; }
        .chkSelecionarTodos label { display: inline; margin-left: -2px; }
        .divGrid
        {
            /*height: 360px;*/
            width: 990px;
            overflow-y: auto;
            margin-top: 3px;            
        }
        .emptyDataRowStyle { background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important; }
        .lblCabecalho { text-transform:uppercase; font-weight:bold; padding-top: 3px; }
        .grdBusca th { background-color: #8B8989; }
        .lblBtnActionRBD { font-size: 1.3em; color: #473C8B; }
        .lblAtuaBDPorRelac { font-size: 1.3em; color: #FF8C00; }
        .liLnkGerarScriptBD { margin-top: 15px; margin-left: 298px; border: 1px solid #CCCCCC; padding: 3px; float: left !important; }
        .liLnkAtualBD { margin-top: 15px; margin-left: 20px; border: 1px solid #CCCCCC; padding: 3px; float: left !important; }
        .grdBusca .selectedRowStyle { background-color: #F8F8FF; color: #333333; }
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
        <li style="margin-right: 0px; padding-left: 5px;margin-top: 40px;">
            <ul>
                <li class="liCabecalho">                    
                    <label class="lblCabecalho" title="Grid de Exportação de Dados - Portal de Relacionamento">Grid de Importação de Dados pelo Excel</label>          
                </li>

                <li class="liClear">
                    <asp:CheckBox ID="chkAluno" CssClass="chkSelecionarTodos" runat="server" style="float:left; margin-left: 6px; margin-top: 3px;" Text="Aluno"/>
                    <asp:CheckBox ID="chkReponsavel" CssClass="chkSelecionarTodos" runat="server" style="float:left; margin-left: 6px; margin-top: 3px;" Text="Responsável"/>
                    <asp:CheckBox ID="chkFuncionario" CssClass="chkSelecionarTodos" runat="server" style="float:left; margin-left: 6px; margin-top: 3px;" Text="Funcionário"/>
                </li>
            </ul>
        </li>
        <li id="liLnkAtualBD" runat="server" class="liLnkAtualBD">
            <asp:LinkButton ID="lnkAtualBP" runat="server" OnClick="lnkAtualBP_Click" CssClass="lnkAtualBP" ToolTip="Importar Tabelas do Excel">
                <img id="imgIconAtualBP" width="20" height="20" src='/Library/IMG/Gestor_DBRestore.png' alt="Importar Tabelas do Excel" />
                <asp:Label runat="server" ID="lblAtuaBDPorRelac" CssClass="lblAtuaBDPorRelac" Text="Importar Tabelas do Excel"></asp:Label>
            </asp:LinkButton>
        </li>
        <li id="li1" runat="server" class="liLnkAtualBD">
            <asp:LinkButton ID="lnkAtualSQL" runat="server" OnClick="lnkAtualSQL_Click" CssClass="lnkAtualBP" ToolTip="Importar Tabelas do SQL">
                <img id="img1" width="20" height="20" src='/Library/IMG/Gestor_DBRestore.png' alt="Importar Tabelas do SQL" />
                <asp:Label runat="server" ID="Label1" CssClass="lblAtuaBDPorRelac" Text="Importar Tabelas do SQL"></asp:Label>
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

            $(".lnkGerarScriptBP").click(function (event) {
                //event.preventDefault();
                $(".liLnkGerarScriptBD").attr("style", "display: none;");
                $(".liLnkAtualBD").attr("style", "display: none;");                
                $(".divTelaExportacaoCarregando").attr("style", "display: block;");
                //$(".chkSelecionarTodos input").attr("disabled", "disabled");
                //$('.lnkAtualBP').attr("disabled", "disabled");
                //$('.lnkGerarScriptBP').attr("disabled", "disabled");
            });

            $(".lnkAtualBP").click(function (event) {
                //var location = $('#link1').attr("href");
                //$("#link1").removeAttr('href');
                //event.preventDefault();
                $(".liLnkGerarScriptBD").attr("style", "display: none;");
                $(".liLnkAtualBD").attr("style", "display: none;");
                $(".divTelaExportacaoCarregando").attr("style", "display: block;");
                //$(".chkSelecionarTodos input").attr("disabled", "disabled");
                //$('.lnkAtualBP').attr("disabled", "disabled");
                //$('.lnkGerarScriptBP').attr("disabled", "disabled");                
            });
        });   
    </script>
</asp:Content>
