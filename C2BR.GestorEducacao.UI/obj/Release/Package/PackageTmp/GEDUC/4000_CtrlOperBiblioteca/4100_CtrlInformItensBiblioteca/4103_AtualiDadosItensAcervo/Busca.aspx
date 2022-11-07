<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4100_CtrlInformItensBiblioteca.F4103_AtualiDadosItensAcervo.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">                
        /*--> CSS DADOS */
        .ddlAreaConhecimento{ width: 145px;}
        .ddlClassificacao{ width: 145px;}
        .ddlEditora{ width: 200px;}
        .ddlAutor{ width: 200px;}
        .txtLivro{ width: 257px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
    <ContentTemplate>
    <li>
        <label for="ddlAreaConhecimento" title="Área do Conhecimento">Área do Conhecimento</label>
        <asp:DropDownList ID="ddlAreaConhecimento" AutoPostback="true" ToolTip="Selecione uma Área do Conhecimento" runat="server" CssClass="ddlAreaConhecimento" OnSelectedIndexChanged="ddlAreaConhecimento_SelectedIndexChanged"></asp:DropDownList>
    </li>
    <li>
        <label for="ddlClassificacao" title="Classificação">Classificação</label>
        <asp:DropDownList ID="ddlClassificacao" ToolTip="Selecione uma Classificação" runat="server" CssClass="ddlClassificacao"></asp:DropDownList>
    </li>
    </ContentTemplate>
    </asp:UpdatePanel>
    <li>
        <label for="ddlEditora" title="Editora" >Editora</label>
        <asp:DropDownList ID="ddlEditora" ToolTip="Selecione uma Editora" runat="server" CssClass="ddlEditora"></asp:DropDownList>
    </li>
    <li>
        <label for="ddlAutor" title="Autor">Autor</label>
        <asp:DropDownList ID="ddlAutor" ToolTip="Selecione um Autor" runat="server" CssClass="campoNomePessoa"></asp:DropDownList>
    </li>
    <li>
        <label for="txtLivro" title="Nome do Livro">Nome do Livro</label>
        <asp:TextBox ID="txtLivro" ToolTip="Informe um Nome de Livro" runat="server" CssClass="txtLivro" MaxLength="50"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
