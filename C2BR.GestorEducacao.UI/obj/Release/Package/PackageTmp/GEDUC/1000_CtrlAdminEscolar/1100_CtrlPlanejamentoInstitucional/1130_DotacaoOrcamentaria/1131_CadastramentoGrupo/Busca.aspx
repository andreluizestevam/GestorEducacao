﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1130_DotacaoOrcamentaria.F1131_CadastramentoGrupo.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtGrupo" title="Grupo" >Grupo</label>
        <asp:TextBox ID="txtGrupo" ToolTip="Pesquise pelo Grupo" runat="server" MaxLength="80" CssClass="campoDescricao"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>