<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="DemonResulCampanhas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9199_Relatorios.DemonResulCampanhas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 350px;
            margin: 30px 0 0 370px !important;
        }
        label
        {
            margin-bottom:1px;
        }
        .ulDados li
        {
            margin: 5px 0 0 10px;
        }
        .chk label
        {
            display: inline;
        }
        .lblsub
        {
            color: #436EEE;
            clear: both;
            margin-bottom: -3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="lblsub" style="margin-bottom: -5px">
            <label>
                Parâmetros</label>
        </li>
        <li style="clear: both">
            <label title="Tipo da Campanha de Saúde">
                Tipo</label>
            <asp:DropDownList runat="server" ID="ddlTipoCamp" ToolTip="Tipo da Campanha de Saúde"
                Width="124px">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Classificado por</label>
            <asp:DropDownList runat="server" ID="ddlClassPor" Width="100px" ClientIDMode="Static"
                OnSelectedIndexChanged="ddlClassPor_OnSelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="C" Text="Campanha" Selected="True"></asp:ListItem>
                <asp:ListItem Value="U" Text="Unidade"></asp:ListItem>
                <asp:ListItem Value="B" Text="Cidade/Bairro"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>
                Período</label>
            <asp:TextBox ID="txtDataIniCamp" Style="margin: 0px !important;" runat="server" CssClass="campoData"
                ToolTip="Data de início para pesquisa de Campanhas de Saúde">
            </asp:TextBox>
            <asp:Label runat="server" ID="lbl"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" ID="txtDataFimCamp" CssClass="campoData" ToolTip="Data de término para pesquisa de Campanhas de Saúde"></asp:TextBox>
        </li>
        <li class="lblsub" style="margin-bottom: -2px">
            <label>
                Visualização</label>
        </li>
        <li style="clear: both; margin-left:5px !important;">
            <asp:CheckBox runat="server" ID="chkGraficos" Text="Gráficos" CssClass="chk" ToolTip="Selecione caso deseje que seja impresso um Gráfico Demonstrativo junto ao Relatório"
                Checked="true" />
        </li>
        <li>
            <asp:CheckBox runat="server" ID="chkRelatorio" Text="Relatório" CssClass="chk" ToolTip="Quando selecionado, apresenta as informações do Relatório"
                Checked="true" />
        </li>
        <li>
            <asp:CheckBox runat="server" ID="chkComValor" Text="Valores" CssClass="chk" ToolTip="Selecione caso deseje que sejam impressos os valores" Checked="true" />
        </li>
        <li>
            <label title="Selecione a ordenação desejada">
                Ordenado por</label>
            <asp:DropDownList runat="server" ID="ddlClassificacao" ToolTip="Selecione a ordenação desejada"
                Width="215px" ClientIDMode="Static">
                <asp:ListItem Value="1" Text="Campanha"></asp:ListItem>
                <asp:ListItem Value="2" Text="Tipo de Campanha"></asp:ListItem>
                <asp:ListItem Value="4" Text="QDC - Quantidade Dias de Campanha"></asp:ListItem>
                <asp:ListItem Value="5" Text="QPC - Quantidade Profissionais de Campanha"></asp:ListItem>
                <asp:ListItem Value="6" Text="QAC - Quantidade Atendimento de Campanha"></asp:ListItem>
                <asp:ListItem Value="7" Text="MDA - Média de Atendimento Diário"></asp:ListItem>
                <asp:ListItem Value="8" Text="R$ Receita"></asp:ListItem>
                <asp:ListItem Value="9" Text="R$ Despesa"></asp:ListItem>
                <asp:ListItem Value="10" Text="R$ Saldo"></asp:ListItem>
                <asp:ListItem Value="11" Text="CMA - Custo Médio Atendimento"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-left:3px">
            <label title="Selecione a Ordenação para o relatório (Crescente ou Decrescente">
                Ordem</label>
            <asp:DropDownList runat="server" ID="ddlTipoOrdem" Width="80px" ToolTip="Selecione a Ordenação para o relatório (Crescente ou Decrescente)"
                ClientIDMode="Static" CssClass="ddlTipoOrdem">
                <asp:ListItem Value="C" Text="Crescente"></asp:ListItem>
                <asp:ListItem Value="D" Text="Decrescente"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            //Verifica qual opção foi selecionada para selecionar a correspondente na Ordenação
            $("#ddlClassificacao").change(function (evento) {
                var e = document.getElementById("ddlClassificacao");
                var itSelec = e.options[e.selectedIndex].value;

                var ord = document.getElementById("ddlTipoOrdem");

                if ((itSelec == 1) || (itSelec == 2) || (itSelec == 3)) {
                    $("#ddlTipoOrdem").val("C");
                }
                else {
                    $("#ddlTipoOrdem").val("D");
                }
            });

        });
    </script>
</asp:Content>
