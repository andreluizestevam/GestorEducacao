<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1103_CadastroSubArea.Busca" %>
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
        <label for="ddlRegiao" title="Região">Região</label>
        <asp:DropDownList ID="ddlRegiao" runat="server" CssClass="campoRegiao"
            ToolTip="Informe a Região" AutoPostBack="true" 
            OnSelectedIndexChanged="ddlRegiao_SelectedIndexChanged">
        </asp:DropDownList>
    </li>    
    <li>
        <label for="ddlArea" title="Cidade">Área</label>
        <asp:DropDownList ID="ddlArea" runat="server" CssClass="campoArea"
            ToolTip="Informe a Área" AutoPostBack="true">
        </asp:DropDownList>
    </li>    
    </ContentTemplate>
    </asp:UpdatePanel>

    <li>
        <label for="txtSubarea" title="Bairro">Subárea</label>
        <asp:TextBox ID="txtSubarea" ToolTip="Informe a Subárea" runat="server" 
            CssClass="campoSubarea" MaxLength="40">
        </asp:TextBox>
    </li>

     <li>
        <label for="txtSigla" title="Bairro">Sigla</label>
        <asp:TextBox ID="txtSigla" ToolTip="Informe a Sigla" runat="server" 
            CssClass="campoSigla" MaxLength="12">
        </asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
