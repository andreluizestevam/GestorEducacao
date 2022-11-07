<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ExtratoFuncionalPlantoes.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8140_AtendimentoPlantoes._8149_Relatorios.ExtratoFuncionalPlantoes" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li style="width: 220px;">
            <asp:Label runat="server" ID="lblUnid">Unidade</asp:Label>
            <asp:DropDownList runat="server" ID="ddlUnidade" Width="220px" OnSelectedIndexChanged="ddlUnidade_OnSelectedIndexChanged"
                AutoPostBack="true" ToolTip="Selecione a Unidade Desejada para Pesquisa.">
            </asp:DropDownList>
        </li>
        <li style="width: 180px;">
            <asp:Label runat="server" ID="lblLocal">Local</asp:Label>
            <asp:DropDownList runat="server" ID="ddlDeptLocal" Width="180px" ToolTip="Selecione o Departamento Desejado para Pesquisa.">
            </asp:DropDownList>
        </li>
        <li style="width: 190px;">
            <asp:Label runat="server" ID="lblEspec">Especialidade</asp:Label>
            <asp:DropDownList runat="server" ID="ddlEspec" Width="190px" ToolTip="Selecione a Especialidade Desejada para Pesquisa.">
            </asp:DropDownList>
        </li>
        <li style="width: 210px;">
            <asp:Label runat="server" ID="lblMedico">Médico</asp:Label>
            <asp:DropDownList runat="server" ID="ddlMedico" Width="210px" ToolTip="Selecione o Médico Desejado para Pesquisa.">
            </asp:DropDownList>
        </li>
        <li class="liboth" style="width: 400px; margin-bottom: -7px;">
            <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio">Período</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator>
        </li>
        <li style="width: 100px;">
            <asp:Label runat="server" ID="lblstatus">Status</asp:Label>
            <asp:DropDownList runat="server" ID="ddlStatus" ToolTip="Selecione o Status Desejado para Pesquisa."
                Width="100px">
                <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                <asp:ListItem Text="Confirmado" Value="S"></asp:ListItem>
                <asp:ListItem Text="Não Confirmado" Value="N"></asp:ListItem>
                <asp:ListItem Text="Indisponível" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="width: 80px;">
            <asp:Label runat="server" ID="lblSitu">Situação</asp:Label>
            <asp:DropDownList runat="server" ID="ddlSituacao" ToolTip="Selecione a Situação Desejada para Pesquisa.">
                <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                <asp:ListItem Text="Em Aberto" Value="A"></asp:ListItem>
                <asp:ListItem Text="Cancelado" Value="C"></asp:ListItem>
                <asp:ListItem Text="Realizado" Value="R"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
