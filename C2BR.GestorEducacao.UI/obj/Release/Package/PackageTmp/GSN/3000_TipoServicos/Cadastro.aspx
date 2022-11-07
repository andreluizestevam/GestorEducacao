<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSN._3000_TipoServicos.Cadastro" %>

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
            <label for="txtSigla" class="lblObrigatorio" title="UF">Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Digite uma sigla para o Tipo de Serviço" CssClass="txtUF" runat="server" MaxLength="12"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtSigla" ValidationExpression="^(.|\s){1,12}$"
                ErrorMessage="Campo Sigla não pode ser maior que 12 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="O campo Sigla é obrigatório" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtServico" title="Descrição" class="lblObrigatorio labelPixel">Serviço</label>
            <asp:TextBox ID="txtServico" ToolTip="Informe a descrição" CssClass="campoDescricao" runat="server" MaxLength="100"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ControlToValidate="txtServico" ValidationExpression="^(.|\s){1,100}$"
                ErrorMessage="O campo Serviço não pode ser maior que 100 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtServico"
                ErrorMessage="O campo Serviço é obrigatório" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtTipo" title="Tipo de serviço" class="lblObrigatorio labelPixel">Tipo</label>
            <asp:TextBox ID="txtTipo" ToolTip="Informe o tipo de serviço" CssClass="campoDescricao" runat="server" MaxLength="12"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                ControlToValidate="txtTipo" ValidationExpression="^(.|\s){1,12}$"
                ErrorMessage="Campo Tipo não pode ser maior que 12 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTipo"
                ErrorMessage="O campo Tipo é obrigatório" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li>
           <label for="ddlStatus">Situação</label>
             <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="A">Ativo</asp:ListItem>
             <asp:ListItem Value="I">Inativo</asp:ListItem>
           </asp:DropDownList>
       </li>
    </ul>
</asp:Content>
