<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacaoUnidadeEscola.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1001_CtrlDadosParamUnidades.Relatorios.RelacaoUnidadeEscola" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 280px; }
        .liTipo
        {
            margin-top: 5px;
            width: 280px;            
        }              
         .liCidade
        {        	
        	margin-top:5px;
        	margin-left:5px;
        }
        .liTipoUnidade, .liNucleo, .liUF, .liBairro
        {
        	clear:both;
        	margin-top:5px;
        }       
        .ddlNucleo, .ddlTipoUnidade { width: 200px; }        
        .ddlTipo { width:100px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">     
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>      
        <li class="liTipo">
            <label id="lblTipo" class="lblObrigatorio" title="Tipo" for="ddlTipo">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" ToolTip="Selecione um Tipo" CssClass="ddlTipo" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlTipo_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="U">Por Tipo Unidade</asp:ListItem>
            <asp:ListItem Value="N">Por Núcleo</asp:ListItem>
            <asp:ListItem Value="B">Por Bairro</asp:ListItem>
            </asp:DropDownList>            
        </li>   
        <li id="liTipoUnidade" runat="server" class="liTipoUnidade">
            <label id="Label1" class="lblObrigatorio" title="Tipo de Unidade" runat="server">
                Tipo Unidade</label>
            <asp:DropDownList ID="ddlTipoUnidade" ToolTip="Selecione um Tipo de Unidade" CssClass="ddlTipoUnidade" runat="server">
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ID="rfvddlTipoUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlTipoUnidade" Text="*" 
            ErrorMessage="Campo Tipo Unidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>        
        </li>
        <li id="liNucleo" runat="server" class="liNucleo">
            <label id="Label2" class="lblObrigatorio" title="Núcleo" runat="server">
                Núcleo</label>
            <asp:DropDownList ID="ddlNucleo" ToolTip="Selecione um Núcleo" CssClass="ddlNucleo" runat="server">
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ID="rfvddlNucleo" runat="server" CssClass="validatorField"
            ControlToValidate="ddlNucleo" Text="*" 
            ErrorMessage="Campo Núcleo é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>          
        </li>
        <li id="liUF" runat="server" class="liUF">
            <label class="lblObrigatorio" title="UF">
                UF</label>               
            <asp:DropDownList ID="ddlUF" ToolTip="Selecione uma UF" CssClass="ddlUF" runat="server" 
                onselectedindexchanged="ddlUF_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUF" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUF" Text="*" 
            ErrorMessage="Campo UF é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>
        <li id="liCidade" runat="server" class="liCidade">
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
        <li id="liBairro" runat="server" class="liBairro">
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
