<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ExtRecepcaoPorPeriodo.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8210_RecepcaoDeAvaliacao._8219_Relatorios.ExtRecepcaoPorPeriodo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ulDados
        {
            width: 275px;
            margin-top: 50px;
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
            <asp:Label runat="server" ID="lblMedic">Paciente</asp:Label><br />
            <asp:DropDownList runat="server" ID="ddlPaciente" ValidationGroup="Pesquisa" ToolTip="Selecine o Medicamento" Width="230px" Visible="false">
            </asp:DropDownList>
            <asp:TextBox ID="txtNomePacPesq" style="margin-bottom:-10px; height:14px;" ValidationGroup="Pesquisa" Width="230px" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" />
        </li>
        <li style="margin-top: -14px;margin-left: 295px;">
            <asp:ImageButton ID="imgbPesqPacNome" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                OnClick="imgbPesqPacNome_OnClick" ValidationGroup="Pesquisa" />
            <asp:ImageButton ID="imgbVoltarPesq" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" ValidationGroup="Pesquisa" />
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
            <asp:DropDownList runat="server" class="ddlPaciente" ID="ddlCategoria">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="Label2">Ordenado Por</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlArea" ID="dllOrdenadoPor">
                <asp:ListItem Value="PA">Paciente</asp:ListItem>
                <asp:ListItem Value="NR">Numero do registro </asp:ListItem>
                <asp:ListItem Value="QP">QPA Procedimentos</asp:ListItem>
                <asp:ListItem Value="QS">QSA Seções</asp:ListItem>
                <asp:ListItem Value="TS">R$ Total de sessões </asp:ListItem>
                <asp:ListItem Value="DT">Data</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="Label3">Procedimentos </asp:Label><br />
            <asp:DropDownList runat="server" class="ddlArea" ID="ddlProcedimentos">
                <asp:ListItem Value="C">Com Procedimentos</asp:ListItem>
                <asp:ListItem Value="S">Sem Procedimentos</asp:ListItem>
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
