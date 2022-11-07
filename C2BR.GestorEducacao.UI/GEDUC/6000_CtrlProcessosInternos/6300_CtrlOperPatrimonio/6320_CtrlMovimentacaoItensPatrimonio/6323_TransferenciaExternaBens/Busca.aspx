<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6320_CtrlMovimentacaoItensPatrimonio.F6323_TransferenciaExternaBens.Busca"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlUnidade">
                Unidade de Origem
            </label>
            <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar" ToolTip="Selecione a Unidade de Origem" 
                AutoPostBack="True" onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList> 
        </li>
        <li>
            <label for="ddlPatrimonio">Patrimônio</label>
            <asp:DropDownList ID="ddlPatrimonio" runat="server" ToolTip="Selecione o Patrimônio">
            </asp:DropDownList>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
