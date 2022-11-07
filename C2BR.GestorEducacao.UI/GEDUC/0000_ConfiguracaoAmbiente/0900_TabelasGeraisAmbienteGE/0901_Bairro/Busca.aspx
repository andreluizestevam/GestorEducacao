<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" 
    Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0901_Bairro.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
    <ContentTemplate>
    <li>
        <label for="ddlUfCB" title="UF">UF</label>
        <asp:DropDownList ID="ddlUfCB" runat="server" CssClass="campoUf"
            ToolTip="Informe a UF" AutoPostBack="true" 
            OnSelectedIndexChanged="ddlUfCB_SelectedIndexChanged">
        </asp:DropDownList>
    </li>    
    <li>
        <label for="ddlCidadeCB" title="Cidade">Cidade</label>
        <asp:DropDownList ID="ddlCidadeCB" runat="server" CssClass="ddlCidade"
            ToolTip="Informe a Cidade" AutoPostBack="true">
        </asp:DropDownList>
    </li>    
    </ContentTemplate>
    </asp:UpdatePanel>
    <li>
        <label for="txtDescricaoCB" title="Bairro">Bairro</label>
        <asp:TextBox ID="txtDescricaoCB" ToolTip="Informe o Bairro" runat="server" 
            CssClass="campoBairro" MaxLength="40">
        </asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>