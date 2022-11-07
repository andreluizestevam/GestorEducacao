<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="DemonPreAtend.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios.DemonPreAtend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 700px;
            margin: 40px 0 0 250px !important;
        }
        input
        {
            height: 13px;
        }
        label
        {
            margin-bottom: 1px;
        }
        .ulDados li
        {
            margin: 0 0 5px 10px;
        }
        .liUnidade, .liPaciente, .liClassRisco, .liTipoEncam, .liEspec, .liPeriodo
        {
            clear: both;
        }
        
        .ddlUnidade, .ddlPaciente, .ddlClassRisco, .ddlTipoEncam, .ddlEspec
        {
            width: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidCoPreAtend" />
    <ul class="ulDados">
        <li class="liUnidade" style="margin-left: 130px;">
            <label title="Unidade onde foi realizado o direcionamento">
                Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnidade" class="ddlUnidade" ToolTip="Unidade onde foi realizado o direcionamento"
                Width="230px" OnSelectedIndexChanged="ddlUnidade_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li class="liPaciente" style="margin-left: 130px;">
            <label title="Paciente" class="lblObrigatorio">
                Paciente</label>
            <asp:DropDownList ID="ddlPaciente" runat="server" class="ddlPaciente" OnSelectedIndexChanged="ddlPaciente_OnSelectedIndexChanged"
                ToolTip="Selecione um paciente para realizar o demonstrativo" AutoPostBack="true" visible="false"/>
                <asp:TextBox ID="txtPaciente" runat="server" style="margin-bottom: -10px;" ToolTip="Digite o nome do paciente" Visible="true"/>
                <asp:ImageButton ID="imgBtnPesqPaciente" runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" OnClick="imgBtnPesqPaciente_OnClick" Visible="true" />
                <asp:ImageButton ID="imgBtnDesfPaciente" runat="server" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico" OnClick="imgBtnDesfPaciente_OnClick" Visible="false" />
            <asp:RequiredFieldValidator ID="rfvPaciente" runat="server" ControlToValidate="ddlPaciente"
                Visible="false" ErrorMessage="Campo Paciente deve ser selecionado" />
        </li>
        <li class="liClassRisco" style="margin-left: 130px;">
            <label title="Classificação de risco">
                Clas. Risco</label>
            <asp:DropDownList ID="ddlClassRisco" runat="server" class="ddlClassRisco" ToolTip="Selecione uma classificação de risco para realizar o demonstrativo" />
        </li>
        <li class="liTipoEncam" style="margin-left: 130px;">
            <label title="Tipo de encaminhamento">
                Tipo Encam.</label>
            <asp:DropDownList ID="ddlTipoEncam" runat="server" class="ddlTipoEncam" ToolTip="Selecione um tipo de encaminhamento para realizar o demonstrativo">
                <asp:ListItem Text="Todos" Value="T" />
                <asp:ListItem Text="Encaminhamento para recepção" Value="P" />
                <asp:ListItem Text="Encaminhamento para atendimento" Value="A" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvTipoEncam" runat="server" ControlToValidate="ddlTipoEncam"
                Visible="false" ErrorMessage="Campo Tipo de Encaminhamento deve ser selecionado" />
        </li>
        <li class="liEspec" style="margin-left: 130px;">
            <label title="Especialidade">
                Especialidade</label>
            <asp:DropDownList ID="ddlEspec" runat="server" class="ddlEspec" ToolTip="Selecione uma especialidade para realizar o demonstrativo" />
            <asp:RequiredFieldValidator ID="rfvEspec" runat="server" ControlToValidate="ddlEspec"
                Visible="false" ErrorMessage="Campo Especialidade deve ser selecionado" />
        </li>
        <li class="liPeriodo" style="margin-left: 130px;">
            <label class="lblObrigatorio">
                Período</label>
            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
