<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1106_CtrlPlanos.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .ulDados
        {
            width: 250px;
            margin:40px 0 0 40px !important;
        }
        .ulDados li
        {
            margin-top: 5px;
        }
        .ddlReg
        {
            width: 100px;
            clear: both;
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
   <ul class="ulDados">
    <li>
            <label title="Operadora">
                Operadora</label>
            <asp:DropDownList ID="ddlOperadora" CssClass="ddlReg" runat="server">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Pesquise pelo nome da Operadora">
                Nome</label>
            <asp:TextBox runat="server" ID="txtNomePlano" Width="180px" ToolTip="Pesquise pelo nome dpo Plano"></asp:TextBox>
        </li>
        <li>
              <label title="Sigla" class="lblObrigatorio">
                Sigla</label>
            <asp:TextBox runat="server" ID="txtSigla" Width="40px" ToolTip="Sigla" MaxLength="6"></asp:TextBox>
        </li>
        <li style="clear:both">
            <label title="Pesquise pela situação da Operadora">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSitu" Width="70px" ToolTip="Pesquise pela situação da Operadora">
                <asp:ListItem Text="Todas" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Ativo" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
