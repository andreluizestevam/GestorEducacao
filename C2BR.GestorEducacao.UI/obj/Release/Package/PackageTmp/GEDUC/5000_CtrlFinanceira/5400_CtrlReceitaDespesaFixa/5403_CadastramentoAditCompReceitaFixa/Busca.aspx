﻿<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5400_CtrlReceitaDespesaFixa.F5403_CadastramentoAditCompReceitaFixa.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .centro { text-align: center;}        
        .txtCnpj { width: 102px; }
        .txtNome { width: 210px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtCnpj" title="CNPJ">CNPJ</label>
            <asp:TextBox ID="txtCnpj" 
                ToolTip="Informe o CNPJ"
                CssClass="txtCnpj" runat="server"></asp:TextBox>
    </li>
    <li>
        <label for="txtNome" title="Nome do Cliente">Nome do Cliente</label>
            <asp:TextBox ID="txtNome" 
                ToolTip="Informe o Nome do Cliente"
                CssClass="txtNome" runat="server"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        jQuery(function($) {
            $(".txtCnpj").mask("99.999.999/9999-99");
        });
    </script>
</asp:Content>