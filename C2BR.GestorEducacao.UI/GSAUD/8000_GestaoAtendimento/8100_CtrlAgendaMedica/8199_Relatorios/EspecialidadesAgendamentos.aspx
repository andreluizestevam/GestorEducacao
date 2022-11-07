<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="EspecialidadesAgendamentos.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios.EspecialidadesAgendamentos" %>
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
            <asp:Label runat="server" ID="lblUnidadeCadastro">Unidade de Cadastro</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="ddlUnidadeCadastro" OnSelectedIndexChanged="dllUnidadeCadastro_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecine a unidade desejada" />
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblUnidadeLotacao">Unidade Lotação/Contrato</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="ddlUnidadeContrato" OnSelectedIndexChanged="ddlUnidadeContrato_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecine a unidade desejada" />
        </li>
        <li style="clear: liboth">
            <label>Local</label>
            <asp:DropDownList runat="server" ID="ddlLocal" class="ddlPadrao">
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <label style="">Contratação</label>
            <asp:DropDownList runat="server" ID="ddlOperadora" Width="195px" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged" AutoPostBack="true" />
        </li>
        <li class="liboth">
            <label>Plano</label>
            <asp:DropDownList runat="server" ID="ddlPlano" Width="85px">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>Profissional</label>
            <asp:DropDownList runat="server" ID="ddlProfissional" class="ddlPadrao" OnSelectedIndexChanged="ddlProfissional_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both; margin-top:5px;">
            <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado">
            Paciente</label>
            <asp:DropDownList ID="ddlPaciente" runat="server" Width="230px" Visible="false">
            </asp:DropDownList>
            <asp:TextBox style="margin-bottom:-10px; height:13px;" ID="txtNomePacPesq" Width="230px" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" />
            
        </li>
        <li style="margin-top: 16px; margin-left: 0px;">
            <asp:ImageButton ID="imgbPesqPacNome" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                OnClick="imgbPesqPacNome_OnClick" />
            <asp:ImageButton ID="imgbVoltarPesq" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
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
