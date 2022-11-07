<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0905_CadastroInternacionalDoencaGeral.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtCID" title="Código do CID">
                CID</label>
            <asp:TextBox ID="txtCID" ToolTip="Pesquise pelo Código do CID" runat="server" CssClass="campoCodigo"
                MaxLength="3"></asp:TextBox>
        </li>
        <li>
            <label for="txtDoencaCID" title="Descrição da Doença">
                Descrição da Doença</label>
            <asp:TextBox ID="txtDoencaCID" ToolTip="Pesquise pela Descrição da Doença" runat="server"
                CssClass="campoDescricao" MaxLength="100"></asp:TextBox>
        </li>
        <li>
            <label title="Pesquise pela Sigla">
                Sigla</label>
            <asp:TextBox runat="server" ID="txtSigla" ToolTip="Pesquise pela Sigla"
                Width="70px" MaxLength="20" />
        </li>
        <li>
            <label title="Situação do CID">
                Situação</label>
            <asp:DropDownList runat="Server" ID="ddlSituacao" Width="100px" ToolTip="Situação do CID">
                <asp:ListItem Value="A" Text="Ativo" Selected="true"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
