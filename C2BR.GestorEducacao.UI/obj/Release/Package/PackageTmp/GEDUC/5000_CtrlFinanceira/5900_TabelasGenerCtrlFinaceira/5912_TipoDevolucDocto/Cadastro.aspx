<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5912_TipoDevolucDocto.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 250px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liEspaco { margin-left: 10px; }
        
        /*--> CSS Dados */
        .txtSigla { width: 40px; }
        .txtCodigDevolBanco { width: 105px; }   
        .txtDescricao { width: 250px; }    
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtSigla" title="Sigla" class="lblObrigatorio">
                Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Informe a Sigla" CssClass="txtSigla" runat="server" MaxLength="4"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="validatorField" runat="server"
                ControlToValidate="txtSigla" ErrorMessage="Descri��o deve ter no m�ximo 40 caracteres"
                Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField" runat="server"
                ControlToValidate="txtSigla" ErrorMessage="Sigla deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="txtCodigDevolBanco" title="C�digo Devolu��o">
                C�digo Devolu��o</label>
            <asp:TextBox ID="txtCodigDevolBanco" ToolTip="Informe o C�digo de Devolu��o" CssClass="txtCodigDevolBanco" runat="server" MaxLength="15"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtDescricao" title="Descri��o" class="lblObrigatorio">
                Descri��o</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe a Descri��o" CssClass="txtDescricao" runat="server" MaxLength="60"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" CssClass="validatorField" runat="server"
                ControlToValidate="txtDescricao" ErrorMessage="Descri��o deve ter no m�ximo 40 caracteres"
                Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" CssClass="validatorField" runat="server"
                ControlToValidate="txtDescricao" ErrorMessage="Descri��o deve ser informada"
                Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        </li>        
    </ul>

    <script type="text/javascript">
        jQuery(function($) {
        });
    </script>

</asp:Content>
