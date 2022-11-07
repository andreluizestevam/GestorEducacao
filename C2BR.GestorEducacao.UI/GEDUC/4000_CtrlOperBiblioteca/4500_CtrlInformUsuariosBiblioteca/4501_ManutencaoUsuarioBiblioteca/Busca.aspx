<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" 
    Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4500_CtrlInformUsuariosBiblioteca.F4501_ManutencaoUsuarioBiblioteca.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .txtCpf { width: 82px; }
        .txtNome { width: 210px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlTipo" title="Tipo de Usuário">Tipo de Usuário</label>
        <asp:DropDownList ID="ddlTipo" CssClass="ddlTipo" runat="server" ToolTip="Selecione o Tipo de Usuário">
            <asp:ListItem Value="">Todos</asp:ListItem>
            <asp:ListItem Value="A">Aluno</asp:ListItem>
            <asp:ListItem Value="P">Professor</asp:ListItem>
            <asp:ListItem Value="F">Funcionário</asp:ListItem>
            <asp:ListItem Value="O">Outros</asp:ListItem>
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlUnidade" title="Unidade/Escola">Unidade/Escola</label>
        <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidade" runat="server" ToolTip="Selecione a Unidade/Escola"></asp:DropDownList>
    </li>
    <li>
        <label for="txtNome">Nome</label>
        <asp:TextBox ID="txtNome" CssClass="txtNome" runat="server" MaxLength="40" ToolTip="Informe o Nome do Usuário"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>