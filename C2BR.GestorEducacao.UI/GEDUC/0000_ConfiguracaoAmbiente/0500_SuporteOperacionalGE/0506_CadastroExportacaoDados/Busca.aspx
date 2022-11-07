<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" 
    Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0506_CadastroExportacaoDados.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS Dados */
        .txtFuncio { width: 275px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">        
    <li>
        <label for="txtFuncio" title="Funcionalidade">Funcionalidade</label>
        <asp:TextBox ID="txtFuncio" ToolTip="Informe a Funcionalidade de Exportação" runat="server" MaxLength="300" CssClass="txtFuncio"></asp:TextBox>
    </li>
    <li>
        <label for="txtModulo" title="Funcionalidade">Módulo</label>
        <asp:TextBox ID="txtModulo" ToolTip="Informe o Módulo de Exportação" runat="server" MaxLength="300" CssClass="txtFuncio"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>