<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5900_TabelasGenerCtrlFinaceira.F5913_Agrup_RecDesp.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">     
        /*--> CSS DADOS */           
        .txtNomeAgrupador { width: 150px; }
        .ddlTpAgrupador { width: 100px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label id="Label1" title="Nome do Agrupador">
            Nome do Agrupador</label>
        <asp:TextBox ID="txtNomeAgrupador"
                ToolTip="Informe o Nome do Agrupador"
                CssClass="txtNomeAgrupador" MaxLength="50" runat="server"></asp:TextBox>       
    </li>
    <li>
        <label id="Label2" title="Tipo do Agrupador" runat="server">
            Tipo do Agrupador</label>
        <asp:DropDownList ID="ddlTpAgrupador" ToolTip="Selecione o Tipo do Agrupador" 
            CssClass="ddlTpAgrupador" runat="server">
        <asp:ListItem Value="T">Todos</asp:ListItem>
        <asp:ListItem Value="R">Receitas</asp:ListItem>
        <asp:ListItem Value="D">Despesas</asp:ListItem>
        </asp:DropDownList>               
    </li>  
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">   
    <script type="text/javascript">
    </script>
</asp:Content>