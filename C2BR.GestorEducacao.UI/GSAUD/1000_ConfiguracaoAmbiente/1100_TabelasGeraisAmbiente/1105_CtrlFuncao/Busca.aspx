<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1104_CtrlFuncao.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 250px;
            margin: 40px 0 0 40px !important;
        }
        .ulDados li
        {
            margin-top: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label title="Nome da Operadora">
            <label title="Nome da Operadora" class="">
                Nome</label>
            <asp:TextBox runat="server" ID="txtNomeOper" Width="200px" ToolTip="Nome da Operadora"
                MaxLength="60"></asp:TextBox>
        </li>
        <li>
            <label title="Status">
                Situação
            </label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="70px" ToolTip="Situação da Operadora">
                <asp:ListItem Text="Todos" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Ativo" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
