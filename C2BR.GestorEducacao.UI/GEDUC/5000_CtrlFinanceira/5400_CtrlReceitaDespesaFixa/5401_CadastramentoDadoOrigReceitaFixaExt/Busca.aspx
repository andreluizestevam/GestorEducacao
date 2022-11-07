<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5400_CtrlReceitaDespesaFixa.F5401_CadastramentoDadoOrigReceitaFixaExt.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .txtNome { width: 210px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtNome" title="Nome do Fornecedor">Nome da Fonte de Receita</label>
            <asp:TextBox ID="txtNome" 
                ToolTip="Informe o Nome do Fornecedor"
                CssClass="txtNome" runat="server"></asp:TextBox>
    </li>
</ul>
</asp:Content>