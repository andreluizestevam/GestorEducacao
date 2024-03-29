﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5912_TipoDevolucDocto.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS Dados */
        .txtDescricao { width: 250px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtDescricao">Descrição</label>
            <asp:TextBox ID="txtDescricao" runat="server" CssClass="txtDescricao" MaxLength="60"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
