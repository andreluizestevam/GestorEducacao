﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="MapaMensalFreqPaciente.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios.MapaMensalFreqPaciente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 400px;
            margin-top: 50px;
            margin-left: 360px;
        }
        input
        {
            height: 13px;
        }
        .ulDados li label
        {
            margin-bottom: 1px;
        }
        .ulDados li
        {
            margin-top: 6px;
        }
        .liboth
        {
            clear: both;
        }
        .ddlPaciente
        {
            width: 280px;
            clear: both;
        }
        
        .ddlUnidade
        {
            width: 280px;
            clear: both;
        }
        .ddlReg
        {
            width: 90px;
            clear: both;
        }
        .ddlArea
        {
            width: 130px;
            clear: both;
        }
        .ddlSubArea
        {
            width: 150px;
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="liboth">
            <label>
                Unidade</label>
            <asp:DropDownList runat="server" class="ddlUnidade" ID="ddlUnidade" ToolTip="Selecine a Unidade Solicitante">
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <label>
                Classificação funcional</label>
            <asp:DropDownList runat="server" Style="width: 125px" class="ddlClassFuncional" ID="ddlClassFuncional"
                OnSelectedIndexChanged="ddlClassFuncional_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <label>
                Profissional
            </label>
            <asp:DropDownList runat="server" Style="width: 260px" class="ddlArea" ID="ddlProfissional">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfv1" ControlToValidate="ddlProfissional"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both">
            <label>
                Ano</label>
            <asp:TextBox ID="txtAno" Style="width: 30px;" MaxLength="4" runat="server"></asp:TextBox>
        </li>
        <li>
            <label>
                Mês</label>
            <asp:DropDownList ID="ddlMes" Style="width: 70px;" runat="server">
                <asp:ListItem Value="1" Text="Janeiro"></asp:ListItem>
                <asp:ListItem Value="2" Text="Fevereiro"></asp:ListItem>
                <asp:ListItem Value="3" Text="Março"></asp:ListItem>
                <asp:ListItem Value="4" Text="Abril"></asp:ListItem>
                <asp:ListItem Value="5" Text="Maio"></asp:ListItem>
                <asp:ListItem Value="6" Text="Junho"></asp:ListItem>
                <asp:ListItem Value="7" Text="Julho"></asp:ListItem>
                <asp:ListItem Value="8" Text="Agosto"></asp:ListItem>
                <asp:ListItem Value="9" Text="Setembro"></asp:ListItem>
                <asp:ListItem Value="10" Text="Outubro "></asp:ListItem>
                <asp:ListItem Value="11" Text="Novembro"></asp:ListItem>
                <asp:ListItem Value="12" Text="Dezembro"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>