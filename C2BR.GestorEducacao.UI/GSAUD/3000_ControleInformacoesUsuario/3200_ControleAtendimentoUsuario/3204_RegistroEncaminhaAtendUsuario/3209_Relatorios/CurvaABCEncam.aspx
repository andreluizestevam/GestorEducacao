<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="CurvaABCEncam.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3204_RegistroEncaminhaAtendUsuario._3209_Relatorios.CurvaABCEncam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
            margin:30px 0 0 370px !important;
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
        .ulDados li
        {
            margin-top: 5px;
            margin-left: 5px;
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
            <label title="Selecione a Unidade do Profissional para pesquisa">
                Unidade Funcional</label>
            <asp:DropDownList runat="server" ID="ddlUnidadeColab" Width="270px" ToolTip="Selecione a Unidade do Profissional para pesquisa">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Selecione a Unidade do Encaminhamento para pesquisa">
                Unidade Encaminhamento</label>
            <asp:DropDownList runat="server" ID="ddlUnidadePlantao" Width="270px" ToolTip="Selecione a Unidade do Encaminhamento para pesquisa">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Selecione o Local para pesqusa">
                Local</label>
            <asp:DropDownList runat="server" ID="ddlLocal" Width="140px" ToolTip="Selecione o Local para pesqusa">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Selecione a Especialidade do Plantão para pesquisa">
                Especialidade Encaminhamento</label>
            <asp:DropDownList runat="server" ID="ddlEspecPlant" Width="180px" ToolTip="Selecione a Especialidade do Plantão para pesquisa">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Selecione a Situação Funcional do Profissional para pesquisa">
                Situação Funcional</label>
            <asp:DropDownList runat="server" ID="ddlSituaFuncional" Width="110px" ToolTip="Selecione a Situação Funcional do Profissional para pesquisa">
                <asp:ListItem Value="0">Todas</asp:ListItem>
                <asp:ListItem Value="ATI">Atividade Interna</asp:ListItem>
                <asp:ListItem Value="ATE">Atividade Externa</asp:ListItem>
                <asp:ListItem Value="FCE">Cedido</asp:ListItem>
                <asp:ListItem Value="FES">Estagiário</asp:ListItem>
                <asp:ListItem Value="LFR">Licença Funcional</asp:ListItem>
                <asp:ListItem Value="LME">Licença Médica</asp:ListItem>
                <asp:ListItem Value="LMA">Licença Maternidade</asp:ListItem>
                <asp:ListItem Value="SUS">Suspenso</asp:ListItem>
                <asp:ListItem Value="TRE">Treinamento</asp:ListItem>
                <asp:ListItem Value="FER">Férias</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-left: 0px; margin-top: 9px;">
            <asp:CheckBox runat="server" ID="chkSomComAgendamentos" class="chk" ToolTip="Ao marcar, apenas os profissionais com encaminhamentos dentro dos parâmetros serão apresentados no Relatório"
                Text="Apenas Colaboradores com Encaminhamentos" Checked="true" />
        </li>
        <li style="clear: both">
            <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio" ToolTip="Perído pelo qual deseja realizar a pesquisa">Período</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="txtIniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="txtIniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="txtFimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="txtFimPeri"></asp:RequiredFieldValidator><br />
        </li>
        <li class="lblsub">
            <label>
                Visualização</label>
        </li>
        <li style="clear: both; margin-left: 0px;">
            <asp:CheckBox runat="server" ID="chkGraficos" Text="Gráficos" CssClass="chk" ToolTip="Quando selecionado, apresenta os gráficos no Relatório"
                Checked="true" />
            <asp:CheckBox runat="server" ID="chkRelatorio" Text="Relatório" CssClass="chk" ToolTip="Quando selecionado, apresenta as informações do Relatório"
                Checked="true" />
        </li>
        <li style="clear: both">
            <label title="Selecione a ordenação desejada">
                Ordenado por</label>
            <asp:DropDownList runat="server" ID="ddlClassificacao" ToolTip="Selecione a ordenação desejada"
                Width="170px" ClientIDMode="Static">
                <asp:ListItem Value="1" Text="Profissional"></asp:ListItem>
                <asp:ListItem Value="2" Text="Função"></asp:ListItem>
                <asp:ListItem Value="3" Text="Unidade"></asp:ListItem>
                <asp:ListItem Value="4" Text="QTUE - Unidades de Encaminhamentos"></asp:ListItem>
                <asp:ListItem Value="5" Text="QTEM - Emergência"></asp:ListItem>
                <asp:ListItem Value="6" Text="QTMU - Média Urgência"></asp:ListItem>
                <asp:ListItem Value="7" Text="QTUG - Urgência"></asp:ListItem>
                <asp:ListItem Value="8" Text="QTPU - Pouca Urgência"></asp:ListItem>
                <asp:ListItem Value="9" Text="QTNU - Não Urgente"></asp:ListItem>
                <asp:ListItem Value="10" Text="QTTE - Quantidade de Encaminhamentos"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-left:-2px">
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
