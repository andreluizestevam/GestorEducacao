<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelProntuario.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8270_Prontuario._8271_Relatorios.RelProntuario" %>
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
        .chk label
        {
            display: inline;
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
            <asp:DropDownList runat="server" ID="ddlPaciente" class="ddlPadrao" />
            <asp:RequiredFieldValidator runat="server" ID="rfvPaciente" CssClass="validatorField"
                ErrorMessage="O campo paciente é obrigatório" ControlToValidate="ddlPaciente" Display="Dynamic" />
        </li>
        <li style="clear: both;">
            <label>Itens do Relatório</label>
        </li>
        <li style="clear: both; margin-bottom:10px;">
            <asp:CheckBox ID="chkAnamnese" runat="server" Checked="true" Text="Imprimir Anamneses e Planejamento de Ações" CssClass="chk"
                ToolTip="Marque caso deseje que seja impresso a anamnese e as ações planejadas" />
        </li>
        <li style="clear: both;">
            <asp:CheckBox ID="chkAgenda" runat="server" Checked="true" Text="Imprimir Agenda  " CssClass="chk"
                ToolTip="Marque caso deseje que seja impresso a agenda do paciente" />
        </li>
        <li>
            <asp:TextBox runat="server" class="campoData" ID="txtDtIniAgenda" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="txtDtFimAgenda" ToolTip="Informe a Data Final do Período"></asp:TextBox>
        </li>
        <li style="clear: both;">
            <asp:CheckBox ID="chkEvolucao" runat="server" Checked="true" Text="Imprimir Evolução" CssClass="chk"
                ToolTip="Marque caso deseje que seja impresso a evolução do paciente assim como seus medicamentos e exames" />
        </li>
        <li>
            <asp:TextBox runat="server" class="campoData" ID="txtDtIniEvolucao" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:Label runat="server" ID="Label2"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="txtDtFimEvolucao" ToolTip="Informe a Data Final do Período"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
