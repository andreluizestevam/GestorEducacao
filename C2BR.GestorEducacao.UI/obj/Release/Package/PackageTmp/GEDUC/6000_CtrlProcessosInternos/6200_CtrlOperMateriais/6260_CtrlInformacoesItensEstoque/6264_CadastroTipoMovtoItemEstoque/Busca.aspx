<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6260_CtrlInformacoesItensEstoque.F6264_CadastroTipoMovtoItemEstoque.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtDescricao" title="Descrição do Tipo de Movimento">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" runat="server" ToolTip="Pesquise pela Descrição do Tipo de Movimento" CssClass="campoDescricao"  MaxLength="40"></asp:TextBox>
        </li>
        <li>
            <label for="txtDescricao" title="Tipo de Movimento">
                Tipo Movimento</label>
            <asp:DropDownList ID="ddlTipoMovimento" runat="server" ToolTip="Pesquise pelo Tipo de Movimento"   >
                <asp:ListItem Text="Todos" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Entrada" Value="E"></asp:ListItem>
                <asp:ListItem Text="Saida" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
