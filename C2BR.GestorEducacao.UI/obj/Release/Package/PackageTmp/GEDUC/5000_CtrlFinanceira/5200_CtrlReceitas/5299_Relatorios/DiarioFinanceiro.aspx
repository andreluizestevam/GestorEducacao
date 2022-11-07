<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="DiarioFinanceiro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5299_Relatorios.DiarioFinanceiro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 500px !important;
            margin:20px 0 0 330px;
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
            width: 226px;
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
        .ddlTipoDoctos
        {
            width: 120px;
        }
        .liAluno
        {
            margin-top: 5px;
        }
        .ddlAgrupador
        {
            width: 200px;
        }
        .ddlOrigPagto
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
                        Unidade de Contrato</label>
                    <asp:DropDownList ID="ddlUnidContrato" ToolTip="Selecione a Unidade de Contrato"
                        CssClass="ddlUnidContrato" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidContrato_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvUnidade" runat="server" ControlToValidate="ddlUnidContrato"
                        ErrorMessage="Informe a unidade de contrato">*</asp:RequiredFieldValidator>
                </li>
                <li runat="server" id="liItensEscola" style="margin-bottom:-10px;">
                    <ul>
                        <li class="liModalidade" runat="server" id="liModalidade">
                            <label title="Modalidade" for="ddlModalidade" class="lblObrigatorio">
                                Modalidade</label>
                            <asp:DropDownList ID="ddlModalidade" runat="server" AutoPostBack="true" CssClass="ddlModalidade"
                                OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged" ToolTip="Selecione a Modalidade">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvModalidade" runat="server" ControlToValidate="ddlModalidade"
                                ErrorMessage="Informe a modalidade">*</asp:RequiredFieldValidator>
                        </li>
                        <li class="liSerie" style="margin: 5px 0 0 0;" runat="server" id="liSerie">
                            <label title="Curso" for="ddlSerieCurso" class="lblObrigatorio">
                                Curso</label>
                            <asp:DropDownList ID="ddlSerieCurso" ToolTip="Selecione a Curso" CssClass="ddlSerieCurso"
                                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged" Width="120px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCurso" runat="server" ControlToValidate="ddlSerieCurso"
                                ErrorMessage="Informe o curso">*</asp:RequiredFieldValidator>
                        </li>
                        <li class="liTurma" style="margin-left:-3px" runat="server" id="liTurma">
                            <label id="lblTurma" title="Turma" for="ddlTurma" class="lblObrigatorio">
                                Turma</label>
                            <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" ToolTip="Selecione a Turma" runat="server"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvTurma" runat="server" ControlToValidate="ddlTurma"
                                ErrorMessage="Informe a turma">*</asp:RequiredFieldValidator>
                        </li>
                    </ul>
                </li>
                <li class="liAluno" style="clear:both">
                    <label title="Tipo de Documento" class="lblObrigatorio">
                        Tipo de Documento</label>
                    <asp:DropDownList ID="ddlTipoDoctos" ToolTip="Selecione o Tipo de Documento" CssClass="ddlTipoDoctos"
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoDoctos_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvDoc" runat="server" ControlToValidate="ddlTipoDoctos"
                        ErrorMessage="Informe o tipo de documento">*</asp:RequiredFieldValidator>
                </li>
                <li class="liAluno" style="margin-left: 5px;">
                    <label title="Origem de Pagamento" class="lblObrigatorio">
                        Origem de Pagamento</label>
                    <asp:DropDownList ID="ddlOrigPagto" ToolTip="Selecione a Origem de Pagamento" CssClass="ddlOrigPagto"
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrigPagto_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOrigPagto"
                        ErrorMessage="Informe a origem de pagamento">*</asp:RequiredFieldValidator>
                </li>
                <li class="liAluno" style="margin-left: 5px;">
                    <label id="lbDdlTipoPagto" title="Origem de Pagamento" for="ddlTipoPagto" runat="server"
                        class="lblObrigatorio" visible="false">
                        Tipo de Pagamento</label>
                    <asp:DropDownList ID="ddlTipoPagto" ToolTip="Selecione a Tipo de Pagamento" CssClass="ddlOrigPagto"
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoPagto_SelectedIndexChanged"
                        Visible="False">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvPag" runat="server" ControlToValidate="ddlTipoPagto"
                        ErrorMessage="Informe tipo de pagamento">*</asp:RequiredFieldValidator>
                </li>
                <li style="clear: both; margin-top: 5px;">
                    <label for="ddlAgrupador" title="Agrupador de Receita" class="lblObrigatorio">
                        Agrupador de Receita</label>
                    <asp:DropDownList ID="ddlAgrupador" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Agrupador de Receita"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlAgrupador_SelectedIndexChanged" />
                    <asp:RequiredFieldValidator ID="rfvAgrupador" runat="server" ControlToValidate="ddlAgrupador"
                        ErrorMessage="Informe o agrupador">*</asp:RequiredFieldValidator>
                </li>
                <li class="liPeriodo" style="clear:both">
                    <label class="lblObrigatorio" for="txtPeriodo" title="Período">
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
