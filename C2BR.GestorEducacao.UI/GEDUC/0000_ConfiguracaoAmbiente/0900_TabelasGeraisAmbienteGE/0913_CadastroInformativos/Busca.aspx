<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" 
    Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0913_CadastroInformativos.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS Dados */
        .ddlTipoUsuar { width: 95px; }
        .txtTitulPublic { width: 300px; }        
        .ddlStatus { width: 60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlTipoUsuar" title="Tipo de Usuário">Tipo</label>
        <asp:DropDownList ID="ddlTipoUsuar" ToolTip="Selecione o Tipo de Usuário" runat="server" CssClass="ddlTipoUsuar">
            <asp:ListItem Text="Todos" Value="T"></asp:ListItem>
            <asp:ListItem Text="Funcionário" Value="F"></asp:ListItem>
            <asp:ListItem Text="Professor" Value="P"></asp:ListItem>            
        </asp:DropDownList>
    </li>    
    <li>
        <label for="txtTitulPublic" title="Título Publicação">Título Publicação</label>
        <asp:TextBox ID="txtTitulPublic" ToolTip="Informe o Título Publicação" runat="server" MaxLength="60" CssClass="txtTitulPublic"></asp:TextBox>
    </li>    
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>