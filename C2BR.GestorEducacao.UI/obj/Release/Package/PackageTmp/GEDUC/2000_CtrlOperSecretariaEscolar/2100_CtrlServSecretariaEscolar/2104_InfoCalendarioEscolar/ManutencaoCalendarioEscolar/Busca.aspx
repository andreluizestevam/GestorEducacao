<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2104_InfoCalendarioEscolar.ManutencaoCalendarioEscolar.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlAnoReferencia{ width: 47px;}
        .ddlMesReferencia{ width: 70px;}
        .ddlTipo{ width: 80px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlAnoReferencia" class="lblObrigatorio" title="Ano de Refência">Ano Ref</label>
        <asp:DropDownList ID="ddlAnoReferencia" runat="server" ToolTip="Selecione um Ano de Refência" CssClass="ddlAnoReferencia"></asp:DropDownList>
    </li>
    <li>
        <label for="ddlMesReferencia" title="Mês de Refência">Mês Ref</label>
        <asp:DropDownList ID="ddlMesReferencia" ToolTip="Selecione um Mês de Refência" runat="server" CssClass="ddlMesReferencia"></asp:DropDownList>
    </li>
    <li>
        <label for="ddlTipo" title="Tipo de Calendário">Tipo Calendário</label>
        <asp:DropDownList ID="ddlTipo" ToolTip="Selecione um Tipo de Calendário" runat="server" CssClass="ddlTipo"></asp:DropDownList>
    </li>
    <li>
        <label for="ddlUnidade" class="lblObrigatorio" title="Unidade/Escola">Unidade/Escola</label>
        <asp:DropDownList ID="ddlUnidade" runat="server" ToolTip="Selecione uma Unidade/Escola" CssClass="campoUnidadeEscolar"></asp:DropDownList>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
