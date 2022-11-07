<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" 
    Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0912_CadastroCEP.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlUf" title="UF">UF</label>
        <asp:DropDownList ID="ddlUf" runat="server" CssClass="campoUf"
            ToolTip="Informe a UF" AutoPostBack="true" 
            OnSelectedIndexChanged="ddlUf_SelectedIndexChanged">
        </asp:DropDownList>
    </li>    
    <li>
        <label for="ddlCidade" title="Cidade">Cidade</label>
        <asp:DropDownList ID="ddlCidade" runat="server" CssClass="campoCidade"
            ToolTip="Informe a Cidade" AutoPostBack="true" 
            onselectedindexchanged="ddlCidade_SelectedIndexChanged">
        </asp:DropDownList>
    </li>    
    <li>
        <label for="ddlBairro" title="Bairro">Bairro</label>
        <asp:DropDownList ID="ddlBairro" runat="server" CssClass="campoBairro"
            ToolTip="Informe o Bairro">
        </asp:DropDownList>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>