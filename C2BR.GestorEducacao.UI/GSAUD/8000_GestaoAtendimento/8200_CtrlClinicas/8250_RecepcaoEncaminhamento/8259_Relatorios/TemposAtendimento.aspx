<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="TemposAtendimento.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8250_RecepcaoEncaminhamento._8259_Relatorios.TemposAtendimento" %>

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
        .rblModelo
        {
            list-style: none;
            margin: 0;
            padding: 0;
            margin-top: 10px;
            margin-left: 69px;
        }
        .rblModelo label
        {
            display: inline;
        }
        .radioButtonList.horizontal li
        {
            display: inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados" style="margin-left: 360px">
        <li class="liboth">
            <asp:Label runat="server" ID="lblUnidadeCadastro">Unidade de Cadastro</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="ddlUnidadeCadastro" OnSelectedIndexChanged="dllUnidadeCadastro_SelectedIndexChanged"
                AutoPostBack="true" ToolTip="Selecine a unidade desejada" />
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblUnidadeLotacao">Unidade Lotação/Contrato</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="ddlUnidadeContrato" OnSelectedIndexChanged="ddlUnidadeContrato_SelectedIndexChanged"
                AutoPostBack="true" ToolTip="Selecine a unidade desejada" />
        </li>
        <li class="liboth">
            <label>
                Classificação Funcional</label>
            <asp:DropDownList runat="server" ID="ddlClassFunci" class="ddlPadrao" OnSelectedIndexChanged="ddlClassFunci_SelectedIndexChanged"
                AutoPostBack="true" />
        </li>
        <li class="liboth">
            <label>
                Profissional</label>
            <asp:DropDownList runat="server" ID="ddlProfissional" class="ddlPadrao" />
        </li>
        <li class="liboth">
            <label>
                Local</label>
            <asp:DropDownList runat="server" ID="ddlLocal" class="ddlPadrao" />
        </li>
        <li class="liboth">
            <label class="lblObrigatorio">
                Data</label>
            <asp:TextBox runat="server" class="campoData" ID="txtData" ToolTip="Informe a Data da Agenda"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvData" CssClass="validatorField"
                ErrorMessage="O campo data é requerido" ControlToValidate="txtData"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 48px;">
            <label>
                Modelo</label>
            <asp:DropDownList Width="160px" runat="server" ID="ddlModelo">
                <asp:ListItem Text="Agenda de Horários por Situação" Value="M1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Agenda de Horários por Intervalo" Value="M2"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
