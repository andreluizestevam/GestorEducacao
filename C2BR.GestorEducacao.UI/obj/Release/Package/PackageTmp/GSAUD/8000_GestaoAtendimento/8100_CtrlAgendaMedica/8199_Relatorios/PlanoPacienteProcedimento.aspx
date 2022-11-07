<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="PlanoPacienteProcedimento.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios.PlanoPacienteProcedimento" %>

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
        .ddlUnidade
        {
            width: 280px;
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados" style="margin-left: 400px">
        <li class="liboth">
            <asp:Label runat="server" ID="lblUnidade">Unidade</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlUnidade" ID="ddlUnidade" OnSelectedIndexChanged="ddlUnidade_OnSelectedIndexChanged"
                AutoPostBack="true"  ToolTip="Selecine a Unidade Solicitante">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label style="">
                Operadora</label>
            <asp:DropDownList runat="server" ID="ddlOperadora" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged"
                AutoPostBack="true" Width="130px">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>
                Plano</label>
            <asp:DropDownList runat="server" ID="ddlPlano" Width="130px">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>
                Profissional
            </label>
            <asp:DropDownList runat="server" ID="ddlProfissional" Width="280px" OnSelectedIndexChanged="ddlProfissional_SelectedIndexChanged">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Situação do Paciente">
                Situação</label>
            <asp:DropDownList ID="ddlSituacaoAlu" ToolTip="Informe a Situação do Aluno"
                runat="server" Width="130px">
                <asp:ListItem Value="" Selected="True">Todos</asp:ListItem>
                <asp:ListItem Value="A">Em Atendimento</asp:ListItem>
                <asp:ListItem Value="V">Em Análise</asp:ListItem>
                <asp:ListItem Value="E">Alta (Normal)</asp:ListItem>
                <asp:ListItem Value="D">Alta (Desistência)</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both; margin-top:5px;">
            <label for="ddlPaciente" title="Nome do usuário selecionado">
            Paciente</label>
            <asp:DropDownList ID="ddlPaciente" runat="server" Width="230px" Visible="false">
            </asp:DropDownList>
            <asp:TextBox style="margin-bottom:-10px; height:13px;" ID="txtNomePacPesq" Width="230px" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" />
            
        </li>
        <li style="margin-top: 20px; margin-left: 0px;">
            <asp:ImageButton ID="imgbPesqPacNome" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                OnClick="imgbPesqPacNome_OnClick" />
            <asp:ImageButton ID="imgbVoltarPesq" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
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
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" ValidationGroup ="ValidacaoData" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" ValidationGroup ="ValidacaoData" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator>
        </li>
        
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
