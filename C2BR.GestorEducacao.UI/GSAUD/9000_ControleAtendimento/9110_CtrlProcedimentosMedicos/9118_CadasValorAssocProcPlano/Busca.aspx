<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9118_CadasValorAssocProcPlano.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            margin: 60px 0 0 20px;
        }
        .ulDados li
        {
            margin: 5px 0 0 10px;
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
        input
        {
            height: 13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
<%--        <li>
            <label title="Pesquise pelo Nome do Procedimento Médico">
                Nome Procedimento</label>
            <asp:TextBox runat="server" ID="txtNoProcedimento" Width="260px" MaxLength="100"
                ToolTip="Pesquise pelo Nome do Procedimento Médico"></asp:TextBox>
        </li>--%>
        <li>
            <label title="Pesquise pelo Tipo do Procedimento Médico">
                Tipo</label>
            <asp:DropDownList runat="server" ID="ddlTipoProcedimento" Width="150px" ToolTip="Pesquise pelo Tipo do Procedimento Médico">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Pesquise pelo Operadora de Planos de Saúde">
                Operadora</label>
            <asp:DropDownList runat="server" ID="ddlOper" Width="130px" ToolTip="Pesquise pelo Operadora de Planos de Saúde"
                OnSelectedIndexChanged="ddlOper_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
           <li>
            <label title="Pesquise pelo Planos de Saúde">
                Plano de Saúde</label>
            <asp:DropDownList runat="server" ID="ddlPlano" Width="130px" ToolTip="Pesquise pelo Planos de Saúde">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Pesquise pelo Grupo de Procedimentos Médicos">
                Grupo</label>
            <asp:DropDownList runat="server" ID="ddlGrupo" Width="160px" ToolTip="Pesquise pelo Grupo de Procedimentos Médicos"
                OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Pesquise pelo SubGrupo de Procedimentos Médicos">
                SubGrupo</label>
            <asp:DropDownList runat="server" ID="ddlSubGrupo" Width="160px" ToolTip="Pesquise pelo SubGrupo de Procedimentos Médicos"    OnSelectedIndexChanged="ddlSubGrupo_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Pesquise pelo Código do Procedimento Médico">
                Procedimento Médico</label>
            <asp:DropDownList ID="ddlProcMedic" Width="160px" CssClass="campoCodigo" runat="server">
            <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
            <%-- <asp:TextBox runat="server" ID="txtCodProcMedic" ToolTip="Pesquise pelo Código do Procedimento Médico"
                Width="50px" CssClass="campoCodigo"></asp:TextBox>--%>
        </li>
     
        <li style="clear: both">
            <label title="Pesquise pelo procedimento médico agrupador">
                Agrupador</label>
            <asp:DropDownList runat="server" ID="ddlAgrupador" Width="260px" ToolTip="Pesquise pelo procedimento médico agrupador">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Pesquise por procedimentos que necessitem de autorização ou não">
                Requer Autorização</label>
            <asp:DropDownList runat="server" ID="ddlRequerAutori" Width="70px" ToolTip="Pesquise por procedimentos que necessitem de autorização ou não">
                <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                <asp:ListItem Value="S" Text="Sim"></asp:ListItem>
                <asp:ListItem Value="N" Text="Não"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Situação cadastral do Procedimento Médico">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="80px" ToolTip="Situação cadastral do Procedimento Médico">
                <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        jQuery(function ($) {
            $(".campoCodigo").mask("99999999");
        });
    </script>
</asp:Content>
