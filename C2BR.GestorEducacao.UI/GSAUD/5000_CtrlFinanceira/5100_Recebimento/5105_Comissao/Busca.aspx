<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5100_Recebimento._5105_Comissao.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label>Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnidade" Width="220px" ToolTip="Selecine a unidade desejada" />
        </li>
        <li style="clear: both;">
            <label>Profissional</label>
            <asp:DropDownList runat="server" ID="ddlProfissional" Width="220px" ToolTip="Selecione o profissional" />
        </li>
        <li style="clear: both">
            <label title="Pesquise pelo Grupo de Procedimentos Médicos">
                Grupo</label>
            <asp:DropDownList runat="server" ID="ddlGrupo" Width="160px" ToolTip="Pesquise pelo Grupo de Procedimentos Médicos"
                OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged" AutoPostBack="true" />
        </li>
        <li style="clear: both;">
            <label title="Pesquise pelo SubGrupo de Procedimentos Médicos">
                SubGrupo</label>
            <asp:DropDownList runat="server" ID="ddlSubGrupo" Width="160px" ToolTip="Pesquise pelo SubGrupo de Procedimentos Médicos"
                OnSelectedIndexChanged="ddlSubGrupo_OnSelectedIndexChanged" AutoPostBack="true" />
        </li>
        <li style="clear: both;">
            <label title="Pesquise pelo Procedimento Médico">
                Procedimento</label>
            <asp:DropDownList runat="server" ID="ddlProcedimento" Width="220px" ToolTip="Pesquise pelo Procedimento Médico">
            </asp:DropDownList>
        </li>
        <li style="clear: both;">
            <label title="Tipo da Ocorrência">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" Width="100px" ToolTip="Informe o tipo da comissão" runat="server" />
        </li>
        <li style="clear: both">
            <label title="Situação cadastral do Procedimento Comissionado">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="80px" ToolTip="Situação cadastral do Procedimento Comissionado">
                <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
