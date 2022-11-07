<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._7000_ControleOperRH._7950_CtrlCadastralParceiros._7951_CadastroParceiros.Busca"
    Title="Buscar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .txtNome
        {
            width: 210px;
        }
        .liCadasTitulPagam
        {
            background-color: #F0FFFF;
            border: 1px solid #D2DFD1;
            clear: both;
            margin-left: 100px !important;
            margin-top: 10px !important;
            padding: 2px 3px 1px 7px;
            width: 85px;
            margin-right: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtNome" title="Nome do Fornecedor">
                Nome do Participante</label>
            <asp:TextBox ID="txtNome" ToolTip="Informe o Nome do Fornecedor" CssClass="txtNome"
                runat="server"></asp:TextBox>
        </li>
        <li>
            <label for="ddlAreaProspeccao" title="Área de prospecção do negócio">
                Classificação</label>
            <asp:DropDownList runat="server" ID="ddlAreaProspeccao" CssClass="txtNome">
                <asp:ListItem Value="" Selected="True">Selecione</asp:ListItem>
                <asp:ListItem Value="EDU">Educação</asp:ListItem>
                <asp:ListItem Value="SAU">Saúde</asp:ListItem>
                <asp:ListItem Value="ENE">Energia</asp:ListItem>
                <asp:ListItem Value="ESP">Esporte</asp:ListItem>
                <asp:ListItem Value="SEG">Seguros</asp:ListItem>
                <asp:ListItem Value="BIA">Bio-Agro</asp:ListItem>
                <asp:ListItem Value="CON">Consórcios</asp:ListItem>
                <asp:ListItem Value="COM">Commodites</asp:ListItem>
                <asp:ListItem Value="INF">Infra-estrutura</asp:ListItem>
                <asp:ListItem Value="MAM">Meio Ambiente</asp:ListItem>
                <asp:ListItem Value="COI">Construção e Incorporação</asp:ListItem>
                <asp:ListItem Value="ASM">Assest Management</asp:ListItem>
                <asp:ListItem Value="GAI">Gestão de Ativos Imobiliários</asp:ListItem>
                <asp:ListItem Value="TEC">Tecnologia</asp:ListItem>
                <asp:ListItem Value="PAG">Meio de Pagamentos</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        jQuery(function ($) {
        });
    </script>
</asp:Content>
