<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" 
    AutoEventWireup="true" CodeBehind="DemonsAgendaPaciente.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios.DemonsAgendaPaciente" %>

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
        <li class="liboth">
            <label style="">Operadora</label>
            <asp:DropDownList runat="server" ID="ddlOperadora" Width="195px" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged" AutoPostBack="true" />
        </li>
        <li class="liboth">
            <label>Plano</label>
            <asp:DropDownList runat="server" ID="ddlPlano" Width="85px">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <label>Classificação funcional</label>
            <asp:DropDownList runat="server" ID="ddlClassFuncional" Width="170px" OnSelectedIndexChanged="ddlClassFuncional_SelectedIndexChanged" AutoPostBack="True" />
        </li>
        <li style="clear: both">
            <label>Profissional</label>
            <asp:DropDownList runat="server" ID="ddlProfissional" class="ddlPadrao" OnSelectedIndexChanged="ddlProfissional_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label class="lblObrigatorio">Paciente</label>
            <asp:DropDownList runat="server" ID="ddlPaciente" class="ddlPadrao" />
            <asp:RequiredFieldValidator runat="server" ID="rfvPaciente" CssClass="validatorField"
                ErrorMessage="O campo paciente é obrigatório" ControlToValidate="ddlPaciente" Display="Dynamic" />
        </li>
        <li style="clear: both">
            <label>Situação</label>
            <asp:DropDownList ID="ddlSituacao" ToolTip="Informe a Situação" runat="server" Width="170px">
                <asp:ListItem Value="" Selected="True">Todos</asp:ListItem>
                <asp:ListItem Value="QCA">Cancelados</asp:ListItem>
                <asp:ListItem Value="QFA">Faltas</asp:ListItem>
                <asp:ListItem Value="QFJ">Faltas Justificadas</asp:ListItem>
                <asp:ListItem Value="QPR">Presenças</asp:ListItem>
                <asp:ListItem Value="QEN">Encaminhamentos</asp:ListItem>
                <asp:ListItem Value="QAT">Atendias</asp:ListItem>
                <asp:ListItem Value="QIN">Inconsistências (Em Aberto)</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>Incluir datas sem procedimento ? </label>
            <asp:RadioButtonList ID="rblSemProc" runat="server" RepeatDirection="Horizontal" BorderWidth="0px">
                <asp:ListItem Value="true" Text="Sim" Selected="True" />
                <asp:ListItem Value="false" Text="Não" />
            </asp:RadioButtonList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" class="lblObrigatorio" ID="lblPeriodo">Período</asp:Label><br />
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
