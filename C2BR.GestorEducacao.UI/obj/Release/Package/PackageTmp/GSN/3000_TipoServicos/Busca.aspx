<%@ Page Language="C#"  MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSN._3000_TipoServicos.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtDescricao" class="labelPixel" title="Descrição da Unidade ou a Sigla">Descrição / Sigla</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Pesquise pela Descrição ou Sigla" CssClass="txtDescricaoUF" runat="server" MaxLength="100"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>