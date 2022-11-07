<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1117_CadastramentoSubGrupo2CtaContabil.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlTipoConta" title="Tipo de conta">Tipo de Conta</label>
        <asp:DropDownList ID="ddlTipoConta" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlTipoConta_SelectedIndexChanged">
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlGrupo" title="Grupo">Grupo</label>
        <asp:DropDownList ID="ddlGrupo" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlGrupo_SelectedIndexChanged">
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlSubGrupo" title="Subgrupo">Subgrupo</label>
        <asp:DropDownList ID="ddlSubGrupo" runat="server" AutoPostBack="True">
        </asp:DropDownList>
    </li>
    <li>
        <label for="txtSubGrupo2" title="Subgrupo2">Subgrupo2</label>
        <asp:TextBox ID="txtSubGrupo2" ToolTip="Pesquise pelo Subgrupo2" runat="server" MaxLength="40" CssClass="campoDescricao"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
