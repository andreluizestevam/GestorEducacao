<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._2000_ControleInformacoesFamilia._2100_ControleInformacoesCadastraisFamilia._2101_CadastroFamilia.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtCodigo" title="Número de Indentificação da Família">
                Código</label>
            <asp:TextBox ID="txtCodigo" CssClass="txtCodigo" ToolTip="Informe um Número de Indentificação da Família"
                runat="server" MaxLength="15"></asp:TextBox>
        </li>
        <li>
            <label for="txtNome" title="Nome do Responsável pela Família">
                Nome</label>
            <asp:TextBox ID="txtNome" ToolTip="Informe o Nome do Responsável pela Família" runat="server"
                MaxLength="80" CssClass="campoNomePessoa"></asp:TextBox>
        </li>
        <li style="display:none;">
            <label for="txtCpf" title="CPF">
                CPF</label>
            <asp:TextBox ID="txtCpf" Visible="false" ToolTip="Informe um CPF" runat="server"
                CssClass="campoCpf"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoCpf").mask("999.999.999-99");
        });
    </script>
</asp:Content>
