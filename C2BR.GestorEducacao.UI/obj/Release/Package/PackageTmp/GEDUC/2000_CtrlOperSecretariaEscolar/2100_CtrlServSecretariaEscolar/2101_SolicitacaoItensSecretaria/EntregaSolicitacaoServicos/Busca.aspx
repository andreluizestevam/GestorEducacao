<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.EntregaSolicitacaoServicos.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
 <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtAluno">Beneficiário</label>
            <asp:TextBox ID="txtAluno" runat="server" CssClass="campoNomePessoa">
            </asp:TextBox>
        </li>
        <li>
            <label for="txtResponsavel">Associado</label>
            <asp:TextBox ID="txtResponsavel" runat="server" CssClass="campoNomePessoa">
            </asp:TextBox>
        </li>
        <li>
            <label for="txtNumeroSolicitacao">N° Solicitação</label>
            <asp:TextBox ID="txtNumeroSolicitacao" runat="server" CssClass="txtNumeroSolicitacao">
            </asp:TextBox>
        </li>
        <li style="clear: both;">
            <label for="ddlSolicitacoes">Serviços</label>
            <asp:DropDownList ID="ddlSolicitacoes" runat="server" Width="255px" AutoPostBack="True">
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        jQuery(function($) {
            $(".txtNumeroSolicitacao").mask("9999.99.999999");
        });
    </script>
</asp:Content>
