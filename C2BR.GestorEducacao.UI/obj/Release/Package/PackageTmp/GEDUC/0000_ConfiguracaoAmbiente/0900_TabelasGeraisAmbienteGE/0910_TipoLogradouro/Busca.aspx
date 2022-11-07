<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0910_TipoLogradouro.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtTipoTL" class="labelPixel" title="Tipo">Tipo</label>
            <asp:TextBox ID="txtTipoTL" runat="server" CssClass="campoDescricao" MaxLength="40"
                ToolTip="Informe o Tipo"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
