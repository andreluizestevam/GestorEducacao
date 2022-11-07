<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1905_Idiomas.Cadastro" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 155px; }
        
        /*--> CSS LIs */
        .liClear { clear:both; }
        
        /*--> CSS DADOS */
       .labelPixel { margin-bottom:1px; }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtCO_IDIOM" class="labelPixel" title="Código">
                Código</label>
            <asp:TextBox ID="txtCO_IDIOM" runat="server" Enabled="false" MaxLength="10" CssClass="txtCod"
                ToolTip="Código">
            </asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtNO_IDIOM" class="lblObrigatorio labelPixel" title="Descrição">
                Descrição</label>
            <asp:TextBox ID="txtNO_IDIOM" runat="server" MaxLength="15" CssClass="txtDescricao"
                ToolTip="Informe a Descrição">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtNO_IDIOM" runat="server" ControlToValidate="txtNO_IDIOM"
                ErrorMessage="Campo Descrição é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtSigla" class="lblObrigatorio labelPixel"
                title="Sigla">
                Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" MaxLength="3" CssClass="txtSigla"
                ToolTip="Informe a Sigla">
            </asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtSigla" ValidationExpression="^(.|\s){1,3}$"
                CssClass="validatorField"
                ErrorMessage="Campo Sigla não pode ser maior que 3 caracteres" SetFocusOnError="true">
            </asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Campo Sigla é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
