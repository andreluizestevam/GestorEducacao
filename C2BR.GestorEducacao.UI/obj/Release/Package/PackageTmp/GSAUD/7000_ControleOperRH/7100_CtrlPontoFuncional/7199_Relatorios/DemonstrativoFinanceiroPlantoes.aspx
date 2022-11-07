<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="DemonstrativoFinanceiroPlantoes.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7199_Relatorios.DemonstrativoFinanceiroPlantoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
            margin-left:400px !important;
        }
        .ulDados li
        {
            margin-top: 5px;
            margin-left: 5px;
        }
        .ddlTipoContratoColab, .ddlDepartamentoColab
        {
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li style="clear: both">
            <label title="Selecione a Unidade do Profissional para pesquisa">
                Unidade Funcional</label>
            <asp:DropDownList runat="server" ID="ddlUnidadeColab" Width="220px" ToolTip="Selecione a Unidade do Profissional para pesquisa">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Selecione a Unidade do Plantão para pesquisa">
                Unidade Plantão</label>
            <asp:DropDownList runat="server" ID="ddlUnidadePlantao" Width="220px" ToolTip="Selecione a Unidade do Plantão para pesquisa">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Selecione o Local para pesqusa">
                Local</label>
            <asp:DropDownList runat="server" ID="ddlLocal" Width="120px" ToolTip="Selecione o Local para pesqusa">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Selecione a Especialidade do Plantão para pesquisa">
                Especialidade Plantão</label>
            <asp:DropDownList runat="server" ID="ddlEspecPlant" Width="130px" ToolTip="Selecione a Especialidade do Plantão para pesquisa">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Selecione a Situação Funcional do Profissional para pesquisa">
                Situação Funcional</label>
            <asp:DropDownList runat="server" ID="ddlSituaFuncional" Width="100px" OnSelectedIndexChanged="ddlSituaFuncional_OnSelectedIndexChanged"
                AutoPostBack="true" ToolTip="Selecione a Situação Funcional do Profissional para pesquisa">
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
        <li style="clear: both">
            <label>
                Tipo de Contrato</label>
            <asp:DropDownList ID="ddlTipoContratoColab" ToolTip="Selecione o Tipo de Contrato do Funcionário"
                OnSelectedIndexChanged="ddlTipoContratoColab_OnSelectedIndexChanged" AutoPostBack="true"
                CssClass="ddlTipoContratoColab" runat="server">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Selecione o Profissional para pesquisa">
                Nome Profissional</label>
            <asp:DropDownList runat="server" ID="ddlNomeProfissional" Width="200px" ToolTip="Selecione o Profissional para pesquisa">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Selecione a ordenação desejada">
                Ordenado por</label>
            <asp:DropDownList runat="server" ID="ddlClassificacao" ToolTip="Selecione a ordenação desejada"
                Width="170px" ClientIDMode="Static">
                <asp:ListItem Value="1" Text="Unidade"></asp:ListItem>
                <asp:ListItem Value="2" Text="Local"></asp:ListItem>
                <asp:ListItem Value="3" Text="Especialidade"></asp:ListItem>
                <asp:ListItem Value="4" Text="R$ Hora - Valor da Hora do Plantão"></asp:ListItem>
                <asp:ListItem Value="5" Text="R$ Total - Valor Total do Plantão"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label title="Selecione a Ordenação para o relatório (Crescente ou Decrescente">
                Ordem</label>
            <asp:DropDownList runat="server" ID="ddlTipoOrdem" Width="110px" ToolTip="Selecione a Ordenação para o relatório (Crescente ou Decrescente)" ClientIDMode="Static">
                <asp:ListItem Value="C" Text="Crescente"></asp:ListItem>
                <asp:ListItem Value="D" Text="Decrescente"></asp:ListItem>
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
