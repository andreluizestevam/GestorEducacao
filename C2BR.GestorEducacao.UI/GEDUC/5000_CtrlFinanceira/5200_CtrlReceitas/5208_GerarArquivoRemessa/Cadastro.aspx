<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Inherits="C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5208_GerarArquivoRemessa.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 375px;
        }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        .liBtnAdd
        {
            clear: both;
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            margin-left: 150px;
            margin-top: 12px;
            margin-bottom: 8px;
            margin-right: 8px !important;
            padding: 2px 3px 1px 3px;
            width: 27px;
        }
        
        /*--> CSS DADOS */
        .divGrid
        {
            width: 370px;
            overflow-y: auto;
            margin-top: 10px;
            height: 300px;
        }
        .emptyDataRowStyle
        {
            background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important;
        }
        #divBarraPadraoContent
        {
            display: none;
        }
        #divBarraComoChegar
        {
            position: absolute;
            margin-left: 750px;
            margin-top: -25px;
            border: 1px solid #CCC;
            overflow: auto;
            width: 230px;
            padding: 3px 0;
        }
        #divBarraComoChegar ul
        {
            display: inline;
            float: left;
            margin-left: 10px;
        }
        #divBarraComoChegar ul li
        {
            display: inline;
            margin-left: -2px;
        }
        #divBarraComoChegar ul li img
        {
            width: 19px;
            height: 19px;
        }
        .ddlUnidade
        {
            width: 260px;
        }
        .check label
        {
            display: inline;
        }
        .check input
        {
            margin-left: -5px;
        }
        .fieldSet
        {
            border: 0px;
        }
        .fieldSet legend
        {
            font-size: 1.2em;
        }
        .liResumo
        {
            clear: both;
            margin-top: 10px;
            margin-left: 105px;
        }
        .liObser
        {
            clear: both;
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="div1" class="bar">
        <div id="divBarraComoChegar" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
            <ul id="ulNavegacao" style="width: 39px;">
                <li id="btnVoltarPainel"><a href="javascript:parent.showHomePage()">
                    <img title="Clique para voltar ao Painel Inicial." alt="Icone Voltar ao Painel Inicial."
                        src="/BarrasFerramentas/Icones/VoltarPainelGestor.png" />
                </a></li>
                <li id="btnVoltar"><a href="javascript:BackToHome();">
                    <img title="Clique para voltar a Pagina Anterior." alt="Icone Voltar a Pagina Anterior."
                        src="/BarrasFerramentas/Icones/VoltarPaginaAnterior.png" />
                </a></li>
            </ul>
            <ul id="ulEditarNovo" style="width: 39px;">
                <li id="btnEditar">
                    <img title="Abre o registro atualmente selecionado em modo de edicao." alt="Icone Editar."
                        src="/BarrasFerramentas/Icones/EditarRegistro_Desabilitado.png" />
                </li>
                <li id="btnNovo">
                    <img title="Abre o formulario para Criar um Novo Registro." alt="Icone de Criar Novo Registro."
                        src="/BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
                </li>
            </ul>
            <ul id="ulGravar">
                <li>
                    <img title="Grava o registro atual na base de dados." alt="Icone de Gravar o Registro."
                        src="/BarrasFerramentas/Icones/Gravar_Desabilitado.png" />
                </li>
            </ul>
            <ul id="ulExcluirCancelar" style="width: 39px;">
                <li id="btnExcluir">
                    <img title="Exclui o Registro atual selecionado." alt="Icone de Excluir o Registro."
                        src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
                </li>
                <li id="btnCancelar"><a href='<%= Request.Url.AbsoluteUri %>'>
                    <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                        alt="Icone de Cancelar Operacao Atual." src="/BarrasFerramentas/Icones/Cancelar.png" />
                </a></li>
            </ul>
            <ul id="ulAcoes" style="width: 39px;">
                <li id="btnPesquisar">
                    <img title="Realiza uma Pesquisa a partir dos Parametros de Pesquisa Informado."
                        alt="Icone de Pesquisa." src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
                </li>
                <li id="liImprimir">
                    <img title="Formula um relatório a partir dos parâmetros especificados e o exibe em Modo de Impressão."
                        alt="Icone de Impressao." src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />
                </li>
            </ul>
        </div>
    </div>
    <div style="height: 500px;">

        <ul class="ulDados" >

            <li>   
                <label for="ddlUnidadeContrato" class="lblObrigatorio" title="Unidade de Contrato">
                Unidade de Contrato</label>
                <asp:DropDownList ID="ddlUnidadeContrato" runat="server" CssClass="campoUnidadeEscolar"
                    ToolTip="Selecione a Unidade de Contrato" AutoPostBack="true" 
                    onselectedindexchanged="ddlUnidadeContrato_SelectedIndexChanged">
                </asp:DropDownList>
             </li>
             <li style="width:100%">
                <label for="ddlBoleto" class="lblObrigatorio" title="Boleto Bancário">
                Boleto</label>
                <asp:DropDownList ID="ddlBoleto" runat="server" CssClass="ddlBoleto"
                    ToolTip="Selecione o Boleto Bancário">
                </asp:DropDownList>
            </li>
            <li style="width:100%">
                <label for="ddlcnab" class="lblObrigatorio" title="CNAB">Formato CNAB</label>
                <asp:DropDownList ID="ddlcnab" runat="server" Width="45" CssClass="ddlBoleto">
                    <asp:ListItem Value="400">400</asp:ListItem>
                    <asp:ListItem Value="240">240</asp:ListItem>
                </asp:DropDownList>
            </li>

            <li id="Li1" runat="server" title="Processar arqui retorno" class="liBtnAdd">
                <asp:LinkButton ID="LinkButton1" style="margin: 0 auto" CssClass="btnAdd" runat="server" OnClick="btnGerar_Click">Gerar</asp:LinkButton>
                <div id="div3" runat="server" class="divTelaExportacaoCarregando" style="display:none" runat="server">
                    <img src="/Library/IMG/Gestor_Carregando.gif" alt="Carregando..." />
                </div> 
            </li>
            
            <li style="width:100%"></li>

            <li style="width:100%">
                <table border="1" cellspacing="3">
                    <tr>
                        <td>
                            <b>Banco</b>
                        </td>
                        <td>
                            <b>Formato CNAB</b>
                        </td>
                    </tr>                    
                    <tr>
                        <td>
                            001 - Banco do Brasil
                        </td>
                        <td>
                            CNAB 240
                        </td>
                    </tr>
                    <tr>
                        <td>
                            033 - Banco Santander
                        </td>
                        <td>
                            CNAB 400
                        </td>
                    </tr>
                    <tr>
                        <td>
                            104 - Caixa Econômica Federal
                        </td>
                        <td>
                            CNAB 240
                        </td>
                    </tr>
                    <tr>
                        <td>
                            237 - Bradesco
                        </td>
                        <td>
                            CNAB 400
                        </td>
                    </tr>
                </table>
            </li>
    </ul>

    <script type="text/javascript">

        $(document).ready(function () {
            $(".divTelaExportacaoCarregando").attr("style", "display: none;");
            $(".btnAdd").attr("style", "display: block;");
            $(".liBtnAdd").attr("style", "");
        });

        function volta() {
            $(".divTelaExportacaoCarregando").attr("style", "display: none;");
            $(".btnAdd").attr("style", "display: block;");
            $(".liBtnAdd").attr("style", "");
        };

//        $(".btnAdd").click(function (event) {
//            $(".divTelaExportacaoCarregando").attr("style", "display: block;");
//            $(".btnAdd").attr("style", "display: none;");
//            $(".liBtnAdd").attr("style", "border:0; background:none");
//        });
      </script>


</asp:Content>
