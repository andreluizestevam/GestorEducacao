<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1500_CtrlContratosCompromissos.F1502_CadastroSubCategoria.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">    
        /*--> CSS DADOS */
        .ddlCateg { width: 150px;}
        .txtSubCateg { width: 210px; }
          
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlCateg" title="Categoria" >Categoria</label>
        <asp:DropDownList ID="ddlCateg"  CssClass="ddlCateg" runat="server">          
        </asp:DropDownList>
    </li>
    <li>
        <label for="txtSubCateg" title="SubCategoria">SubCategoria</label>
        <asp:TextBox ID="txtSubCateg" ToolTip="Pesquise pela SubCategoria" runat="server" MaxLength="80" CssClass="txtSubCateg"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
