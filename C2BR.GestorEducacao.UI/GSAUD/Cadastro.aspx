<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD.Cadastro1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:textbox runat="server" ID="plsql" TextMode="MultiLine" Height="75px" Width="347px"></asp:textbox>
    <br /><br />
    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
    <br />
    <asp:Button runat="server" ID="btnsql" Text="pesquisar" OnClick="btnsql_Click" />
</asp:Content>
