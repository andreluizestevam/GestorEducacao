<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2906_MotivosTrancamentoMatricula.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 305px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }  	
        
        /*--> CSS DADOS */
        .txtDescricao { width: 300px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">    
        <li class="liClear">
            <label for="txtDescricao" title="Descrição" class="lblObrigatorio">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Descrição do Motivo" runat="server" MaxLength="60" CssClass="txtDescricao"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ter no máximo 60 caracteres" CssClass="validatorField"
                ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
