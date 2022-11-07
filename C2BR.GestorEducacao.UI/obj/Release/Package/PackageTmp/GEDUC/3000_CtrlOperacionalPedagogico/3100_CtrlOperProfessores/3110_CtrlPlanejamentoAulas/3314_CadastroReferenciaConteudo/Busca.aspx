<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" 
    Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3100_CtrlOperProfessores.F3110_CtrlPlanejamentoAulas.F3314_CadastroReferenciaConteudo.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS Dados */
        .txtTitulConte { width: 275px; }
        .ddlTipoConte { width: 100px; }
        .ddlNivelConte { width: 85px; }
        .ddlStatus  { width: 60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">        
    <li>
        <label for="txtTitulConte" title="Título do Conteúdo">Título do Conteúdo</label>
        <asp:TextBox ID="txtTitulConte" ToolTip="Informe o Título do Conteúdo" runat="server" MaxLength="80" CssClass="txtTitulConte"></asp:TextBox>
    </li>
    <li>
        <label for="ddlNivelConte" title="Nível do Conteúdo">Nível</label>
        <asp:DropDownList ID="ddlNivelConte" ToolTip="Selecione o Nível do Conteúdo" runat="server" CssClass="ddlNivelConte">
            <asp:ListItem Text="Todos" Value="T"></asp:ListItem>
            <asp:ListItem Text="Fácil" Value="F"></asp:ListItem>
            <asp:ListItem Text="Médio" Value="M"></asp:ListItem>
            <asp:ListItem Text="Difícil" Value="D"></asp:ListItem>
            <asp:ListItem Text="Avançado" Value="A"></asp:ListItem>
            <asp:ListItem Text="Sem Registro " Value="S"></asp:ListItem>
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlTipoConte" title="Tipo do Conteúdo">Tipo</label>
        <asp:DropDownList ID="ddlTipoConte" ToolTip="Selecione o status" runat="server" CssClass="ddlTipoConte">
            <asp:ListItem Text="Todos" Value="T"></asp:ListItem>
            <asp:ListItem Text="Bibliográfico" Value="B"></asp:ListItem>
            <asp:ListItem Text="Conteúdo Escolar" Value="C"></asp:ListItem>
        </asp:DropDownList>
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