<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelatoItensEstoque.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6200_CtrlOperMateriais.F6230_CtrlOperacionalItensEstoque.F6239_Relatorios.RelatoItensEstoque" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 250px; }                               
        .liUnidade,.liGrupo,.liSubGrupo,.liTpProduto
        {
        	clear:both;
            margin-top: 5px;
            width: 250px;            
        }        
        .ddlGrupo, .ddlSubGrupo { width:200px; }
        .ddlTpProduto { width:150px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">        
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" runat="server" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" ToolTip="Selecione a Unidade/Escola" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>      
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>     
        <li class="liGrupo">
            <label class="lblObrigatorio" title="Grupo">
                Grupo</label>               
            <asp:DropDownList ID="ddlGrupo" ToolTip="Selecione o Grupo" CssClass="ddlGrupo" runat="server" 
                AutoPostBack="True" onselectedindexchanged="ddlGrupo_SelectedIndexChanged">           
            </asp:DropDownList>            
            <asp:RequiredFieldValidator ID="rfvddlGrupo" runat="server" CssClass="validatorField"
            ControlToValidate="ddlGrupo" Text="*" 
            ErrorMessage="Campo Grupo é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>                     
        <li class="liSubGrupo">
            <label class="lblObrigatorio" title="Sub-Grupo">
                Sub-Grupo</label>
            <asp:DropDownList ID="ddlSubGrupo" ToolTip="Selecione o Sub-Grupo" CssClass="ddlSubGrupo" runat="server">
            </asp:DropDownList>        
            <asp:RequiredFieldValidator ID="rfvddlSubGrupo" runat="server" CssClass="validatorField"
            ControlToValidate="ddlSubGrupo" Text="*" 
            ErrorMessage="Campo Sub-Grupo é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>         
        </li>        
        <li class="liTpProduto">
            <label class="lblObrigatorio" title="Tipo Produto"> 
                Tipo Produto</label>
            <asp:DropDownList ID="ddlTpProduto" ToolTip="Selecione o Tipo Produto" CssClass="ddlTpProduto" runat="server">
            </asp:DropDownList>       
            <asp:RequiredFieldValidator ID="rfvddlTpProduto" runat="server" CssClass="validatorField"
            ControlToValidate="ddlTpProduto" Text="*" 
            ErrorMessage="Campo Tipo Produto é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>       
        </li> 
        </ContentTemplate>
        </asp:UpdatePanel>                                             
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
