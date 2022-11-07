<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F5000_CtrlFinanceira.F5200_CtrlReceitas.F5203_BoletoBancarioTitulo.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .txtNome { width: 210px; }
        .ddlTipo { width: 65px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li runat="server" id="liUnidade">
        <label for="txtUnidade" title="Unidade/Escola">
            Unidade</label>
        <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade"
            AutoPostBack="True">
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlTipo" title="Tipo da Fonte de Receita">Tipo</label>
        <asp:DropDownList ID="ddlTipo" ToolTip="Selecione o Tipo" CssClass="ddlTipo" runat="server">
        </asp:DropDownList>
    </li>
    <li>
        <label for="txtNome" title="Nome">Nome</label>
        <asp:DropDownList ID="ddlAluno" runat="server" CssClass="txtNome" ToolTip="Selecione Um aluno" Visible="false"></asp:DropDownList>
        <asp:TextBox ID="txtNome" ToolTip="Informe o Nome" CssClass="txtNome" runat="server"></asp:TextBox>
    </li>
    <li  runat="server" id="liUnidadeContrato">
        <label for="ddlUnidadeContrato" title="Unidade de Contrato">Unidade de Contrato</label>
        <asp:DropDownList ID="ddlUnidadeContrato" runat="server" CssClass="campoUnidadeEscolar"
            ToolTip="Selecione a Unidade de Contrato">
        </asp:DropDownList>
    </li>
</ul>
</asp:Content>