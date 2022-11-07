<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0304_ResetarSenhaUsuario.Busca"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidadeMUS" runat="server" CssClass="campoUnidadeEscolar" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Classificação</label>
            <asp:DropDownList ID="ddlClassUsuarioMUS" runat="server" ToolTip="Selecione a Classificação do Usuário">
                <asp:ListItem Text="Todos" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Comum" Value="C"></asp:ListItem>
                <asp:ListItem Text="Master" Value="M"></asp:ListItem>
                <asp:ListItem Text="Suporte" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Tipo</label>
            <asp:DropDownList ID="ddlTipoUsuarioMUS" runat="server" ToolTip="Selecione o Tipo de Usuário">
                <asp:ListItem Text="Todos" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Funcionáro" Value="F"></asp:ListItem>
                <asp:ListItem Text="Responsável/Aluno" Value="R"></asp:ListItem>
                <asp:ListItem Text="Professor" Value="P"></asp:ListItem>
                <asp:ListItem Text="Outros" Value="O"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Status</label>
            <asp:DropDownList ID="ddlStatusUsuarioMUS" runat="server" ToolTip="Selecione o Status">
                <asp:ListItem Text="Todos" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Ativo" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Usuário</label>
            <asp:TextBox ID="txtNomeUsuarioMUS" CssClass="campoNomePessoa" runat="server" ToolTip="Informe o Nome do Usuário"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
