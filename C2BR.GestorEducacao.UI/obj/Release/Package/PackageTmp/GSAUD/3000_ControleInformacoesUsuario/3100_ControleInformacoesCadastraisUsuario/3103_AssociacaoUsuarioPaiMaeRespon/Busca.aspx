<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3100_ControleInformacoesCadastraisUsuario._3103_AssociacaoUsuarioPaiMaeRespon.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtNome" title="Nome do Aluno ou do Responsável">Usuário de Saúde ou Responsável</label>
        <asp:TextBox ID="txtNome" CssClass="campoNomePessoa" ToolTip="Pesquisar pelo Nome do Usuário de Saúde ou do Responsável" runat="server" MaxLength="40"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
<script type="text/javascript">
</script>
</asp:Content>
