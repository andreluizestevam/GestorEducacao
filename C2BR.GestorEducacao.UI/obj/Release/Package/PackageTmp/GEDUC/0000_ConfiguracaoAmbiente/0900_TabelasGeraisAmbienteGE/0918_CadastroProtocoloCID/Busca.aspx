<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0918_CadastroProtocoloCID.Busca" %>

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
        <li style="margin-top: 15px;">
            <label for="lblCID" title="Código do CID">
                CID</label>
            <asp:TextBox ID="txtCID" ToolTip="Pesquise pelo Código do CID" runat="server" CssClass="campoDescricao" MaxLength="30"></asp:TextBox>
        </li>
        <li>
            <label for="lblNome" title="Nome reduzido do protocolo de CID">
                Nome Reduzido</label>
            <asp:TextBox ID="txtNome" ToolTip="Pesquise pelo nome do protocolo CID" runat="server" CssClass="campoDescricao" MaxLength="30"></asp:TextBox>
        </li>
        <%--<li>
            <label title="Tipo">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" runat="server" ToolTip="Pesquise pelo tipo">
                <asp:ListItem Text="Todos" Value="0" Selected="true" />
                <asp:ListItem Text="Consulta" Value="1" />
                <asp:ListItem Text="Exame" Value="2" />
                <asp:ListItem Text="Procedimento" Value="3" />
                <asp:ListItem Text="Vacina" Value="4" />
            </asp:DropDownList>
        </li>--%>
        <li>
            <label title="Situação do protocolo CID">
                Situação</label>
            <asp:DropDownList runat="Server" ID="ddlSituacao" ToolTip="Pesquise pela situação do CID">
                <asp:ListItem Value="T" Text="Todos" Selected="true"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
