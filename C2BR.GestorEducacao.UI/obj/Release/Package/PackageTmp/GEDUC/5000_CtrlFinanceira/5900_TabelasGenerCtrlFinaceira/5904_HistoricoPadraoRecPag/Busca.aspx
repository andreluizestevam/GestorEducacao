<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5904_HistoricoPadraoRecPag.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtNomeHisto" title="Descrição">
                Descrição</label>
            <asp:TextBox ID="txtNomeHisto" ToolTip="Pesquise pela Descrição" CssClass="txtDescricao" runat="server" MaxLength="40"></asp:TextBox>
        </li>
        <li style="clear: both;">
            <label for="ddlBanco" title="Tipo">Tipo</label>
            <asp:DropDownList ID="ddlTipo" runat="server" ToolTip="Selecione o Tipo">
                <asp:ListItem Selected="True" Value="T" Text="Todos"></asp:ListItem>
                <asp:ListItem Text="Crédito" Value="C"></asp:ListItem>
                <asp:ListItem Text="Débito" Value="D"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
