<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0303_AssociaUsuUnidEducFrequencia.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlUsuarioAssoc" title="Nome do Usuário">
                Usuário</label>
            <asp:DropDownList ID="ddlUsuarioAssoc" ToolTip="Informe o Usuário" CssClass="campoNomePessoa"
            runat="server">
            </asp:DropDownList>     
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
