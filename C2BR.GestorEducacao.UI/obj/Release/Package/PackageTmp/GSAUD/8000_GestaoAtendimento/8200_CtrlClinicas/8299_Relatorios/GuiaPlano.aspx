<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="GuiaPlano.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8299_Relatorios.GuiaPlano" %>

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
        <li>
            <label title="Data Comparecimento" class="lblObrigatorio">
                Data</label>
            <asp:TextBox ID="txtDtGuia" runat="server" ValidationGroup="guia" class="campoData"
                ToolTip="Informe a data de comparecimento" AutoPostBack="true" OnTextChanged="txtDtGuia_OnTextChanged" />
            <asp:RequiredFieldValidator runat="server" ValidationGroup="guia" ID="rfvDtGuia"
                CssClass="validatorField" ErrorMessage="O campo data é requerido" ControlToValidate="txtDtGuia"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both; margin-top: -6px;">
            <label title="Paciente">
                Paciente</label>
            <%--<asp:DropDownList ID="drpPacienteGuia" AutoPostBack="true" runat="server" Width="240px"
                OnSelectedIndexChanged="drpPacienteGuia_OnSelectedIndexChanged" />--%>
            <asp:TextBox runat="server" ID="txtPacienteGuia" Style="margin: 0; width: 240px;"
                ToolTip="Digite o nome ou parte do nome do paciente, no mínimo 3 letras."></asp:TextBox>
            <asp:ImageButton ID="imgPesqPacienteGuia" ToolTip="Pesquisar nome do paciente" runat="server"
                ImageUrl="~/Library/IMG/IC_PGS_Recepcao_CadPacien.png" OnClick="imgPesqPacienteGuia_OnClick" />
            <asp:DropDownList ID="drpPacienteGuia" Width="240px" runat="server" Visible="false"
                OnSelectedIndexChanged="drpPacienteGuia_OnSelectedIndexChanged" AutoPostBack="true"/>
            <asp:ImageButton ID="imgVoltarPesqPacienteGuia" ValidationGroup="pesqPac" Width="16px"
                Height="16px" Style="margin: 0 0 -4px 0px;" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                OnClick="imgVoltarPesqPacienteGuia_OnClick" Visible="false" runat="server" />
        </li>
        <li>
            <label title="Operadora">
                Operadora</label>
            <asp:DropDownList ID="drpOperGuia" runat="server" Width="80px" />
        </li>
        <li style="clear: both;">
            <label title="Agendamentos">
                Agendamentos</label>
            <asp:DropDownList ID="ddlAgendGuia" runat="server" Width="240px" />
        </li>
        <li style="clear: both; margin-top: 5px;">
            <label>
                <asp:CheckBox runat="server" ID="chkGuiaConsol" />
                Guia com procedimentos consolidados</label>
        </li>
        <li style="margin-top: 5px; clear: both;">
            <asp:TextBox runat="server" class="campoData" ID="txtDtGuiaIni" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:Label runat="server" ID="Label25"> &nbsp até &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="txtDtGuiaFim" ToolTip="Informe a Data Final do Período"></asp:TextBox>
        </li>
        <li style="clear: both; margin-top: -5px;">
            <label title="Observações">
                Observações / Justificativa</label>
            <asp:TextBox ID="txtObsGuia" Width="290px" Height="40px" TextMode="MultiLine" MaxLength="180"
                runat="server" />
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
