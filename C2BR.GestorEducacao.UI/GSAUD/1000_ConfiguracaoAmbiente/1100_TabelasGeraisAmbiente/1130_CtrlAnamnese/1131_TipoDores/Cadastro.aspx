<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1130_CtrlAnamnese._1131_TipoDores.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 240px;
            margin-top: 40px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <asp:HiddenField runat="server" ID="hidSituacao" />
        <li>
            <label title="Nome do Tipo de Dor" class="lblObrigatorio">
                Nome</label>
            <asp:TextBox runat="server" ID="txtNomeTipo" Width="210px" MaxLength="130" ToolTip="Nome do Tipo de Dor"></asp:TextBox>
        </li>
        <li style="clear: both">
            <label title="Sigla do Tipo de Dor" class="lblObrigatorio">
                Sigla
            </label>
            <asp:TextBox runat="server" ID="txtSiglaTipo" Width="90px" MaxLength="4" ToolTip="Sigla do Tipo de Dor"></asp:TextBox>
        </li>
        <li>
            <label>
                Observação</label>
            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtObser" Width="210px" Height="80px"
                MaxLength="200" ToolTip="Observação do Tipo de Dor"></asp:TextBox>
        </li>
        <li style="clear: both; margin-top:10px;">
            <label title="Situação do Tipo de Dor" class="lblObrigatorio">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituaTipo" Width="110px" ToolTip="Situação do Tipo de Dor">
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
