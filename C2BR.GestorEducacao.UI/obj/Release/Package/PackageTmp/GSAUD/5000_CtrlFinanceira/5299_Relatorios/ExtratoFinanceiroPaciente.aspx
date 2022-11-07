<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="ExtratoFinanceiroPaciente.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD.F5000_CtrlFinanceira.F5299_Relatorios.ExtratoFinanceiroPaciente"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 500px !important;
            margin: 20px 0 0 382px;
        }
        .liUnidade, .liNumDoc
        {
            margin-top: 5px;
            width: 275px;
        }
        .liAnoRefer, .liTurma, .liStaDocumento
        {
            margin-top: 5px;
        }
        
        .liModalidade
        {
            width: 140px;
            margin-top: 5px;
        }
        .liSerie
        {
            margin-top: 5px;
            margin-left: 5px;
        }
        .liUnidContrato
        {
            margin-top: 5px;
            width: 300px;
        }
        .ddlUnidContrato
        {
            width: 237px;
        }
        .liPesqPor
        {
            margin-top: 5px;
            width: 115px;
            display: inline-block;
        }
        .liPeriodo
        {
            margin-top: 5px;
        }
        .lblDivData
        {
            margin: 0 5px;
            margin-top: 0px;
            height: 27px;
        }
        .liNumDoc
        {
            display: inline-block;
            margin-top: 5px;
            width: 100px;
            height: 27px;
        }
        .ddlPaciente
        {
            width: 120px;
        }
        .liAluno
        {
            margin-top: 5px;
        }
        .ddlProficional
        {
            width: 200px;
        }
        .ddlRap
        {
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liUnidContrato" style="margin: 5px 0 0 0; width: 265px;">
                    <label id="Label3" class="lblObrigatorio" runat="server" title="Unidade de Contrato">
                        Unidade</label>
                    <asp:DropDownList ID="ddlUnidContrato" ToolTip="Selecione a Unidade de Contrato"
                        CssClass="ddlUnidContrato" runat="server" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvUnidade" runat="server" ControlToValidate="ddlUnidContrato"
                        ErrorMessage="Informe a unidade de contrato">*</asp:RequiredFieldValidator>
                </li>
                <li class="liAluno" style="clear: both">
                    <label title="Paciente" class="lblObrigatorio">
                        Paciente</label>
                    <asp:DropDownList ID="ddlPaciente" ToolTip="Selecione o paciente" CssClass="ddlPaciente"
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaciente_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvPac" runat="server" ControlToValidate="ddlPaciente"
                        ErrorMessage="Selecione o paciente">*</asp:RequiredFieldValidator>
                </li>
                <li class="liAluno" style="margin-left: 5px;">
                    <label title="Registro de Agendamento Paciente">
                        RAP</label>
                    <asp:DropDownList ID="ddlRap" ToolTip="Selecione o RAP" CssClass="ddlRap" runat="server">
                    </asp:DropDownList>
                </li>
                <li class="liAluno" style="clear: both;">
                    <label title="Local do agendamento">
                        Local</label>
                    <asp:DropDownList ID="ddlLocal" ToolTip="Selecione o local do agendamento" CssClass="ddlOrigem"
                        runat="server">
                    </asp:DropDownList>
                </li>
                <li class="liAluno" style="clear: both;">
                    <label title="Origem Procedimento">
                        Origem</label>
                    <asp:DropDownList ID="ddlOrigem" ToolTip="Selecione a Origem" CssClass="ddlOrigem"
                        runat="server">
                    </asp:DropDownList>
                </li>
                <li style="clear: both; margin-top: 5px;">
                    <label for="ddlProfissional" title="Profissional">
                        Profissional</label>
                    <asp:DropDownList ID="ddlProfissional" CssClass="ddlProfissional" runat="server"
                        ToolTip="Selecione o Profissional" />
                </li>
                <li style="clear: both; margin-top: 5px;">
                    <label for="ddlOperadora" title="Operadora Plano de Saúde">
                        Operadora</label>
                    <asp:DropDownList ID="ddlOperadora" CssClass="ddlOperadora" runat="server" ToolTip="Selecione a Operadora" />
                </li>
                <li style="clear: both; margin-top: 10px;">
                    <asp:CheckBox Style="margin-left: -6px;" ID="chkPorLocal" runat="server" />
                    <label for="lblPorLocal" style="display: inline-block; margin-left: -5px;" title="Emitir relatório por Local">
                        Relatório por Local</label>
                </li>
                <li style="clear: both; margin-top: 5px;">
                    <label for="ddlSituacao" title="Situação do Agendamento">
                        Situação</label>
                    <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server" ToolTip="Selecione a Situação" />
                </li>
                <li class="liPeriodo" style="clear: both">
                    <label class="lblObrigatorio" for="txtPeriodo" title="Período Cadastro Procedimento">
                        Período</label>
                    <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" ToolTip="Informe a Data Inicial"
                        runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDataPeriodoIni"
                        ErrorMessage=" Informe a data inicial">*</asp:RequiredFieldValidator>
                    <asp:Label ID="Label1" CssClass="lblDivData" runat="server"> à </asp:Label>
                    <asp:TextBox ID="txtDataPeriodoFim" ToolTip="Informe a Data Final" CssClass="campoData"
                        runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="cvFim" runat="server" ControlToCompare="txtDataPeriodoIni"
                        ControlToValidate="txtDataPeriodoFim" ErrorMessage="Data final deve ser maior que inicial"
                        Operator="GreaterThanEqual" Type="Date">*</asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDataPeriodoFim"
                        ErrorMessage="Informe a data final">*</asp:RequiredFieldValidator>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $("input.campoData").datepicker();
            $(".campoData").mask("99/99/9999");
        });

        $(document).ready(function () {
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
