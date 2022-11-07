<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6390_TabelasGenerCtrlOperPatrimonio.F6394_CadastroSubGrupo.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">    
        /*--> CSS DADOS */
        .ddlTpGrupo{ width: 150px;}
          
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlGrupo" title="Tipo do Grupo" >Grupo</label>
        <asp:DropDownList ID="ddlGrupo"  CssClass="ddlTpGrupo" runat="server">          
        </asp:DropDownList>
    </li>
    <li>
        <label for="txtSubGrupo" title="Sub Grupo" >SubGrupo</label>
        <asp:TextBox ID="txtSubGrupo" ToolTip="Pesquise pelo SubGrupo" runat="server" MaxLength="40" CssClass="campoDescricao"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
