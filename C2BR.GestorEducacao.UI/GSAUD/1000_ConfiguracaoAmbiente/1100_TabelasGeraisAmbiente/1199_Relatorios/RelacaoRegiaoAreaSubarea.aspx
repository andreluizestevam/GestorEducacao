<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="RelacaoRegiaoAreaSubarea.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1199_Relatorios.RelacaoRegiaoAreaSubarea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 370px;
        }
        
        
        .ddlRegiao, .txtOrdenacao
        {
            clear: both;
            margin-top: 5px;
        }
        
        .ddlArea
        {
            margin-top: 5px;
            margin-left: 5px;
        }
        .txtSubarea
        {
            margin-top: 5px;
            margin-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <li class="ddlRegiao">
                    <label for="ddlRegiao" title="Região">
                        Região</label>
                    <asp:DropDownList ID="ddlRegiao" runat="server" CssClass="campoRegiao" ToolTip="Informe a Região"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlRegiao_SelectedIndexChanged">
                        <asp:ListItem Text="Todas" Value="0">Todas</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="ddlArea">
                    <label for="ddlArea" title="Area">
                        Área</label>
                    <asp:DropDownList ID="ddlArea" runat="server" CssClass="campoArea" ToolTip="Informe a Área"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                        <asp:ListItem Text="Todas" Value="0">Todas</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="txtSubarea">
                    <label for="txtSubarea" title="Bairro">
                        Subárea</label>
                    <asp:DropDownList ID="ddlSubarea" runat="server" CssClass="campoSubarea" ToolTip="Informe a Subárea"
                        AutoPostBack="true">
                        <asp:ListItem Text="Todas" Value="0">Todas</asp:ListItem>
                    </asp:DropDownList>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li class="txtOrdenacao">
            <label for="txtOrdenacao" title="Bairro">
                Ordenar por:</label>
            <asp:DropDownList ID="ddlOrdenacao" runat="server" CssClass="campoOrdenacao" ToolTip="Informe a Ordenação"
                AutoPostBack="true">
                <asp:ListItem Text="Região" Value="regiao">Região</asp:ListItem>
                <asp:ListItem Text="Área" Value="area">Área</asp:ListItem>
                <asp:ListItem Text="Subárea" Value="subarea">Subárea</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
