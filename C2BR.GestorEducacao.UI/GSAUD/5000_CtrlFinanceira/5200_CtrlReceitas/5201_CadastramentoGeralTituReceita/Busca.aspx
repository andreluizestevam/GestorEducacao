<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5201_CadastramentoGeralTituReceita.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS LIs */
        .liPeriodoAte { clear: none !important; display:inline; margin-left: 0px; margin-top:13px;} 
        .liAux { margin-left: -10px !important; margin-right: 5px; clear:none !important; display:inline;}
        .liEspaco { clear: none !important; margin-left: 15px !important; }

        /*--> CSS DADOS */
        .labelAux { margin-top: 16px; }
        .centro { text-align: center;}
        .direita { text-align: right;}
        .ddlTipoFonte { width: 70px; }
        .ddlNomeFonte { width: 270px; }
        .ddlHistorico { width: 300px; }
        
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
        <label for="ddlTipoPeriodo" title="Data de Pesquisa">Tipo de Pesquisa</label>
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
    <li class="liAux">
        <label class="labelAux">até</label>
    </li>
    <li class="liPeriodoAte">
        <asp:TextBox ID="txtPeriodoAte" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
    </li>
    <li>
        <label for="ddlTipoOrdem" title="Ordem da Gride">Ordem</label>
        <asp:DropDownList ID="ddlTipoOrdem" CssClass="ddlTipoPeriodo" runat="server" ToolTip="Selecione a Ordem da Gride">
            <asp:ListItem Value="C">Crescente</asp:ListItem>
            <asp:ListItem Value="D">Decrescente</asp:ListItem>
        </asp:DropDownList>
    </li>
    <li class="liHistorico">
        <label for="ddlHistorico" title="Histórico">Histórico</label>
        <asp:DropDownList ID="ddlHistorico" CssClass="ddlHistorico" runat="server" ToolTip="Selecione o Histórico"></asp:DropDownList>
    </li>
    <li>
        <label for="ddlSituacao" title="Situação">Situação</label>
        <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server" ToolTip="Selecione a Situação">
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlNomeFonte" title="Nome da Fonte">Aluno</label>
        <asp:TextBox ID="txtNomePacPesq" Width="270px" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" />
    </li>
    <li>
        <label for="ddlUnidadeContrato" title="Unidade de Contrato">Unidade de Contrato</label>
        <asp:DropDownList ID="ddlUnidadeContrato" runat="server" CssClass="campoUnidadeEscolar"
            ToolTip="Selecione a Unidade de Contrato">
        </asp:DropDownList>
    </li>
</ul>
</asp:Content>