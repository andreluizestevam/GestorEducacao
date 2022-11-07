<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7110_RegisPontoFuncional.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 560px;
        }
        /*--> CSS LIs */
        .liUnidadeFrequencia
        {
            clear: both;
            margin-top: 5px;
        }
        .liData
        {
            margin-top: -5px;
            clear: both;
        }
        .liMatricula
        {
            margin-top: -5px;
            clear: both;
        }
        .liFuncao
        {
            margin-top: -5px;
        }
        .liHoraCadastro
        {
            margin-top: -5px;
            margin-left: 5px;
        }
        .liTipo
        {
            margin-top: -5px;
            margin-left: 5px;
        }
        .liMsg
        {
            margin-top: 5px !important;
            margin-bottom: 5px !important;
            margin-left: 30px !important;
        }
        .liGrid
        {
            float: right !important;
            padding-top: 0px;
        }
        .liBarraTituloDados
        {
            background-color: #EEEEEE;
            margin-bottom: 5px;
            padding: 2px;
            text-align: center;
            width: 208px;
            float: left !important;
        }
        .liBarraTituloGrid
        {
            background-color: #EEEEEE;
            margin-bottom: 5px;
            padding: 2px;
            text-align: center;
            width: 230px;
            float: right !important;
        }
        .liDadosFrequencia
        {
            float: left !important;
            clear: both !important;
        }
        .liHistoricoFrequencia
        {
            float: right !important;
        }
        
        /*--> CSS DADOS */
        .txtData
        {
            width: 80px;
        }
        .lblMsgInformacao
        {
            color: RED;
            font: 10px Arial;
        }
        .divFreq
        {
            <%--border:1px solid black;--%>;
            width:380px;
            height:155px;
            overflow-y:scroll;
        }
        .labelPixel
        {
            margin-bottom: 0px;
        }
        .grdBusca .rowStyle:Hover, .grdBusca .alternatingRowStyle:Hover
        {
            color: #FFFFFF !important;
            <%--background-color: #ffd000;--%>;
            cursor: pointer;
        }
        .fldDadosFrequencia, .fldHistoricoFrequencia
        {
            border: none;
            margin: 0;
        }
        .grdBusca th
        {
            font-size: 9px;
        }
        .grdBusca
        {
            width: 225px;
        }
        .txtFuncao
        {
            width: 143px;
        }
        #divBarraPadraoContent
        {
            display: none;
        }
        #divBarraContrFrequ
        {
            position: absolute;
            margin-left: 770px;
            margin-top: -50px;
            border: 1px solid #CCC;
            overflow: auto;
            width: 230px;
            padding: 3px 0;
        }
        #divBarraContrFrequ ul
        {
            display: inline;
            float: left;
            margin-left: 10px;
        }
        #divBarraContrFrequ ul li
        {
            display: inline;
            margin-left: -2px;
        }
        #divBarraContrFrequ ul li img
        {
            width: 19px;
            height: 19px;
        }
    </style>
    <!--[if IE]>
