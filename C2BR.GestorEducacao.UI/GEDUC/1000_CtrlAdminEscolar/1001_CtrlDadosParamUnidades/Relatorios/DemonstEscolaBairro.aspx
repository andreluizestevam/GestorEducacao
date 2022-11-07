<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="DemonstEscolaBairro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.Relatorios.DemonstEscolaBairro" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">                       
        .ulDados { width: 280px; }
        .liUF, .liBairro
        {
        	clear:both;
        	margin-top:5px;
        }
        .liCidade
        {        	
        	margin-top:5px;
        	margin-left:5px;
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
                onselectedindexchanged="ddlUF_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUF" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUF" Text="*" 
            ErrorMessage="Campo UF é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator> 
        </li>
        <li class="liCidade">
            <label class="lblObrigatorio" title="Cidade">
                Cidade</label>               
            <asp:DropDownList ID="ddlCidade" ToolTip="Selecione uma Cidade" CssClass="ddlCidade" runat="server" 
                onselectedindexchanged="ddlCidade_SelectedIndexChanged" 
                AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlCidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlCidade" Text="*" 
            ErrorMessage="Campo Cidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator> 
        </li>
        <li class="liBairro">
            <label class="lblObrigatorio" title="Bairro">
                Bairro</label>               
            <asp:DropDownList ID="ddlBairro" ToolTip="Selecione um Bairro" CssClass="ddlBairro" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlBairro" runat="server" CssClass="validatorField"
            ControlToValidate="ddlBairro" Text="*" 
            ErrorMessage="Campo Bairro é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
