<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="RelacCID.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0999_Relatorios.RelacCID" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 200px;
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
                CID Geral
            </label>
            <asp:DropDownList runat="server" ID="ddlCidGeral" Width="160px" ToolTip="Selecione o CID Geral">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Situação</label>
            <asp:DropDownList runat="server" Width="110px" ID="ddlSituacao" ToolTip="Selecione a Situação que deseja visualizar no Relatório">
                <asp:ListItem Value="A" Text="Ativo" Selected="True"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Ordenado Por</label>
            <asp:DropDownList runat="server" ID="ddlOrdPor" Width="90px" ToolTip="Selecione a Ordenação que deseja para o Relatório">
            <asp:ListItem Value="N" Text="Nome" Selected="True"></asp:ListItem>
            <asp:ListItem Value="C" Text="Código"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
