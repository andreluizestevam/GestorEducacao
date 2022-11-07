<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5902_BancoAgencia.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 155px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
                
        /*--> CSS Dados */
        .txtNossoNumero { width:110px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtIDEBANCO" title="Código do Banco" class="lblObrigatorio">
                Número</label>
            <asp:TextBox ID="txtIDEBANCO" ToolTip="Informe o Código do Banco" CssClass="txtCod" runat="server" MaxLength="3"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtIDEBANCO"
                ErrorMessage="Campo Número não pode ser maior que 3 caracteres" SetFocusOnError="true"
                ValidationExpression="^(.|\s){1,3}$" CssClass="validatorField">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIDEBANCO"
                ErrorMessage="Campo Número é requerido" SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDESBANCO" title="Nome do Banco" class="lblObrigatorio">
                Nome do Banco</label>
            <asp:TextBox ID="txtDESBANCO" ToolTip="Informe o Nome do Banco" CssClass="txtDescricao" runat="server" MaxLength="40">
            </asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDESBANCO"
                ErrorMessage="Campo Nome do Banco não pode ser maior que 40 caracteres" SetFocusOnError="true"
                ValidationExpression="^(.|\s){1,40}$" CssClass="validatorField">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDESBANCO"
                ErrorMessage="Campo Nome do Banco é requerido" SetFocusOnError="true" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtNossoNumero" title="Nosso Número">Nosso Número</label>
            <asp:TextBox ID="txtNossoNumero" runat="server" CssClass="txtNossoNumero"
                ToolTip="Informe o Nosso Número"
                MaxLength="20">
            </asp:TextBox>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function($) {
            $(".txtCod").mask("?999");
            $(".txtNossoNumero").mask("?999999999999999999");
        });
    </script>
</asp:Content>
