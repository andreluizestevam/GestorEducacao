<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3032_AssociacaoMonitoria.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulParamsFormBusca" id="ulParamsFormBusca" style="margin-left: 10px;">
        <li>
            <label for="txtAno" title="Ano">
                Ano</label>
            <asp:TextBox runat="server" ID="txtAno" Width="40px" CssClass="txtAno" ToolTip="Pesquise pelo Ano"></asp:TextBox>
        </li>
        <li>
            <label for="txtNome" title="Nome">
                Professor</label>
            <asp:TextBox ID="txtNome" ToolTip="Pesquise pelo Professor" runat="server" CssClass="campoRegiao"
                MaxLength="70"></asp:TextBox>
        </li>
        <li>
            <label for="txtSigla" title="Sigla">
                CPF</label>
            <asp:TextBox ID="txtCPF" ToolTip="Pesquise pelo CPF" runat="server" CssClass="campoCpf"
                MaxLength="12"></asp:TextBox>
        </li>
        <li style="width: 70px;">
            <asp:Label ID="lblSitu" runat="server">Situação</asp:Label>
            <asp:DropDownList runat="server" ID="ddlSitu" Width="70px">
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $(".campoCpf").mask("999.999.999-99");
            $(".txtAno").mask("9999");
        });
    </script>
</asp:Content>
