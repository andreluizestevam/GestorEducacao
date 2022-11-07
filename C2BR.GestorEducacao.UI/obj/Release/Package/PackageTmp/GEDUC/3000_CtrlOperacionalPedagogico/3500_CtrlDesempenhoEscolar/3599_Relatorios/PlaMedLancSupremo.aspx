<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="PlaMedLancSupremo.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3599_Relatorios.PlaMedLancSupremo" %>
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
        .liAnoRefer, .liModalidade, .liSerie, .liUnidade, .liUnidadeCont
        {
            clear: both;
        }
        .chk label
        {
            display: inline;
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
                <li id="liAnoRefer" runat="server" class="liAnoRefer">
                    <label class="lblObrigatorio" title="Ano de Referência">
                        Ano Referência</label>
                    <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione um Ano de Referência" CssClass="ddlAno"
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAnoRefer_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlAnoRefer" Text="*" ErrorMessage="Campo Ano Referência é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liUnidadeCont">
                    <label class="lblObrigatorio" title="Unidade/Escola">
                        Unidade</label>
                    <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server">
                    </asp:DropDownList>
                </li>
                <li id="li1" runat="server" style="clear: both">
                    <label class="lblObrigatorio" for="ddlBimestre" title="Turma">
                        Bimestre</label>
                    <asp:DropDownList ID="ddlBimestre" ToolTip="Selecione um Bimestre" CssClass="ddlBimestre"
                        runat="server">
                        <asp:ListItem Value="B1">Bimestre 1</asp:ListItem>
                        <asp:ListItem Value="B2">Bimestre 2</asp:ListItem>
                        <asp:ListItem Value="B3">Bimestre 3</asp:ListItem>
                        <asp:ListItem Value="B4">Bimestre 4</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvBimestre" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlBimestre" Text="*" ErrorMessage="Campo Bimestre é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li id="liModalidade" runat="server" class="liModalidade">
                    <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" runat="server" Width="130px"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged"
                        ClientIDMode="Static" CssClass="clear">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlModalidade" Text="*" ErrorMessage="Campo Modalidade é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li id="liSerie" runat="server" class="liSerie">
                    <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                        Série/Curso</label>
                    <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" runat="server" Width="110px"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged"
                        CssClass="clear">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" ControlToValidate="ddlSerieCurso"
                        Text="*" ErrorMessage="Campo Série/Curso é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li id="liTurma" class="liTurma" runat="server">
                    <label class="lblObrigatorio" title="Turma">
                        Turma</label>
                    <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" Width="130px" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged"
                        AutoPostBack="true" runat="server" CssClass="clear">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlTurma" Text="*" ErrorMessage="Campo Turma é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li style="clear: both;">
                    <label title="Selecione o agrupamento desejado do relatório">
                        Agrupado por</label>
                    <asp:DropDownList runat="server" ID="ddlAgrupPor" Width="130px" ToolTip="Selecione o agrupamento desejado do relatório"
                        OnSelectedIndexChanged="ddlAgrupPor_OnSelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="A" Text="Aluno"></asp:ListItem>
                        <asp:ListItem Value="D" Text="Disciplina"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li id="liDisc" class="liTurma" runat="server" style="margin-bottom: 10px;" visible="false">
                    <label class="lblObrigatorio" for="ddlMateria" title="Turma">
                        Disciplina</label>
                    <asp:DropDownList ID="ddlMateria" ToolTip="Selecione a Disciplina" CssClass="ddlMateria"
                        Width="230px" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvMateria" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlMateria" Text="*" ErrorMessage="Campo Disciplina é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li style="margin-bottom: 10px; clear: both;" id="liAlun" runat="server">
                    <label title="Selecione o Aluno para visualização no relatório">
                        Aluno</label>
                    <asp:DropDownList runat="server" ID="ddlAluno" ToolTip="Selecione o Aluno para visualização no relatório"
                        Width="230px">
                    </asp:DropDownList>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li>
            <label>
                Escolha o tipo de Planilha</label></li>
        <li style="clear: both">
            <asp:CheckBox runat="server" ID="chkForm" Text="Formulário" CssClass="chk" ClientIDMode="Static"
                Checked="true" />
        </li>
        <li style="margin-left: 10px">
            <asp:CheckBox runat="server" ID="chkComLanc" Text="Com Lançamentos" CssClass="chk"
                ClientIDMode="Static" />
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            JavscriptReturn();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            JavscriptReturn();
        });

        function JavscriptReturn() {

            $("#chkForm").click(function (evento) {
                if ($("#chkForm").attr("checked")) {
                    $("#chkComLanc").attr("checked", false);
                }
                else {
                    $("#chkForm").attr("checked", true);
                }
            });

            $("#chkComLanc").click(function (evento) {
                if ($("#chkComLanc").attr("checked")) {
                    $("#chkForm").attr("checked", false);
                }
                else {
                    $("#chkComLanc").attr("checked", true);
                }
            });

            $(".clear").click(function (evento) {
                $("#divMensEfe").css("display", "none");
            });
        }

        function geraPadrao(el) {

            document.getElementById("pEfeM").innerHTML = "PREPARANDO RELATÓRIO...";

            //Mostra a mensagem de Efetivando matrícula e a imagem de Carregando.
            $("#divMensEfe").css("display", "block");

            setInterval(function () {
                $("#pEfeM").fadeIn();
            }, 800);

            setInterval(function () {
                $("#pEfeM").fadeOut();
            }, 400);
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
