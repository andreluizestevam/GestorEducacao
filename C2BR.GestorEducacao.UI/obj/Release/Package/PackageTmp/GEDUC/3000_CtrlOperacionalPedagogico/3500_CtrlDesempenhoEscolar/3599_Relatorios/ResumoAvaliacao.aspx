<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ResumoAvaliacao.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3500_CtrlDesempenhoEscolar._3599_Relatorios.ResumoAvaliacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
        }
        .liUnidade, .liAluno
        {
            clear: both;
            margin-top: 5px;
            width: 300px;
        }
        .liObservacao
        {
            clear: both;
            margin-top: 5px;
            width: 300px;
        }
        .txtObservacao
        {
            width: 300px;
            height: 100px;
        }
        .liAnoRefer
        {
            clear: both;
            margin-top: 5px;
        }
        .liModalidade
        {
            clear: both;
            width: 140px;
            margin-top: 5px;
        }
        .liSerie
        {
            clear: both;
            margin-top: 5px;
        }
        .liTurma
        {
            clear: both;
            margin-left: 5px;
            margin-top: 5px;
        }
        .ddlCategoria
        {
            width: 110px;
        }
        .ddlDisciplina
        {
            width: 190px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liUnidade">
                    <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                        Unidade/Escola</label>
                    <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged1" ToolTip="Unidade/Escola">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                    &nbsp;</li>
                <li class="liAnoRefer">
                    <label class="lblObrigatorio" title="Ano de Referência">
                        Ano Referência</label>
                    <asp:DropDownList ID="ddlAnoRefer" CssClass="ddlAno" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlAnoRefer_SelectedIndexChanged" ToolTip="Selecione o Ano de Referência">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlAnoRefer" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlAnoRefer" Text="*" ErrorMessage="Campo Ano Referência é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <%--<li class="liAnoRefer">
                    <label for="ddlReferencia" class="lblObrigatorio" title="Referência">
                        Referência</label>
                    <asp:DropDownList ID="ddlReferencia" CssClass="ddlReferencia" runat="server" ToolTip="Selecione a Referência"
                        OnSelectedIndexChanged="ddlReferencia_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlReferencia" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlReferencia" Text="*" ErrorMessage="Campo Referência é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>--%>
                <li class="liAnoRefer">
                    <label for="ddlReferencia" title="Selecione a Referência em que a frequência será lançada"
                        class="lblObrigatorio">
                        Referência</label>
                    <asp:DropDownList ID="ddlReferencia" CssClass="ddlReferencia" ToolTip="Selecione a Referência em que a frequência será lançada"
                        runat="server" OnSelectedIndexChanged="ddlReferencia_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlReferencia" runat="server" ControlToValidate="ddlReferencia"
                        CssClass="validatorField" ErrorMessage="A Referência em que a frequência será lançada deve ser informado."
                        Text="*" Display="Static"></asp:RequiredFieldValidator>
                </li>
                <li class="liModalidade">
                    <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                        ToolTip="Selecione a Modalidade" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlModalidade" Text="*" ErrorMessage="Campo Modalidade é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liSerie">
                    <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                        Série/Curso</label>
                    <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged" ToolTip="Selecione a Série/Curso">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlSerieCurso" Text="*" ErrorMessage="Campo Série/Curso é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liSerie">
                    <label id="lblTurma" class="lblObrigatorio" for="ddlTurma" title="Turma">
                        Turma</label>
                    <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged" ToolTip="Selecione a Turma">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlTurma" Text="*" ErrorMessage="Campo Turma é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liAluno">
                    <label id="Label2" class="lblObrigatorio" for="ddlTurma" title="Disciplina">
                        Disciplina</label>
                    <asp:DropDownList ID="ddlDisciplina" CssClass="ddlDisciplina" runat="server" ToolTip="Selecione a Disciplina"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlDisciplina_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlDisciplina" Text="*" ErrorMessage="Campo Disciplina é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liAluno">
                    <label class="lblObrigatorio" title="Aluno">
                        Aluno</label>
                    <asp:DropDownList ID="ddlAlunos" CssClass="ddlNomePessoa" runat="server" ToolTip="Selecione o Aluno"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlAlunos" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlAlunos" Text="*" ErrorMessage="Campo Aluno é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
