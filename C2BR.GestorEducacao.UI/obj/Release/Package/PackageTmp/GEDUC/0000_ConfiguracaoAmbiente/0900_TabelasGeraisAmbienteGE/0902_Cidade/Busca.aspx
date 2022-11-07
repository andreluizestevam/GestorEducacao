<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0902_Cidade.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">            
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlUf" title="UF">UF</label>
        <asp:DropDownList ID="ddlUf" runat="server" CssClass="campoUf"
            ToolTip="Informe a UF">
        </asp:DropDownList>
    </li> 
    <li>
        <label for="txtCidade" title="Cidade">Cidade</label>
            <asp:TextBox ID="txtCidade" ToolTip="Pesquise pela Cidade" runat="server" CssClass="campoCidade" MaxLength="40"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
