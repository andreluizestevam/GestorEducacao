<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3500_CtrlDesempenhoEscolar.F3506_LancNotaAtivPorMateria.Cadastro" %>

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
            margin-left: 0px;
        }
        
        /*-- CSS DADOS */
        .divGrid
        {
            height: 360px;
            width: 500px;
            overflow-y: auto;
            margin-left: 130px;
        }
        .mascaradecimal
        {
            width: 40px;
            text-align: right;
        }
        .ddlBimestre
        {
            width: 75px;
        }
        .ddlClassi
        {
            width: 70px;
        }
        .liPesqAtiv
        {
            margin-top: 10px;
        }
        .liPesqAtiv img
        {
            width: 14px;
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
    </style>
    <!--[if IE]>
<style type="text/css">
       #divBarraLanctoExameFinal { width: 238px; margin-left: 760px; }
</style>
<![endif]-->
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
                    <img title="Cancela TODAS as altera��es feitas no registro." alt="Icone de Cancelar Operacao Atual."
                        src="/BarrasFerramentas/Icones/Cancelar.png" />
                </a></li>
            </ul>
            <ul id="ulAcoes" style="width: 43px;">
                <li id="btnPesquisar">
                    <img title="Volta ao formul�rio de pesquisa." alt="Icone de Pesquisa." src="/BarrasFerramentas/Icones/Pesquisar_Desabilitado.png" />
                </li>
                <li id="liImprimir">
                    <img title="Formula um relat�rio a partir dos par�metros especificados e o exibe em Modo de Impress�o."
                        alt="Icone de Impressao." src="/BarrasFerramentas/Icones/Imprimir_Desabilitado.png" />
                </li>
            </ul>
        </div>
    </div>
    <ul id="ulDados" class="ulDados">
        <li style="margin-left: 70px">
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
            <label for="ddlSerieCurso" class="lblObrigatorio" title="S�rie/Curso">
                S�rie/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a S�rie/Curso" CssClass="campoSerieCurso"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="S�rie/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlTurma" class="lblObrigatorio" title="Turma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione a Turma" CssClass="campoTurma"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlDisciplina" class="lblObrigatorio" title="Disciplina">
                Disciplina</label>
            <asp:DropDownList ID="ddlDisciplina" ToolTip="Selecione a Disciplina" CssClass="campoMateria"
                OnSelectedIndexChanged="ddlDisciplina_OnSelectedIndexChanged" AutoPostBack="true"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="ddlDisciplina"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 220px">
            <label for="ddlReferencia" title="Selecione a Refer�ncia em que a frequ�ncia ser� lan�ada" class="lblObrigatorio">
                Refer�ncia</label>
            <asp:DropDownList ID="ddlReferencia" ToolTip="Selecione a Refer�ncia em que a frequ�ncia ser� lan�ada"
               OnSelectedIndexChanged="ddlReferencia_OnSelectedIndexChanged" AutoPostBack="true" runat="server">   
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlReferencia" runat="server" ControlToValidate="ddlReferencia" CssClass="validatorField"
             ErrorMessage="A Refer�ncia em que a frequ�ncia ser� lan�ada deve ser informado." Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTipoAtiv" class="lblObrigatorio" title="Tipo de Atividade">
                Tipo</label>
            <asp:DropDownList ID="ddlTipoAtiv" ToolTip="Selecione o Tipo de Atividade" CssClass="ddlTipoAtiv"
                runat="server" OnSelectedIndexChanged="ddlTipoAtiv_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlTipoAtiv"
                CssClass="validatorField" ErrorMessage="Tipo de Atividade deve ser informado"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlBimestre" class="lblObrigatorio" title="Classifica��o">
                Classifica��o</label>
            <asp:DropDownList ID="ddlClassi" CssClass="ddlClassi" runat="server" ToolTip="Selecione a Classifica��o">
                <asp:ListItem Value="N1">Nota 1</asp:ListItem>
                <asp:ListItem Value="N2">Nota 2</asp:ListItem>
                <asp:ListItem Value="N3">Simulado</asp:ListItem>
                <asp:ListItem Value="S1">Extra</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlClassi"
                CssClass="validatorField" ErrorMessage="Classifica��o deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtDataAtiv" title="Data da Atividade" class="lblObrigatorio">
                Data da Atividade</label>
            <asp:TextBox ID="txtDataAtiv" CssClass="campoData" runat="server" ToolTip="Selecione a Data da Atividade"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataAtiv" runat="server" CssClass="validatorField"
                ControlToValidate="txtDataAtiv" Text="*" ErrorMessage="Campo Data � requerido"
                SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liPesqAtiv">
            <asp:LinkButton ID="btnPesqGride" runat="server" OnClick="btnPesqGride_Click" ValidationGroup="vgBtnPesqGride">
                <img title="Clique para gerar Gride de Notas dos Alunos."
                        alt="Icone de Pesquisa das Grides." 
                        src="/Library/IMG/Gestor_BtnPesquisa.png" />
            </asp:LinkButton>
        </li>
        <li class="liClear" style="margin-left: 20px">
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
                        <asp:BoundField DataField="DescNU_NIRE" HeaderText="NIRE">
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NO_ALU" HeaderText="Aluno">
                            <ItemStyle Width="340px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="NOTA">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:TextBox ID="txtNota" CssClass="mascaradecimal" runat="server" Text='<%# bind("NOTA") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".mascaradecimal").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });   
    </script>
</asp:Content>
