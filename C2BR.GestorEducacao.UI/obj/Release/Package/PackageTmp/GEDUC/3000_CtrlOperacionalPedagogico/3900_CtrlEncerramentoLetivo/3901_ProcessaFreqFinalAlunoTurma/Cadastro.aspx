<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.GEDUC.F3000_CtrlOperacionalPedagogico.F3900_CtrlEncerramentoLetivo.F3901_ProcessaFreqFinalAlunoTurma.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 660px;
        }
        .ulDados input
        {
            margin-bottom: 0;
        }
        .ulDados table
        {
            border: none !important;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-right: 10px;
            margin-bottom: 10px;
        }
        .liClear
        {
            clear: both;
        }
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            float: right !important;
            margin-top: 12px;
            margin-bottom: 10px;
            margin-right: 55px !important;
            padding: 2px 3px 1px 3px;
        }
        .liEspaco
        {
            margin-left: 2px;
        }
        
        /*--> CSS DADOS */
        .divGrid
        {
            height: 368px;
            width: 620px;
            overflow-y: auto;
        }
        #divBarraPadraoContent
        {
            display: none;
        }
        
        #divBarraLanctoExameFinal
        {
            position: absolute;
            margin-left: 743px;
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
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>--%>
    <div id="divMensEfe" style="display: none; position: absolute; left: 50%; right: 50%;
        top: 50%; z-index: 1000; width: 150px" clientidmode="Static">
        <p id="pEfeM" clientidmode="Static" style="color: #6495ED; font-weight: bold; text-align: center;">
        </p>
        <asp:Image runat="server" ID="imgLoad" ImageUrl="~/Navegacao/Icones/carregando.gif"
            Style="z-index: 99"></asp:Image>
        <p style="color: #CCC; text-align: center">
            Poderá levar alguns minutos</p>
  <%--      <asp:UpdatePanel ID="updCount" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
                <p id="pQtCalc">
                </p>
    <%--        </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    <div id="div1" class="bar">
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
                <li id="btnNovo"><a href='<%= Request.Url.AbsoluteUri %>'>
                    <img title="Abre o formulario para Criar um Novo Registro." alt="Icone de Criar Novo Registro."
                        src="/BarrasFerramentas/Icones/NovoRegistro.png" />
                </a></li>
            </ul>
            <ul id="ulGravar">
                <li>
                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" ClientIDMode="Static"
                        OnClientClick="geraPadrao(this);">
                        <img title="Grava o registro atual na base de dados."
                             alt="Icone de Gravar o Registro." 
                             src="/BarrasFerramentas/Icones/Gravar.png" />
                    </asp:LinkButton>
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
                <li id="liImprimir">
                    <img title="Formula um relatório a partir dos parâmetros especificados e o exibe em Modo de Impressão."
                        alt="Icone de Impressao." src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />
                </li>
            </ul>
        </div>
    </div>
    <%--<asp:UpdatePanel ID="updGeral" runat="server">
        <ContentTemplate>--%>
            <ul id="ulDados" class="ulDados">
                <li>
                    <label for="ddlAno" title="Ano" class="lblObrigatorio">
                        Ano</label>
                    <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="ddlAno" runat="server"
                        OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAno"
                        ErrorMessage="Ano deve ser informado" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liEspaco">
                    <label for="ddlModalidade" title="Modalidade" class="lblObrigatorio">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade"
                        ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liEspaco">
                    <label for="ddlSerieCurso" title="Série/Curso" class="lblObrigatorio">
                        Série/Curso</label>
                    <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSerieCurso"
                        ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liEspaco">
                    <label for="ddlTurma" title="Turma" class="lblObrigatorio">
                        Turma</label>
                    <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma"
                        runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlTurma"
                        ErrorMessage="Turma deve ser informada" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li runat="server" title="Clique para Processar a Média Final" class="liBtnAdd">
                    <asp:LinkButton ID="btnAddQuestao" runat="server" class="btnLabel" OnClick="imgAdd_Click"
                        ClientIDMode="Static" OnClientClick="geraPadrao(this);">Processar Frequência Final</asp:LinkButton>
                </li>
                <li class="liClear">
                    <div id="divGrid" runat="server" class="divGrid">
                        <asp:GridView ID="grdBusca" CssClass="grdBusca" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="grdBusca_RowDataBound">
                            <RowStyle CssClass="rowStyle" />
                            <AlternatingRowStyle CssClass="alternatingRowStyle" />
                            <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                            <EmptyDataTemplate>
                                Nenhum registro encontrado.<br />
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="nire" HeaderText="NIRE">
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NoAlu" HeaderText="Aluno">
                                    <ItemStyle Width="290px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="StatusMateria" HeaderText="STATUS">
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="StatusMatricula" HeaderText="OBS">
                                    <ItemStyle Width="120px" HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </li>
            </ul>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <script type="text/javascript">
        function geraPadrao(el) {

            if (el == document.getElementById("btnAddQuestao")) {
                document.getElementById("pEfeM").innerHTML = "PROCESSANDO DADOS. AGUARDE...";
            }
            if (el == document.getElementById("btnSave")) {
                document.getElementById("pEfeM").innerHTML = "SALVANDO INFORMAÇÕES...";
            }

            //Mostra a mensagem de Efetivando matrícula e a imagem de Carregando.
            $("#divMensEfe").css("display", "block");
//            var qtTotal = "<%=qtTotal%>";

            setInterval(function () {
                $("#pEfeM").fadeIn();

//                $.ajax({
//                    type: "POST",
//                    url: "/Cadastro.aspx/RetornaQuantidade",
//                    data: "{}",
//                    contentType: "application/json",
//                    dataType: "json",
//                    success: function (msg) {
//                        console.log(msg.d);
//                        //                        document.getElementById("pQtCalc").innerHTML = (qtTotal + " / " + msg.d + "Processadas");
//                        alert(msg.d);
//                    },
//                    error: function () {
//                        document.getElementById("pQtCalc").innerHTML = "Erro ao Carregar";
//                    }
//                });

                //                var qtCalculado = "<%=qtCalculado%>";
                //                document.getElementById("pQtCalc").innerHTML = (qtTotal + " / " + qtCalculado + "Processadas");
            }, 800);

            setInterval(function () {
                $("#pEfeM").fadeOut();
            }, 400);
        }
    </script>
</asp:Content>
