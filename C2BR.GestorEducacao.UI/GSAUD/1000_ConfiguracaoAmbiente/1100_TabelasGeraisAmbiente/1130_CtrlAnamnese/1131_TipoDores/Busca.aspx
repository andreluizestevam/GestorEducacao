<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1130_CtrlAnamnese._1131_TipoDores.Busca" %>

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
            <label title="Nome do Tipo de Dor" class="lblObrigatorio">
                Nome</label>
            <asp:TextBox runat="server" ID="txtNomeTipo" Width="210px" MaxLength="40"
                ToolTip="Nome do Tipo de Dor"></asp:TextBox>
        </li>
        <li>
            <label title="Sigla do Tipo de Dor" class="lblObrigatorio">
                Sigla
            </label>
            <asp:TextBox runat="server" ID="txtSiglaTipo" Width="90px" MaxLength="4"
                ToolTip="Sigla do Tipo de Dor"></asp:TextBox>
        </li>
        <li>
            <label title="Situação do Tipo de Dor" class="lblObrigatorio">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituaTipo" Width="110px" ToolTip="Situação do Tipo de Dor">
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
