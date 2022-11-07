<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="RelacaoDocumentos.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5300_CtrlDespesas.F5399_Relatorios.RelacaoDocumentos"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 308px;
            height: 211px;
        }
        .liUnidade
        {
            margin-top: 5px;
            width: 300px;
        }
        .liPesqPor
        {
            margin-top: 5px;
            width: 112px;
        }
        .liHistorico, .liAgrupador
        {
            margin-top: 5px;
            width: 300px;
        }
        .liNumDoc, .liFornecedor
        {
            display:block;
            margin-top: -5px;
            width: 300px;
        }
        .liSituacao
        {
            margin-top: 5px;
            width: 112px; 
        }
        .liPeriodo
        {
            margin-left: 5px;
            margin-top: 5px;
        }
        .ddlFornecedor, .ddlHistorico
        {
           width: 200px;
        }
        .lblDivData
        {
            display: inline;
            margin: 0 5px;
            margin-top: 0px;
        }
        .ddlStaDocumento
        {
            width: 85px;
        }
        .txtNumDoc
        {
            width: 80px;
        }
        .ddlAgrupador
        {
            width: 200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
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
        <li class="liPesqPor">
            <label class="lblObrigatorio" title="Pesquisar Por">
                Pesquisa Por</label>
            <asp:DropDownList ID="ddlPesqPor" CssClass="ddlPesqPor" runat="server" ToolTip="Selecione o tipo da pesquisa">
                <asp:ListItem Value="M">Data de Movimento</asp:ListItem>
                <asp:ListItem Value="V">Data de Vencimento</asp:ListItem>
                <asp:ListItem Value="P">Data de Pagamento</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liPeriodo">
            <label class="lblObrigatorio" for="txtPeriodo" title="Período">
                Período</label>
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
                ControlToValidate="txtDataPeriodoIni" Text="*" ErrorMessage="Campo Data Período Início é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            <asp:TextBox ID="txtDataPeriodoFim" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
        </li>
        <li class="liNumDoc">
            <label class="label" title="Número do Documento">
                Nº do Documento</label>
            <asp:TextBox ID="txtNumDoc" CssClass="txtNumDoc" runat="server" ToolTip="Informe o Número do Documento"></asp:TextBox>
        </li>
        <li class="liFornecedor">
            <label title="Fornecedor">
                Fornecedor</label>
            <asp:DropDownList ID="ddlFornecedor" CssClass="ddlFornecedor" runat="server" ToolTip="Selecione o Fornecedor">
            </asp:DropDownList>
        </li>
        <li class="liHistorico">
            <label title="Historico">
                Histórico</label>
            <asp:DropDownList ID="ddlHistorico" CssClass="ddlHistorico" runat="server" ToolTip="Selecione o Histórico">
            </asp:DropDownList>
        </li>
        <!--li class="liDataVencimento">
            <label for="txtPeriodo" title="Data de Vencimento">
                Data Vencimento</label>
            <asp:TextBox ID="txtDataVencimento" CssClass="campoData" runat="server" ToolTip="Infome a Data de Vencimento"></asp:TextBox>
        </li-->
       
        <li class="liAgrupador" >
            <label for="ddlAgrupador" title="Agrupador de Despesa">
                Agrupador de Despesas</label>
            <asp:DropDownList ID="ddlAgrupador" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Agrupador de Despesa" />
        </li>
         <li class="liSituacao">
            <label title="Situação">
                Situação</label>
            <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server"
                ToolTip="Selecione a Situação">
                <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="A">Em aberto(s)</asp:ListItem>
                <asp:ListItem Value="Q">Quitado(s)</asp:ListItem>
                <asp:ListItem Value="C">Cancelado(s)</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
