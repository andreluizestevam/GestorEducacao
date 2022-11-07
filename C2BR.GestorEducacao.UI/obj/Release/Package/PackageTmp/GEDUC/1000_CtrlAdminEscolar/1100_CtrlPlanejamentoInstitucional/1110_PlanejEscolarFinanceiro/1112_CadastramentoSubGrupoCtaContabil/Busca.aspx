<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1112_CadastramentoSubGrupoCtaContabil.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
    <label for="ddlTipo" title="Grupo" >Tipo de Conta</label>
    <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlTipo_SelectedIndexChanged">
    </asp:DropDownList>
    </li>
    <li>
    <label for="ddlGrupo" title="Grupo" >Grupo</label>
    <asp:DropDownList ID="ddlGrupo" runat="server" AutoPostBack="True">
    </asp:DropDownList>
    </li>
    <li>
        <label for="txtSubGrupo" title="Subgrupo">Subgrupo</label>
        <asp:TextBox ID="txtSubGrupo" ToolTip="Pesquise pelo Subgrupo" runat="server" MaxLength="40" CssClass="campoDescricao"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
