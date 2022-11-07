<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="DemonEnvioEmailParce.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3060_CtrlEmailInstitucional._3069_Relatorios.DemonEnvioEmailParce" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 280px;
        }
        .ulDados li
        {
            margin-top: 5px;
            margin-left: 5px;
        }
        .liModalidade, .liSerie, .liResponsavel
        {
            clear: both;
        }
        .chk label
        {
            display: inline;
        }
        .lblDivData
        {
        	display:inline;
        	margin: 0 5px;
        	margin-top: 0px;
        }
        #divBarraPadraoContent
        {
            display: none;
        }
        
        #divBarraLanctoExameFinal
        {
            position: absolute;
            margin-left: 773px;
            margin-top: -50px;
            border: 1px solid #CCC;
            overflow: auto;
            width: 250px !important;
            padding: 3px 0;
        }
        #divBarraLanctoExameFinal ul
        {
            display: inline;
            float: left;
            margin-left: 10px;
        }
        #divBarraLanctoExameFinal ul li
        {
            display: inline;
            margin-left: -2px;
        }
        #divBarraLanctoExameFinal ul li img
        {
            width: 19px;
            height: 19px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divMensEfe" style="display: none; position: absolute; left: 50%; right: 50%;
        top: 50%; z-index: 1000; width: 150px" clientidmode="Static">
        <p id="pEfeM" clientidmode="Static" style="color: #6495ED; font-weight: bold; text-align: center;">
        </p>
        <asp:Image runat="server" ID="imgLoad" ImageUrl="~/Navegacao/Icones/carregando.gif"
            Style="z-index: 99"></asp:Image>
    </div>
    <div id="div1" class="bar" style="margin-left: -40px">
        <div id="divBarraLanctoExameFinal" class="boxRoundCorner" style="border-radius: 10px 10px 10px 10px;">
            <ul id="ulNavegacao" style="width: 43px;">
                <li id="btnVoltarPainel"><a href="javascript:parent.showHomePage()">
                    <img title="Clique para voltar ao Painel Inicial." alt="Icone Voltar ao Painel Inicial."
                        src="/BarrasFerramentas/Icones/VoltarPainelGestor.png" />
                </a></li>
                <li id="btnVoltar"><a href="javascript:BackToHome();">
                    <img title="Clique para voltar a Pagina Anterior." alt="Icone Voltar a Pagina Anterior."
                        src="/BarrasFerramentas/Icones/VoltarPaginaAnterior.png" />
                </a></li>
            </ul>
            <ul id="ulEditarNovo" style="width: 43px;">
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
            <ul id="ulExcluirCancelar" style="width: 43px;">
                <li id="btnExcluir">
                    <img title="Exclui o Registro atual selecionado." alt="Icone de Excluir o Registro."
                        src="/BarrasFerramentas/Icones/Excluir_Desabilitado.png" />
                </li>
                <li id="btnCancelar"><a href='<%= Request.Url.AbsoluteUri %>'>
                    <img title="Cancela TODAS as alterações feitas no registro." alt="Icone de Cancelar Operacao Atual."
                        src="/BarrasFerramentas/Icones/Cancelar.png" />
                </a></li>
            </ul>
            <ul id="ulAcoes" style="width: 43px;">
                <li id="btnPesquisar">
                    <img title="Volta ao formulário de pesquisa." alt="Icone de Pesquisa." src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
                </li>
                <asp:LinkButton runat="server" ID="lnkImprimir" OnClick="lnkImprimir_Click" ClientIDMode="Static"
                    OnClientClick="geraPadrao(this);">
                <li id="liImprimir">
                    <img title="Formula um relatório a partir dos parâmetros especificados e o exibe em Modo de Impressão."
                        alt="Icone de Impressao." src="/BarrasFerramentas/Icones/Imprimir.png" />
                </li>
                </asp:LinkButton>
            </ul>
        </div>
    </div>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                
                <li id="liAlun" runat="server">
                    <label title="Selecione o Parceiro para visualização no relatório">Parceiros</label>
                    <asp:DropDownList runat="server" ID="ddlParce" ToolTip="Selecione o Parceiro para visualização no relatório"
                        Width="230px" >
                    </asp:DropDownList>
                </li>
                <li class="liPeriodo">
                    <label for="txtPeriodo" title="Período">Período de Pesquisa</label>
                    <asp:TextBox ID="txtDataPeriodoIni" ToolTip="Informe a Data Inicial do Período" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
                    <asp:TextBox ID="txtDataPeriodoFim" ToolTip="Informe a Data Final do Período" CssClass="campoData" runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="cvDataPeriodoFim" runat="server" CssClass="validatorField"
                        ForeColor="Red" ControlToValidate="txtDataPeriodoFim" ControlToCompare="txtDataPeriodoIni"
                        Type="Date" Operator="GreaterThanEqual" ErrorMessage="Data Final não pode ser menor que Data Inicial.">
                    </asp:CompareValidator>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
    <%--<script type="text/javascript">
        //        $(document).ready(function () {
        //            JavscriptReturn();
        //        });

        //        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //        prm.add_endRequest(function () {
        //            JavscriptReturn();
        //        });

        //        function JavscriptReturn() {

        //            $("#chkForm").click(function (evento) {
        //                if ($("#chkForm").attr("checked")) {
        //                    $("#chkComLanc").attr("checked", false);
        //                }
        //                else {
        //                    $("#chkForm").attr("checked", true);
        //                }
        //            });

        //            $("#chkComLanc").click(function (evento) {
        //                if ($("#chkComLanc").attr("checked")) {
        //                    $("#chkForm").attr("checked", false);
        //                }
        //                else {
        //                    $("#chkComLanc").attr("checked", true);
        //                }
        //            });

        //            $(".clear").click(function (evento) {
        //                $("#divMensEfe").css("display", "none");
        //            });
        //        }

        //        function geraPadrao(el) {

        //            document.getElementById("pEfeM").innerHTML = "PREPARANDO RELATÓRIO...";

        //            //Mostra a mensagem de Efetivando matrícula e a imagem de Carregando.
        //            $("#divMensEfe").css("display", "block");

        //            setInterval(function () {
        //                $("#pEfeM").fadeIn();
        //            }, 800);

        //            setInterval(function () {
        //                $("#pEfeM").fadeOut();
        //            }, 400);
        //        }

    </script>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
