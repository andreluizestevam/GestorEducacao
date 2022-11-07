<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2907_ClassifSerie.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 250px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }  	
        
        /*--> CSS DADOS */
        .txtDescricao { width: 250px; }
        .txtSigla { width: 20px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">  
        <li>
            <label for="txtSigla" title="Sigla">
                Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Sigla da Classifica��o da S�rie" runat="server" MaxLength="2" CssClass="txtSigla"></asp:TextBox>
        </li>  
        <li class="liClear">
            <label for="txtDescricao" title="Descri��o" class="lblObrigatorio">
                Descri��o</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Descri��o da Classifica��o da S�rie" runat="server" MaxLength="40" CssClass="txtDescricao"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descri��o deve ter no m�ximo 40 caracteres" CssClass="validatorField"
                ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descri��o da Classifica��o da S�rie deve ser informada" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>        
    </ul>
</asp:Content>
