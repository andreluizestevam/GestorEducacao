<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSN._10000_Credenciados.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">   
        .centro{margin-left:378px}               
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server" >
<div class="centro">
   <ul id="ulDados" class="ulDados">
       <li class="liClear">
            <label for="txtNome" title="Nome" class="lblObrigatorio labelPixel">Nome</label>
            <asp:TextBox ID="txtNome" ToolTip="Informe o Nome do profissional" CssClass="campoDescricao" Width="200px" runat="server" MaxLength="300"></asp:TextBox>
            <asp:RegularExpressionValidator ID="ValidatorLink" runat="server" 
                ControlToValidate="txtNome" ValidationExpression="^(.|\s){1,300}$"
                ErrorMessage="Campo Nome não pode ser maior que 300 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNome"
                ErrorMessage="O Nome é requerido" CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtConselho" title="Conselho" class="lblObrigatorio labelPixel">Conselho</label>
            <asp:TextBox ID="txtConselho" ToolTip="Informe o Conselho do profissional." Width="60px" CssClass="campoDescricao" runat="server" MaxLength="50"></asp:TextBox>
            <asp:RegularExpressionValidator ID="ValidatorImagem" runat="server" 
                ControlToValidate="txtConselho" ValidationExpression="^(.|\s){1,50}$"
                ErrorMessage="Campo Conselho não pode ser maior que 50 caracteres" CssClass="validatorField" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConselho"
                ErrorMessage="Campo Conselho é requerido." CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li><br /><br /><br />
        <li class="liClear">
           <label for="ddlUnidPrincipal">Unid. Principal</label>
             <asp:DropDownList ID="ddlUnidPrincipal" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="">Selecione</asp:ListItem>
             <asp:ListItem Value="S">Sim</asp:ListItem>
             <asp:ListItem Value="N">Não</asp:ListItem>
           </asp:DropDownList>
       </li>
       <li class="liClear">
           <label for="ddlSituacao">Situação</label>
             <asp:DropDownList ID="ddlSituacao" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="">Selecione</asp:ListItem>
             <asp:ListItem Value="A">Ativo</asp:ListItem>
             <asp:ListItem Value="I">Inativo</asp:ListItem>
           </asp:DropDownList>
       </li>
        <li class="liClear">
            <label for="txtCodUnidadeFaturamento" title="Conselho" class="lblObrigatorio labelPixel">Cod. Unidade de Faturamento</label>
            <asp:TextBox ID="txtCodUnidadeFaturamento" ToolTip="Informe o Codigo da Unidade Principal." Width="60px" CssClass="campoDescricao" runat="server" MaxLength="50"></asp:TextBox> 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCodUnidadeFaturamento"
                ErrorMessage="Campo Cod. Unidade de Faturamento é requerido." CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li><br /><br /><br />
        <li class="liClear">
            <label for="txtCodUnidade" title="Conselho" class="lblObrigatorio labelPixel">Cod. Unidade Principal</label>
            <asp:TextBox ID="txtCodUnidade" ToolTip="Informe o Codigo da Unidade Principal." Width="60px" CssClass="campoDescricao" runat="server" MaxLength="50"></asp:TextBox> 
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCodUnidade"
                ErrorMessage="Campo Cod. Unidade Principal é requerido." CssClass="validatorField" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
           <label for="ddlAcessibilidade">Acessibilidade</label>
             <asp:DropDownList ID="ddlAcessibilidade" runat="server" CssClass="ddlStatus" AutoPostBack="True" Width="60px">
             <asp:ListItem Value="">Selecione</asp:ListItem>
             <asp:ListItem Value="S">Sim</asp:ListItem>
             <asp:ListItem Value="N">Não</asp:ListItem>
           </asp:DropDownList>
       </li>
    </ul>
</div>
</asp:Content>