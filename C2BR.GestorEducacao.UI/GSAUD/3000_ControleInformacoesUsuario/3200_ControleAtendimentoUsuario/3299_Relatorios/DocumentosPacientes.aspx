<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="DocumentosPacientes.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios.DocumentosPacientes" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 260px; }
        .ddlPadrao
        {
            width: 235px;
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liboth">
            <asp:Label runat="server" ID="lblUnidadeCadastro">Unidade de Cadastro</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="ddlUnidadeCadastro" OnSelectedIndexChanged="dllUnidade_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecine a unidade desejada" />
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblUnidadeLotacao">Unidade Lotação/Contrato</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="ddlUnidadeContrato" OnSelectedIndexChanged="dllUnidade_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecine a unidade desejada" />
        </li>
        <li style="clear: both;">
            <label>Paciente</label>
            <asp:DropDownList runat="server" ID="ddlPaciente" class="ddlPadrao" AutoPostBack="true" OnSelectedIndexChanged="ddlPaciente_OnSelectedIndexChanged" />
        </li>
        <li style="clear: both">
            <label>Operadora</label>
            <asp:DropDownList runat="server" ID="ddlOperadora" Width="100" ToolTip="Selecione a operadora" />
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
