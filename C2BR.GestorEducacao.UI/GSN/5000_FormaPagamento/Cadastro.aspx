<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSN._5000_FormaPagamento.Cadastro" %>

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
            <label for="txtSigla" title="Sigla" class="lblObrigatorio labelPixel">Sigla</label>
            <asp:TextBox ID="txtSigla" Width="20px" ToolTip="Informe a Sigla" CssClass="campoDescricao" runat="server" MaxLength="2"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ControlToValidate="txtSigla" ValidationExpression="^(.|\s){1,2}$"
                ErrorMessage="O campo Sigla não pode ser maior que 2 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="O campo Sigla é obrigatório" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtForma" class="lblObrigatorio" title="Forma de Pagamento">Forma de Pagamento</label>
            <asp:TextBox ID="txtForma" Width="150px" ToolTip="Informe a Forma de Pagamento." CssClass="txtUF" runat="server" MaxLength="30" ></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtForma" ValidationExpression="^(.|\s){1,30}$"
                ErrorMessage="O campo Forma de Pagamento não pode ser maior que 30 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtForma"
                ErrorMessage="A Forma de Pagamento é obrigatória" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>

