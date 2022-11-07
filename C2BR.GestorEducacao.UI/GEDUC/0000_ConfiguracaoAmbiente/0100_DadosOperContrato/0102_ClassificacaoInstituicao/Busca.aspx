﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0100_DadosOperContrato.F0102_ClassificacaoInstituicao.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtNomeClass" title="Descrição da Classificação da Instituição" >Descrição</label>
        <asp:TextBox ID="txtNomeClass" ToolTip="Pesquise por uma Descrição da Classificação da Instituição" CssClass="campoDescricao" runat="server" MaxLength="40"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
