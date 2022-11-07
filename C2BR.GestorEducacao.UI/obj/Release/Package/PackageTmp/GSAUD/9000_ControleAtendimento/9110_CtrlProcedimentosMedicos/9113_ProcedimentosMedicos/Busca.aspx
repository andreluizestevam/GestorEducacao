<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9113_ProcedimentosMedicos.Busca" %>

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
        <li>
            <label title="Pesquise pelo Nome do Procedimento Médico">
                Nome Procedimento</label>
            <asp:TextBox runat="server" ID="txtNoProcedimento" Width="188px" MaxLength="100"
                ToolTip="Pesquise pelo Nome do Procedimento Médico"></asp:TextBox>
            <asp:ImageButton ID="imgbPesqPacNome" CssClass="btnProcurar" ValidationGroup="pesqPac"
                runat="server" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png" OnClick="imgbPesqProcedimento_OnClick" />
            <asp:DropDownList runat="server" ID="ddlNoProcedimento"
                Visible="false" AutoPostBack="true" Width="188px" ToolTip="Nome do produto a ser movimentado">
            </asp:DropDownList>
            <asp:ImageButton ID="imgbVoltarPesq" ValidationGroup="pesqPac" CssClass="btnProcurar"
                Width="16px" Height="16px" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico" OnClick="imgbVoltarPesq_OnClick"
                Visible="false" runat="server" />
        </li>
        <li style="clear: both">
            <label title="Pesquise pelo Código do Procedimento Médico">
                Código Proc</label>
            <asp:TextBox runat="server" ID="txtCodProcMedic" ToolTip="Pesquise pelo Código do Procedimento Médico"
                Width="50px" CssClass="campoCodigo"></asp:TextBox>
        </li>
        <li>
            <label title="Pesquise pelo Tipo do Procedimento Médico">
                Tipo</label>
            <asp:DropDownList runat="server" ID="ddlTipoProcedimento" Width="140px" ToolTip="Pesquise pelo Tipo do Procedimento Médico">
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
            <asp:DropDownList runat="server" ID="ddlSubGrupo" Width="160px" ToolTip="Pesquise pelo SubGrupo de Procedimentos Médicos">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Classificação" class="">
                Classificações
            </label>
            <asp:ListBox runat="server" SelectionMode="Multiple" ID="lstClassificacao" Height="60px"
                Width="130px" ToolTip="Lista das Classificação"></asp:ListBox>
        </li>
        <li style="clear: both">
            <label title="Pesquise pelo Operadora de Planos de Saúde">
                Contratação</label>
            <asp:DropDownList runat="server" ID="ddlOper" Width="160px" ToolTip="Pesquise pelo Operadora de Planos de Saúde">
            </asp:DropDownList>
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
        <li style="">
            <label title="Protocolo de ações do procedimento">
                Protocolo</label>
            <asp:DropDownList runat="server" ID="ddlProtocoloAcoes" Width="80px" ToolTip="Protocolo de ações do procedimento">
                <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                <asp:ListItem Value="1" Text="Sim"></asp:ListItem>
                <asp:ListItem Value="2" Text="Não"></asp:ListItem>
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
