<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2908_AgrupadorBolsa.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtNome" title="Nome">Nome</label>
        <asp:TextBox ID="txtNome" ToolTip="Pesquise pelo Nome do Agrupador" runat="server" MaxLength="40" CssClass="campoDescricao"></asp:TextBox>
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
