<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" 
    Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0911_SistemPublicAcessoFacil.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS Dados */
        .ddlTipoConexao { width: 95px; }
        .txtSisteServi { width: 275px; }
        .ddlStatus { width: 60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlTipoConexao" title="Tipo de Conexão">Tipo</label>
        <asp:DropDownList ID="ddlTipoConexao" ToolTip="Selecione o tipo de conexão" runat="server" CssClass="ddlTipoConexao">
            <asp:ListItem Text="Todos" Value="T"></asp:ListItem>
            <asp:ListItem Text="Acesso Fácil" Value="A"></asp:ListItem>
            <asp:ListItem Text="Sistema Público" Value="S"></asp:ListItem>
        </asp:DropDownList>
    </li>    
    <li>
        <label for="txtSisteServi" title="Sistema / Serviço">Sistema / Serviço</label>
        <asp:TextBox ID="txtSisteServi" ToolTip="Informe o Sistema / Serviço" runat="server" MaxLength="50" CssClass="txtSisteServi"></asp:TextBox>
    </li>
    <li>
        <label for="ddlStatus" title="Status">Status</label>
        <asp:DropDownList ID="ddlStatus" ToolTip="Selecione o status" runat="server" CssClass="ddlStatus">
            <asp:ListItem Text="Todos" Value="T"></asp:ListItem>
            <asp:ListItem Text="Ativa" Value="A"></asp:ListItem>
            <asp:ListItem Text="Inativa" Value="I"></asp:ListItem>
        </asp:DropDownList>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>