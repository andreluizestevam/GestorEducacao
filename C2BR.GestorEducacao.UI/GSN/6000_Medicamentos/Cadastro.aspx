<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/App_Masters/PadraoCadastros.Master" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSN._6000_Medicamentos.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">        
        .txDescricao { width: 155px; } /* Usado para definir o formulário ao centro */
        .ulDados
        {
            width: 155px;
        }
        /* Usado para definir o formulário ao centro */
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        
        /*--> CSS DADOS */
        .labelPixel
        {
            margin-bottom: 1px;
        }        
          
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="txtPrincipioAtivo" class="lblObrigatorio" title="Princípio Ativo">Princípio Ativo</label>
            <asp:TextBox ID="txtPrincipioAtivo" ToolTip="Digite o Princípio Ativo." CssClass="txtPrincipioAtivo" runat="server" MaxLength="100" ></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtPrincipioAtivo" ValidationExpression="^(.|\s){1,100}$"
                ErrorMessage="O campo Princípio Ativo não pode ser maior que 100 caracteres." CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPrincipioAtivo"
                ErrorMessage="O Princípio Ativo é requerido." CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
       <li class="liClear">
            <label for="txtNomeApresentacao" title="Nome Apresentação." class="lblObrigatorio labelPixel">Nome Apresentação</label>
            <asp:TextBox ID="txtNomeApresentacao" ToolTip="Informe o Nome de Apresentação do medicamento." CssClass="txtNomeApresentacao" runat="server" MaxLength="100"></asp:TextBox>
            <asp:RegularExpressionValidator ID="ValidatorLink" runat="server" 
                ControlToValidate="txtNomeApresentacao" ValidationExpression="^(.|\s){1,100}$"
                ErrorMessage="O Nome de Apresentação não pode ser maior que 100 caracteres." CssClass="txtNomeApresentacao" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNomeApresentacao"
                ErrorMessage="O Nome de Apresentação é requerido." CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtIndicacao" title="Indicação" class="lblObrigatorio labelPixel">Indicação</label>
            <asp:TextBox ID="txtIndicacao" ToolTip="Informe a indicação do medicamento." CssClass="campoDescricao" runat="server" MaxLength="100"></asp:TextBox>
            <asp:RegularExpressionValidator ID="ValidatorImagem" runat="server" 
                ControlToValidate="txtIndicacao" ValidationExpression="^(.|\s){1,100}$"
                ErrorMessage="Campo Indicação não pode ser maior que 100 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtIndicacao"
                ErrorMessage="A Indicação é requerida." CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
           <label for="ddlStatus">Situação</label>
             <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="A">Ativo</asp:ListItem>
             <asp:ListItem Value="I">Inativo</asp:ListItem>
           </asp:DropDownList>
       </li>
    </ul>
</asp:Content>