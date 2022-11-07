<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3800_CtrlEncerramentoLetivo.F3810_CtrlRegioesEnsino.F3813_CadastramentoTpAvalDesempRegEnsino.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    /*--> CSS DADOS */
    .txtTB_TIPO_AVAL_INST{ width: 125px; }
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtTB_TIPO_AVAL_INST" title="Tipo de Avaliação ou Sigla">Tipo / Sigla</label>
        <asp:TextBox ID="txtTB_TIPO_AVAL_INST" ToolTip="Informe um Tipo de Avaliação ou uma Sigla" CssClass="campoDescricao" runat="server" MaxLength="40"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