<style type="text/css">
       #divBarraContrFrequ { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="div1" class="bar">
        <div id="divBarraContrFrequ" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
            <ul id="ulNavegacao" style="width: 39px;">
                <asp:HiddenField ID="HidColabPlanto" runat="server" />
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
                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnContrFrequ_Click">
                    <img title="Grava o registro atual na base de dados."
                            alt="Icone de Gravar o Registro." 
                            src="/BarrasFerramentas/Icones/Gravar.png" />
                    </asp:LinkButton>
                </li>
            </ul>
            <ul id="ulExcluirCancelar" style="width: 39px;">
                <li id="btnExcluir">
                    <img title="Exclui o Registro atual selecionado." alt="Icone de Excluir o Registro."
                        src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
                </li>
                <li id="btnCancelar">
                    <img title="Cancela a Pesquisa atual e limpa o Formulário de Parâmetros de Pesquisa."
                        alt="Icone de Cancelar Operacao Atual." src="/BarrasFerramentas/Icones/Cancelar_Desabilitado.png" />
                </li>
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
    <ul id="ulDados" class="ulDados">
        <li class="liBarraTituloDados" style="margin-top: 10px;"><span>DADOS DA FREQUÊNCIA</span></li>
        <li class="liBarraTituloGrid" style="margin-top: 10px; width: 375px; margin-right:-80px;"><span>HISTÓRICO
            DE FREQUÊNCIA MENSAL</span></li>
        <li class="liDadosFrequencia">
            <fieldset class="fldDadosFrequencia">
                <ul>
                    <li id="liColaborador" class="liColaborador">
                        <label id="lblColaborador" for="txtColaborador" runat="server" style="margin-bottom:1px;">
                            Colaborador</label>
                        <asp:TextBox ID="txtColaborador" CssClass="campoNomePessoa" ToolTip="Nome do Colaborador"
                            Enabled="false" runat="server"></asp:TextBox>
                    </li>
                    <li class="liMatricula">
                        <label id="lblMatricula" for="txtMatricula" runat="server" style="margin-bottom:1px;">
                            Matrícula</label>
                        <asp:TextBox ID="txtMatricula" Width="60" MaxLength="9" ToolTip="Numéro da Matrícula"
                            Enabled="false" runat="server"></asp:TextBox>
                    </li>
                    <li class="liFuncao">
                        <label id="lblFuncao" for="txtFuncao" runat="server" style="margin-bottom:1px;">
                            Função</label>
                        <asp:TextBox ID="txtFuncao" CssClass="txtFuncao" Enabled="false" ToolTip="Função do Colaborador"
                            runat="server"></asp:TextBox>
                    </li>
                    <li id="liCategoriaFuncional" style="margin-top: -5px; clear: both;">
                        <label id="lblCategoriaFuncional" for="txtCategoriaFuncional" runat="server" style="margin-bottom:1px;">
                            Categoria Funcional</label>
                        <asp:TextBox ID="txtCategoriaFuncional" CssClass="txtCategoriaFuncional" Enabled="false"
                            ToolTip="Categoria Funcional" runat="server"></asp:TextBox>
                    </li>
                    <li style="margin-top: -6px;">
                        <label id="lblTipoPonto" for="ddlTipoPonto" runat="server" style="margin-bottom:1px;">
                            Tipo Ponto</label>
                        <asp:DropDownList runat="server" ToolTip="Selecione o Tipo de Ponto" Width="100px" ID="ddlTipoPonto"
                            OnSelectedIndexChanged="ddlTipoPonto_OnSelectedIndexChanged" AutoPostBack="true"
                            Enabled="false">
                            <asp:ListItem Text="Normal" Value="N" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Plantão" Value="P"></asp:ListItem>
                        </asp:DropDownList>
                    </li>
                    <li class="liUnidadeFrequencia">
                        <label id="lblUnidadeFrequencia" for="txtUnidadeFrequencia" runat="server" style="margin-bottom:1px;">
                            Unidade de Freqüência</label>
                        <asp:TextBox ID="txtUnidadeFrequencia" CssClass="campoUnidadeEscolar" Enabled="false"
                            runat="server" ToolTip="Unidade de Frequência"></asp:TextBox>
                    </li>
                    <li class="liData">
                        <label for="txtData" class="labelPixel">
                            Data</label>
                        <asp:TextBox ID="txtData" runat="server" CssClass="campoData" Enabled="false" MaxLength="8"
                            ToolTip="Data da Freqüência"></asp:TextBox>
                    </li>
                    <li class="liHoraCadastro">
                        <label id="lblData" for="txtHora" style="margin-bottom:1px;" runat="server">
                            Hora</label>
                        <asp:TextBox ID="txtHora" runat="server" MaxLength="5" Enabled="false" CssClass="txtHora"
                            ToolTip="Hora da Freqüência"></asp:TextBox>
                    </li>
                    <li class="liTipo">
                        <label id="lblTipo" for="txtTipo" style="margin-bottom:1px;" runat="server">
                            TP Registro</label>
                        <asp:TextBox ID="txtTipo" runat="server" Width="50px" Enabled="false" CssClass="txtTipo"
                            ToolTip="Tipo de Ponto da Freqüência"></asp:TextBox>
                    </li>
                    <li class="liMsg">
                        <asp:Label ID="lblMsgInformacao" runat="server" CssClass="lblMsgInformacao"></asp:Label>
                    </li>
                    <asp:TextBox runat="server" ID="txtidagendapla" Visible="false"></asp:TextBox>
                </ul>
            </fieldset>
        </li>
        <li class="liHistoricoFrequencia" style="width: 300px; margin-right:-1px;">
            <div class="divFreq">
                <fieldset class="fldHistoricoFrequencia">
                    <ul>
                        <li class="liGrid" style="float: none !important;">
                            <asp:GridView runat="server" ID="grdHistoricoFrequencia" CssClass="grdBusca" AutoGenerateColumns="False"
                                ToolTip="Histórioco de Freqüência" GridLines="Vertical">
                                <RowStyle CssClass="rowStyle" />
                                <AlternatingRowStyle CssClass="alternatingRowStyle" />
                                <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                                <EmptyDataTemplate>
                                    Nenhum registro encontrado no dia de hoje.<br />
                                </EmptyDataTemplate>
                                <PagerStyle CssClass="grdFooter" />
                            </asp:GridView>
                        </li>
                    </ul>
                </fieldset>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function ($) {
            $(".txtHora").mask("?99:99");
            $(".txtMatricula").mask("?99.999-9");
        });
        $(document).ready(function () {
            $(".rowStyle, .alternatingRowStyle").unbind();
            $(".rowStyle:Hover, .alternatingRowStyle:Hover").unbind();
            $(".txtHora").mask("?99:99");
            $(".txtMatricula").mask("?99.999-9");
        });
    </script>
</asp:Content>
