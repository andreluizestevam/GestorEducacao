<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacCEPs.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0999_Relatorios.RelacCEPs" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 480px; } /* Usado para definir o formulário ao centro */
        .liUF { margin-top:15px; }
        .liCidade, .liBairro
        {
        	margin-top:15px;
        	margin-left: 10px;   
        }
        .liImpressao
        {
        	clear: both;
        	margin-top: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUF">
            <label class="lblObrigatorio" title="UF">
                UF</label>               
            <asp:DropDownList ID="ddlUF" ToolTip="Selecione uma UF" CssClass="ddlUF" runat="server" 
                onselectedindexchanged="ddlUF_SelectedIndexChanged1" AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li class="liCidade">
            <label class="lblObrigatorio" title="Cidade">
                Cidade</label>               
            <asp:DropDownList ID="ddlCidade" ToolTip="Selecione uma Cidade" CssClass="ddlCidade" runat="server" 
                onselectedindexchanged="ddlCidade_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li class="liBairro" title="Bairro">
            <label class="lblObrigatorio">
                Bairro</label>               
            <asp:DropDownList ID="ddlBairro" ToolTip="Selecione um Bairro" CssClass="ddlBairro" runat="server">
            </asp:DropDownList>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liImpressao">
            <label class="lblObrigatorio" title="Ordem de Impressão">
                Ordem de Impressão</label>               
            <asp:DropDownList ID="ddlImpressao" ToolTip="Selecione a Ordem de Impressão" CssClass="ddlImpressao" runat="server">
                <asp:ListItem Value="B">Bairro</asp:ListItem>
                <asp:ListItem Value="C">Nº CEP</asp:ListItem>
                <asp:ListItem Value="E">Endereço</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>