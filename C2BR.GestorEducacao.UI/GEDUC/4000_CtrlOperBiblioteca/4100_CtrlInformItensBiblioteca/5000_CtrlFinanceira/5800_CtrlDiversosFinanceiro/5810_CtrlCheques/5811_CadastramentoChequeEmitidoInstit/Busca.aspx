<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5800_CtrlDiversosFinanceiro.F5810_CtrlCheques.F5811_CadastramentoChequeEmitidoInstit.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">     
        /*--> CSS DADOS */   
        .ddlBanco { width: 45px; }
        .ddlAgencia { width: 70px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">                     
    <ContentTemplate>
    <li>
        <label title="Banco">
            Banco</label>
        <asp:DropDownList ID="ddlBanco" ToolTip="Selecione um Banco" 
            CssClass="ddlBanco" runat="server" AutoPostBack="True" 
            onselectedindexchanged="ddlBanco_SelectedIndexChanged">
        </asp:DropDownList>        
    </li>
    <li>
        <label title="Agência">
            Agência</label>
        <asp:DropDownList ID="ddlAgencia" ToolTip="Selecione uma Agência" 
            CssClass="ddlAgencia" runat="server">
        </asp:DropDownList>                
    </li>
    </ContentTemplate>
    </asp:UpdatePanel>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>