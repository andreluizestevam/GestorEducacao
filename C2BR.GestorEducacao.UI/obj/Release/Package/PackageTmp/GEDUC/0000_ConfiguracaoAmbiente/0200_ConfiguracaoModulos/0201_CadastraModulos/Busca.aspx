<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0200_ConfiguracaoModulos.F0201_CadastraModulos.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlModulo" title="Módulo Pai" >Módulo Pai</label>
            <asp:DropDownList ID="ddlModulo" runat="server" CssClass="campoModalidade" ToolTip="Selecione um Módulo Pai"
                AutoPostBack="true" Width="290px">
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtNomeModuloPai" title="Nome do Módulo">Nome do Módulo</label>
            <asp:TextBox ID="txtNomeModulo" runat="server" Width="290px" ToolTip="Digite o nome do módulo"></asp:TextBox>
        </li>
        <li>
            <label for="ddlStatusMod" title="Status">Status</label>
            <asp:DropDownList ID="ddlStatusMod" Width="60px" runat="server" ToolTip="Status do módulo pai">
                <asp:ListItem Text="Todos" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Ativo" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
