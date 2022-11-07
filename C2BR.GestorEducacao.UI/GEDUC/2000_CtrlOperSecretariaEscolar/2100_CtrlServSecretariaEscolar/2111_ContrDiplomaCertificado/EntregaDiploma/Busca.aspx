<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F2000_CtrlOperSecretariaEscolar.F2100_CtrlServSecretariaEscolar.F2111_ContrDiplomaCertificado.EntregaDiploma.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul class="ulParamsFormBusca">
    <li>
        <label for="txtNoAlu" title="Nome do Aluno">Aluno</label>
        <asp:TextBox ID="txtNoAlu" CssClass="campoNomePessoa" runat="server" MaxLength="80"
            ToolTip="Informe o Nome do Aluno"></asp:TextBox>
    </li>    
</ul>
</asp:Content>
