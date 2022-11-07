<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ResumoRegistroVacinacao.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9200_CtrlVacinas._9299_Relatorios.ResumoRegistroVacinacao" %>

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
    <ul class="ulDados" style="margin-left: 360px">
        <li class="liboth">
            <label>Tipo Campanha</label>
            <asp:DropDownList runat="server" Width="155px" ID="drpTipoCampanha" ToolTip="Selecine o grupo de risco desejado" />
        </li>
        <li style="margin-top: 0px;">
            <label>Classificação da Campanha</label>
            <asp:DropDownList runat="server" Width="124px" ID="drpClassificacaoCampanha" />
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="Label2">Campanha de Vacinação</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="drpCampanhaVacinacao" ToolTip="Selecine a campanha desejada" />
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblUnidadeCadastro">Unidade de Vacinação</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="drpUnidadeVacinacao" ToolTip="Selecine a unidade desejada" />
        </li>
        <li style="clear: both">
            <label>Profissional Saúde Aplicação</label>
            <asp:DropDownList Width="80px" runat="server" ID="drpProfissional" />
        </li>
        <li style="clear: both">
            <asp:Label runat="server" class="lblObrigatorio" ID="lblPeriodo">Período de Registro de Vacina</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
