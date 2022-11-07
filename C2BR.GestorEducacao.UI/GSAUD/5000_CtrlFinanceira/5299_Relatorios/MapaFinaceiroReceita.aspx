<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="MapaFinaceiroReceita.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5299_Relatorios.MapaFinaceiroReceita" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
            margin:0 0 0 380px;
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
        .chk label
        {
            display:inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liUnidade">
                    <label id="Label1" title="Unidade" class="lblObrigatorio" runat="server">
                        Unidade</label>
                    <asp:DropDownList ID="ddlUnidade" Width="220px" ToolTip="Selecione a Unidade" CssClass="ddlUnidadeEscolar"
                        runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liUnidade" style="margin-bottom:5px;">
                    <label id="Label2" title="Unidade de Contrato" class="lblObrigatorio" runat="server">
                        Unidade de Contrato</label>
                    <asp:DropDownList ID="ddlUnidadeContrato" Width="220px" ToolTip="Selecione a Unidade de Contrato"
                        CssClass="ddlUnidadeEscolar" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlUnidadeContrato" Text="*" ErrorMessage="Campo Unidade de Contrato é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liStaDocumento">
                    <label title="Documento(s)">
                        Documento(s)</label>
                    <asp:DropDownList ID="ddlStaDocumento" ToolTip="Selecione o Status do Documento"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlStaDocumento_SelectedIndexChanged"
                        runat="server" CssClass="ddlStaDocumento">
                    </asp:DropDownList>
                    <asp:CheckBox CssClass="chk" ID="chkIncluiCancel" TextAlign="Right" runat="server"
                        Text="Incluir Cancelados" />
                </li>
                <li class="liPeriodo" style="margin: 5px  0 0 0; height: 30px;">
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
