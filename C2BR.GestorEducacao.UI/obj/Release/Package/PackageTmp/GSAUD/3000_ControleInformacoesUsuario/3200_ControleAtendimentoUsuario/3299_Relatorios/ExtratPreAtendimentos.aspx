<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ExtratPreAtendimentos.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios.ExtratPreAtendimentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 250px;
            margin: 50px 0 0 347px !important;
        }
        .ulDados li
        {
            margin-left: 5px;
            margin-bottom: 5px;
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
    </style>
    <ul class="ulDados">
        <li>
            <label>
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" Style="width: 240px;">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>
                Classe de risco</label>
            <asp:DropDownList ID="ddlClasseDeRisco" runat="server" Style="width: 100px;">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Especialidade
            </label>
            <asp:DropDownList ID="ddlEspecialidade" runat="server" Style="width: 220px;">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Sexo
            </label>
            <asp:DropDownList ID="ddlSexo" runat="server" Style="width: 100px;">
                <asp:ListItem Value="">Todos..</asp:ListItem>
                <asp:ListItem Value="M">Masculino</asp:ListItem>
                <asp:ListItem Value="F">Ferminino</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Período</label>
            <asp:TextBox ID="txtDataInicial" CssClass="campoData" runat="server"></asp:TextBox>
            à
            <asp:TextBox ID="txtDataFinal" CssClass="campoData" runat="server"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
