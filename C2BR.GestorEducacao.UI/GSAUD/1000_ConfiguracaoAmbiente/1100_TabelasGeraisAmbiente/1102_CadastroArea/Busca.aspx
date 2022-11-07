<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1102_CadastroArea.Busca" %>

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
                    <label for="ddlRegiao" title="Região">
                        Região</label>
                    <asp:DropDownList ID="ddlRegiao" runat="server" CssClass="campoRegião" ToolTip="Informe a Região">
                    </asp:DropDownList>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li>
            <label for="txtArea" title="Área">
                Área</label>
            <asp:TextBox ID="txtArea" ToolTip="Pesquise pela Área" runat="server" CssClass="campoArea"
                MaxLength="40"></asp:TextBox>
        </li>
        <li>
            <label for="txtSigla" title="Sigla">
                Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Pesquise pela Sigla" runat="server" CssClass="campoSigla"
                MaxLength="12"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
