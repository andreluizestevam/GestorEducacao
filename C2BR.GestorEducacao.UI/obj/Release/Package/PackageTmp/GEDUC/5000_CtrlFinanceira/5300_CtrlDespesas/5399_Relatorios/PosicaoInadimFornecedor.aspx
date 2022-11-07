<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="PosicaoInadimFornecedor.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5299_Relatorios.PosicaoInadimFornecedor" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 275px; }
        .liUnidade
        {
            margin-top: 5px;
            width: 275px;            
        }                                   
        .liResponsavel, .liTpInadimplencia, .liAgrupador
        {
        	clear:both;
        	margin-top:5px;
        }            
        .ddlTpInadimplencia { width: 165px; }  
        .ddlAgrupador { width:200px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" title="Unidade/Escola" class="lblObrigatorio" runat="server">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server" />
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>           
        <li class="liTpInadimplencia">
            <label class="lblObrigatorio" title="Tipo de Inadimplência">
                Tipo de Inadimplência</label>               
            <asp:DropDownList ID="ddlTpInadimplencia" ToolTip="Selecione um Tipo de Inadimplência" CssClass="ddlTpInadimplencia" 
                runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlTpInadimplencia_SelectedIndexChanged">        
            <asp:ListItem Selected="True" Value="I">Ficha de Inadimplência</asp:ListItem>
            <asp:ListItem Value="F">Inadimplência Fornecedor/Credor</asp:ListItem>
            </asp:DropDownList>            
        </li>             
        <li id="liResponsavel" class="liResponsavel" runat="server">
            <label class="lblObrigatorio" title="Responsável">
                Fornecedor</label>               
            <asp:DropDownList ID="ddlResponsavel" ToolTip="Selecione um Fornecedor" CssClass="ddlNomePessoa" runat="server">
            </asp:DropDownList>    
            <asp:RequiredFieldValidator ID="rfvddlResponsavel" runat="server" CssClass="validatorField"
                ControlToValidate="ddlResponsavel" Text="*" 
                ErrorMessage="Campo Fornecedor é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>                 
        </li>                    
        <li class="liAgrupador">
            <label for="ddlAgrupador" title="Agrupador de Despesa">Agrupador de Despesa</label>
            <asp:DropDownList ID="ddlAgrupador" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Agrupador de Despesa"/>
        </li>                                                                                                         
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
