<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1907_TipoContratoTrabalho.Cadastro" Title="TipoContratoTrabalho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 150px; }
        
        /*--> CSS LIs */  
        .liClear{ clear:both; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="txtCO_TPCON" title="Código">
                Código</label>
            <asp:TextBox ID="txtCO_TPCON" CssClass="campoCodigo" runat="server" Enabled="false" MaxLength="10"
                ToolTip="Código"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtNO_TPCON" class="lblObrigatorio" title="Descrição"> 
                Descrição</label>
            <asp:TextBox ID="txtNO_TPCON" runat="server" MaxLength="15" CssClass="campoDescricao"
                ToolTip="Informe a Descrição"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTxtNO_TPCON" runat="server" ControlToValidate="txtNO_TPCON"
                ErrorMessage="Campo Descrição é requerido" 
                CssClass="validatorField">
            </asp:RequiredFieldValidator>
            
        </li>
    </ul>
</asp:Content>
