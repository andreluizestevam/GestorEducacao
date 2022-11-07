<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1332_CadastramentoPesquisaInst.Busca"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlTipoAvaliacao" title="Tipo de Avaliação">
                Grupo de Questões
            </label>
            <asp:DropDownList ID="ddlTipoAvaliacao" runat="server" CssClass="campoDescricao"
                ToolTip="Selecione o Tipo de Avaliação">
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtTituloAvaliacao" title="Título da Avaliação">
                Nome da Questões
            </label>
            <asp:TextBox ID="txtTituloAvaliacao" runat="server" MaxLength="60" CssClass="campoDescricao"
                ToolTip="Informe o Título da Avaliação">
            </asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
