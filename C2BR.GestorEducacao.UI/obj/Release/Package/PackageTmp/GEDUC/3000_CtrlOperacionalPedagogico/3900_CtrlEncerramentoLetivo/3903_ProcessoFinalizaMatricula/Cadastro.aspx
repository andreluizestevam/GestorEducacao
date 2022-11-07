<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3900_CtrlEncerramentoLetivo.F3903_ProcessoFinalizaMatricula.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 760px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liBtnAdd
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;            
            margin-left: 15px;
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
            width: 590px;
        }
        .liEspaco { margin-left: 10px; }
        
        /*--> CSS DADOS */
        .divGrid
        {
            height: 360px;
            width: 600px;
            overflow-y: auto;
            margin-top:10px;
        }
        .emptyDataRowStyle { background: #FFFFFF url(/Library/IMG/Gestor_ImgInformacao.png) no-repeat scroll 201px bottom !important; }        
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
        .chk label {display:inline; margin-left:-2px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
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
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlAno" title="Ano" class="lblObrigatorio">
                Ano</label>
            <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="ddlAno" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlAno" ErrorMessage="Ano deve ser informado"
                Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlModalidade" title="Modalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlSerieCurso" class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" CssClass="campoSerieCurso" ToolTip="Selecione a Série/Curso" runat="server" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlTurma" class="lblObrigatorio" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informada"  Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top:12px;">
            <asp:CheckBox runat="server" ID="chkFinaApenasAprovados" Text="Finalizar apenas os Aprovados" CssClass="chk" ToolTip="Quando marcado, Salva e Finaliza apenas os(as) Alunos(as) Aprovados(as)" Checked="true"/>
        </li>
        <li runat="server" title="Clique para Processar a Média Final" class="liBtnAdd">
            <asp:LinkButton ID="btnAddQuestao" runat="server" class="btnLabel" OnClick="imgAdd_Click" ClientIDMode="Static" OnClientClick="geraPadrao(this);">Listar Alunos</asp:LinkButton>
        </li>
        <li id="barraTitulo" runat="server" class="liBarraTitulo" style="margin-top: 20px;"
            visible="false"><span>Alunos</span> </li>
        <li class="liClear" style="margin-left:80px;">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdAlunos" CssClass="grdBusca" Width="580px" runat="server" 
                    AutoGenerateColumns="False" onrowdatabound="grdAlunos_RowDataBound">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle HorizontalAlign="Center"   CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="NU_NIRE" HeaderText="NIRE">
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                            <ItemStyle Width="240px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OBS_MAT" HeaderText="Obs">
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="status" runat="server" Text='<%# bind("STATUS") %>'></asp:Label>                                
                                <asp:HiddenField ID="hdSitMat" runat="server" Value='<%# bind("CO_STA_APROV") %>' />
                                <asp:HiddenField ID="hdOB" runat="server" Value='<%# bind("OBS_MAT") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
<%--    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
    <div class="divCarregando">
        <asp:Image ID="Image1" runat="server" 
            ImageUrl="~/Navegacao/Icones/carregando.gif" />
    </div>
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
    </script>
</asp:Content>
