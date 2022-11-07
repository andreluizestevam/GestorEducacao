<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9111_Grupo.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 200px;
            margin: 30px 0 0 390px !important;
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
        <li style="clear: both">
            <label title="Área do Grupo de Procedimentos de Saúde" class="lblObrigatorio">
                Área de Abrangência</label>
            <asp:DropDownList runat="server" ID="drpArea" Width="120px" ToolTip="Área do Grupo de Procedimentos" />
            <asp:RequiredFieldValidator runat="server" ID="rfvArea" ControlToValidate="drpArea"
                ErrorMessage="A Área do Grupo é Requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-top:18px;">
            <label class="lblObrigatorio" title="Nome da Grupo de Procedimentos de Saúde">
                Grupo de Procedimentos</label>
            <asp:TextBox runat="server" ID="txtNomeGrupo" Width="220px" MaxLength="100" ToolTip="Nome da Grupo de Procedimentos Médicos"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvtxtNomeCampanha" ControlToValidate="txtNomeGrupo"
                ErrorMessage="Nome do Grupo é requerido" Text="*"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Situação do Grupo de Procedimentos Médicos" class="lblObrigatorio">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="80px" ToolTip="Situação do Grupo de Procedimentos Médicos">
                <asp:ListItem Value="" Text="Selecione"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfvSituacao" ControlToValidate="ddlSituacao"
                ErrorMessage="A Situação do Grupo é Requerida" Text="*"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
