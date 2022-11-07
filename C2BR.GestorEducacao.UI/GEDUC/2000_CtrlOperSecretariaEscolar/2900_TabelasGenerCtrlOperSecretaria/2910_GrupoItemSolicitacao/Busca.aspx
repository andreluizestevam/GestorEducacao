<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2900_TabelasGenerCtrlOperSecretaria._2910_GrupoItemSolicitacao.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtTipoSolicitacao">
                Descrição</label>
            <asp:TextBox ID="txtTipoSolicitacao" runat="server" CssClass="campoDescricao"></asp:TextBox>
        </li>
        <li>
            <label for="ddlSituacao">
                Situação</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" CssClass="ddlSituacao">
                <asp:ListItem Value="A" Text="Ativo" />
                <asp:ListItem Value="I" Text="Inativo" />
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
