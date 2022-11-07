﻿<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="AlunosPorCidadeBairro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3699_Relatorios.AlunosPorCidadeBairro" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 480px; }
        .liCidade,.liBairro
        {
            margin-top: 5px;
            width: 250px;
        }
        .liUnidade, .liUF
        {
            clear: both;
            margin-top:5px;
        }
        .liCidade
        {
        	margin-top: 5px;
        	margin-left: 10px;        	
        	width:220px;
        }
        .liBairro
        {
        	margin-top: 5px;        	
        	margin-left: 10px;
        	width:180px;
        }
        .liTpRelatorio 
        {
        	display:inline;
        	margin-left:10px;
        	margin-top:5px;
        }
        .ddlTpRelatorio { width:95px; }       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label class="lblObrigatorio" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
        </li>
        <li class="liTpRelatorio">
            <label class="lblObrigatorio" title="Tipo de Relatório">
                Tipo de Relatório</label>
            <asp:DropDownList ID="ddlTpRelatorio" ToolTip="Selecione um Tipo de Relatório" CssClass="ddlTpRelatorio" runat="server">
            <asp:ListItem Value="M" Selected="True">Matriculado</asp:ListItem>
            <asp:ListItem Value="N">Não Matriculado</asp:ListItem>
            <asp:ListItem Value="T">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liUF">
            <label class="lblObrigatorio" title="UF">
                UF</label>               
            <asp:DropDownList ID="ddlUF" ToolTip="Selecione uma UF" CssClass="ddlUF" runat="server" 
                onselectedindexchanged="ddlUF_SelectedIndexChanged1" AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li class="liCidade">
            <label class="lblObrigatorio" title="Cidade">
                Cidade</label>               
            <asp:DropDownList ID="ddlCidade" ToolTip="Selecione uma Cidade" CssClass="ddlCidade" runat="server" 
                onselectedindexchanged="ddlCidade_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li id="liBairro" class="liBairro" title="Bairro">
            <label class="lblObrigatorio">
                Bairro</label>               
            <asp:DropDownList ID="ddlBairro" ToolTip="Selecione um Bairro" CssClass="ddlBairro" runat="server" AutoPostBack="True">
            </asp:DropDownList>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>