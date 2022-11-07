<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1902_GrauInstrucao.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 160px; }
        
        /*--> CSS LIs */
        .liClear {clear:both;}
        
        /*--> CSS DADOS */
        .txtSigla { width: 30px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label for="txtCod" title="Código">Código</label>
            <asp:TextBox ID="txtCod" runat="server" Enabled="false" CssClass="txtCod" Text="0"
                ToolTip="Código"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio" title="Descrição">Descrição</label>
            <asp:TextBox ID="txtDescricao" runat="server" MaxLength="40" CssClass="campoDescricao"
                ToolTip="Informe a Descrição"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revTxtDescricaoMaxChars" runat="server" CssClass="validatorField"
            ControlToValidate="txtDescricao" Text="*"
            ValidationExpression="^(.|\s){1,40}$" 
            ErrorMessage="Campo Descrição não pode ser maior que 40 caracteres" SetFocusOnError="true"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvTxtDescricao" runat="server" CssClass="validatorField"
            ControlToValidate="txtDescricao" Text="*" 
            ErrorMessage="Campo Descrição é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="txtSigla" class="lblObrigatorio" title="Sigla">Sigla</label>
            <asp:TextBox ID="txtSigla" CssClass="txtSigla" runat="server" MaxLength="3"
                ToolTip="Informe a Sigla">
            </asp:TextBox>
        </li>
    </ul>
</asp:Content>