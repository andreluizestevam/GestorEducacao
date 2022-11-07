<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSN._2000_Artigos.Cadastro" Title="Untitled Page" %>


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
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server" >
   <ul id="ulDados" class="ulDados">
       <li class="liClear">
            <label for="txtLink" title="Estado" class="lblObrigatorio labelPixel">Link</label>
            <asp:TextBox ID="txtLink" ToolTip="Informe o Link do Artigo" CssClass="campoDescricao" Width="200px" runat="server" MaxLength="400"></asp:TextBox>
            <asp:RegularExpressionValidator ID="ValidatorLink" runat="server" 
                ControlToValidate="txtLink" ValidationExpression="^(.|\s){1,400}$"
                ErrorMessage="Campo Link não pode ser maior que 400 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLink"
                ErrorMessage="O link é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtImagem" title="Estado" class="lblObrigatorio labelPixel">Imagem</label>
            <asp:TextBox ID="txtImagem" ToolTip="Informe o Link da Imagem." Width="200px" CssClass="campoDescricao" runat="server" MaxLength="1000"></asp:TextBox>
            <asp:RegularExpressionValidator ID="ValidatorImagem" runat="server" 
                ControlToValidate="txtImagem" ValidationExpression="^(.|\s){1,1000}$"
                ErrorMessage="Campo Imagem não pode ter um link maior que 1000 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtImagem"
                ErrorMessage="A imagem é requerida." CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>

        </li>
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio" title="UF">Descrição</label>
            <center><asp:TextBox ID="txtDescricao" TextMode="multiline" Rows="5" ToolTip="Digite a descrição do artigo." CssClass="txtUF" runat="server" Width="200px" MaxLength="100" ></asp:TextBox></center>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtDescricao" ValidationExpression="^(.|\s){1,100}$"
                ErrorMessage="Campo Descrição não pode ser maior que 100 caracteres." CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="A Descrição é requerida" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
