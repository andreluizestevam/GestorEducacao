<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5300_CtrlDespesas.F5302_BaixaTituloDespesaPagto.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS LIs */
        .liPeriodoAte { clear: none !important; display:inline; margin-left: 0px; margin-top:13px;}
        .liAux { margin-left: 5px; margin-right: 5px; clear:none !important; display:inline;}

        /*--> CSS DADOS */
        .labelAux { margin-top: 16px; }
        .centro { text-align: center;}
        .direita { text-align: right;}        
        .ddlNomeFornecedor{ width: 200px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtNumeroDocumento" title="Número do Documento">N° Documento</label>
        <asp:TextBox ID="txtNumeroDocumento" CssClass="txtNumeroDocumento" MaxLength="20" runat="server" ToolTip="Informe o Número do Documento"></asp:TextBox>
    </li>
    <li>
        <label for="ddlTipoPeriodo" title="Tipo de Período">Tipo</label>
        <asp:DropDownList ID="ddlTipoPeriodo" CssClass="ddlTipoPeriodo" runat="server" ToolTip="Selecione a Situação">
            <asp:ListItem Value="E">Emissão</asp:ListItem>
            <asp:ListItem Value="C">Cadastro</asp:ListItem>
            <asp:ListItem Value="V">Vencimento</asp:ListItem>
        </asp:DropDownList>
    </li>
    <li>
        <label for="txtPeriodoDe" title="Data de Vencimento">Período</label>
        <asp:TextBox ID="txtPeriodoDe" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial"></asp:TextBox>
    </li>
    <li class="liAux">
        <label class="labelAux">até</label>
    </li>
    <li class="liPeriodoAte">
        <asp:TextBox ID="txtPeriodoAte" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
    </li>
    <li>
        <label for="ddlSituacao" title="Situação">Situação</label>
        <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server" ToolTip="Selecione a Situação">
            <asp:ListItem Value="">Todos</asp:ListItem>
            <asp:ListItem Value="A">Em Aberto</asp:ListItem>
            <asp:ListItem Value="P">Parcialmente Quitado</asp:ListItem>
        </asp:DropDownList>
    </li>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
    <ContentTemplate>
    <li>
        <label for="txtCodigoFornecedor" title="Código do Fornecedor">Código</label>
        <asp:TextBox ID="txtCodigoFornecedor" CssClass="txtCodigoFornecedor campoNumerico" runat="server" Enabled="false"></asp:TextBox>
    </li>
    <li>
        <label for="ddlNomeFornecedor" title="Nome do Fornecedor">Fornecedor</label>
        <asp:DropDownList ID="ddlNomeFornecedor" CssClass="ddlNomeFornecedor" runat="server" ToolTip="Selecione o Nome do Fornecedor" 
            OnSelectedIndexChanged="ddlNomeFornecedor_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    </li>
    </ContentTemplate>
    </asp:UpdatePanel>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>