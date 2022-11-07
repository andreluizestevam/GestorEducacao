<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0920_CadastroProtocoloAcaoItens.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ddlTipo, .ddlSituacao
        {
            width: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="lblNome" title="Nome do protocolo">
                Protocolo</label>
            <asp:TextBox ID="txtNome" ToolTip="Pesquise pelo nome do protocolo" runat="server"
                CssClass="campoDescricao" MaxLength="30"></asp:TextBox>
        </li>
        <li>
            <label for="lblTipo" title="Tipo do protocolo">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" Width="120px" ToolTip="Escolha o tipo do protocolo"
                runat="server" class="ddltipo">
                <asp:ListItem Text="Todos" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Acomodação" Value="ACO"></asp:ListItem>
                <asp:ListItem Text="Controles Internos" Value="CTI"></asp:ListItem>
                <asp:ListItem Text="Esterilização" Value="EST"></asp:ListItem>
                <asp:ListItem Text="Higienização" Value="HIG"></asp:ListItem>
                <asp:ListItem Text="Lavanderia" Value="LAV"></asp:ListItem>
                <asp:ListItem Text="Manutenção" Value="MAN"></asp:ListItem>
                <asp:ListItem Text="Procedimento" Value="PRO"></asp:ListItem>
                <asp:ListItem Text="Segurança" Value="SEG"></asp:ListItem>
                <asp:ListItem Text="Serviços Camareira" Value="SCA"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label title="Situação do protocolo">
                Situação</label>
            <asp:DropDownList runat="Server" ID="ddlSituacao" ToolTip="Pesquise pela situação">
                <asp:ListItem Value="T" Text="Todos" Selected="true"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
