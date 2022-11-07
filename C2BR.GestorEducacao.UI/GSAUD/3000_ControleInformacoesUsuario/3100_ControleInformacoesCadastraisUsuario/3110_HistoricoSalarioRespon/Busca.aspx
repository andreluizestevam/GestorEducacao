<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3110_HistoricoSalarioRespon.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlUnidade">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="ddlUnidade" ToolTip="Informe a Unidade" Width="200px"
                AutoPostBack="True" />
        </li>
        <li>
            <label for="ddlAssociado">
                Associado</label>
            <asp:DropDownList ID="ddlAssociado" runat="server" ToolTip="Selecione o Associado"  CssClass="campoNomePessoa">
            </asp:DropDownList>           
        </li>
        <li>
            <label for="txtAnoMes" title="Ano/Mês Referência">
                Ano/Mês Referência</label>
            <asp:TextBox ID="txtAnoMes" ToolTip="Informe Ano e Mês de Referência" runat="server" Width="45px"
                CssClass="campoAnoMes"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoAnoMes").mask("9999/99");
        });
    </script>
</asp:Content>