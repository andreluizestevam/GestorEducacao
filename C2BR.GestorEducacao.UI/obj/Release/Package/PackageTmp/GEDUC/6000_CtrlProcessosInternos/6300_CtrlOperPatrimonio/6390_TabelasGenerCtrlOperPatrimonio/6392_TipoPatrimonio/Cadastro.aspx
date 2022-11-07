<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6390_TabelasGenerCtrlOperPatrimonio.F6392_TipoPatrimonio.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 150px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        .txtDescricao { width: 150px; }
        .txtCodigo { width: 20px; text-transform: uppercase; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">   
        <li>
            <label for="txtCodigo" class="lblObrigatorio" title="Código do Tipo de Patrimônio">
                Código</label>
            <asp:TextBox ID="txtCodigo" CssClass="txtCodigo" ToolTip="Informe o Código do Tipo de Patrimônio" runat="server" MaxLength="2"></asp:TextBox>    
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Código do Tipo de Patrimônio deve ser informado" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>    
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio" title="Descrição do Tipo de Patrimônio">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" CssClass="txtDescricao" ToolTip="Informe a Descrição do Tipo de Patrimônio" runat="server" MaxLength="30"></asp:TextBox>    
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>    
    </ul>
</asp:Content>
