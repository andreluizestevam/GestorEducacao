<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1101_CadastroRegiao.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
 
    <li>
        <label for="txtRegiao" title="Cidade">Região</label>
            <asp:TextBox ID="txtRegiao" ToolTip="Pesquise pela Região" runat="server" CssClass="campoRegiao" MaxLength="40"></asp:TextBox>
    </li>
        <li>
        <label for="txtSigla" title="Cidade">Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Pesquise pela Sigla" runat="server" CssClass="campoSigla" MaxLength="12"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
