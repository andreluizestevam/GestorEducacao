﻿<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4900_TabelasGenerCtrlOperBiblioteca.F4903_InformacaoEditora.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 200px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        .txtDescricao { width: 200px; }
        .labelPixel { margin-bottom: 1px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">     
        <li class="liClear">
            <label for="txtDescricao" title="Descrição" class="lblObrigatorio labelPixel">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe a Descrição" CssClass="txtDescricao" runat="server" MaxLength="30"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ter no máximo 30 caracteres" ValidationExpression="^(.|\s){1,60}$"
                CssClass="validatorField">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
