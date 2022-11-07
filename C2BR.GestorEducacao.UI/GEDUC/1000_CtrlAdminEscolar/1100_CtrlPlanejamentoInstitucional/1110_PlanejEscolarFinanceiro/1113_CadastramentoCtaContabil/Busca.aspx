<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1113_CadastramentoCtaContabil.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
    <ContentTemplate>
    <li>
        <label for="ddlTipoConta" title="Tipo de conta" class="labelPixel">Tipo de Conta</label>
        <asp:DropDownList ID="ddlTipoConta" ToolTip="Selecione um tipo de conta" 
            Width="220px" runat="server" 
             AutoPostBack="true" 
            onselectedindexchanged="ddlTipoConta_SelectedIndexChanged">
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlGrupo" title="Grupo" class="labelPixel">Grupo</label>
        <asp:DropDownList ID="ddlGrupo" ToolTip="Selecione um Grupo" Width="220px" runat="server" 
            onselectedindexchanged="ddlGrupo_SelectedIndexChanged" AutoPostBack="true">
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlSubgrupo" class="labelPixel" title="Subgrupo">Subgrupo</label>
        <asp:DropDownList ID="ddlSubgrupo" ToolTip="Selecione um Subgrupo" Width="220px" runat="server"
        onselectedindexchanged="ddlSubgrupo_SelectedIndexChanged" AutoPostBack="true">
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlSubgrupo" class="labelPixel" title="Subgrupo 2">Subgrupo 2</label>
        <asp:DropDownList ID="ddlSubGrupo2" ToolTip="Selecione um Subgrupo 2" Width="220px" runat="server">
        </asp:DropDownList>
    </li>
    </ContentTemplate>
    </asp:UpdatePanel>
    <li>
        <label for="ddlGrupo" class="labelPixel" title="Conta">Conta</label>
        <asp:TextBox ID="txtConta" runat="server" ToolTip="Informe uma Conta" MaxLength="40" CssClass="campoDescricao"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>