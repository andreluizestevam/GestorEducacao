<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0906_TipoPessoa.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 250px; } 
        
        /*--> CSS LIs */
        .liCodigoTP { width: 80px; clear: both;}
        .liNomeTP { width: 250px; clear: both; }        
        .liddlSituacaoTP { width: 70px; clear: both; }
        
        /*--> CSS DADOS */
        .labelPixel{ margin-bottom: 1px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="liCodigoTP">
            <label for="txtCodigoTP" class="lblObrigatorio" title="Código">Código</label>
            <asp:TextBox ID="txtCodigoTP" runat="server" Style="width:100%" MaxLength="12" ToolTip="Código"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revtxtCodigoTPMaxChars" runat="server" CssClass="validatorField"
            ControlToValidate="txtCodigoTP" Text="*" ValidationExpression="^(.|\s){1,12}$" 
            ErrorMessage="Campo Código não pode ser maior que 12 caracteres" SetFocusOnError="true"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtCodigoTP" runat="server" CssClass="validatorField"
            ControlToValidate="txtCodigoTP" Text="*"  ErrorMessage="Campo Código é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liNomeTP">
            <label for="txtNomeTP" class="lblObrigatorio" title="Nome">Nome</label>
            <asp:TextBox ID="txtNomeTP" runat="server" style="width:100%" MaxLength="40" CssClass="campoDescricao"
                ToolTip="Informe o Nome"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revtxtNomeTPMaxChars" runat="server" CssClass="validatorField"
            ControlToValidate="txtNomeTP" Text="*" ValidationExpression="^(.|\s){1,40}$" 
            ErrorMessage="Campo Nome não pode ser maior que 40 caracteres" SetFocusOnError="true"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtNomeTP" runat="server" CssClass="validatorField"
            ControlToValidate="txtNomeTP" Text="*" 
            ErrorMessage="Campo Nome é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liddlSituacaoTP">
            <label for="ddlSituacaoTP" class="labelPixel" title="Situação">
                Situação</label>
            <asp:DropDownList ID="ddlSituacaoTP" class="selectedRowStyle" style="width:100%;" 
                runat="server" ToolTip="Informe a Situação">                
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>