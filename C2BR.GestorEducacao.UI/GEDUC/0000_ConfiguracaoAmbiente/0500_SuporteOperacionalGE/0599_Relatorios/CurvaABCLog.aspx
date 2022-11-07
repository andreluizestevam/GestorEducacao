<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="CurvaABCLog.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._0000_ConfiguracaoAmbiente._0500_SuporteOperacionalGE._0599_Relatorios.CurvaABCLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 400px;
            margin-left: 360px !important;
        }
        .ulDados li
        {
            margin-top: 5px;
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
        <li>
            <label>
                Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnidade" ToolTip="Selecione a Unidade para filtro no relatório" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged" AutoPostBack="true"
                Width="200px">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>
                Usuário</label>
            <asp:DropDownList runat="server" ID="ddlUsuario" ToolTip="Selecione o usuário para filtro no relatório"
                Width="220px">
            </asp:DropDownList>
        </li>
        <li class="liAcao">
            <label class="lblObrigatorio" title="Ação">
                Ação</label>
            <asp:DropDownList ID="ddlAcao" ToolTip="Selecione a Ação" CssClass="ddlAcao" runat="server">
                <asp:ListItem Selected="True" Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="G">Gravação</asp:ListItem>
                <asp:ListItem Value="E">Alteração</asp:ListItem>
                <asp:ListItem Value="D">Exclusão</asp:ListItem>
                <asp:ListItem Value="P">Consulta</asp:ListItem>
                <asp:ListItem Value="R">Relatório</asp:ListItem>
                <asp:ListItem Value="X">Sem Ação</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlAcao" runat="server" CssClass="validatorField"
                ControlToValidate="ddlAcao" Text="*" ErrorMessage="Campo Ação é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="liPeriodo" style="clear: both">
            <label class="lblObrigatorio" for="txtPeriodo">
                Período</label>
            <asp:TextBox ID="txtDataPeriodoIni" CssClass="campoData" runat="server" ToolTip="Data Início"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoIni" runat="server" CssClass="validatorField"
                ControlToValidate="txtDataPeriodoIni" Text="*" ErrorMessage="Campo Data Período Início é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:Label ID="lblDivData" CssClass="lblDivData" runat="server"> à </asp:Label>
            <asp:TextBox ID="txtDataPeriodoFim" CssClass="campoData" runat="server" ToolTip="Data Fim"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="validatorField"
                ForeColor="Red" ControlToValidate="txtDataPeriodoFim" ControlToCompare="txtDataPeriodoIni"
                Type="Date" Operator="GreaterThanEqual" ErrorMessage="Data Final não pode ser menor que Data Inicial.">
            </asp:CompareValidator>
            <asp:RequiredFieldValidator ID="rfvtxtDataPeriodoFim" runat="server" CssClass="validatorField"
                ControlToValidate="txtDataPeriodoFim" Text="*" ErrorMessage="Campo Data Período Fim é requerido"
                SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
        <li class="lblsub">
            <label>
                Visualização</label>
        </li>
        <li style="clear: both">
            <label title="Selecione a Classificação desejada">
                Classificado por</label>
            <asp:DropDownList runat="server" ID="ddlClassif" ToolTip="Selecione a Classificação desejada"
                Width="80px" ClientIDMode="Static" OnSelectedIndexChanged="ddlClassif_OnSelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="A" Text="Atividade"></asp:ListItem>
                <asp:ListItem Value="U" Text="Usuário"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label title="Selecione a ordenação desejada">
                Ordenado por</label>
            <asp:DropDownList runat="server" ID="ddlClassificacao" ToolTip="Selecione a ordenação desejada"
                Width="187px" ClientIDMode="Static">
                <asp:ListItem Value="1" Text="Atividade"></asp:ListItem>
                <asp:ListItem Value="2" Text="QUA - Quantidade Usuários de Acesso"></asp:ListItem>
                <asp:ListItem Value="3" Text="TAF - Tempo de Acesso Funcionalidade"></asp:ListItem>
                <asp:ListItem Value="4" Text="QASA - Qtde de Acessos Sem Acao"></asp:ListItem>
                <asp:ListItem Value="5" Text="QACA - Qtde de Acessos Com Acao"></asp:ListItem>
                <asp:ListItem Value="6" Text="QTA - Qtde Total de Acessos"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-left: -2px">
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

                if ((itSelec == 1)) {
                    $("#ddlTipoOrdem").val("C");
                }
                else {
                    $("#ddlTipoOrdem").val("D");
                }
            });

        });
    </script>
</asp:Content>
