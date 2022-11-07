<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3504ManuHomolMedias.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 818px;
        }
        .ulDados input
        {
            margin-bottom: 0;
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
        .liEspaco
        {
            margin-left: 5px;
        }
        .chk
        {
            display: inline;
            margin-left: 200px;
            margin-top: -3px;
        }
        
        /*-- CSS DADOS */
        .divGrid
        {
            height: 335px;
            width: 815px;
            border: 1px solid #CCCCCC;
            overflow-y: scroll;
        }
        .mascaradecimal
        {
            width: 40px;
            text-align: right;
        }
        
        .lilnkBolCarne
        {
            background-color: #F0FFFF;
            border: 1px solid #D2DFD1;
            clear: none;
            margin-bottom: 4px;
            padding: 2px 3px 1px;
            margin-left: 2px;
            margin-right: 0px;
            width: 84px;
            height: 14px;
        }
        
        #divBarraPadraoContent
        {
            display: none;
        }
        
        #divBarraLanctoExameFinal
        {
            position: absolute;
            margin-left: 749px !important;
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
        .lblHomol
        {
            color: Red;
        }
        .chk label
        {
            display: inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
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
                    <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click">
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
        <li style="margin-left: -10px;">
            <label for="ddlAno" title="Ano">
                Ano</label>
            <asp:DropDownList ID="ddlAno" ToolTip="Selecione o Ano" CssClass="ddlAno" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAno"
                ErrorMessage="Ano deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione a Modalidade" CssClass="campoModalidade"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlSerieCurso" title="Série/Curso">
                Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Série/Curso" CssClass="campoSerieCurso"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlTurma" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlDisciplina" title="Disciplina">
                Disciplina</label>
            <asp:DropDownList ID="ddlDisciplina" ToolTip="Selecione a Disciplina" CssClass="campoMateria"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDisciplina"
                ErrorMessage="Disciplina deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <%--<li>
            <label for="ddlBimestre" class="lblObrigatorio" title="">
            Referência
            </label>
            <asp:DropDownList ID="ddlBimestre" CssClass="ddlBimestre" runat="server" ToolTip="Selecione a Referência em que a frequência será lançada">
                <asp:ListItem Value="" Text="Selecione" Selected="True"></asp:ListItem>
                <asp:ListItem Value="B1">1º Bimestre</asp:ListItem>
                <asp:ListItem Value="B2">2º Bimestre</asp:ListItem>
                <asp:ListItem Value="B3">3º Bimestre</asp:ListItem>
                <asp:ListItem Value="B4">4º Bimestre</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlBimestre"
                CssClass="validatorField" ErrorMessage="A referência em que a frequência será lançada deve ser informada." Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>--%>
        <li>
            <label for="ddlReferencia" title="Selecione a Referência em que a frequência será lançada" class="lblObrigatorio">
                Referência</label>
            <asp:DropDownList ID="ddlReferencia" ToolTip="Selecione a Referência em que a frequência será lançada"
                runat="server">   
            </asp:DropDownList> 
            <asp:RequiredFieldValidator ID="rfvddlReferencia" runat="server" ControlToValidate="ddlReferencia" CssClass="validatorField"
             ErrorMessage="A Referência em que a frequência será lançada deve ser informado." Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top: 13px !important; margin-left: -5px;">
            <asp:ImageButton ID="lbkPesq" Width="12px" runat="server" ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png"
                CssClass="btnPesqDescMed" ToolTip="Pesquisa de acordo com as informações preenchidas nos parâmetros"
                OnClick="lbkPesq_OnClick" />
        </li>
        <li runat="server" 
            style=" margin-left:700px; margin-top:-5; width: 216px; height: 37px;" id="liChk"><%--margin: -5px 0 0px 600px; --%>
            <asp:RadioButtonList ID="Chk" AutoPostBack="true" CssClass="chk" OnSelectedIndexChanged="ChkHomologaTodos_SelectedIndexChanged"
                runat="server" Width="111px" Height="37px">
                <asp:ListItem Value="D" Selected="True"> Desmarcar todos</asp:ListItem>
                <asp:ListItem Value="M"> Marcar todos</asp:ListItem>
            </asp:RadioButtonList> 
        </li>
        <li class="liClear" runat="server" visible="false" id="ligrid">
            <div id="divGrid" runat="server" class="divGrid">
                <asp:GridView ID="grdBusca" CssClass="grdBusca" runat="server" AutoGenerateColumns="False">
                    <RowStyle CssClass="rowStyle" />
                    <AlternatingRowStyle CssClass="alternatingRowStyle" />
                    <EmptyDataRowStyle CssClass="emptyDataRowStyle" />
                    <EmptyDataTemplate>
                        Nenhum registro encontrado.<br />
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="CO_ALU_CAD" HeaderText="NIRE / STATUS">
                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                            <ItemStyle Width="340px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AV1" HeaderText="AV1">
                            <ItemStyle Width="55px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AV2" HeaderText="AV2">
                            <ItemStyle Width="55px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AV3" HeaderText="AV3">
                            <ItemStyle Width="55px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AV4" HeaderText="AV4">
                            <ItemStyle Width="55px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AV5" HeaderText="AV5">
                            <ItemStyle Width="55px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MB" HeaderText="MB">
                            <ItemStyle Width="55px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="HOMOL">
                            <ItemStyle HorizontalAlign="Center" Width="55px" />
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hidCoAtividade" Value='<%# bind("CO_ALU") %>' />
                                <asp:CheckBox ID="ckSelect" runat="server" Checked='<%# bind("FL_HOMOL_ATIV") %>' />
                                <asp:HiddenField runat="server" ID="HiddenMB" Value='<%# bind("MB") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
         
            </div>
            <div style="margin-top: 2px; float: left;">
                <label>
                    Legenda: MB(Média do Bimestre) - AV1(Avaliação 1) - AV2(Avaliação 2) - AV3(Avaliação
                    3) - AV4(Avaliação 4) - AV5(Avaliação 5)</label>
            </div>
        </li>
    </ul>
    <script type="text/javascript">   
    </script>

</asp:Content>
