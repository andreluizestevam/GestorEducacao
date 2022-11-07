<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5910_DadosBoletoBancario.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlBanco {width:160px;}
        .ddlAgencia {width:160px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
        <ContentTemplate>
        <li>
            <label for="ddlBanco" title="Banco">Banco</label>
            <asp:DropDownList ID="ddlBanco" runat="server" CssClass="ddlBanco"
                ToolTip="Selecione o Banco" 
                onselectedindexchanged="ddlBanco_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlAgencia" title="Agência">Agência</label>
            <asp:DropDownList ID="ddlAgencia" runat="server" CssClass="ddlAgencia"
                ToolTip="Selecione a Agência">
            </asp:DropDownList>
        </li>
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
