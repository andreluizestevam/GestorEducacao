<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="DemonConsultMedic.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios.DemonConsultMedic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 320px;
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
        .ddlTipoContratoColab
        {
            width: 114px;
        }
        .lblsub
        {
            color: #436EEE;
            clear: both;
            margin-bottom: -3px;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul class="ulDados">
        <asp:UpdatePanel ID="updCampanha" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <li class="lblsub" style="margin-bottom: -5px">
                    <label>
                        Parâmetros</label>
                </li>
                <li style="clear: both">
                    <label title="Selecione a Unidade do Plantão para pesquisa">
                        Unidade</label>
                    <asp:DropDownList runat="server" ID="ddlUnidade" Width="270px" ToolTip="Selecione a Unidade do Plantão para pesquisa">
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
                        Especialidade</label>
                    <asp:DropDownList runat="server" ID="ddlEspecPlant" Width="180px" ToolTip="Selecione a Especialidade do Plantão para pesquisa">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>
                        Classificação</label>
                    <asp:DropDownList runat="server" ID="ddlClassif" Width="130px" ToolTip="Selecione a classificação da Relação desejada"
                        OnSelectedIndexChanged="ddlClassif_OnSelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="U" Text="Unidade" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="P" Text="Profissional"></asp:ListItem>
                        <asp:ListItem Value="E" Text="Especialidade"></asp:ListItem>
                        <asp:ListItem Value="B" Text="Cidade/Bairro"></asp:ListItem>
                        <%--<asp:ListItem Value="R" Text="Região/Área"></asp:ListItem>--%>
                    </asp:DropDownList>
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
                    <asp:CheckBox Style="width: 130px;" ID="CheckBoxVerValor" CssClass="chk" runat="server"
                        Text="Com valor" Checked="True" />
                </li>
                <li>
                    <label title="Selecione a ordenação desejada">
                        Ordenado por</label>
                    <asp:DropDownList runat="server" ID="ddlOrdenadoPor" ToolTip="Selecione a ordenação desejada"
                        Width="210px" ClientIDMode="Static">
                        <asp:ListItem Value="1" Text="Unidade"></asp:ListItem>
                        <asp:ListItem Value="2" Text="QAP - Qtde Atendimentos Planejados"></asp:ListItem>
                        <asp:ListItem Value="3" Text="QAR - Qtde Atendimentos Realizados"></asp:ListItem>
                        <asp:ListItem Value="4" Text="QCN - Qtde Consultas Normais"></asp:ListItem>
                        <asp:ListItem Value="5" Text="QCR - Qtde Consultas Retorno"></asp:ListItem>
                        <asp:ListItem Value="6" Text="QCU - Qtde Consultas Urgentes"></asp:ListItem>
                        <asp:ListItem Value="7" Text="MAD - Média Atendimento Diário"></asp:ListItem>
                        <asp:ListItem Value="8" Text="MDE - Média Atendimento por Especialidade"></asp:ListItem>
                        <asp:ListItem Value="9" Text="MAP - Média Atendimento por Profissional"></asp:ListItem>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            CarregaCss();
        });

        function CarregaCss() {
            //Verifica qual opção foi selecionada para selecionar a correspondente na Ordenação
            $("#ddlOrdenadoPor").change(function (evento) {
                var e = document.getElementById("ddlOrdenadoPor");
                var itSelec = e.options[e.selectedIndex].value;

                var ord = document.getElementById("ddlTipoOrdem");

                if (itSelec == 1) {
                    $("#ddlTipoOrdem").val("C");
                }
                else {
                    $("#ddlTipoOrdem").val("D");
                }
            });

            $(".campoData").datepicker();
            $(".campoData").mask("99/99/9999");
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            CarregaCss();
        });

    </script>
</asp:Content>
