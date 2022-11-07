<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelatoItensPatrimonio.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6310_CtrlManutencaoItensPatrimonio.F6319_Relatorios.RelatoItensPatrimonio" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 250px; }
        .liUnidade,.liTpPatr,.liClasPatr
        {
        	clear:both;
            margin-top: 5px;
            width: 250px;            
        }         
        .ddlTpPatr { width:75px; }
        .ddlClasPatr { width:125px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
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
        <li class="liTpPatr">
            <label class="lblObrigatorio" title="Tipo de Patrimônio">
                Tipo de Patrimônio</label>
            <asp:DropDownList ID="ddlTpPatr" ToolTip="Selecione o Tipo de Patrimônio" CssClass="ddlTpPatr" runat="server">
            <asp:ListItem Value="T">Todos</asp:ListItem>
            <asp:ListItem Value="1">Móvel</asp:ListItem>
            <asp:ListItem Value="2">Imóvel</asp:ListItem>
            </asp:DropDownList>        
            <asp:RequiredFieldValidator ID="rfvddlTpPatr" runat="server" CssClass="validatorField"
            ControlToValidate="ddlTpPatr" Text="*" 
            ErrorMessage="Campo Tipo de Patrimônio é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>         
        </li>        
        <li class="liClasPatr">
            <label class="lblObrigatorio" title="Classificação do Patrimônio"> 
                Classificação do Patrimônio</label>
            <asp:DropDownList ID="ddlClasPatr" ToolTip="Selecione a Classificação do Patrimônio" CssClass="ddlClasPatr" runat="server">
            </asp:DropDownList>       
            <asp:RequiredFieldValidator ID="rfvddlClasPatr" runat="server" CssClass="validatorField"
            ControlToValidate="ddlClasPatr" Text="*" 
            ErrorMessage="Campo Classificação do Patrimônio é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>       
        </li>                                              
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
