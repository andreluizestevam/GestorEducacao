﻿<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="FichaInformUnidadeEscola.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.Relatorios.FichaInformUnidadeEscola" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 225px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 225px;            
        }             
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>
     </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>