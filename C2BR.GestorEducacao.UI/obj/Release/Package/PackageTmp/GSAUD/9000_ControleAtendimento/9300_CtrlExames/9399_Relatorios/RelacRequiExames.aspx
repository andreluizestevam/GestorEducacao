<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="RelacRequiExames.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9399_Relatorios.RelacRequiExames" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label>
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" Style="width: 240px;">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>
                Especialidade</label>
            <asp:DropDownList ID="ddlEspecialidade" runat="server" Style="width: 200px;">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Exame</label>
            <asp:DropDownList ID="ddlExame" runat="server" Style="width: 220px;">
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
