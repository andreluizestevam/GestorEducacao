<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="CreditosAlunos.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2114_ControleCredito.TransfCredito.CreditosAlunos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ulDados
        {
            width: 275px;
            margin-top: 30px;
        }
        
        .ulDados li
        {
            margin-top: 6px;
            margin-left: 40px;
            margin-right: 0px !important;
        }
        
        .liboth
        {
            clear: both;
        }
        
        .ddlMat
        {
            width: 140px;
            clear: both;
        }
        .liUnidade, .liTipoRelatorio
        {
            margin-top: 5px;
            width: 240px;
        }
        
        .ddlModalidade
        {
            width: 120px;
            clear: both;
        }
        
        .ddlSerie
        {
            width: 60px;
            clear: both;
        }
        
        .ddlTurma
        {
            width: 100px;
            clear: both;
        }
        
        .ddlAluno
        {
            width: 205px;
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <%--      <li class="liboth">
        <asp:Label runat="server" ID="lblAno" class="lblObrigatorio">Ano Referência</asp:Label>
        <asp:DropDownList ID="ddlAno" runat="server" CssClass="campoAno" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAno_SelectedIndexChanged" ToolTip="Selecione o Ano">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="ddlAno" ErrorMessage="Ano deve ser informado"
                Display="None"></asp:RequiredFieldValidator>

      </li>--%>
        <li class="liboth">
            <asp:Label runat="server" ID="lblModalidade" class="lblObrigatorio">Modalidade</asp:Label><br />
            <asp:DropDownList ID="ddlModalidade" class="ddlModalidade" runat="server" ToolTip="Selecione a Modalidade"
                OnSelectedIndexChanged="ddlModalidade_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblSerieCurso" class="lblObrigatorio">Série/Curso</asp:Label><br />
            <asp:DropDownList ID="ddlSerieCurso" class="ddlSerie" runat="server" ToolTip="Selecione a Série/Curso"
                OnSelectedIndexChanged="ddlSerieCurso_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblTurma" class="lblObrigatorio">Turma</asp:Label><br />
            <asp:DropDownList ID="ddlTurma" class="ddlTurma" runat="server" ToolTip="Selecione a Turma"
                OnSelectedIndexChanged="ddlTurma_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liboth">
            <asp:Label class="lblObrigatorio" runat="server" ID="lblMateria">Matéria</asp:Label><br />
            <asp:DropDownList ID="ddlMateria" runat="server" class="ddlMat" ToolTip="Selecione a Matéria">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlMateria"
                ErrorMessage="A Matéria deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblAlunoTransCred" class="lblObrigatorio">Aluno que Transfere o Crédito</asp:Label>
            <asp:DropDownList ID="ddlAlunoTransCred" class="ddlAluno" runat="server" ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAlunoTransCred"
                ErrorMessage="O Aluno que transfere deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblAlunoReceb" class="lblObrigatorio">Aluno que Recebe o Crédito</asp:Label>
            <asp:DropDownList ID="ddlAlunoReceb" class="ddlAluno" runat="server" ToolTip="Selecione o Aluno que Recebeu o Crédito">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAlunoTransCred"
                ErrorMessage="O Aluno que Recebe o crédito deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio">Período</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp&nbsp&nbsp à &nbsp&nbsp&nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
