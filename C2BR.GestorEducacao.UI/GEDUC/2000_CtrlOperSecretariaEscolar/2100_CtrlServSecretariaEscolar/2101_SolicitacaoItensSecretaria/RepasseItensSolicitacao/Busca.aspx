<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2101_SolicitacaoItensSecretaria.RepasseItensSolicitacao.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
     /*-- CSS DADOS */
    .ddlTipo{ width: 80px;}
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlTipo">Tipo</label>
            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="ddlTipo" 
                AutoPostBack="true" onselectedindexchanged="ddlTipo_SelectedIndexChanged">
                <asp:ListItem Value="E">Envio</asp:ListItem>
                <asp:ListItem Value="R">Recebimento</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label id="lblUnidade" runat="server" for="ddlUnidade">Unidade de Destino</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar">
            </asp:DropDownList>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
