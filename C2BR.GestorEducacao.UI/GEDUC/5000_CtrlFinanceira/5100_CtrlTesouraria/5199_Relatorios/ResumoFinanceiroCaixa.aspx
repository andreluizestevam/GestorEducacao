<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="ResumoFinanceiroCaixa.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5199_Relatorios.ResumoFinanceiroCaixa"
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
        .liPeriodo, .liNumDoc, .liVencimento
        {
            margin-top: 10px;
            width: 300px;
        }
        .liStaDocumento
        {
        }
        .liOrigem
        {
            margin-top: 10px;
            clear: both;
        }
        .liAluno
        {
            clear: both;
            margin-top: -5px;
        }
        .ddlStaDocumento
        {
            width: 85px;
        }
        .lblDivData
        {
            display: inline;
            margin: 0 5px;
            margin-top: 0px;
        }
        .txtNumDoc
        {
            width: 80px;
        }
        .ddlAgrupador
        {
            width: 200px;
        }
        .ddlFornecedor
        {
            width: 270px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liUnidade">
            <label id="Label3" runat="server" title="Unidade de Contrato">
                Unidade de Contrato</label>
            <asp:DropDownList ID="ddlUnidContrato" CssClass="ddlUnidadeEscolar" runat="server"
                ToolTip="Selecione a Unidade de Contrato">
            </asp:DropDownList>
        </li>
        <li class="liUnidade">
            <label id="Label4" runat="server" title="Caixa">
                Caixa</label>
            <asp:DropDownList ID="ddlCaixa" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione o Caixa">
            </asp:DropDownList>
        </li>
        <li class="liVencimento">
            <label for="txtPeriodo" class="lblObrigatorio" title="Período">
                Período</label>
            <asp:TextBox ID="txtDtInicio" ToolTip="Informe a Data Inicial do Periodo" CssClass="campoData"
                runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
                ControlToValidate="txtDtInicio" Text="*" ErrorMessage="Campo Data Período Início é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:Label ID="Label2" CssClass="lblDivData" runat="server"> à </asp:Label>
            <asp:TextBox ID="txtDtFim" CssClass="campoData" runat="server" ToolTip="Informe a Data Final do Periodo"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="validatorField"
                ForeColor="Red" ControlToValidate="txtDtFim" ControlToCompare="txtDtInicio" Type="Date"
                Operator="GreaterThanEqual" ErrorMessage="Data Final não pode ser menor que Data Inicial.">
            </asp:CompareValidator>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
                ControlToValidate="txtDtFim" Text="*" ErrorMessage="Campo Data Período Fim é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liStaDocumento">
            <label title="Documento(s)">
                Situação do Caixa</label>
            <asp:DropDownList ID="ddlSituCaixa" ToolTip="Selecione a Situação do Caixa" CssClass="ddlStaDocumento"
                runat="server">
                <asp:ListItem Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="A">Aberto</asp:ListItem>
                <asp:ListItem Value="F">Fechado</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
