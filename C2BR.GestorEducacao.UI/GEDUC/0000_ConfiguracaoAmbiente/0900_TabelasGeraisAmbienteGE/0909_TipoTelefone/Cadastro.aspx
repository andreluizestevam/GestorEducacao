<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0909_TipoTelefone.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 250px; } 
        
        /*--> CSS LIs */
        .liCodigoTT { width: 80px; clear: both;}
        .liNomeTT { width: 250px; clear: both; }        
        .liddlSituacaoTT { width: 70px; clear: both; }
        
        /*--> CSS DADOS */
        .labelPixel{ margin-bottom: 1px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="liCodigoTT">
            <label for="txtCodigoTT" class="lblObrigatorio" title="Código">Código</label>
            <asp:TextBox ID="txtCodigoTT" runat="server" Style="width:100%" MaxLength="12" ToolTip="Código"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revtxtCodigoTTMaxChars" runat="server" CssClass="validatorField"
            ControlToValidate="txtCodigoTT" Text="*" ValidationExpression="^(.|\s){1,12}$" 
            ErrorMessage="Campo Código não pode ser maior que 12 caracteres" SetFocusOnError="true"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtCodigoTT" runat="server" CssClass="validatorField"
            ControlToValidate="txtCodigoTT" Text="*"  ErrorMessage="Campo Código é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liNomeTT">
            <label for="txtNomeTT" class="lblObrigatorio" title="Nome">Nome</label>
            <asp:TextBox ID="txtNomeTT" runat="server" style="width:100%" MaxLength="40" CssClass="campoDescricao"
                ToolTip="Informe o Nome"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revtxtNomeTTMaxChars" runat="server" CssClass="validatorField"
            ControlToValidate="txtNomeTT" Text="*" ValidationExpression="^(.|\s){1,40}$" 
            ErrorMessage="Campo Nome não pode ser maior que 40 caracteres" SetFocusOnError="true"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtNomeTT" runat="server" CssClass="validatorField"
            ControlToValidate="txtNomeTT" Text="*" 
            ErrorMessage="Campo Nome é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liddlSituacaoTT">
            <label for="ddlSituacaoTT" class="labelPixel" title="Situação">
                Situação</label>
            <asp:DropDownList ID="ddlSituacaoTT" class="selectedRowStyle" style="width:100%;" 
                runat="server" ToolTip="Informe a Situação">                
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>