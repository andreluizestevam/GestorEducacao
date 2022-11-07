<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="ExtratFreqColaborador.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1220_CtrlFrequenciaFuncional.F1229_Relatorios.ExtratFreqColaborador"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
        }
        .liPeriodo, .liFuncionarios, .liUnidade, .liTipoCol
        {
            margin-top: 5px;
            width: 300px;
        }
        .liFuncionarios
        {
            clear: both;
        }
        .lblDivData
        {
            display: inline;
            margin: 0 5px;
            margin-top: 0px;
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
                    <label class="lblObrigatorio" for="txtUnidade">
                        Unidade/Escola</label>
                    <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged" ToolTip="Selecione a Unidade/Escola">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liTipoCol">
                    <label class="lblObrigatorio" for="ddlTipoColaborador">
                        Tipo do Colaborador</label>
                    <asp:DropDownList ID="ddlTipoColaborador" CssClass="ddlTipoCol" runat="server" ToolTip="Selecione o Tipo do Colaborador"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlTipoColaborador_SelectedIndexChanged"
                        Width="120px">
                        <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
                        <asp:ListItem Value="N">Funcionários</asp:ListItem>
                        <asp:ListItem Value="S">Professor</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="liTipoCol">
                    <label for="ddlSituacaoColab" class="lblObrigatorio" title="Situação Atual">
                        Situação Atual</label>
                    <asp:DropDownList ID="ddlSituacaoColab" ToolTip="Selecione a Situação Atual do Funcionário" OnSelectedIndexChanged="ddlSituacaoColab_OnSelectedIndexChanged"
                    AutoPostBack="true" CssClass="ddlSituacaoColab" Width="110px" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvSituacao" runat="server" ControlToValidate="ddlSituacaoColab"
                        ErrorMessage="Situação deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
                </li>
                <li class="liFuncionarios">
                    <label class="lblObrigatorio" for="txtFuncionarios">
                        Colaboradores</label>
                    <asp:DropDownList ID="ddlFuncionarios" CssClass="ddlNomePessoa" runat="server" ToolTip="Selecione o Colaborador">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlFuncionarios" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlFuncionarios" Text="*" ErrorMessage="Campo Colaborador é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liPeriodo">
            <label class="lblObrigatorio" for="txtPeriodo">
                Período</label>
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" runat="server" ToolTip="Data Início"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
                ControlToValidate="txtDataPeriodoIni" Text="*" ErrorMessage="Campo Data Período Início é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            <asp:TextBox ID="txtDataPeriodoFim" CssClass="campoData" runat="server" ToolTip="Data Fim"></asp:TextBox>
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
