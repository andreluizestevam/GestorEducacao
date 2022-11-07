<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0200_ConfiguracaoModulos.F0202_CadastraFuncionalidades.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlFuncionalidade" title="Grupo de Informação">Grupo de Informação</label>
            <asp:DropDownList ID="ddlFuncionalidade" runat="server" 
                AutoPostBack="true" Width="290px" ToolTip="Selecione um Grupo de Informação">
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtNomeFuncionalidade" title="Nome da Funcionalidade">Nome da Funcionalidade</label>
            <asp:TextBox ID="txtNomeFuncionalidade" runat="server" Width="290px" ToolTip="Digite o nome da funcionalidade"></asp:TextBox>
        </li>
        <li>
            <label for="ddlStatusFunc" title="Status">Status</label>
            <asp:DropDownList ID="ddlStatusFunc" Width="60px" runat="server" ToolTip="Status da funcionalidade">
                <asp:ListItem Text="Todos" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Ativo" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
