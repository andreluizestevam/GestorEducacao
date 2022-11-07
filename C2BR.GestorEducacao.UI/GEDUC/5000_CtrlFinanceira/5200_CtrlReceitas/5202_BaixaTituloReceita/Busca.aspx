<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5202_BaixaTituloReceita.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS LIs */
        #liPeriodoAte { clear: none !important; display:inline;margin-top:13px;margin-left:0px;}
        #liAux { margin-left: -10px; margin-right: 5px; clear:none !important; display:inline;}
        .liEspaco { clear: none !important; margin-left: 15px !important; }

        /*--> CSS DADOS */
        .labelAux { margin-top: 16px; }
        .centro { text-align: center; }
        .direita { text-align: right; }
        .ddlTipoFonte { width: 70px; }
        .ddlNomeFonte { width: 270px; }
        
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
        <label for="ddlTipoPeriodo" title="Data de Pesquisa">Data de Pesquisa</label>
        <asp:DropDownList ID="ddlTipoPeriodo" CssClass="ddlTipoPeriodo" runat="server" ToolTip="Selecione a Data de Pesquisa">
            <asp:ListItem Value="E">Emissão</asp:ListItem>
            <asp:ListItem Value="C">Cadastro</asp:ListItem>
            <asp:ListItem Value="V">Vencimento</asp:ListItem>
        </asp:DropDownList>
    </li>
    <li class="liEspaco">
        <label for="txtPeriodoDe" title="Intervalo de Pesquisa">Intervalo de Pesquisa</label>
        <asp:TextBox ID="txtPeriodoDe" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial"></asp:TextBox>
    </li>
    <li id="liAux">
        <label class="labelAux">até</label>
    </li>
    <li id="liPeriodoAte">
        <asp:TextBox ID="txtPeriodoAte" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
    </li>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
    <ContentTemplate>
    <li>
        <label for="ddlTipoFonte" title="Tipo da Fonte de Receita">TFR</label>
        <asp:DropDownList ID="ddlTipoFonte" CssClass="ddlTipoFonte" runat="server" ToolTip="Selecione o Tipo da Fonte" 
            AutoPostBack="true" OnSelectedIndexChanged="ddlTipoFonte_SelectedIndexChanged">
            <asp:ListItem Value="A">Aluno</asp:ListItem>
            <asp:ListItem Value="O">Não Aluno</asp:ListItem>
        </asp:DropDownList>
    </li>
    <li class="liEspaco">
        <label for="txtCodigoFonte" title="Código da Fonte">Código</label>
        <asp:TextBox ID="txtCodigoFonte" CssClass="txtCodigoFonte campoNumerico" runat="server" Enabled="false"></asp:TextBox>
    </li>
    <li>
        <label for="ddlNomeFonte" title="Nome da Fonte">Nome</label>
        <asp:DropDownList ID="ddlNomeFonte" CssClass="ddlNomeFonte" runat="server" ToolTip="Selecione o Nome da Fonte" 
            OnSelectedIndexChanged="ddlNomeFonte_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    </li>
    </ContentTemplate>
    </asp:UpdatePanel>
    <li>
        <label for="ddlSituacao" title="Situação">Situação</label>
        <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server" ToolTip="Selecione a Situação">
            <asp:ListItem Value="">Todos</asp:ListItem>
            <asp:ListItem Value="A">Em Aberto</asp:ListItem>
            <asp:ListItem Value="P">Parcialmente Quitado</asp:ListItem>
            <asp:ListItem Value="R">Pré-Matrícula</asp:ListItem>
        </asp:DropDownList>
    </li>
</ul>
</asp:Content>