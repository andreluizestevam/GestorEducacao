<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5100_CtrlTesouraria.F5105_FechamentoCaixa.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">       
        /*--> CSS DADOS */         
        .ddlNomeCaixa { width: 160px; }
        .ddlFuncCaixa { width: 235px; }
        .ddlDtMovto { width: 80px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label id="Label1" title="Nome do Caixa">
            Nome Caixa</label>
        <asp:DropDownList ID="ddlNomeCaixa" ToolTip="Selecione o Nome do Caixa" 
            CssClass="ddlNomeCaixa" runat="server">
        </asp:DropDownList>     
    </li>
    <li>
        <label id="Label3" title="Usuário Responsável">
            Responsável Abertura Caixa</label>
        <asp:DropDownList ID="ddlRespAberCaixa" ToolTip="Selecione o Usuário Responsável" 
            CssClass="ddlFuncCaixa" runat="server">
        </asp:DropDownList>     
    </li>
    <li>
        <label id="Label4" title="Funcionário do Caixa">
            Colaborador Caixa</label>
        <asp:DropDownList ID="ddlFuncCaixa" ToolTip="Selecione o Funcionário do Caixa" 
            CssClass="ddlFuncCaixa" runat="server">
        </asp:DropDownList>     
    </li>
    <li>
        <label title="Data Movimento">
            Data Movto</label>
        <asp:DropDownList ID="ddlDtMovto" ToolTip="Selecione a Data de Movimento" 
            CssClass="ddlDtMovto" runat="server">
        </asp:DropDownList>               
    </li>  
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">   
    <script type="text/javascript">
    </script>
</asp:Content>