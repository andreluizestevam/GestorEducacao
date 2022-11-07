<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="DemonExameSolic.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9399_Relatorios.DemonExameSolic" %>
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
    <ul id="ulDadosPesquisa" runat="server" class="ulDados">
        <li style="clear: both">
            <label style="">Contratação</label>
            <asp:DropDownList runat="server" ID="ddlOperadora" OnSelectedIndexChanged="ddlOperadora_SelectedIndexChanged" AutoPostBack="true" Width="195px" />
        </li>
        <li style="clear: both">
            <label style="">Procedimento</label>
            <asp:DropDownList runat="server" ID="ddlProcedimento" Width="195px" />
        </li>
        <li style="clear: both">
            <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado">Paciente</label>
            <asp:DropDownList ID="ddlPaciente" runat="server" class="ddlPadrao" OnSelectedIndexChanged="ddlPaciente_SelectedIndexChanged" AutoPostBack="true" Visible="false" Width="195px"></asp:DropDownList>
            <asp:TextBox ID="txtNomePacPesq" ValidationGroup="Pesquisa" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" style="width: 193px; margin-bottom:0px;"/>
            <asp:RequiredFieldValidator runat="server" ID="rfvPaciente" CssClass="validatorField"
                ErrorMessage="O campo Paciente é requerido" ControlToValidate="ddlPaciente"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 200px;margin-top: -17px;">
            <asp:ImageButton ID="imgbPesqPacNome" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                OnClick="imgbPesqPacNome_OnClick" ValidationGroup="Pesquisa" runat="server"/>
            <asp:ImageButton ID="imgbVoltarPesq" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                OnClick="imgbVoltarPesq_OnClick" Visible="false" ValidationGroup="Pesquisa" runat="server"/>
        </li>
        <li style="clear: both">
            <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado">Nº Registro</label>
            <asp:DropDownList ID="DropDownList3" runat="server" class="ddlPadrao" OnSelectedIndexChanged="ddlPaciente_SelectedIndexChanged" AutoPostBack="true" Visible="false"></asp:DropDownList>
            <asp:TextBox ID="TextBox1" ValidationGroup="Pesquisa" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" style="width: 75px; margin-bottom:0px;" MaxLength="12"/>
        </li>
        <li style="clear: both">
            <label style="">Solicitante</label>
            <asp:DropDownList runat="server" ID="ddlSolic" Width="285px" />
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
        <li style="clear: both; margin-right: 15px;">
            <label style="">Situação</label>
            <asp:DropDownList runat="server" ID="DropDownList1" Width="55px">
                <asp:ListItem Value="A">
                    Ativo
                </asp:ListItem>
                <asp:ListItem Value="I">
                    Inativo
                </asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label>Cortesia</label>
            <asp:DropDownList runat="server" ID="DropDownList2" Width="40px">
                <asp:ListItem Value="S">
                    Sim
                </asp:ListItem>
                <asp:ListItem Value="N">
                    Não
                </asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
