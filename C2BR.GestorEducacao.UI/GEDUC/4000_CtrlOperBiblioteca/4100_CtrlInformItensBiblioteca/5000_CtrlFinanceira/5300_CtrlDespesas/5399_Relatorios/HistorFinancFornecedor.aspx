<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="HistorFinancFornecedor.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5299_Relatorios.HistorFinancFornecedor" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }
        .liUnidade, .liStaDocumento, .liFornec, .liAgrup
        {
            margin-top: 5px;
            width: 300px;            
        }           
        .ddlStaDocumento { width: 85px; } 
        .ddlAgrupador { width:200px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" title="Unidade/Escola" class="lblObrigatorio" runat="server">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>
        <li class="liStaDocumento">
            <label title="Documento(s)">
                Documento(s)</label>               
            <asp:DropDownList ID="ddlStaDocumento" ToolTip="Selecione o Status do Documento" CssClass="ddlStaDocumento" runat="server">   
            <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>        
            <asp:ListItem Value="A">Em aberto(s)</asp:ListItem>
            <asp:ListItem Value="Q">Quitado(s)</asp:ListItem>
            <asp:ListItem Value="C">Cancelado(s)</asp:ListItem>
            </asp:DropDownList>            
        </li>        
        <li class="liAgrup">
            <label for="ddlAgrupador" title="Agrupador de Receita">Agrupador de Receita</label>
            <asp:DropDownList ID="ddlAgrupador" CssClass="ddlAgrupador" runat="server" ToolTip="Selecione o Agrupador de Receita"/>
        </li>                             
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
