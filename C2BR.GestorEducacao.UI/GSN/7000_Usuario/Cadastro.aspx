<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSN._7000_Usuario.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">        
        .txDescricao { width: 155px; } /* Usado para definir o formulário ao centro */
        .ulDados
        {
            width: 155px;
        }

        /*--> CSS LIs */
        .liClear { clear:both; }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom:1px; }      
          
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
    <asp:HiddenField ID="txtMobiDevice" runat="server"/>
        <li class="liClear">
            <label for="txtNome" class="lblObrigatorio" title="Nome">Nome</label>
            <asp:TextBox ID="txtNome" ToolTip="Digite o nome do Usuário." Width="200px" CssClass="txtUF" runat="server" MaxLength="200" ></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtNome" ValidationExpression="^(.|\s){1,200}$"
                ErrorMessage="Campo Nome não pode ser maior que 200 caracteres." CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome"
                ErrorMessage="O Nome é requerido." CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
       <li class="liClear">
            <label for="txtCPF" title="CPF" class="lblObrigatorio labelPixel">CPF</label>
            <asp:TextBox ID="txtCPF" ToolTip="Informe o CPF." CssClass="campoDescricao" runat="server" MaxLength="11"></asp:TextBox>
            <asp:RegularExpressionValidator ID="ValidatorLink" runat="server" 
                ControlToValidate="txtCPF" ValidationExpression="^(.|\s){1,11}$"
                ErrorMessage="Campo CPF não pode ser maior que 11 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCPF"
                ErrorMessage="O CPF é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtEmail" title="EMAIL" class="lblObrigatorio labelPixel">EMAIL</label>
            <asp:TextBox ID="txtEmail" ToolTip="Informe o Email." CssClass="campoDescricao" runat="server" MaxLength="255"></asp:TextBox>
            <asp:RegularExpressionValidator ID="ValidatorImagem" runat="server" 
                ControlToValidate="txtEmail" ValidationExpression="^(.|\s){1,255}$"
                ErrorMessage="Campo Email não pode ser maior que 255 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="O Email é requerido." CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li>
             <label for="ddlSexo">
                            Sexo</label>
             <asp:DropDownList ID="ddlSexo" runat="server" CssClass="ddlSexo" AutoPostBack="True"
                            Width="60px">
                            <asp:ListItem Value="M">Masculino</asp:ListItem>
                            <asp:ListItem Value="F">Feminino</asp:ListItem>
             </asp:DropDownList>
        </li>
        <li>
             <label for="ddlStatus">
                            Status</label>
             <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddlStatus" AutoPostBack="True"
                            Width="60px">
                            <asp:ListItem Value="A">Ativo</asp:ListItem>
                            <asp:ListItem Value="I">Inativo</asp:ListItem>
             </asp:DropDownList>
        </li>
        <li class="liDataNascimento">
                    <label for="txtDataNascimento" class="lblObrigatorio" title="Data de Nascimento">
                        Nascimento</label>
                    <asp:TextBox ID="txtDataNascimento" CssClass="campoData" runat="server" ToolTip="Informe a Data de Nascimento"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDataNascimento"
                        ErrorMessage="Data de Nascimento deve ser informada" Text="*" Display="None"
                        CssClass="validatorField"></asp:RequiredFieldValidator>
       </li>
       <li class="liClear">
            <label for="txtImagem" title="Imagem" class="lblObrigatorio labelPixel">Imagem</label>
            <asp:TextBox ID="txtImagem" ToolTip="Insira o Link da Imagem." CssClass="campoDescricao" runat="server" MaxLength="1000"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                ControlToValidate="txtImagem" ValidationExpression="^(.|\s){1,1000}$"
                ErrorMessage="O campo Imagem não pode ser maior que 1000 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtImagem"
                ErrorMessage="A Imagem é requerida." CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
    </ul>

</asp:Content>
