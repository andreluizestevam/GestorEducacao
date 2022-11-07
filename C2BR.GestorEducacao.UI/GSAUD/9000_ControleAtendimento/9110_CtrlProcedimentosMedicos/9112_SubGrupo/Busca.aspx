<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9112_SubGrupo.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            margin: 60px 0 0 20px;
        }
        .ulDados li
        {
            margin: 5px 0 0 5px;
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
            <label title="Pesquise pelo nome do SubGrupo de procedimentos Médicos">
                Nome SubGrupo</label>
            <asp:TextBox runat="server" ID="txtNoSubGrupo" Width="200px" MaxLength="100" ToolTip="Pesquise pelo nome do SubGrupo de procedimentos Médicos"></asp:TextBox>
        </li>
        <li style="clear:both">
            <label title="Pesquise pelo Grupo de Procedimentos Médicos">Grupo</label>
            <asp:DropDownList runat="server" ID="ddlGrupo" Width="160px" ToolTip="Pesquise pelo Grupo de Procedimentos Médicos"></asp:DropDownList>
        </li>
        <li style="clear:both">
            <label>Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="80px">
                <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
