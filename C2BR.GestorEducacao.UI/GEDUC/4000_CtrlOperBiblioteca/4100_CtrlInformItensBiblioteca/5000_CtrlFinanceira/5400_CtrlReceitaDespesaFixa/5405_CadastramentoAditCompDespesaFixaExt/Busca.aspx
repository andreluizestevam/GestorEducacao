<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5400_CtrlReceitaDespesaFixa.F5405_CadastramentoAditCompDespesaFixaExt.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .centro { text-align: center;}       
        .txtNome { width: 210px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li id="liNome" class="liNome">
        <label for="txtNome" title="Nome do Fornecedor">Nome do Fornecedor</label>
            <asp:TextBox ID="txtNome" 
                ToolTip="Informe o Nome do Fornecedor"
                CssClass="txtNome" runat="server"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        jQuery(function($) {
        });
    </script>
</asp:Content>