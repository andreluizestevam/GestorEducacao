<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0200_ConfiguracaoModulos.F0203_CadastroComoFazer.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">      
        <li>
            <label for="ddlFuncionalidade" title="Funcionalidade">Funcionalidade</label>
            <asp:DropDownList ID="ddlFuncionalidadeCF" runat="server" CssClass="campoModalidade" 
                AutoPostBack="true" Width="290px" ToolTip="Selecione uma funcionalidade">
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtNomeFuncionalidadeCF" title="Nome do Item de Próx. Passos">Nome do Item de Próx. Passos</label>
            <asp:TextBox ID="txtNomeFuncionalidadeCF" runat="server" Width="290px" ToolTip="Digite o Nome do Item de Próx. Passos"></asp:TextBox>
        </li>
        <li>
            <label for="ddlStatusCF" title="Status">Status</label>
            <asp:DropDownList ID="ddlStatusCF"  runat="server" ToolTip="Status da funcionalidade">
                <asp:ListItem Text="Todos" Value="T" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Ativo" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
