<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ExtratoPreAtendimento.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios.ExtratoPreAtendimento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
        }
        .ulDados li
        {
            margin: 5px 0 0 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label>
                Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnidade" ToolTip="Selecione a Unidade" Width="200px">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Especilidade</label>
            <asp:DropDownList runat="server" ID="ddlEspec" ToolTip="Selecione a Especialidade"
                Width="160px">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Classf. Risco</label>
            <asp:DropDownList runat="server" ID="ddlClassRisco" Width="110px" ToolTip="Selecione a Classificação de Risco">
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
