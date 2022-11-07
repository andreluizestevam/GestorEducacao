<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="ImpostosPorUnidade.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._10000_Ctrl_Tributario._10100_CtrlImpostos._10199_Relatorios.ImpostosPorUnidade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados
    {
        width:200px;
    }
    .ulDados li
    {
        margin-left:5px;
        margin-top:6px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul class="ulDados">
    <li style="clear:both">
          <asp:Label runat="server" ID="lblTp">Tipo de Pesquisa</asp:Label> <br />
          <asp:DropDownList runat="server" ID="ddlTpPesq" Width="100px">
            <asp:ListItem Text="Por Modalidade" Value="M"></asp:ListItem>
            <asp:ListItem Text="Por Unidade" Value="U" Enabled="true"></asp:ListItem>
          </asp:DropDownList>
    </li>
    <li style="clear:both">
        <asp:Label runat="server" ID="lblUnidade">Unidade</asp:Label><br />
        <asp:DropDownList runat="server" ID="ddlUnidade" Width="180px"></asp:DropDownList>
    </li>
    <li style="clear:both">
        <asp:Label runat="server" ID="lblModa">Modalidade</asp:Label><br />
        <asp:DropDownList runat="server" ID="ddlModalidade" Width="140px"></asp:DropDownList>
    </li>
    <li class="liboth">
        <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio">Período</asp:Label><br />
        <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" id="rfvIniPeri" CssClass="validatorField" ErrorMessage="O campo data Inicial é requerido"
        ControlToValidate="IniPeri"></asp:RequiredFieldValidator>

        <asp:Label runat="server" ID="Label1" > &nbsp&nbsp&nbsp à &nbsp&nbsp&nbsp </asp:Label>

        <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" id="rfvFimPeri" CssClass="validatorField" ErrorMessage="O campo data Final é requerido"
        ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
