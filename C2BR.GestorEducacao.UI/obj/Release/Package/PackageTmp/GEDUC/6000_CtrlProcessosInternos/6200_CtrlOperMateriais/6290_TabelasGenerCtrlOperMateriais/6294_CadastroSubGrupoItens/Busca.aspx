<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6290_TabelasGenerCtrlOperMateriais.F6294_CadastroSubGrupoItens.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">    
        /*--> CSS DADOS */
        .ddlTpGrupo, .txtSubGrupo { width: 250px;}
          
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlGrupo" title="Tipo do Grupo" >Grupo de Itens</label>
        <asp:DropDownList ID="ddlGrupo"  CssClass="ddlTpGrupo" runat="server">          
        </asp:DropDownList>
    </li>
    <li>
        <label for="txtSubGrupo" title="Sub Grupo" >SubGrupo de Itens</label>
        <asp:TextBox ID="txtSubGrupo" ToolTip="Pesquise pelo SubGrupo" runat="server" MaxLength="80" CssClass="txtSubGrupo"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
