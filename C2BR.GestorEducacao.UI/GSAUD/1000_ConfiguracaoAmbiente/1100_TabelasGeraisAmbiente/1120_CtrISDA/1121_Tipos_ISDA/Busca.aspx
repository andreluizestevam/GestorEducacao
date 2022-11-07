<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1120_CtrISDA._1121_Tipos_ISDA.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados
    {
        width:200px;
        margin-top:60px;
    }
    .ulDados li
    {
        margin: 5px 0 0 5px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label title="Pesquise pelo Nome do Tipo ISDA desejado">
                Nome</label>
            <asp:TextBox ID="txtNome" ToolTip="Pesquise pelo Nome do Tipo ISDA desejado" runat="server"
                CssClass="campoRegiao" MaxLength="40" Width="210px"></asp:TextBox>
        </li>
        <li style="clear:both">
            <label title="Pesquise pela Sigla do Tipo ISDA desejada">
                Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Pesquise pela Sigla do Tipo ISDA desejada" runat="server"
                Width="70px" MaxLength="4"></asp:TextBox>
        </li>
        <li style="clear:both">
            <label title="Selecione a Situação do Tipo ISDA que deseja pesquisar">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituaTipo" Width="110px" ToolTip="Selecione a Situação do Tipo ISDA que deseja pesquisar">
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
