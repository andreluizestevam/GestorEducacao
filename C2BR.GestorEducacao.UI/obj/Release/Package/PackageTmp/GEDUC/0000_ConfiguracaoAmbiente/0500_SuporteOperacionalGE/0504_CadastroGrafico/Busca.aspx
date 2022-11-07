<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" 
    Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0504_CadastroGrafico.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS Dados */
        .txtTitulGrafi { width: 275px; }
        .ddlStatus, .ddlTipoGrafi { width: 60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">        
    <li>
        <label for="txtTitulGrafi" title="Título do Gráfico">Título do Gráfico</label>
        <asp:TextBox ID="txtTitulGrafi" ToolTip="Informe o Título do Gráfico" runat="server" MaxLength="80" CssClass="txtTitulGrafi"></asp:TextBox>
    </li>
    <li>
        <label for="ddlTipoGrafi" title="Tipo de Gráfico">Tipo</label>
        <asp:DropDownList ID="ddlTipoGrafi" ToolTip="Selecione o Tipo de Gráfico" runat="server" CssClass="ddlTipoGrafi">
            <asp:ListItem Text="Todos" Value="T"></asp:ListItem>
            <asp:ListItem Text="Coluna" Value="C"></asp:ListItem>
            <asp:ListItem Text="Pirâmide" Value="P"></asp:ListItem>
            <asp:ListItem Text="Pizza" Value="I"></asp:ListItem>
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlStatus" title="Status">Status</label>
        <asp:DropDownList ID="ddlStatus" ToolTip="Selecione o status" runat="server" CssClass="ddlStatus">
            <asp:ListItem Text="Todos" Value="T"></asp:ListItem>
            <asp:ListItem Text="Ativa" Value="A"></asp:ListItem>
            <asp:ListItem Text="Inativa" Value="I"></asp:ListItem>
        </asp:DropDownList>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>