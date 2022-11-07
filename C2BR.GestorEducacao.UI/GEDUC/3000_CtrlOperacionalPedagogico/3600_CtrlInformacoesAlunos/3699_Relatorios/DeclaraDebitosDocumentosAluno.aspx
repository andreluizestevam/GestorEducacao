<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="DeclaraDebitosDocumentosAluno.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3699_Relatorios.DeclaraDebitosDocumentosAluno" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 260px;
        }
        .liUnidade, .liAnoRefer, .liAluno
        {
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
            margin-left: 5px;
            margin-top: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" title="Unidade/Escola" runat="server">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged1">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liAnoRefer">
            <label class="lblObrigatorio" title="Ano de Referência">
                Ano Referência</label>
            <asp:DropDownList ID="ddlAnoRefer" ToolTip="Selecione um Ano de Referência" CssClass="ddlAno"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAnoRefer_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li class="liModalidade">
            <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" ErrorMessage="Campo Modalidade é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liSerie">
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" ErrorMessage="Campo Série/Curso é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liTurma">
            <label id="lblTurma" class="lblObrigatorio" title="Turma" for="ddlTurma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlTurma"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" ErrorMessage="Campo Turma é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liAluno">
            <label class="lblObrigatorio" title="Aluno">
                Aluno</label>
            <asp:DropDownList ID="ddlAlunos" ToolTip="Selecione um Aluno" CssClass="ddlNomePessoa"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlAlunos" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAlunos" Text="*" ErrorMessage="Campo Aluno é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
