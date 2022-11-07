<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5216_BoletoBancarioResp.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .txtNome { width: 210px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtUnidade" title="Unidade/Escola">
            Unidade</label>
        <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade"
            AutoPostBack="True">
        </asp:DropDownList>
    </li>
    <li>
        <label for="txtNome" title="Nome">Nome</label>
        <asp:TextBox ID="txtNome" ToolTip="Informe o Nome" CssClass="txtNome" runat="server"></asp:TextBox>
    </li>
    <li>
        <label for="ddlUnidadeContrato" title="Unidade de Contrato">Unidade de Contrato</label>
        <asp:DropDownList ID="ddlUnidadeContrato" runat="server" CssClass="campoUnidadeEscolar"
            ToolTip="Selecione a Unidade de Contrato">
        </asp:DropDownList>
    </li>
</ul>
</asp:Content>