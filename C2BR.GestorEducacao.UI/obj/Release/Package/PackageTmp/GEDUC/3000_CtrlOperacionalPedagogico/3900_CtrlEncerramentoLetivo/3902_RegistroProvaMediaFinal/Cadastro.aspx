<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3900_CtrlEncerramentoLetivo.F3902_RegistroProvaMediaFinal.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 790px;
        }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            float: right !important;
            margin-left: 10px;
            margin-top: 12px;
            margin-bottom: 8px;
            margin-right: 8px !important;
            padding: 2px 3px 1px 3px;
        }
        .liBarraTitulo
        {
            background-color: #EEEEEE;
            margin: 20px 0 5px 0;
            padding: 2px;
            text-align: center;
            width: 900px;
        }
        .liEspaco
        {
            margin-left: 10px;
        }
        
        /*--> CSS DADOS */
        .divGrid
        {
            height: 360px;
            width: 900px;
            overflow-y: auto;
            margin-top: 10px;
            <%--margin-left: 140px !important;--%>;
        }
        
        .divCarregando
        {
            width: 100%;
            text-align: center;
            position: absolute;
            z-index: 9999;
            left: 50px;
            top: 40%;
        }
        .chk label
        {
            display: inline;
            margin-left: -4px;
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
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
    <div id="divMensEfe" style="display: none; position: absolute; left: 50%; right: 50%;
        top: 50%; z-index: 1000; width: 150px" clientidmode="Static">
        <p id="pEfeM" clientidmode="Static" style="color: #6495ED; font-weight: bold; text-align: center;">
        </p>
        <asp:Image runat="server" ID="imgLoad" ImageUrl="~/Navegacao/Icones/carregando.gif"
            Style="z-index: 99"></asp:Image>
        <p style="color: #CCC; text-align: center">
            Poderá levar alguns minutos</p>
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
    <div id="conteudoGeral" class="conteudoGeral">
        <ul id="ulDados" class="ulDados">
            <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <li>
                <label for="ddlAno" title="Ano" class="lblObrigatorio">
                    Ano</label>
                <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="ddlAno" runat="server"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlAno" ErrorMessage="Ano deve ser informado"
                    Display="None"></asp:RequiredFieldValidator>
            </li>
            <li class="liEspaco">
                <label for="ddlModalidade" title="Modalidade" class="lblObrigatorio">
                    Modalidade</label>
                <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade"
                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlModalidade"
                    ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li class="liEspaco">
                <label for="ddlSerieCurso" title="Série/Curso" class="lblObrigatorio">
                    Série/Curso</label>
                <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso"
                    runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="ddlSerieCurso"
                    ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li class="liEspaco">
                <label for="ddlTurma" title="Turma" class="lblObrigatorio">
                    Turma</label>
                <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma"
                    runat="server">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="ddlTurma"
                    ErrorMessage="Turma deve ser informada" Display="None"></asp:RequiredFieldValidator>
            </li>
            <li style="margin-top: 13px;">
                <asp:CheckBox runat="server" ID="chkPrioriAnalFreq" Text="Priorizar Análise Anual de Frequência"
                    CssClass="chk" />
            </li>
            <li runat="server" title="Clique para Processar a Média Final" class="liBtnAdd">
                <asp:LinkButton ID="btnAddQuestao" runat="server" class="btnLabel" OnClick="imgAdd_Click"
                    ClientIDMode="Static" OnClientClick="geraPadrao(this);">Processar Média Final</asp:LinkButton>
            </li>
            <li id="barraTitulo" runat="server" class="liBarraTitulo" style="margin-top: 20px;"
                visible="false"><span>Alunos</span> </li>
            <li class="liClear" style="margin-left: -40px;">
                <div id="divGrid" runat="server" class="divGrid">
                    <asp:GridView ID="grdAlunos" CssClass="grdBusca" Width="100%" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="codigoCadastroAluno">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum registro encontrado.<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="nire" HeaderText="Nire">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nomeAluno" HeaderText="Aluno"></asp:BoundField>
                            <asp:TemplateField HeaderText="OBS">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# bind("codigoAluno") %>' />
                                    <ul>
                                        <li style="float: left">
                                            <asp:Label runat="server" ID="lblObs" Text='<%# bind("obs") %>'></asp:Label>
                                        </li>
                                        <li style="float: right">
                                                     <asp:ImageButton runat="server" ID="imgDet" ImageUrl="/Library/IMG/Gestor_ImgInformacao.png"
                                                        ToolTip="Clique para abrir tela com informações sobre as análises das Disciplinas e Notas do(a) Aluno(a)" OnClick="imgDet_OnClick" Width="10px" Height="10px" />
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="obs" HeaderText="Obs"></asp:BoundField>--%>
                            <asp:BoundField DataField="status" HeaderText="Status">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nota" HeaderText="Conceito" DataFormatString="{0:N2}">
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="faltas" HeaderText="FALTAS">
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
            <%--    </ContentTemplate>
            </asp:UpdatePanel>--%>
        </ul>
    </div>
    <div id="divDetalhesNotas" style="display: none; height: 385px !important;">
        <ul>
            <li>
                <asp:Label runat="server" ID="lblNomeAluno" Style="font-weight: bold; font-size: 14px;"></asp:Label>
            </li>
            <li class="liClear">
                <div id="div2" runat="server" class="divGrid" style="width: 450px">
                    <asp:GridView ID="grdDetalhes" CssClass="grdBusca" Width="100%" runat="server" AutoGenerateColumns="False"
                        OnRowDataBound="grdDetalhes_OnRowDataBound">
                        <RowStyle CssClass="rowStyle" />
                        <AlternatingRowStyle CssClass="alternatingRowStyle" />
                        <EmptyDataTemplate>
                            Nenhum registro encontrado.<br />
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:TemplateField HeaderText="DISCIPLINA">
                                <ItemTemplate>
                                    <itemstyle width="120px" horizontalalign="Left" />
                                    <asp:Label runat="server" ID="lblNoMat" Text='<%# bind("NO_MATERIA") %>'></asp:Label>
                                    <asp:HiddenField runat="server" ID="hidCoAprov" Value='<%# bind("DE_RESULTADO") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="VL_MEDIA_FINAL_V" HeaderText="MDFIM">
                                <ItemStyle Width="50px" HorizontalAlign="Right"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="VL_NOTA_RECU_V" HeaderText="NTRECU">
                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DE_RESULTADO_V" HeaderText="RESULTADO"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </li>
        </ul>
    </div>
    <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="divCarregando">
                <asp:Image ID="imgCarregando" runat="server" AlternateText="Carregando" ImageAlign="Middle"
                    ImageUrl="~/Navegacao/Icones/carregando.gif" ToolTip="Carregando a solicitação" /></div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
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

            setInterval(function () {
                $("#pEfeM").fadeIn();
            }, 800);

            setInterval(function () {
                $("#pEfeM").fadeOut();
            }, 400);
        }

        function mostraModal() {
            //Mostra a grid de tipos de exames
            $('#divDetalhesNotas').dialog({ autoopen: false, modal: true, width: 470, height: 400, resizable: false, title: "DETALHES DE NOTAS",
                //                open: function () { $('#divLoadShowExames').show(); }
                open: function (type, data) { $(this).parent().appendTo("form"); },
                close: function (type, data) { ($(this).parent().replaceWith("")); }
            });
        }
    </script>
</asp:Content>
