<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="PrevAtendUnidades.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._5000_CtrlFinanceira._5300_CtrlGerencial._5399_Relatorios.PrevAtendUnidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
        }
        input
        {
            height: 13px;
        }
        .liboth
        {
            clear: both;
            margin-top:10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados" style="margin-left: 580px">
        <li>
            <label>UF</label>
            <asp:DropDownList ID="drpUF" runat="server" Width="40px" ToolTip="Selecione o Estado desejado" OnSelectedIndexChanged="drpUf_SelectedIndexChanged" AutoPostBack="true" />
        </li>
        <li>
            <label>Cidade</label>
            <asp:DropDownList ID="drpCidade" runat="server" Width="179px" ToolTip="Selecione a Cidade desejada" OnSelectedIndexChanged="drpCidade_SelectedIndexChanged" AutoPostBack="true" />
        </li>
        <li class="liboth">
            <label>Unidade</label>
            <asp:DropDownList runat="server" Width="225px" ID="drpUnidade" ToolTip="Selecine a Unidade desejada" />
        </li>
        <li class="liboth">
            <label title="Mês de Referência">Mês de Referência</label>
            <asp:DropDownList ID="drpMesReferencia" runat="server" ToolTip="Selecione o Mês de Referência">
                <asp:ListItem Value="1">Janeiro</asp:ListItem>
                <asp:ListItem Value="2">Fevereiro</asp:ListItem>
                <asp:ListItem Value="3">Março</asp:ListItem>
                <asp:ListItem Value="4">Abril</asp:ListItem>
                <asp:ListItem Value="5">Maio</asp:ListItem>
                <asp:ListItem Value="6">Junho</asp:ListItem>
                <asp:ListItem Value="7">Julho</asp:ListItem>
                <asp:ListItem Value="8">Agosto</asp:ListItem>
                <asp:ListItem Value="9">Setembro</asp:ListItem>
                <asp:ListItem Value="10">Outubro</asp:ListItem>
                <asp:ListItem Value="11">Novembro</asp:ListItem>
                <asp:ListItem Value="12">Dezembro</asp:ListItem>                    
            </asp:DropDownList>
        </li>
        <li style="margin-top:10px;">
            <label>Ano</label>
            <asp:TextBox ID="txtAno" CssClass="Ano" runat="server" Width="30px" />
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Ano").mask("9999");
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
