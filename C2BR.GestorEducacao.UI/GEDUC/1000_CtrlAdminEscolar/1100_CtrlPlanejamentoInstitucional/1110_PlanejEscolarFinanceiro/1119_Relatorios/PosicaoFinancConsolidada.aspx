<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="PosicaoFinancConsolidada.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._1000_CtrlAdminEscolar._1100_CtrlPlanejamentoInstitucional._1110_PlanejEscolarFinanceiro._1119_Relatorios.PosicaoFinancConsolidada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 250px;
        }
        .ulDados li
        {
            margin-top: 5px;
        }
        .cblSituacao
        {
            display: inline-block;
            border-style: none;
            margin-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li>
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade Contrato</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
                ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTipo" title="Tipo de Conta" class="lblObrigatorio labelPixel">
                Emissão Por:
            </label>
            <asp:DropDownList ID="ddlTipo" ToolTip="Selecione o Tipo de Conta" Width="110px"
                runat="server" AutoPostBack="true">
                <asp:ListItem Value="P">Conta Contábil</asp:ListItem>
                <asp:ListItem Value="C">Cento de Custo</asp:ListItem>
                <asp:ListItem Value="M">Tipo Movimentação</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTipo"
                ErrorMessage="Tipo de Conta deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="cblSituacao" title="Situação" class="lblObrigatorio labelPixel">
                Situação</label>
            <asp:CheckBoxList ID="cblSituacao" CssClass="cblSituacao" AutoPostBack="true" CellPadding="5"
                RepeatColumns="4" CellSpacing="5" RepeatDirection="Vertical" RepeatLayout="Table"
                runat="server" Width="235px" TextAlign="Right">
                <asp:ListItem Value="A">Em aberto</asp:ListItem>
                <asp:ListItem Value="P">Parcialmente</asp:ListItem>
                <asp:ListItem Value="Q">Quitado</asp:ListItem>
                <asp:ListItem Value="C">Cancelado</asp:ListItem>
            </asp:CheckBoxList>
        </li>
        <li class="liPeriodo">
            <label class="lblObrigatorio" for="txtPeriodo" title="Período">
                Período</label>
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" ToolTip="Informe a Data Inicial"
                runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
                ControlToValidate="txtDataPeriodoIni" Text="*" ErrorMessage="Campo Data Período Início é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            <asp:TextBox ID="txtDataPeriodoFim" ToolTip="Informe a Data Final" CssClass="campoData"
                runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red" ControlToValidate="txtDataPeriodoFim" ControlToCompare="txtDataPeriodoIni"
                Type="Date" Operator="GreaterThanEqual" ErrorMessage="Data Final não pode ser menor que Data Inicial.">
            </asp:CompareValidator>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
                ControlToValidate="txtDataPeriodoFim" Text="*" ErrorMessage="Campo Data Período Fim é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
