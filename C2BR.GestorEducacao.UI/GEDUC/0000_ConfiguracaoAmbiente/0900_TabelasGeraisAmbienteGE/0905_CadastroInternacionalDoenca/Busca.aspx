<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0905_CadastroInternacionalDoenca.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtCID" title="Código da CID">
                CID</label>
            <asp:TextBox ID="txtCID" ToolTip="Pesquise pelo Código do CID" runat="server" CssClass="campoCodigo"
                MaxLength="12"></asp:TextBox>
        </li>
        <li>
            <label for="txtDoencaCID" title="Descrição da Doença">
                Descrição da Doença</label>
            <asp:TextBox ID="txtDoencaCID" ToolTip="Pesquise pela Descrição da Doença" runat="server"
                CssClass="campoDescricao" MaxLength="80"></asp:TextBox>
        </li>
        <li>
            <label title="Pesquise pela CID Geral">
                CID Geral</label>
            <asp:DropDownList runat="server" ID="ddlCIDGeral" ToolTip="Pesquise pelo CID Geral" Width="170px"/>
        </li>
        <li>
            <label title="Situação da CID">
                Situação</label>
            <asp:DropDownList runat="Server" ID="ddlSituacao" Width="110px" ToolTip="Situação do CID">
                <asp:ListItem Value="A" Text="Ativo" Selected="true"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
