<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6290_TabelasGenerCtrlOperMateriais.F6293_CadastroGrupoItens.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">      
        /*--> CSS DADOS */
        .txtGrupo { width: 250px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtGrupo" title="Grupo" >Grupo de Itens</label>
        <asp:TextBox ID="txtGrupo" ToolTip="Pesquise pelo Grupo" runat="server" MaxLength="80" CssClass="txtGrupo"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
