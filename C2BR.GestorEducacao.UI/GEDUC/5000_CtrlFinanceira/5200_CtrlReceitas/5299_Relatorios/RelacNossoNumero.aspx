<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="RelacNossoNumero.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._5000_CtrlFinanceira._5200_CtrlReceitas._5299_Relatorios.RelacNossoNumero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 250px;
        }
        .ulDados li
        {
            margin-left: 5px;
            margin-top: 5px;
        }
        .chkLabel label
        {
            display: inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label>
                Ano Matr</label>
            <asp:DropDownList runat="server" ID="ddlAno" Width="55px">
            </asp:DropDownList>
        </li>
        <li>
            <label id="Label1" class="lblObrigatorio" title="Unidade/Escola" runat="server">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar"
                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged1">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio" for="ddlModalidade" title="Modalidade">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma Modalidade" CssClass="ddlModalidade"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_OnSelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlModalidade" Text="*" ErrorMessage="Campo Modalidade é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label class="lblObrigatorio" for="ddlSerieCurso" title="Série/Curso">
                Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione uma Série/Curso" CssClass="ddlSerieCurso"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_OnSelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                ControlToValidate="ddlSerieCurso" Text="*" ErrorMessage="Campo Série/Curso é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label id="lblTurma" class="lblObrigatorio" title="Turma" for="ddlTurma">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma Turma" CssClass="ddlTurma" OnSelectedIndexChanged="ddlTurma_OnSelectedIndexChanged" AutoPostBack="true"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                ControlToValidate="ddlTurma" Text="*" ErrorMessage="Campo Turma é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label>
                Aluno</label>
            <asp:DropDownList runat="server" ID="ddlAluno" Width="230px">
            </asp:DropDownList>
        </li>
        <li>
            <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio">Período</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="txtIniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="txtIniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label2"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="TxtFimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="TxtFimPeri"></asp:RequiredFieldValidator><br />
        </li>
        <li style="clear:both; margin-left:0px; margin-top:-3px">
            <asp:CheckBox runat="server" ID="chkPesqNossNum" Text="Pesquisa por nosso Número" cssClass="chkLabel"
             OnCheckedChanged="chkPesqNossNum_OnCheckedChanged" AutoPostBack="true"/>
        </li>
        <li style="clear:both">
            <asp:TextBox runat="server" ID="txtNuNossNu" Width="140px" Enabled="false"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
