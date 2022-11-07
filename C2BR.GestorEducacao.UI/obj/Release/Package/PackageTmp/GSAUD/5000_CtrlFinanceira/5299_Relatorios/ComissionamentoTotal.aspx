<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="ComissionamentoTotal.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5299_Relatorios.ComissionamentoTotal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 250px;
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
                <li style="clear: both; margin-top:5px;">
                    <label>Profissional</label>
                    <asp:DropDownList runat="server" ID="ddlProfissional" Width="220px" ToolTip="Selecione o profissional" />
                </li>
                <li style="clear: both; margin-top:5px;">
                    <label title="Pesquise pelo Grupo de Procedimentos Médicos">
                        Grupo</label>
                    <asp:DropDownList runat="server" ID="ddlGrupo" Width="160px" ToolTip="Pesquise pelo Grupo de Procedimentos Médicos"
                        OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged" AutoPostBack="true" />
                </li>
                <li style="clear: both; margin-top:5px;">
                    <label title="Pesquise pelo SubGrupo de Procedimentos Médicos">
                        SubGrupo</label>
                    <asp:DropDownList runat="server" ID="ddlSubGrupo" Width="160px" ToolTip="Pesquise pelo SubGrupo de Procedimentos Médicos"
                        OnSelectedIndexChanged="ddlSubGrupo_OnSelectedIndexChanged" AutoPostBack="true" />
                </li>
                <li style="clear: both; margin-top:5px;">
                    <label title="Pesquise pelo Procedimento Médico">
                        Procedimento</label>
                    <asp:DropDownList runat="server" ID="ddlProcedimento" Width="220px" ToolTip="Pesquise pelo Procedimento Médico">
                    </asp:DropDownList>
                </li>
                <li style="clear: both; margin-top:5px;">
                    <label title="Tipo da Ocorrência">
                        Tipo</label>
                    <asp:DropDownList ID="ddlTipo" Width="100px" ToolTip="Informe o tipo da comissão" runat="server" />
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
