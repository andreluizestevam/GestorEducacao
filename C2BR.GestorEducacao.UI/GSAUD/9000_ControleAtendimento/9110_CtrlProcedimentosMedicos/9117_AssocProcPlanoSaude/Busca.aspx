<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9117_AssocProcPlanoSaude.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados
    {
        width:300px;
    }
    .ulDados li
    {
        margin:5px;
    }
    input
    {
        height:13px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul class="ulDados" style="margin:20px 0 0 10px;">
    <li>
        <label title="Pesquise pela Operadora">Operadora</label>
        <asp:DropDownList runat="server" ID="ddlOper" ToolTip="Pesquise pela Operadora" Width="180px"></asp:DropDownList>
    </li>
    <li>
        <label title="Pesquise pelo Plano">Plano de Saúde</label>
        <asp:TextBox runat="server" ID="txtPlano" ToolTip="Pesquise pelo Plano" Width="220px"></asp:TextBox>
    </li>
    <li style="clear:both">
        <label>Situação</label>
        <asp:DropDownList runat="server" ID="ddlSituacao">
            <asp:ListItem Value="0" Text="Todos"></asp:ListItem>
            <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
            <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
        </asp:DropDownList>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
