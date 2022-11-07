<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ExtratoAgendaConsulta.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios.ExtratoAgendaConsulta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
            margin-top: 40px !important;
        }
        .ulDados li
        {
            margin-top: 5px;
            margin-left: 5px;
        }
        .lblsub
        {
            color: #436EEE;
        }
        .chk label
        {
            display: inline;
        }
        .lblsub
        {
            color: #436EEE;
            clear: both;
            margin-bottom: -3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="lblsub" style="margin-bottom: -5px">
            <label for="ddlAgrupador" title="">
                Contexo Funcional</label>
        </li>
        <li style="width: 220px;">
            <asp:Label runat="server" ID="lblUnid">Unidade Cadastro </asp:Label>
            <asp:DropDownList runat="server" ID="ddlUnidade" Width="280px" OnSelectedIndexChanged="ddlUnidade_OnSelectedIndexChanged"
                AutoPostBack="true" ToolTip="Selecione a Unidade Desejada para Pesquisa.">
            </asp:DropDownList>
        </li>
        <li style="width: 220px;">
            <asp:Label runat="server" ID="Label2">Unidade Lotação/Contrato</asp:Label>
            <asp:DropDownList runat="server" ID="ddlUnidContrato" Width="280px" ToolTip="Selecione"
                OnSelectedIndexChanged="ddlUnidContrato_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="width: 190px;">
            <asp:Label runat="server" ID="lblEspec">Especialidade</asp:Label>
            <asp:DropDownList runat="server" ID="ddlEspec" Width="190px" ToolTip="Selecione"
                OnSelectedIndexChanged="lblEspec_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="width: 190px;">
            <asp:Label runat="server" ID="Label3">Classificação Profissional</asp:Label>
            <asp:DropDownList runat="server" ID="ddlClassificacaoProfissional" Width="210px"
                OnSelectedIndexChanged="ddlClassificacaoProfissional_OnSelectedIndexChanged"
                AutoPostBack="true" ToolTip="Selecione">
            </asp:DropDownList>
        </li>
        <li style="width: 210px;">
            <asp:Label runat="server" ID="lblMedico">Profissional Saúde</asp:Label>
            <asp:DropDownList runat="server" ID="ddlMedico" Width="280px" ToolTip="Selecione">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="lblsub" style="margin-bottom: -5px">
            <label for="ddlAgrupador" title="">
                 Contexo da Consulta</label>
        </li>
     
        <li style="width: 220px;">
            <asp:Label runat="server" ID="Label5">Unidade da Consulta </asp:Label>
            <asp:DropDownList ID="ddlUnidConsulta" Width="210px" runat="server">
            </asp:DropDownList>
        </li>
        <li style="width: 180px;">
            <asp:Label runat="server" ID="lblLocal">Local</asp:Label>
            <asp:DropDownList runat="server" ID="ddlDeptLocal" Width="180px" ToolTip="Selecione">
            </asp:DropDownList>
        </li>
        <li style="width: 190px;">
            <asp:Label runat="server" ID="Label4">Especialidade Consulta</asp:Label>
            <asp:DropDownList runat="server" ID="ddlEspecialidadeConsulta" Width="190px" ToolTip="Selecione">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="width: 210px;">
            <li style="width: 100px;">
                <asp:Label runat="server" ID="lblstatus">Status</asp:Label>
                <asp:DropDownList runat="server" ID="ddlStatus" ToolTip="Selecione " Width="100px">
                    <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Confirmado" Value="S"></asp:ListItem>
                    <asp:ListItem Text="Não Confirmado" Value="N"></asp:ListItem>
                    <asp:ListItem Text="Indisponível" Value="I"></asp:ListItem>
                </asp:DropDownList>
            </li>
            <li style="width: 80px;">
                <asp:Label runat="server" ID="lblSitu" ToolTip="Situação de horário de agenda de consultas">Situação</asp:Label>
                <asp:DropDownList runat="server" ID="ddlSituacao" ToolTip="Situação de horário de agenda de consultas">
                    <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Agendados" Value="G"></asp:ListItem>
                    <asp:ListItem Text="Em Aberto" Value="A"></asp:ListItem>
                    <asp:ListItem Text="Cancelado" Value="C"></asp:ListItem>
                    <asp:ListItem Text="Realizado" Value="R"></asp:ListItem>
                </asp:DropDownList>
            </li>
            <li style="clear: both; margin-left: 0px;">
                <asp:CheckBox Style="width: 130px;" ID="CheckBoxVerValor" CssClass="chk" runat="server"
                    Text="Com valor" />
            </li>
            <li class="liboth" style="width: 500px; margin-bottom: -7px;">
                <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio">Período</asp:Label><br />
                <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                    ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
                <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
                <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                    ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator>
            </li>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
