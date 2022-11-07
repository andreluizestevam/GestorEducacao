<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1901_TipoOcorrencia.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       .ddlTipo { width: 75px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtNome" title="Tipo/Sigla">Tipo / Sigla</label>
            <asp:TextBox ID="txtNome" runat="server" CssClass="txtDescricao" MaxLength="40"
                ToolTip="Informe o Tipo/Sigla"></asp:TextBox>
    </li>
    <li>
        <label for="ddlTipo" title="Tipo">Tipo</label>
        <asp:DropDownList ID="ddlTipo" CssClass="ddlTipo" runat="server" ToolTip="Selecione o Tipo">
            <asp:ListItem Text="Todos" Value="T" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Aluno" Value="A"></asp:ListItem>
            <asp:ListItem Text="Funcionário" Value="F"></asp:ListItem>
            <asp:ListItem Text="Responsável" Value="R"></asp:ListItem>
            <asp:ListItem Text="Faltas Aluno" Value="R"></asp:ListItem>
            <asp:ListItem Text="Outros" Value="O"></asp:ListItem>
        </asp:DropDownList>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
