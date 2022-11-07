<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ExtAgendamento.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8210_RecepcaoDeAvaliacao._8219_Relatorios.ExtAgendamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ulDados
        {
            width: 275px;
            margin-top: 50px;
            margin-left: 300px;
        }
        .liLabel
        {
            margin-bottom: 2px;
        }
        .ulDados li
        {
            margin-top: 6px;
            margin-left: 60px;
        }
        
        .liboth
        {
            clear: both;
        }
        .ddlPaciente
        {
            width: 280px;
            clear: both;
        }
        
        .ddlUnidade
        {
            width: 280px;
            clear: both;
        }
        .ddlReg
        {
            width: 90px;
            clear: both;
        }
        .ddlArea
        {
            width: 130px;
            clear: both;
        }
        .ddlSubArea
        {
            width: 150px;
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="liboth">
            <asp:Label runat="server" classe="liLabel">Unidade</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlUnidade" ID="ddlUnidade" ToolTip="Selecine a Unidade Solicitante">
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblMedic">Paciente</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPaciente" ID="ddlPaciente" ToolTip="Selecine o Medicamento">
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblOperadora">Operadora</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlReg" ID="ddlOperadora" OnSelectedIndexChanged="ddlddlOperadora_SelectedIndexChanged"
                AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblArea">Plano</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlArea" ID="ddlPlano" OnSelectedIndexChanged="ddlPlano_SelectedIndexChanged"
                AutoPostBack="True">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblCategoria">Categoria </asp:Label>
            <br />
            <asp:DropDownList runat="server" Style="width: 150px" class="" ID="ddlCategoria">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <asp:Label runat="server" ID="Label3">Tipo</asp:Label>
            <br />
            <asp:DropDownList runat="server" Style="width: 120px" ID="ddlTipo">
                <asp:ListItem Value="0">Todos</asp:ListItem>
                <asp:ListItem Value="L">Lista de espera</asp:ListItem>
                <asp:ListItem Value="C">Consulta de avaliação</asp:ListItem>
                <asp:ListItem Value="P">Procedimentos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <asp:Label runat="server" ID="Label2">Situações</asp:Label>
            <br />
            <asp:DropDownList runat="server" Style="width: 80px" ID="ddlSituacao">
                <asp:ListItem Value="0">Todos</asp:ListItem>
                <asp:ListItem Value="A">Aberto</asp:ListItem>
                <asp:ListItem Value="R">Relizada</asp:ListItem>
                <asp:ListItem Value="C">Cancelada</asp:ListItem>
            </asp:DropDownList>
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
