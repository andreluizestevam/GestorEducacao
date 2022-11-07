<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0914_Paises.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 160px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        .txtNomePais { width: 300px; }
        .txtCodISO { width: 20px; text-transform:uppercase; }
        .txtCodISO3, .txtIDPais { width: 30px; text-transform:uppercase; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtCodISO"  title="Código do ISO">
                ISO</label>
            <asp:TextBox ID="txtCodISO" ToolTip="Informe o código ISO do País" runat="server" CssClass="txtCodISO"  MaxLength="2"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField" runat="server" ControlToValidate="txtCodISO"
                ErrorMessage="Código ISO do País deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtCodISO3"  title="Código do ISO 3">
                ISO</label>
            <asp:TextBox ID="txtCodISO3" ToolTip="Informe o código ISO 3 do País" runat="server" CssClass="txtCodISO3"  MaxLength="3"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField" runat="server" ControlToValidate="txtCodISO3"
                ErrorMessage="Código ISO 3 do País deve ser informado">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtIDPais"  title="ID do País">
                ID</label>
            <asp:TextBox ID="txtIDPais" ToolTip="Informe o ID do País" runat="server" CssClass="txtIDPais"  MaxLength="3"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtNomePais"  title="Nome do País">
                Nome do País</label>
            <asp:TextBox ID="txtNomePais" ToolTip="Informe o Nome do País" runat="server" CssClass="txtNomePais"  MaxLength="255"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField" runat="server" ControlToValidate="txtNomePais"
                ErrorMessage="Nome do País deve ser informado">
            </asp:RequiredFieldValidator>
        </li>      
    </ul>
    <script type="text/javascript">
        jQuery(function ($) {
            $(".txtIDPais").mask("?999");
        });
    </script>
</asp:Content>
