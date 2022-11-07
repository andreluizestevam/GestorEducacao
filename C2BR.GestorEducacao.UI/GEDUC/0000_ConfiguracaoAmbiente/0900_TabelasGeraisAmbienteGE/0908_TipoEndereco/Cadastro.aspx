<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0908_TipoEndereco.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 250px; } 
        
        /*--> CSS LIs */
        .liCodigoTE { width: 80px; clear: both;}
        .liNomeTE { width: 250px; clear: both; }        
        .liddlSituacaoTE { width: 70px; clear: both; }
        
        /*--> CSS DADOS */
        .labelPixel{ margin-bottom: 1px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="liCodigoTE">
            <label for="txtCodigoTE" class="lblObrigatorio" title="Código">Código</label>
            <asp:TextBox ID="txtCodigoTE" runat="server" Style="width:100%" MaxLength="12" ToolTip="Código"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revtxtCodigoTEMaxChars" runat="server" CssClass="validatorField"
            ControlToValidate="txtCodigoTE" Text="*" ValidationExpression="^(.|\s){1,12}$" 
            ErrorMessage="Campo Código não pode ser maior que 12 caracteres" SetFocusOnError="true"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtCodigoTE" runat="server" CssClass="validatorField"
            ControlToValidate="txtCodigoTE" Text="*"  ErrorMessage="Campo Código é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liNomeTE">
            <label for="txtNomeTE" class="lblObrigatorio" title="Nome">Nome</label>
            <asp:TextBox ID="txtNomeTE" runat="server" style="width:100%" MaxLength="40" CssClass="campoDescricao"
                ToolTip="Informe o Nome"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revtxtNomeTEMaxChars" runat="server" CssClass="validatorField"
            ControlToValidate="txtNomeTE" Text="*" ValidationExpression="^(.|\s){1,40}$" 
            ErrorMessage="Campo Nome não pode ser maior que 40 caracteres" SetFocusOnError="true"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtNomeTE" runat="server" CssClass="validatorField"
            ControlToValidate="txtNomeTE" Text="*" 
            ErrorMessage="Campo Nome é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liddlSituacaoTE">
            <label for="ddlSituacaoTE" class="labelPixel" title="Situação">
                Situação</label>
            <asp:DropDownList ID="ddlSituacaoTE" class="selectedRowStyle" style="width:100%;" 
                runat="server" ToolTip="Informe a Situação">                
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>