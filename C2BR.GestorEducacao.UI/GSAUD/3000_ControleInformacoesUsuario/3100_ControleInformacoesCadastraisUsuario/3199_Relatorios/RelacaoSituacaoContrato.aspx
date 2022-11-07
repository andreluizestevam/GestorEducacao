<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="RelacaoSituacaoContrato.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3199_Relatorios.RelacaoSituacaoContrato" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
        }
        .ulDados li
        {
            margin-top: 5px;
            margin-right: 5px;
            height: 30px;
        }
        .lblDivData
        {
            display: inline;
            margin: 0 5px 0 0;
        }
        .liClear
        {
            clear: both;
        }
        .lblCampo 
        {
            margin-bottom: 2px;
        }  
        .liUF
        {
            clear: both;
            margin-top: 5px;
            width: 45px;
        }
        .liCidade
        {
            margin-top: 5px;
            margin-left: 5px;
            width: 220px;
        }
        .liBairro
        {
            margin-top: 5px;
            width: 180px;
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label title="Unidade de Contrato" class="lblCampo" for="txtUnidade">
                Unidade de Contrato</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar"
                runat="server" Width="240px">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label title="Unidade de Contrato" class="lblCampo"  for="txtUnidade">
                Operadora</label>
            <asp:DropDownList ID="ddlOperadora" ToolTip="Selecione uma Operadora" CssClass="ddlUnidadeEscolar"
                runat="server" Width="240px">
            </asp:DropDownList>
        </li>
        <li id="liSituacao" class="liClear" runat="server">
            <label class="lblObrigatorio" class="lblCampo"  title="Situação">
                Situação</label>
            <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione uma Situação" CssClass="ddlSituacao"
                runat="server" Width="110px">
                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                <asp:ListItem Value="F">Agenda Futura</asp:ListItem>
                <asp:ListItem Value="A">Em Atendimento</asp:ListItem>
                <asp:ListItem Value="V">Em Análise</asp:ListItem>
                <asp:ListItem Value="E">Alta (Normal)</asp:ListItem>
                <asp:ListItem Value="D">Alta (Desistência)</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li id="liOrdem" class="liClear"   runat="server">
            <label class="lblObrigatorio" class="lblCampo" title="Ordem de Impressão">
                Ordem de Impressão</label>
            <asp:DropDownList ID="ddlOrdemImpressao" ToolTip="Selecione a Ordem de Impressão"
                CssClass="ddlOrdemImpressao" runat="server" Width="135px">
                <asp:ListItem Value="1" Selected="True">Unidade de Contrato</asp:ListItem>
                <asp:ListItem Value="3">Data Cadastro</asp:ListItem>
                <asp:ListItem Value="4">Nome do Paciente </asp:ListItem>
                <asp:ListItem Value="5">Nome do Responsável</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
