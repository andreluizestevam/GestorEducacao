<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="FichaAtendimento.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8299_Relatorios.FichaAtendimento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
        }
        .ulDados li
        {
            margin: 3px;
        }
        input
        {
            height: 13px;
        }
        label
        {
            margin-bottom: 2px;
        }
        .ddlPadrao
        {
            width: 285px;
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados" style="margin-left: 400px">
        <li class="liboth">
            <asp:Label runat="server" ID="lblUnidadeCadastro">Unidade de Cadastro</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="ddlUnidadeCadastro" OnSelectedIndexChanged="dllUnidadeCadastro_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecine a unidade desejada" />
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblUnidadeLotacao">Unidade Lotação/Contrato</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="ddlUnidadeContrato" OnSelectedIndexChanged="ddlUnidadeContrato_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecine a unidade desejada" />
        </li>
        <li style="clear: both;">
            <label class="lblObrigatorio">Paciente</label>
            <asp:DropDownList runat="server" ID="ddlPaciente" class="ddlPadrao" OnSelectedIndexChanged="ddlPaciente_SelectedIndexChanged" AutoPostBack="true" />
            <asp:RequiredFieldValidator runat="server" ID="rfvPaciente" CssClass="validatorField"
                ErrorMessage="O campo paciente é obrigatório" ControlToValidate="ddlPaciente" Display="Dynamic" />
        </li>
        <li id="liNumAtend" runat="server" visible="false" style="clear: both;">
            <label class="lblObrigatorio">Atendimento</label>
            <asp:DropDownList runat="server" ID="drpAtendimento" class="ddlPadrao" />
            <asp:RequiredFieldValidator runat="server" ID="rfvAtendimento" CssClass="validatorField"
                ErrorMessage="O campo atendimento é obrigatório" ControlToValidate="drpAtendimento" Display="Dynamic" />
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
