<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6230_CtrlOperacionalItensEstoque.F6234_CadastroFornecedorProdServ.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .txtNome { width: 210px; }
        .liCadasTitulPagam
        {
            background-color:#F0FFFF;
            border:1px solid #D2DFD1;
            clear:both;
            margin-left:100px !important;
            margin-top:10px !important;
            padding:2px 3px 1px 7px;
            width: 85px;
            margin-right: 0px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtNome" title="Nome do Fornecedor">Nome do Fornecedor</label>
            <asp:TextBox ID="txtNome" 
                ToolTip="Informe o Nome do Fornecedor"
                CssClass="txtNome" runat="server"></asp:TextBox>
    </li>
    <li>
        <div style="margin-top: 40px; position: absolute;">
        <ul>
                <li title="Clique para Redirecionar para o Cadastro de Títulos do Contas a Pagar" class="liCadasTitulPagam">                                    
                    <a id="lnkCadasTitulPagam" runat="server" href="" style="cursor: pointer;">INCLUIR TÍTULOS</a>
                </li>
            </ul>
        </div> 
    </li>       
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        jQuery(function($) {
        });
    </script>
</asp:Content>