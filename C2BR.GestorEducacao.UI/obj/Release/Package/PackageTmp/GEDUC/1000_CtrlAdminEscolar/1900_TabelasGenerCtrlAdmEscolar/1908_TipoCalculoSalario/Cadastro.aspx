<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1908_TipoCalculoSalario.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 150px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtCodigo" title="Código">
                Código</label>
            <asp:TextBox ID="txtCodigo" Enabled="false" runat="server" MaxLength="10" CssClass="campoCodigo"
                ToolTip="Informe o Código"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio" title="Descrição">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" runat="server" MaxLength="40" CssClass="campoDescricao"
                ToolTip="Informe a Descrição"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ter no máximo 40 caracteres" CssClass="validatorField"
                ValidationExpression="^(.|\s){1,40}$">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtSigla" class="lblObrigatorio" title="Sigla">
                Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" MaxLength="1" CssClass="campoSigla"
                ToolTip="Informe a Sigla"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ter 1 caractere" CssClass="validatorField" ValidationExpression="^(.|\s){1,1}$">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
