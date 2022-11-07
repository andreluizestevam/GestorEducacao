<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1300_ServicosApoioAdministrativo.F1330_CtrlPesquisaInstitucional.F1335_RegistroDadosNotasPesquisaInst.Busca"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlTipoAvaliacao{ width: 200px;}
        .ddlPublicoAlvo{ width: 115px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlTipoAvaliacao" title="Tipo de Avaliação">Tipo de Avaliação</label>
            <asp:DropDownList ID="ddlTipoAvaliacao" runat="server" CssClass="ddlTipoAvaliacao"
                ToolTip="Selecione o Tipo de Avaliação">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlPublicoAlvo" title="Público Alvo">Público Alvo</label>
            <asp:DropDownList ID="ddlPublicoAlvo" runat="server" CssClass="ddlPublicoAlvo"
                ToolTip="Selecione o Público Alvo">
            </asp:DropDownList>
        </li>
        <li>
            <label for="txtNome" title="Nome da Pessoa">Nome da Pessoa</label>
            <asp:TextBox ID="txtNome" runat="server" CssClass="campoNomePessoa"
                ToolTip="Informe o Nome da Pessoa que respondeu a Pesquisa/Questionário">
            </asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
