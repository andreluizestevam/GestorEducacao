<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="RelAgendamentosRecepcao.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8250_RecepcaoEncaminhamento._8259_Relatorios.RelAgendamentosRecepcao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ulDados
        {
            width: 400px;
            margin-top: 50px;
        }
        
        .ulDados li label
        {
            margin-bottom: 5px;
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
            <asp:Label runat="server" ID="lblUnidade">Unidade</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlUnidade" ID="ddlUnidade" ToolTip="Selecine a Unidade Solicitante">
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblOperadora">Classificação  funcional</asp:Label><br />
            <asp:DropDownList runat="server"  style=" width:150px" class="ddlClassFuncional" ID="ddlClassFuncional"
                OnSelectedIndexChanged="ddlClassFuncional_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblArea"> Profissional </asp:Label><br />
            <asp:DropDownList runat="server" style=" width:280px"  class="ddlArea" ID="ddlProfissional">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblCategoria">Tipo</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlStatus" Style="width: 190px" ID="ddlStatus">
                <asp:ListItem Value="AT">Agendamento de atendimento </asp:ListItem>
                <asp:ListItem Value="AA">Agendamento de avaliação</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblOrdem">Ordenado Por</asp:Label><br />
            <asp:DropDownList runat="server" ID="dllOrdem">
                <asp:ListItem Value="DT">Data/Hora</asp:ListItem>
                <asp:ListItem Value="PA">Paciente</asp:ListItem>
                <asp:ListItem Value="RS">Responsável</asp:ListItem>
                <asp:ListItem Value="PF">Profissional</asp:ListItem>
                <asp:ListItem Value="ST">Status</asp:ListItem>
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
