<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1111_UsuarioApp.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 400px;
            margin-left:350px !important;
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
        .liBtnAddA
        {
            background-color: #F1FFEF;
            border: 1px solid #D2DFD1;
            padding: 2px 3px 1px 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidSituacao" />
    <ul class="ulDados">
        <li style="clear: both">
            <label title="Tipo do usuário">
                Tipo</label>
            <asp:DropDownList runat="server" ID="ddlTipo" ToolTip="Tipo do usuário" OnSelectedIndexChanged="ddlTipo_OnSelectedIndexChanged"
                AutoPostBack="true" Width="90px">
                <asp:ListItem Value="" Text="Selecione" Selected="True"></asp:ListItem>
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
        <li style="clear:both">
            <label title="Login de acesso do usário ao aplicativo">
                Login</label>
            <asp:TextBox runat="server" ID="txtLogin" ToolTip="Login de acesso do usuário ao aplicativo" MaxLength="25"></asp:TextBox>
        </li>
        <li>
            <label title="Senha de acesso do usuário ao aplicativo">
                Senha</label>
            <asp:TextBox runat="server" ID="txtSenha" ToolTip="Login de Senha do usuário ao aplicativo"
                TextMode="Password"></asp:TextBox>
        </li>
        <li id="liReset" runat="server" class="liBtnAddA" style="margin: 15px 0 0 5px !important; height: 15px;" title="Reseta a senha para o CPF do usuário em questão"
            visible="false">
            <asp:LinkButton ID="lnkResetarSenha" runat="server" ValidationGroup="atuEndAlu" OnClick="lnkResetarSenha_OnClick">
                <asp:Label runat="server" ID="Label3" Text="RESETAR" Style="margin-left: 4px;"></asp:Label>
            </asp:LinkButton>
        </li>
        <li style="clear: both">
            <label title="Situação do usuário de acesso ao Aplicativo">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSitu" ToolTip="Situação do usuário de acesso ao Aplicativo">
                <asp:ListItem Value="" Text="Selecione" Selected="True"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
                <asp:ListItem Value="S" Text="Suspenso"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
