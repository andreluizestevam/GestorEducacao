<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1500_CtrlContratosCompromissos.F1502_CadastroSubCategoria.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       .ulDados { width: 220px; } 
       
       /*--> CSS LIs */
       .liClear { clear:both; }
       
       /*--> CSS DADOS */ 
       .txtCodigo { width: 70px;}       
       .txtNome { width: 210px; }       
       .ddlCateg{ width: 210px;}
       .ddlStatus { width: 70px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">  
        <li>
            <label for="txtCodigo" class="lblObrigatorio" title="Código da SubCategoria" >
                Código</label>
            <asp:TextBox id="txtCodigo" runat="server" ToolTip="Digite código da SubCategoria" CssClass="txtCodigo" MaxLength="12">
              </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtCodigo" runat="server" CssClass="validatorField"
            ControlToValidate="txtCodigo" Text="*" 
            ErrorMessage="Campo Código é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtNome" class="lblObrigatorio" title="Nome da SubCategoria">
                Nome</label>
            <asp:TextBox ID="txtNome" runat="server" MaxLength="80" ToolTip="Digite o nome da SubCategoria" CssClass="txtNome">
            </asp:TextBox>            
            <asp:RequiredFieldValidator ID="rfvttxtNome" runat="server" CssClass="validatorField"
            ControlToValidate="txtNome" Text="*" 
            ErrorMessage="Campo Nome é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>        
        <li>
             <label for="ddlCateg" title="Tipo de Categoria" class="lblObrigatorio">Categoria</label>
            <asp:DropDownList ID="ddlCateg"  CssClass="ddlCateg" runat="server" ToolTip="Selecione a Categoria">
              </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlGrupo" runat="server" CssClass="validatorField"
            ControlToValidate="ddlCateg" Text="*" 
            ErrorMessage="Campo Categoria é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>   
        <li style="margin-top: 10px;">
            <label for="ddlStatus" title="Situação" class="lblObrigatorio" >Situação</label>
            <asp:DropDownList ID="ddlStatus"  CssClass="ddlStatus" runat="server" ToolTip="Selecione a Situação">
                <asp:ListItem Value="A" Text="Ativa"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativa"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
            ControlToValidate="ddlStatus" Text="*" 
            ErrorMessage="Campo Situação é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
