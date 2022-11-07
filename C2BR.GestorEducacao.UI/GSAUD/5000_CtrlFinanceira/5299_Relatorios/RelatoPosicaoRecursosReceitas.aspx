<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="RelatoPosicaoRecursosReceitas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD.F5000_CtrlFinanceira.F5299_Relatorios.RelatoPosicaoRecursosReceitas"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 320px;
            margin: 20px 0 0 370px;
        }
        .liUnidade, .liNumDoc
        {
            margin-top: 5px;
            width: 275px;
        }
        .liAnoRefer, .liTurma
        {
            margin-top: 5px;
        }
        .liStaDocumento 
        { 
            margin-top: 5px;
            clear: both;
        }
        .liModalidade
        {
            width: 140px;
            margin-top: 5px;
            clear: both;
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
            width: 165px;
            height: 27px;
        }
        .lblDivData,
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
        .liTipoPessoa
        {
            clear:both;
            margin-top: 5px;
            width: 120px;
        }
        .ddlTipoPessoa
        {
            width: 120px;
        }
        .liAluno
        {
            margin-top: 5px;
        }        
        .ddlStaDocumento
        {
            width: 120px;
        }
        .txtNumDoc
        {
            width: 80px;
        }
        .ddlAgrupador
        {
            width: 200px;
        }
        .chkLocais label { display: inline !important; margin-left:-4px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liUnidade" style="margin: 5px 0 0 0; width: 265px;">
                    <label id="Label1" title="Unidade/Escola" class="lblObrigatorio" runat="server">
                        Unidade</label>
                    <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade/Escola" CssClass="ddlUnidadeEscolar"
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liUnidContrato" style="margin: 5px 0 0 0; width: 265px;">
                    <label id="Label3" class="lblObrigatorio" runat="server" title="Unidade de Contrato">
                        Unidade de Contrato</label>
                    <asp:DropDownList ID="ddlUnidContrato" ToolTip="Selecione a Unidade de Contrato"
                        CssClass="ddlUnidContrato" runat="server">
                    </asp:DropDownList>
                </li>
                <li class="liPesqPor">
                    <label id="Label2" class="lblObrigatorio" runat="server" title="Pesquisar Por">
                        Pesquisar Por</label>
                    <asp:DropDownList Style="width: 115px" ID="ddlPesqPor" ToolTip="Selecione o Tipo da Pesquisa"
                        CssClass="ddlPesqPor" runat="server" OnSelectedIndexChanged="ddlPesqPor_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Value="M">Data Movimentação</asp:ListItem>
                        <asp:ListItem Value="V">Data Vencimento</asp:ListItem>
                        <asp:ListItem Value="P">Data Pagamento</asp:ListItem>
                        <asp:ListItem Value="R">Responsável</asp:ListItem>
                    </asp:DropDownList>
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
                <li class="liAluno" runat="server" id="liResponsavel">
                    <label title="Selecione o Responsável do aluno">
                        Responsável</label>
                    <asp:DropDownList ID="ddlResponsavel" ToolTip="Selecione o Responsável do aluno"
                        CssClass="ddlNomePessoa" runat="server" OnSelectedIndexChanged="ddlResponsavel_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li class="liTipoPessoa">
                    <label id="Label4" class="lblObrigatorio" runat="server" title="Tipo">
                        Tipo</label>
                    <asp:DropDownList ID="ddlTipoPessoa" ToolTip="Selecione o Tipo" CssClass="ddlTipoPessoa"
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoPessoa_SelectedIndexChanged">
                        <asp:ListItem Value="A" Selected="true">Paciente</asp:ListItem>
                        <asp:ListItem Value="C">Cliente</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="liNumDoc">
                    <label class="label" title="Nº do Documento">
                        Nº do Documento</label>
                    <asp:TextBox ID="txtNumDoc" ToolTip="Informe o Nº do Documento" CssClass="txtNumDoc"
                        runat="server"></asp:TextBox>
                </li>
                <li class="liAluno" runat="server" id="liCliente" visible="false">
                    <label title="Cliente">
                        Cliente</label>
                    <asp:DropDownList ID="ddlClientes" ToolTip="Selecione o Cliente" CssClass="ddlNomePessoa"
                        runat="server">
                    </asp:DropDownList>
                </li>
                <li class="liAluno" runat="server" id="liAluno">
                    <label title="Paciente">
                        Paciente</label>
                    <asp:DropDownList ID="ddlPacientes" ToolTip="Selecione o Paciente" CssClass="ddlNomePessoa"
                        runat="server">
                    </asp:DropDownList>
                </li>
                <li class="liStaDocumento">
                    <label title="Documento(s)">
                        Documento(s)</label>
                    <asp:DropDownList ID="ddlStaDocumento" ToolTip="Selecione o Status do Documento"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlStaDocumento_SelectedIndexChanged"
                        CssClass="ddlStaDocumento" runat="server">
                        <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
                        <asp:ListItem Value="A">Em aberto(s)</asp:ListItem>
                        <asp:ListItem Value="Q">Quitado(s)</asp:ListItem>
                        <asp:ListItem Value="C">Cancelado(s)</asp:ListItem>
                        <asp:ListItem Value="P">Parcialmente Quitado(s)</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CheckBox CssClass="chkLocais" ID="chkIncluiCancel" TextAlign="Right" runat="server"
                        Text="Incluir Cancelados" />
                </li>
                <li style="clear: both; margin-top: 5px;">
                    <label for="ddlAgrupador" title="Agrupador de Receita">
                        Agrupador de Receita</label>
                    <asp:DropDownList ID="ddlAgrupador" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Agrupador de Receita" />
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
