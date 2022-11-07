<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="RelacEquipeCampanha.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9199_Relatorios.RelacEquipeCampanha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 200px;
        }
        .ulDados li
        {
            margin:5px 0 0 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label>
                Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnid" Width="180px" ToolTip="Unidade da Campanha de Saúde">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label class="lblObrigatorio" title="Tipo da Campanha de Saúde">
                Tipo</label>
            <asp:DropDownList runat="server" ID="ddlTipoCamp" ToolTip="Tipo da Campanha de Saúde"
                Width="124px">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>
                Período</label>
            <asp:TextBox ID="txtDataIniCamp" Style="margin: 0px !important;" runat="server" CssClass="campoData"
                ToolTip="Data de início para pesquisa de Campanhas de Saúde">
            </asp:TextBox>
            <asp:Label runat="server" ID="lbl"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" ID="txtDataFimCamp" CssClass="campoData" ToolTip="Data de término para pesquisa de Campanhas de Saúde"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
