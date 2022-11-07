<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1500_CtrlContratosCompromissos.F1501_CadastroCategoria.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       .ulDados { width: 220px; } 
       
       /*--> CSS LIs */
       .liClear { clear:both; }
       
       /*--> CSS DADOS */ 
       .txtCodigo { width: 70px;}       
       .txtNome { width: 210px; }       
       .ddlStatus { width: 70px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">  
        <li>
            <label for="txtCodigo" class="lblObrigatorio" title="C�digo da Categoria">
                C�digo</label>
            <asp:TextBox id="txtCodigo" runat="server" ToolTip="Digite c�digo da Categoria" CssClass="txtCodigo" MaxLength="12">
              </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtCodigo" runat="server" CssClass="validatorField"
            ControlToValidate="txtCodigo" Text="*" 
            ErrorMessage="Campo c�digo � requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtNome" class="lblObrigatorio" title="Nome da Categoria">
                Nome</label>
            <asp:TextBox ID="txtNome" runat="server" MaxLength="80" ToolTip="Digite o nome da Categoria" CssClass="txtNome">
            </asp:TextBox>            
            <asp:RequiredFieldValidator ID="rfvttxtNome" runat="server" CssClass="validatorField"
            ControlToValidate="txtNome" Text="*" 
            ErrorMessage="Campo Nome � requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li> 
        <li>
            <label for="ddlStatus" title="Situa��o" class="lblObrigatorio" >Situa��o</label>
            <asp:DropDownList ID="ddlStatus"  CssClass="ddlStatus" runat="server" ToolTip="Selecione a Situa��o">
                <asp:ListItem Value="A" Text="Ativa"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativa"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlGrupo" runat="server" CssClass="validatorField"
            ControlToValidate="ddlStatus" Text="*" 
            ErrorMessage="Campo Situa��o � requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>  
    </ul>
</asp:Content>
