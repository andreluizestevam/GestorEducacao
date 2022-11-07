<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6290_TabelasGenerCtrlOperMateriais.F6293_CadastroGrupoItens.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       .ulDados { width: 300px; } 
       
       /*--> CSS LIs */
       .liClear { clear:both; }
       
       /*--> CSS DADOS */      
       .txtNome { width: 300px; }       
       .txtDataAlter { width: 65px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados"> 
        <li>
            <label for="txtNome" class="lblObrigatorio" title="Nome do Grupo de Itens">
                Nome</label>
            <asp:TextBox ID="txtNome" runat="server" MaxLength="80" ToolTip="Digite o nome do Grupo de Itens" CssClass="txtNome">
            </asp:TextBox>            
            <asp:RequiredFieldValidator ID="rfvttxtNome" runat="server" CssClass="validatorField"
            ControlToValidate="txtNome" Text="*" 
            ErrorMessage="Campo Nome é requerido" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li> 
        <li class="liClear">
            <label for="txtDataAlter" class="lblObrigatorio" title="Data de Alteração">Dt Alteração</label>
            <asp:TextBox ID="txtDataAlter" Enabled="false" ToolTip="Informe a Data de Alteração" CssClass="txtDataAlter"
                runat="server"></asp:TextBox>
        </li>    
    </ul>
</asp:Content>
