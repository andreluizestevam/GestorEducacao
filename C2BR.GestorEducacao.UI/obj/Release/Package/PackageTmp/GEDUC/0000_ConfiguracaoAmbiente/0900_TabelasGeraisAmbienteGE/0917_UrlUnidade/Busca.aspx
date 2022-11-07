<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0900_TabelasGeraisAmbienteGE._0917_UrlUnidade.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label title="Unidade">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar"
                ToolTip="Selecione a Unidade" Height="16px" Width="253px">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                URL</label>
            <asp:TextBox ID="txtUrl" CssClass="campoNomePessoa" runat="server" 
                ToolTip="Informe  a Url" Width="221px"></asp:TextBox>
        </li>
        
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
