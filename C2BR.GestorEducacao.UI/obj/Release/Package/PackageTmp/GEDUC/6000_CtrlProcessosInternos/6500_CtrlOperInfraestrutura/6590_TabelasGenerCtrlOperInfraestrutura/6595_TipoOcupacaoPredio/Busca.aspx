<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6500_CtrlOperInfraestrutura.F6590_TabelasGenerCtrlOperInfraestrutura.F6595_TipoOcupacaoPredio.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtNomePredio" title="Tipo de Ocupação">Tipo de Ocupação</label>
        <asp:TextBox ID="txtNomePredio" ToolTip="Informe o Tipo de Ocupação" runat="server" MaxLength="40" CssClass="campoDescricao"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
