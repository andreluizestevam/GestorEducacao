<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="ResultadoExames.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9399_Relatorios.ResultadoExames" %>
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
        }
        .chk
        {
            margin-left: -5px;
        }
        .chk label
        {
            display: inline;
            margin-left: -4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados" style="margin-left: 355px">
        <li style="clear: both">
            <label>Unidade</label>
            <asp:DropDownList runat="server" class="ddlPadrao" ID="ddlUnidade" ToolTip="Selecine a unidade desejada" />
        </li>
        <li style="clear: both">
            <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado">Paciente</label>
            <asp:DropDownList ID="ddlPaciente" runat="server" class="ddlPadrao" OnSelectedIndexChanged="ddlPaciente_SelectedIndexChanged" AutoPostBack="true" Visible="false"></asp:DropDownList>
            <asp:TextBox ID="txtNomePacPesq" ValidationGroup="Pesquisa" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" style="width: 283px; margin-bottom:0px;"/>
            <asp:RequiredFieldValidator runat="server" ID="rfvPaciente" CssClass="validatorField"
                ErrorMessage="O campo Paciente é requerido" ControlToValidate="ddlPaciente"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 290px;margin-top: -18px;">
            <asp:ImageButton ID="imgbPesqPacNome" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                OnClick="imgbPesqPacNome_OnClick" ValidationGroup="Pesquisa" runat="server"/>
            <asp:ImageButton ID="imgbVoltarPesq" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                OnClick="imgbVoltarPesq_OnClick" Visible="false" ValidationGroup="Pesquisa" runat="server"/>
        </li>
    </ul>
    <ul id="ulDadosPesquisa" runat="server" visible="false" class="ulDados" style="margin-left: 355px">
        <li style="clear: both">
            <label style="">Contratação</label>
            <asp:DropDownList runat="server" ID="ddlOperadora" OnSelectedIndexChanged="ddlOperadora_SelectedIndexChanged" AutoPostBack="true" Width="195px" />
        </li>
        <li style="clear: both">
            <label style="">Procedimento</label>
            <asp:DropDownList runat="server" ID="ddlProcedimento" Width="195px" />
        </li>
        <li style="clear: both">
            <label>Período</label>
            <asp:TextBox runat="server" class="campoData" ID="txtIniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="txtIniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="txtFimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="txtFimPeri"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
