<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._1000_CtrlAdminEscolar._1300_ServicosApoioAdministrativo._1330_CtrlPesquisaInstitucional._1334_GeraPesquisaInstSaude.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="ddlUnidade" title="Unidade">Unidade</label>
        <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar"
            ToolTip="Selecione a Unidade/Escola">
        </asp:DropDownList>
    </li>
    <li>
        <label for="ddlAvaliacao" title="Tipo de Avaliação">Nome da Questão</label>
        <asp:DropDownList ID="ddlAvaliacao" CssClass="ddlAvaliacao" runat="server"
            ToolTip="Selecione o Tipo de Avaliação">
        </asp:DropDownList>
    </li>
    <li>
        <label for="txtAno" title="Ano de Cadastro da Avaliação">Ano</label>
        <asp:TextBox ID="txtAno" CssClass="txtAno" runat="server"
            ToolTip="Informe o Ano de Cadastro da Avaliação">
        </asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        $(".txtAno").mask("9999");
    });
</script>
</asp:Content>
