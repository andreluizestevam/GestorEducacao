<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.CadastramentoInfInstEnsino.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .txtCnpjIIE { width: 102px; }
        .txtNomeInstIIE { width: 210px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtCnpjIIE" title="CNPJ">CNPJ</label>
            <asp:TextBox ID="txtCnpjIIE" 
                ToolTip="Informe o CNPJ"
                CssClass="txtCnpjIIE" runat="server"></asp:TextBox>
    </li>
    <li>
        <label for="txtNomeInstIIE" title="Nome da Instituição">Nome da Institui&ccedil;&atilde;o</label>
            <asp:TextBox ID="txtNomeInstIIE" 
                ToolTip="Informe o Nome da Instituição"
                CssClass="txtNomeInstIIE" runat="server"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        jQuery(function($) {
            $(".txtCnpjIIE").mask("99.999.999/9999-99");
        });
    </script>
</asp:Content>