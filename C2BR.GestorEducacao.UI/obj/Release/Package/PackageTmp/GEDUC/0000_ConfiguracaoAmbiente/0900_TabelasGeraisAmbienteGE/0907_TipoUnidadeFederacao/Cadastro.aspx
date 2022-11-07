<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0907_TipoUnidadeFederacao.Cadastro" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">        
        .ulDados { width: 155px; } /* Usado para definir o formulário ao centro */

        /*--> CSS LIs */
        .liClear { clear:both; }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom:1px; }      
          
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="txtUF" class="lblObrigatorio" title="UF">UF</label>
            <asp:TextBox ID="txtUF" ToolTip="Informe a UF" CssClass="txtUF" runat="server" MaxLength="2" ></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtUF" ValidationExpression="^(.|\s){1,2}$"
                ErrorMessage="Campo UF não pode ser maior que 2 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUF"
                ErrorMessage="Sigla da UF é requerida" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtDescricaoUF" title="Estado" class="lblObrigatorio labelPixel">Estado</label>
            <asp:TextBox ID="txtDescricaoUF" ToolTip="Informe o Estado" CssClass="campoDescricao" runat="server" MaxLength="30"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revtxtDescricaoUF" runat="server" 
                ControlToValidate="txtDescricaoUF" ValidationExpression="^(.|\s){1,30}$"
                ErrorMessage="Campo Descrição não pode ser maior que 30 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescricaoUF"
                ErrorMessage="Estado é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
