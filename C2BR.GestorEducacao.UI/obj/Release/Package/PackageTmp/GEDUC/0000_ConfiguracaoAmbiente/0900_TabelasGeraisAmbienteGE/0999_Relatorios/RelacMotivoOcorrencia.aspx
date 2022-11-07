<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacMotivoOcorrencia.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0999_Relatorios.RelacMotivoOcorrencia" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 150px; } /* Usado para definir o formulário ao centro */     
        .liTpRelacao { margin-top: 10px; }
        .liDescricao,.liSituacao
        {
        	clear:both;
        	margin-top: 5px;
        }
        .ddlSituacao { width: 55px; } 
        .ddlTpOcorrencia { width: 90px; }
        .ddlClassif { width: 80px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liTpRelacao">
            <label class="lblObrigatorio" title="Classificação">
                Classificação</label>               
            <asp:DropDownList ID="ddlClassif" ToolTip="Selecione a Classificação" 
                CssClass="ddlClassif" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlClassif_SelectedIndexChanged">
                <asp:ListItem Value="A">Aluno</asp:ListItem>
                <asp:ListItem Value="F">Funcionário</asp:ListItem>
                <asp:ListItem Value="R">Responsável</asp:ListItem>
                <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liDescricao">
            <label class="lblObrigatorio" title="Tipo de Ocorrência">
                Tipo de Ocorrência</label>               
            <asp:DropDownList ID="ddlTpOcorrencia" 
                ToolTip="Selecione um Tipo de Ocorrência" CssClass="ddlTpOcorrencia" 
                runat="server" AutoPostBack="True">
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