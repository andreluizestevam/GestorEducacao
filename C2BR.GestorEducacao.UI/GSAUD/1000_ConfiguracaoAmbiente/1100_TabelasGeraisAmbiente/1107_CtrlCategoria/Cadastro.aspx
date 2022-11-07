<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1107_CtrlCategoria.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 270px;
        }
        .ulDados li
        {
            margin: 5px 0 0 5px;
        }
        input
        {
            height: 13px;
        }
        .ddlReg
        {
            width: 150px;
            clear: both;
        }
        .sigla 
        {
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidSituacao" />
    <ul class="ulDados">
        <li>
            <label title="Operadora">
                Operadora
            </label>
            <asp:DropDownList ID="ddlOperadora" CssClass="ddlReg" OnSelectedIndexChanged="ddOperadora_OnSelectedIndexChanged"
                AutoPostBack="true" runat="server">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Operadora">
                Plano
            </label>
            <asp:DropDownList ID="ddlPlano" CssClass="ddlReg" runat="server">
                <asp:ListItem Value="0">Selecione</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label title="Sigla" class="lblObrigatorio">
                Sigla</label>
            <asp:TextBox runat="server" ID="txtSigla" Width="40px" CssClass="sigla" ToolTip="Sigla" MaxLength="6"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtSigla"
                Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Nome da Categoria" class="lblObrigatorio">
                Nome</label>
            <asp:TextBox runat="server" ID="txtNomeCategoria" Width="200px" ToolTip="Nome da Categoria"
                MaxLength="60"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfv1" ControlToValidate="txtNomeCategoria"
                Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="border-top: 30px">
            <label title="Situação da categoria">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSitu" Width="70px" ToolTip="Situação da Operadora">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
