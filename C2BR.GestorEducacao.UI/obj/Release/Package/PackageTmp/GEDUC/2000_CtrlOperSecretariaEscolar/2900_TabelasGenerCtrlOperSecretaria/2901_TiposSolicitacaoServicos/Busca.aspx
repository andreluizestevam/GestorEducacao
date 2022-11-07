<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2901_TiposSolicitacaoServicos.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
 <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlGrupoTipo" >
                Grupo Item Solicitação</label>
            <asp:DropDownList ID="ddlGrupoTipo" CssClass="campoGrupoTipo" ToolTip="Selecione a Grupo Item Solicitação" Width="240px" runat="server">
            </asp:DropDownList>          
        </li>
        <li>
            <label for="txtTipoSolicitacao">Tipo de Solicitação</label>
            <asp:TextBox ID="txtTipoSolicitacao" runat="server" CssClass="campoDescricao"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
