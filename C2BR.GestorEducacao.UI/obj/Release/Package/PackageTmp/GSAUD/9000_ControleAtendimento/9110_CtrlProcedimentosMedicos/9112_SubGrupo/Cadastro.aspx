<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9112_SubGrupo.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
            margin-left: 400px !important;
        }
        .ulDados li
        {
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidCoSitua" />
    <ul class="ulDados">
        <li>
            <label title="Grupo de Procedimentos Médicos" class="lblObrigatorio">
                Grupo</label>
            <asp:DropDownList runat="server" ID="ddlGrupo" Width="160px" ToolTip="Grupo de Procedimentos Médicos">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlGrupo"
                ErrorMessage="Nome do Grupo é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both; margin-top:10px;">
            <label class="lblObrigatorio" title="Nome da SubGrupo de Procedimentos Médicos">
                SubGrupo</label>
            <asp:TextBox runat="server" ID="txtNoSubGrupo" Width="220px" MaxLength="100" ToolTip="Nome da SubGrupo de Procedimentos Médicos"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvtxtNomeCampanha" ControlToValidate="txtNoSubGrupo"
                ErrorMessage="Nome do SubGrupo é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Situação do SubGrupo de Procedimentos Médicos" class="lblObrigatorio">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="80px" ToolTip="Situação do SubGrupo de Procedimentos Médicos">
                <asp:ListItem Value="" Text="Selecione"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlSituacao"
                ErrorMessage="A Situação do SubGrupo é Requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
