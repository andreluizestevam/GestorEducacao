<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3207_DocumentacaoPaciente._TiposDocumentos.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 200px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }        
        
        /*--> CSS DADOS */
        .txtDescricao { width: 200px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio" title="Descrição">Descrição</label>
            <asp:TextBox ID="txtDescricao" runat="server" MaxLength="40" CssClass="txtDescricao" ToolTip="Informe a Descrição"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDescricao" ErrorMessage="Descrição deve ter no máximo 40 caracteres" CssClass="validatorField" ValidationExpression="^(.|\s){1,40}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescricao" ErrorMessage="Descrição deve ser informada" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear"> 
            <label for="txtSigla" class="lblObrigatorio" title="Sigla">Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" MaxLength="3" CssClass="campoSigla" ToolTip="Informe a Sigla"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSigla" ErrorMessage="Sigla deve ser informada" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
