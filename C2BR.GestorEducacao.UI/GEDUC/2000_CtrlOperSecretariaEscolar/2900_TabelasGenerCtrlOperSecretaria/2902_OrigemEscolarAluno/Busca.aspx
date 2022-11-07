<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2900_TabelasGenerCtrlOperSecretaria.F2902_OrigemEscolarAluno.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">     
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtDE_ORIGEM" title="Origem da Matricula ou Sigla">Origem da Matricula / Sigla</label>
        <asp:TextBox ID="txtDE_ORIGEM" ToolTip="Informe uma Origem da Matricula ou uma Sigla" runat="server" MaxLength="40" CssClass="campoDescricao"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
