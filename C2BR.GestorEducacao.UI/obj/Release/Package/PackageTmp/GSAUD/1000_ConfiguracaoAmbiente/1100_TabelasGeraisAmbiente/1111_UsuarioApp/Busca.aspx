<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1111_UsuarioApp.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
         .ulDados
        {
            width: 300px;
            margin-top:20px;
        }
        input
        {
            height: 13px;
        }
        .ulDados li
        {
            margin: 5px 0 0 5px;
        }
        label
        {
            margin-bottom: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label title="Login de acesso do usuário ao aplicativo">
                Login</label>
            <asp:TextBox runat="server" ID="txtLogin" ToolTip="Login de acesso do usuário ao aplicativo"></asp:TextBox>
        </li>
        <li style="clear: both">
            <label title="Tipo do usuário">
                Tipo</label>
            <asp:DropDownList runat="server" ID="ddlTipo" ToolTip="Tipo do usuário" OnSelectedIndexChanged="ddlTipo_OnSelectedIndexChanged" AutoPostBack="true" Width="90px">
                <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                <asp:ListItem Value="C" Text="Colaborador/Profissional"></asp:ListItem>
                <%--<asp:ListItem Value="G" Text="Gestor"></asp:ListItem>
                <asp:ListItem Value="P" Text="Paciente"></asp:ListItem>--%>
                <asp:ListItem Value="R" Text="Responsável/Paciente"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Usuário do login">
                Usuário
            </label>
            <asp:DropDownList runat="server" ID="ddlUsuario" ToolTip="Usuário do login" Width="280px">
            </asp:DropDownList>
        </li>
         <li style="clear: both">
            <label title="Situação do usuário de acesso ao Aplicativo">Situação</label>
            <asp:DropDownList runat="server" ID="ddlSitu" ToolTip="Situação do usuário de acesso ao Aplicativo">
                <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
                <asp:ListItem Value="S" Text="Suspenso"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
