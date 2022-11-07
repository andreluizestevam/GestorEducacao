<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2903_TipoBolsaEscolar.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*CSS LIs*/
        .liClear { clear: both; }   
        
        /*CSS Dados*/
        .ddlTipo { width: 70px; }
        .ddlGrupoBolsa { width: 150px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlTipo" title="Tipo" >
            Tipo</label>
        <asp:DropDownList ID="ddlTipo" ToolTip="Selecione o Tipo" runat="server" CssClass="ddlTipo">
            <asp:ListItem Value="T">Todos</asp:ListItem>
            <asp:ListItem Value="B">Bolsa</asp:ListItem>
            <asp:ListItem Value="C">Convênio</asp:ListItem>
        </asp:DropDownList>
    </li>
    <li class="liClear">
        <label for="ddlGrupoBolsa" title="Agrupador da Bolsa/Convênio" >
            Agrupador</label>
        <asp:DropDownList ID="ddlGrupoBolsa" ToolTip="Selecione o Agrupador da Bolsa/Convênio" runat="server" CssClass="ddlGrupoBolsa">
        </asp:DropDownList>
    </li>
    <li>
        <label for="txtDE_TIPO_BOLSA" title="Nome">Nome</label>
        <asp:TextBox ID="txtDE_TIPO_BOLSA" ToolTip="Pesquise pelo Nome de Bolsa/Convênio" runat="server" MaxLength="20" CssClass="campoDescricao"></asp:TextBox>
    </li>
    <li class="liClear">
        <label for="ddlSituacao" title="Situação">
            Situação</label>
        <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione a Situação" runat="server" CssClass="ddlSituacao">
            <asp:ListItem Value="T">Todas</asp:ListItem>
            <asp:ListItem Value="A">Ativo</asp:ListItem>
            <asp:ListItem Value="I">Inativo</asp:ListItem>
        </asp:DropDownList>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
