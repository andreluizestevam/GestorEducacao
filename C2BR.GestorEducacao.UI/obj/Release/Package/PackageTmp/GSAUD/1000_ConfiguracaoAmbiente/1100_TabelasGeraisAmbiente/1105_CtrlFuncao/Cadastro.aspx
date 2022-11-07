<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1104_CtrlFuncao.Cadastro" %>

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
        .chk label
        {
            display: inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidSituacao" />
    <ul class="ulDados">
        <li>
            <label title="Nome da Operadora" class="lblObrigatorio">
                Nome</label>
            <asp:TextBox runat="server" ID="txtNomeOper" Width="200px" ToolTip="Nome da Operadora"
                MaxLength="60"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfv1" ControlToValidate="txtNomeOper"
                Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Status">
                Situação
            </label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="70px" ToolTip="Situação da Operadora">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
