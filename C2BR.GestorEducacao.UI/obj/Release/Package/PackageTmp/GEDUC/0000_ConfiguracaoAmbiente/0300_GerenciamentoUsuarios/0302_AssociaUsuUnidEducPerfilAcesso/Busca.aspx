<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0302_AssociaUsuUnidEducPerfilAcesso.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
         <li>
            <label for="ddlEmp" class="labelPixel" title="Nome da unidade de origem">
                Unidade de Origem</label>
            <asp:DropDownList ID="ddlEmp" ToolTip="Selecione a Unidade de origem" CssClass="campoNomePessoa"
                runat="server" AutoPostBack="True" 
                 onselectedindexchanged="ddlEmp_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
         <li>
            <label for="ddlUsuarioAssoc" class="labelPixel" title="Nome do Usuário">
                Usuário</label>
            <asp:DropDownList ID="ddlUsuarioAssoc" ToolTip="Selecione o Usuário" CssClass="campoNomePessoa"
                runat="server">
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
