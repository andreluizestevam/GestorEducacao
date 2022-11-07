<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="AtestadoMedico.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8299_Relatorios.AtestadoMedico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 315px;
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
        <li class="liboth" style="margin-top: 15px;">
            <label style="color: blue;">
                PARÂMETROS DE BUSCA DE ATENDIMENTO</label>
            <li class="liboth" style="margin-top: -5px;">
                <asp:Label runat="server" ID="lblUnidadeCadastro">Unidade</asp:Label><br />
                <asp:DropDownList Width="250px" runat="server" class="ddlPadrao" ID="ddlUnidadeCadastro"
                    OnSelectedIndexChanged="dllUnidadeCadastro_SelectedIndexChanged" AutoPostBack="true"
                    ToolTip="Selecine a unidade desejada" />
            </li>
            <li style="clear: both; margin-top: 5px;">
                <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado">
                    Paciente</label>
                <asp:DropDownList ID="ddlPaciente" runat="server" Width="230px" Visible="false" OnSelectedIndexChanged="ddlPacientes_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
                <asp:TextBox Style="margin-bottom: -10px; height: 13px;" ID="txtNomePacPesq" Width="230px"
                    ToolTip="Digite o nome ou parte do nome do paciente" runat="server" />
            </li>
            <li style="margin-top: 16px; margin-left: 0px;">
                <asp:ImageButton ID="imgbPesqPacNome" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                    OnClick="imgbPesqPacNome_OnClick" />
                <asp:ImageButton ID="imgbVoltarPesq" Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                    OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" />
            </li>
            <li class="liboth">
                <asp:Label runat="server" class="lblObrigatorio" ID="lblPeriodo" ToolTip="Periodo de atendimento finalizados">Periodo</asp:Label><br />
                <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Periodo"
                    AutoPostBack="true" OnTextChanged="txtPeriodo_OnTextChanged"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                    ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
                <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
                <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Periodo"
                    AutoPostBack="true" OnTextChanged="txtPeriodo_OnTextChanged"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                    ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator>
            </li>
            <li class="liboth" style="margin-top: -7px;">
                <asp:Label runat="server" ID="Label2">Atendimento</asp:Label><br />
                <asp:DropDownList Width="250px" runat="server" class="ddlPadrao" ID="ddlAtendimento"
                    ToolTip="Selecine o atendimento desejado" />
            </li>
        </li>
        <li class="liboth" style="margin-top: 10px;">
            <label style="color: blue;">
                OPÇÕES DE ATENDIMENTO</label>
        </li>
        <li class="liboth">
            <label style="margin-top: 30px; margin-left: -138px;">
                <asp:RadioButton Style="margin-right: -4px;" Checked="true" runat="server" OnCheckedChanged="rbAtestado_OnCheckedChanged"
                    ID="rbAtestado" ToolTip="Marcar para emitir um atestado médico" AutoPostBack="true" />Atestado</label>
            <li class="" style="margin-top: 23px; margin-left: -61px;">
                <label style="">
                    Dias</label>
                <asp:TextBox runat="server" Width="25px" ID="txtDiasAtest" Style="margin-bottom: -10px;"></asp:TextBox>
            </li>
            <li style="margin-top: 23px; margin-left: -31px;">
                <label style="margin-left: 5px;">
                    CID</label>
                <asp:CheckBox Style="" runat="server" ID="chkMostraCid" Checked="true" />
                <asp:DropDownList ID="ddlCid" runat="server" Width="123px" Style="margin-left: -7px;">
                </asp:DropDownList>
            </li>
        </li>
        <li>
            <label style="margin-top: 20px; margin-left: -5px;">
                <asp:RadioButton Style="margin-right: -4px;" runat="server" ID="rbComparecimento"
                    OnCheckedChanged="rbAtestado_OnCheckedChanged" ToolTip="Marcar para emitir um atestado de comparecimento"
                    AutoPostBack="true" />Comparecimento</label>
            <li class="liboth">
                <label style="margin-top: 10px;">
                    Período</label>
                <asp:DropDownList ID="ddlPeriodoCompar" runat="server" Width="96px">
                </asp:DropDownList>
            </li>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
