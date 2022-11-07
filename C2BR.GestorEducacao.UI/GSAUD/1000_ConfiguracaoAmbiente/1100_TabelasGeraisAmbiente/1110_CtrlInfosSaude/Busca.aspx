﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1110_CtrlInfosSaude.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 270px;
            margin-top: 20px;
        }
        .ulDados li
        {
            margin-bottom: 5px;
        }
        .ulDados li label
        {
            margin-bottom: 2px;
        }
        .top
        {
            margin-top: 4px;
        }
        input
        {
            height: 13px;
        }
        .ddlReg
        {
            width: 150px;
            clear: both;
        }
        .txtNomeFeriado
        {
            width: 240px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtCodigo" class="" title="Sigla ">
                Sigla
            </label>
            <asp:TextBox ID="txtSigla" Style="width: 43px" ToolTip="Sigla" MaxLength="6" runat="server"></asp:TextBox>
        </li>
        <li class="liOcorrencia">
            <label for="NomeFeriado" title="Nome">
                Nome
            </label>
            <asp:TextBox ID="txtNome" runat="server" MaxLength="100" ToolTip="Nome" 
                CssClass="txtNomeFeriado"></asp:TextBox>
        </li>
        <li>
            <label for="txtCodigo" class="" title="Sigla ">
                Descrição
            </label>
            <asp:TextBox ID="txtDescricao"  style="width:240px; height:40px"  
                ToolTip="Descricao" MaxLength="400"
                runat="server" TextMode="MultiLine"></asp:TextBox>
        </li>
        <li class="top">
            <label title="Status">
                Situação
            </label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="70px" ToolTip="Situação de cadastro">
                <asp:ListItem Text="Todos" Value="T" Selected="True"> </asp:ListItem>
                <asp:ListItem Text="Ativo" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>