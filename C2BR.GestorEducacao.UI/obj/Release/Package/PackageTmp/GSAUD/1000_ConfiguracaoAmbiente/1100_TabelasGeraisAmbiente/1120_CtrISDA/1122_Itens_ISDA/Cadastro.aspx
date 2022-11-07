<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1120_CtrISDA._1122_Itens_ISDA.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 240px;
            margin-top:40px !important;
        }
        .ulDados li
        {
            margin: 0 0 0 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <asp:HiddenField runat="server" ID="hidSituacao" />
        <li>
            <label title="Nome do Tipo ISDA" class="lblObrigatorio">
                Nome</label>
            <asp:TextBox runat="server" ID="txtNomeTipo" Width="210px" MaxLength="60" ToolTip="Nome do Tipo ISDA"></asp:TextBox>
        </li>
        <li style="clear: both">
            <label title="Sigla do Tipo ISDA" class="lblObrigatorio">
                Sigla
            </label>
            <asp:TextBox runat="server" ID="txtSiglaTipo" Width="70px" MaxLength="12" ToolTip="Sigla do Tipo ISDA"></asp:TextBox>
        </li>
        <li style="clear: both">
            <label title="Selecione o Tipo ISDA">
                Tipo ISDA</label>
            <asp:DropDownList runat="server" ID="ddlTipoISDA" Width="130px" ToolTip="Selecione o Tipo ISDA">
            </asp:DropDownList>
        </li>
        <li style="clear: both; margin-top:10px">
            <label title="Situação do Tipo ISDA" class="lblObrigatorio">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituaTipo" Width="110px" ToolTip="Sigla do Tipo ISDA">
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
