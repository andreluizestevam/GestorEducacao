<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="Comissionamento.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5299_Relatorios.Comissionamento"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
        }
        .liUnidade
        {
            margin-top: 5px;
            width: 300px;
        }
        .liStaDocumento
        {
            margin-top: 10px;
            width: 300px;
        }
        .liUnidContrato
        {
            margin-top: 5px;
            width: 300px;
        }
        .ddlUnidContrato
        {
            width: 226px;
        }
        .liAnoRefer
        {
            margin-top: 5px;
        }
        .liModalidade
        {
            clear: both;
            width: 140px;
            margin-top: 5px;
        }
        .liSerie
        {
            clear: both;
            margin-top: 5px;
        }
        .liTurma
        {
            margin-left: 5px;
            margin-top: 5px;
        }
        .ddlStaDocumento
        {
            width: 85px;
        }
        .ddlAgrupador
        {
            width: 200px;
        }
        .ddlAluno
        {
            width: 250px;
        }
        
        .ddlStaDocumento
        {
            width: 85px;
        }
        .ddlAgrupador
        {
            width: 200px;
        }
        .chkLocais
        {
            margin-left: 5px;
        }
        .chkLocais label
        {
            display: inline !important;
            margin-left: -4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
