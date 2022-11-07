<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0903_InstituicaoEspecializadaAlunoEspecial.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 150px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
         .labelPixel { margin-bottom: 1px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="liClear">
            <label for="txtInstEspec" title="Descrição" class="lblObrigatorio labelPixel">
                Descrição</label>
            <asp:TextBox ID="txtInstEspec" ToolTip="Informe a Descrição" runat="server" MaxLength="30" CssClass="campoDescricao"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revtxtInstEspecMaxChars"
                runat="server" ControlToValidate="txtInstEspec" ValidationExpression="^(.|\s){1,30}$"
                ErrorMessage="Campo Descrição não pode ser maior que 30 caracteres" Text="*"
                Display="Dynamic" SetFocusOnError="true" CssClass="validatorField">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtInstEspec" runat="server" ControlToValidate="txtInstEspec"
                ErrorMessage="Campo Descrição é requerido" Text="*" Display="Dynamic" SetFocusOnError="true"
                CssClass="validatorField" EnableClientScript="True"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
