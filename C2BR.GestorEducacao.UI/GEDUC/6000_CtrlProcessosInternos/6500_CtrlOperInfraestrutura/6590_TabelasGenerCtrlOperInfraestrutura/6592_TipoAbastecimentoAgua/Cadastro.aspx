<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" 
MasterPageFile="~/App_Masters/PadraoCadastros.Master" Title="Cadastro"
Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6500_CtrlOperInfraestrutura.F6590_TabelasGenerCtrlOperInfraestrutura.F6592_TipoAbastecimentoAgua.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .ulDados{ width: 150px; }  
        
        /*--> CSS LIs */      
        .liClear { clear:both; }   
           
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">  
        <li class="liClear">
            <label for="txtNO_ABAST" title="Abastecimento de Água" class="lblObrigatorio">Abastecimento de Água</label>
            <asp:TextBox ID="txtNO_ABAST" ToolTip="Informe o Abastecimento de Água" runat="server" MaxLength="50" CssClass="campoDescricao"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revTxtNO_ABAST" runat="server" CssClass="validatorField"
                ControlToValidate="txtNO_ABAST" ValidationExpression="^(.|\s){1,50}$"
                ErrorMessage="Campo Abastecimento de Água não pode ser maior que 50 caracteres" Text="*" Display="Dynamic" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvTxtNO_ABAST" runat="server" ControlToValidate="txtNO_ABAST" CssClass="validatorField"
                ErrorMessage="Campo Abastecimento de Água é requerido" Text="*" Display="Dynamic" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>