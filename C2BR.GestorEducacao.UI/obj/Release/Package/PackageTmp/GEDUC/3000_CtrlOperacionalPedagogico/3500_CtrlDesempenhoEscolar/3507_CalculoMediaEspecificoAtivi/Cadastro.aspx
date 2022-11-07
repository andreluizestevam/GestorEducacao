<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3507_CalculoMediaEspecificoAtivi.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 900px;
        }
        .ulDados input
        {
            margin-bottom: 0;
        }
        
        /*--> CSS LIs */
        .ulDados li
        {
            margin-right: 5px;
            margin-bottom: 10px;
        }
        .liClear
        {
            clear: both;
        }
        .liEspaco
        {
            margin-left: 5px;
        }
        
        /*-- CSS DADOS */
        .divGrid
        {
            height: 350px;
            width: 800px;
            overflow-y: auto;
        }
        .txtNota
        {
            width: 40px;
            text-align: center;
        }
        
        .ddlBimestre
        {
            width: 75px;
        }
        .liPesqAtiv
        {
            margin-top: 10px;
            background-color: #F5DEB3;
            border: 1px solid #D2DFD1;
            padding: 2px 3px 1px 3px;
            <%--background-color: #F1FFEF;--%>
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
        .chk label
        {
            display:inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="divMensEfe" style="display: none; position: absolute; left: 50%; right: 50%;
        top: 50%; z-index: 1000; width: 150px" clientidmode="Static">
        <p id="pEfeM" clientidmode="Static" style="color: #6495ED; font-weight: bold; text-align: center;">
        </p>
        <asp:Image runat="server" ID="imgLoad" ImageUrl="~/Navegacao/Icones/carregando.gif"
            Style="z-index: 99"></asp:Image>
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
                <li id="btnNovo">
                    <img title="Abre o formulario para Criar um Novo Registro." alt="Icone de Criar Novo Registro."
                        src="/BarrasFerramentas/Icones/NovoRegistro_Desabilitado.png" />
                </li>
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
        <li style="margin-left: 150px;">
            <label for="ddlAno" class="lblObrigatorio" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="ddlAno" runat="server"
                AutoPostBack="true" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAno"
                ErrorMessage="Ano deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlModalidade" class="lblObrigatorio" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlSerieCurso" class="lblObrigatorio" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlTurma" class="lblObrigatorio" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma"
                Width="160px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlBimestre" class="lblObrigatorio" title="Bimestre">
                Bimestre</label>
            <asp:DropDownList ID="ddlBimestre" CssClass="ddlBimestre" runat="server" ToolTip="Selecione o Bimestre">
                <asp:ListItem Value="B1">1º Bimestre</asp:ListItem>
                <asp:ListItem Value="B2">2º Bimestre</asp:ListItem>
                <asp:ListItem Value="B3">3º Bimestre</asp:ListItem>
                <asp:ListItem Value="B4">4º Bimestre</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlBimestre"
                CssClass="validatorField" ErrorMessage="Bimestre deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both; margin-left: 150px">
            <label for="ddlAluno" title="Aluno">
                Aluno</label>
            <asp:DropDownList ID="ddlAluno" CssClass="campoNomePessoa" runat="server" ToolTip="Aluno">
            </asp:DropDownList>
        </li>
        <li class="liEspaco">
            <label for="ddlDisciplina" class="lblObrigatorio" title="Disciplina">
                Disciplina</label>
            <asp:DropDownList ID="ddlDisciplina" ToolTip="Selecione a Disciplina" CssClass="campoMateria"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDisciplina"
                ErrorMessage="Disciplina deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liPesqAtiv">
            <asp:LinkButton ID="btnPesqGride" runat="server" OnClick="btnPesqGride_Click" ValidationGroup="vgBtnPesqGride"
                ClientIDMode="Static" OnClientClick="geraPadrao(this);__doPostBack('btnPesqGride', '')">
                <asp:Label runat="server" ID="lblBoleto" Text="Calcular Média"></asp:Label>
            </asp:LinkButton>
        </li>
        <li class="liClear" style="margin-left: 50px">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdBusca" CssClass="grdBusca" runat="server" AutoGenerateColumns="False">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="DescNU_NIRE" HeaderText="NIRE">
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                            <ItemStyle Width="260px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="noMateria" HeaderText="DISCIPLINA">
                            <ItemStyle Width="170px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="NOTA 1">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNota1" CssClass="txtNota" runat="server" Text='<%# bind("NOTA1") %>'
                                    Enabled="false" ToolTip="Nota da prova 2 do Aluno(a)"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hidCoMat" Value='<%# bind("CO_MAT") %>' />
                                <asp:HiddenField runat="server" ID="hidCoAlu" Value='<%# bind("CO_ALU") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NOTA 2">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNota2" CssClass="txtNota" runat="server" Text='<%# bind("NOTA2") %>'
                                    Enabled="false" ToolTip="Nota da Prova 2 do Aluno(a)"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SIMUL">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtMDSimu" CssClass="txtNota" runat="server" Text='<%# bind("MDSML_V") %>'
                                    Enabled="false" ToolTip="Média de notas de simulados do Aluno(a)"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ATIV">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtMDAtiv" CssClass="txtNota" runat="server" Text='<%# bind("MDATI_V") %>'
                                    Enabled="false" ToolTip="Média de notas de atividades do Aluno(a)"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="RESULT">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtMDFinal" CssClass="txtNota" runat="server" Text='<%# bind("MDBIM_V") %>'
                                    Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div runat="server" id="divLegenda" visible="false">
                <label>
                    *Legenda: SIMUL (Nota de Simulado) - ATIV (Nota de Atividades) - RESULT (Resultado)</label>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            //$(".txtNota").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });

        function geraPadrao(el) {

            if (el == document.getElementById("btnPesqGride")) {
                document.getElementById("pEfeM").innerHTML = "PREENCHENDO GRID...";
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
