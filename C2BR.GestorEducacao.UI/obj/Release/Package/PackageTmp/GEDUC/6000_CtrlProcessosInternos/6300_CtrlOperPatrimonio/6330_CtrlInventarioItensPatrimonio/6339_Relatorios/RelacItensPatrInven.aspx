<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacItensPatrInven.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6330_CtrlInventarioItensPatrimonio.F6339_Relatorios.RelacItensPatrInven" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 250px; }
        .liUnidade,.liTpPatr
        {
        	clear:both;
            margin-top: 5px;
            width: 250px;            
        }
        .ddlTpPatr { width:75px; }
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
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
