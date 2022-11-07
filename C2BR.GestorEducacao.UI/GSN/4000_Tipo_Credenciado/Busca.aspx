<%@ Page Language="C#"  MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSN._4000_Tipo_Credenciado.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtTIPO_CREDENCIADO" class="labelPixel" title="Tipo do Credenciado">Tipo / Credenciado</label>
            <asp:TextBox ID="txtTIPO_CREDENCIADO" ToolTip="Pesquise pelo Tipo do Credenciado" CssClass="txtTIPO_CREDENCIADO" runat="server" MaxLength="60"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
