﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6500_CtrlOperInfraestrutura.F6590_TabelasGenerCtrlOperInfraestrutura.CadastramentoTipoCobertura.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtDescricao"  title="Tipo de Cobertura da Efidicação">
                Tipo de Cobertura da Edificação</label>
            <asp:TextBox ID="txtDescricao"  ToolTip="Pesquise pelo Tipo de Cobertura da Edificação" runat="server" CssClass="campoDescricao"  MaxLength="40"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
