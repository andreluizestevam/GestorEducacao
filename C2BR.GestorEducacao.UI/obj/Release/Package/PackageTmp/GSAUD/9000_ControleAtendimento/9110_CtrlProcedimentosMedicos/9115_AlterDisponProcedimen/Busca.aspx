﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9115_AlterDisponProcedimen.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style type="text/css">        
        /*--> CSS DADOS */
        .txtCnpj { width: 102px; }
        .txtNomeUnidade { width: 210px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtCnpj" title="CNPJ">
                CNPJ</label>
            <asp:TextBox ID="txtCnpj" ToolTip="Informe o CNPJ" CssClass="txtCnpj" runat="server"></asp:TextBox>
        </li>
        <li>
            <label for="txtNomeUnidade" title="Nome da Escola">
                Nome da Unidade</label>
            <asp:TextBox ID="txtNomeUnidade" ToolTip="Informe o Nome da Escola" CssClass="txtNomeUnidade"
                runat="server"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>