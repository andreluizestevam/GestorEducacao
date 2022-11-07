<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0906_TipoPessoa.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtNomeTP" class="labelPixel" title="Nome">
                Nome</label>
            <asp:TextBox ID="txtNomeTP" runat="server" CssClass="campoDescricao" MaxLength="40"
                ToolTip="Informe o Nome"></asp:TextBox>
        </li>
        <li>
            <label for="ddlSituacaoTP" class="labelPixel" title="Situação">
                Situação</label>
            <asp:DropDownList ID="ddlSituacaoTP" class="selectedRowStyle" style="width:100%;" 
                runat="server" ToolTip="Informe a Situação">
                <asp:ListItem Value="">Todos</asp:ListItem>
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>        
    </ul>
</asp:Content>
