<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5101_CadastramentoCaixa.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">     
        /*--> CSS DADOS */           
        .txtNomeCaixa { width: 150px; }
        .ddlUsoCaixa { width: 70px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label id="Label1" title="Nome do Caixa">
            Nome Caixa</label>
        <asp:TextBox ID="txtNomeCaixa"
                ToolTip="Informe o Nome do Caixa"
                CssClass="txtNomeCaixa" MaxLength="60" runat="server"></asp:TextBox>       
    </li>
    <li>
        <label id="Label2" title="Estado de Uso do Caixa" runat="server">
            Estado do Caixa</label>
        <asp:DropDownList ID="ddlUsoCaixa" ToolTip="Selecione o Estado de Uso do Caixa" 
            CssClass="ddlUsoCaixa" runat="server">
        <asp:ListItem Value="T">Todos</asp:ListItem>
        <asp:ListItem Value="F">Fechado</asp:ListItem>
        <asp:ListItem Value="A">Em aberto</asp:ListItem>
        </asp:DropDownList>               
    </li>  
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">   
    <script type="text/javascript">
    </script>
</asp:Content>