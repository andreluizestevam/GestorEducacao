<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" 
MasterPageFile="~/App_Masters/PadraoCadastros.Master" Title="Cadastro"
Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0100_DadosOperContrato.F0102_ClassificacaoInstituicao.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .ulDados{ width: 155px; }    
            
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">      
        <li>
            <label for="txtDescricaoClasInst" title="Descri��o da Classifica��o da Institui��o" class="lblObrigatorio">Descri��o</label>
            <asp:TextBox ID="txtDescricaoClasInst" ToolTip="Informe uma Descri��o da Classifica��o da Institui��o" runat="server" CssClass="campoDescricao" MaxLength="30"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricaoClasInst" CssClass="validatorField"
            ErrorMessage="Descri��o deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>                
        </li>
    </ul>
</asp:Content>