<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" 
MasterPageFile="~/App_Masters/PadraoCadastros.Master" Title="Cadastro"
Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6500_CtrlOperInfraestrutura.F6590_TabelasGenerCtrlOperInfraestrutura.F6595_TipoOcupacaoPredio.Cadastro" %>
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
            <label for="txtNO_PREDIO" title="Tipo de Ocupação" class="lblObrigatorio">Tipo de Ocupação</label>
            <asp:TextBox ID="txtNO_PREDIO" ToolTip="Informe o Tipo de Ocupação" runat="server" MaxLength="50" CssClass="campoDescricao"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revTxtNO_PREDIO" runat="server" CssClass="validatorField"
                ControlToValidate="txtNO_PREDIO" ValidationExpression="^(.|\s){1,50}$"
                ErrorMessage="Tipo de Ocupação não pode ser maior que 50 caracteres" Text="*" Display="Dynamic" SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvTxtNO_PREDIO" runat="server" ControlToValidate="txtNO_PREDIO" CssClass="validatorField"
                ErrorMessage="Tipo de Ocupação é requerido" Text="*" Display="Dynamic" SetFocusOnError="true">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>