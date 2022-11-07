<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacTipos.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0999_Relatorios.RelacTipos" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 150px; } /* Usado para definir o formulário ao centro */     
        .liTpRelacao { margin-top: 10px; }
        .liDescricao,.liSituacao
        {
        	clear:both;
        	margin-top: 5px;
        }
        .ddlDescricao { width: 140px; } 
        .ddlSituacao { width: 55px; }
        .ddlTpRelacao { width: 120px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liTpRelacao">
            <label class="lblObrigatorio" title="Relação Por">
                Relação Por</label>               
            <asp:DropDownList ID="ddlTpRelacao" ToolTip="Selecione o Tipo de Relação" 
                CssClass="ddlTpRelacao" runat="server"
            AutoPostBack="true" onselectedindexchanged="ddlTpRelacao_SelectedIndexChanged">
                <asp:ListItem Value="P">Pessoa</asp:ListItem>
                <asp:ListItem Value="T">Telefone</asp:ListItem>
                <asp:ListItem Value="E">Endereço</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liDescricao">
            <label class="lblObrigatorio" title="Descrição">
                Descrição</label>               
            <asp:DropDownList ID="ddlDescricao" ToolTip="Selecione uma Descrição" CssClass="ddlDescricao" runat="server">
            </asp:DropDownList>
        </li>        
        </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liSituacao">
            <label class="lblObrigatorio" title="Situação">
                Situação</label>               
            <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione a Situação" CssClass="ddlSituacao" runat="server">
                <asp:ListItem Selected="true" Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>                
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>