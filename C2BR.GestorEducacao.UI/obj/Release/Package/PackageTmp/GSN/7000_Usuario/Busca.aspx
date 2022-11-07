<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/App_Masters/PadraoBuscas.Master" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSN._7000_Usuario.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtBusca" class="labelPixel" title="Nome do Usuário">Nome</label>
            <asp:TextBox ID="txtBusca" ToolTip="Pesquise pelo Nome." CssClass="txtDescricaoUF" runat="server" MaxLength="200"></asp:TextBox>

    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>