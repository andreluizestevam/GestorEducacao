<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0916_GeraTxtExport.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados
    {
        width:200px;
    }
    .ulDados li
    {
        margin-top:5px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul class="ulDados">
    <li>
        <label>Escolha a tabela à Exportar</label>
        <asp:DropDownList runat="server" ID="ddlTab" Width="200px">
            <asp:ListItem Value="A" Text="Aluno"></asp:ListItem>
            <asp:ListItem Value="R" Text="Responsável"></asp:ListItem>
            <asp:ListItem Value="M" Text="Matrícula"></asp:ListItem>
            <asp:ListItem Value="H" Text="Histórico"></asp:ListItem>
            <asp:ListItem Value="F" Text="Financeiro"></asp:ListItem>
            <asp:ListItem Value="T" Text="Atividades"></asp:ListItem>
            <asp:ListItem Value="Q" Text="Frequência"></asp:ListItem>
        </asp:DropDownList>
    </li>
    <li>
        <asp:TextBox runat="server" ID="txtCami" Width="300px" Text="C:\" ToolTip="Local onde o arquivo será salvo"></asp:TextBox>
    </li>
</ul>
</asp:Content>
