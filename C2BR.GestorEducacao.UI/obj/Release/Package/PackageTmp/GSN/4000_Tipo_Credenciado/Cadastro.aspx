<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSN._4000_Tipo_Credenciado.Cadastro" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
    <style type="text/css">
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
</asp:content>
<asp:content id="Content2" contentplaceholderid="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="txtSIGLA" class="lblObrigatorio" title="Sigla">Sigla</label>
            <asp:TextBox ID="txtSIGLA" ToolTip="Informe a Sigla" CssClass="txtSIGLA" runat="server" MaxLength="6" ></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtSIGLA" ValidationExpression="^(.|\s){1,6}$"
                ErrorMessage="Campo Sigla não pode ser maior que 06 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSIGLA"
                ErrorMessage="Sigla é requerida" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>

        <li class="liClear">
            <label for="txtTIPO_CREDENCIADO" class="lblObrigatorio" title="TIPO CREDENCIADO">TIPO CREDENCIADO</label>
            <asp:TextBox ID="txtTIPO_CREDENCIADO" ToolTip="Informe a Tipo" CssClass="txtTIPO_CREDENCIADO" runat="server" MaxLength="60" ></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ControlToValidate="txtTIPO_CREDENCIADO" ValidationExpression="^(.|\s){1,60}$"
                ErrorMessage="Campo Tipo não pode ser maior que 60 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTIPO_CREDENCIADO"
                ErrorMessage="Tipo é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>

        <li>
             <label for="ddlStatus">
                            Situação</label>
             <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddlStatus" AutoPostBack="True"
                            Width="60px">
                            <asp:ListItem Value="A">Ativos</asp:ListItem>
                            <asp:ListItem Value="I">Inativos</asp:ListItem>
             </asp:DropDownList>
        </li>

         <%--</asp:DropDownList>
         <asp:Button ID="btnCadastrar" CssClass="btnCadastrar" runat="server" Text="CADASTRAR" OnClick="BtnSalvar_OnClick"
            Style="float: right; margin-right: 14px; margin-top: 2px;" ToolTip="Cadastrar o Tipo do Credenciado" />--%>
       
    </ul>
</asp:content>
