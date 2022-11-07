<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="DemonAtendMedic1.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3299_Relatorios.DemonAtendMedic1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 400px;
            margin:30px 0 0 370px !important;
        }
        .ulDados li
        {
            margin-left: 5px;
            margin-top: 5px;
        }
        .chk label
        {
            display: inline;
        }
        .lblsub
        {
            color: #436EEE;
            clear: both;
            margin-bottom:-3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="lblsub" style="margin-bottom:-5px">
            <label>
                Parâmetros</label>
        </li>
        <li style="clear: both">
            <label>
                Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnid" Width="270px" ToolTip="Unidade do Atendimento"
                OnSelectedIndexChanged="ddlUnid_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>
                Especialidade</label>
            <asp:DropDownList runat="server" ID="ddlEspec" Width="190px" ToolTip="Especialidade do Atendimento">
            </asp:DropDownList>
        </li>
        <li style="margin-left: 0px; margin-top: 9px; clear:both">
            <asp:CheckBox runat="server" ID="chkComAtendimento" class="chk" ToolTip="Ao marcar, apenas as Unidades e Especialidades com Atendimentos dentro dos parâmetros serão apresentadas no Relatório"
                Text="Apenas com Atendimentos" Checked="true" />
        </li>
        <li style="clear: both">
            <label>
                Período</label>
            <asp:TextBox runat="server" ID="txtIniPeri" CssClass="campoData"></asp:TextBox>
            à
            <asp:TextBox runat="server" ID="txtFimPeri" CssClass="campoData"></asp:TextBox>
        </li>
        <li class="lblsub">
            <label>
                Visualização</label>
        </li>
        <li style="clear: both; margin-left:0px">
            <asp:CheckBox runat="server" ID="chkGraficos" Text="Gráficos" CssClass="chk" ToolTip="Quando selecionado, apresenta os gráficos no Relatório"
                Checked="true" />
            <asp:CheckBox runat="server" ID="chkRelatorio" Text="Relatório" CssClass="chk" ToolTip="Quando selecionado, apresenta as informações do Relatório"
                Checked="true" />
        </li>
        <li style="clear: both">
            <label title="Selecione a ordenação desejada">
                Ordenado por</label>
            <asp:DropDownList runat="server" ID="ddlClassificacao" ToolTip="Selecione a ordenação desejada"
                Width="230px" ClientIDMode="Static">
                <asp:ListItem Value="1" Text="Unidade"></asp:ListItem>
                <asp:ListItem Value="2" Text="Especialidade"></asp:ListItem>
                <asp:ListItem Value="3" Text="QPMC - Quantidade Marcação Consulta"></asp:ListItem>
                <asp:ListItem Value="4" Text="QAMC - Quantidade Atendimento Médico Consulta"></asp:ListItem>
                <asp:ListItem Value="5" Text="QEPA - Quantidade Pré-Atendimento"></asp:ListItem>
                <asp:ListItem Value="6" Text="QEEM - Quantidade Encaminhamentos Médicos"></asp:ListItem>
                <asp:ListItem Value="7" Text="QEAT - Quantidade Atendimento Médico Emergencial"></asp:ListItem>
                <asp:ListItem Value="8" Text="QAMT - Quantidade Atendimento Médico Total"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-left:-2px">
            <label title="Selecione a Ordenação para o relatório (Crescente ou Decrescente">
                Ordem</label>
            <asp:DropDownList runat="server" ID="ddlTipoOrdem" Width="80px" ToolTip="Selecione a Ordenação para o relatório (Crescente ou Decrescente)"
                ClientIDMode="Static">
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

                if ((itSelec == 1) || (itSelec == 2)) {
                    $("#ddlTipoOrdem").val("C");
                }
                else {
                    $("#ddlTipoOrdem").val("D");
                }
            });

        });
    </script>
</asp:Content>
