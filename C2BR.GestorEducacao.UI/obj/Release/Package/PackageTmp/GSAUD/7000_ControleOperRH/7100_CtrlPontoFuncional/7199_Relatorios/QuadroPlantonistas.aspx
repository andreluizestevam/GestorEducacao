<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="QuadroPlantonistas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7199_Relatorios.QuadroPlantonistas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
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
        .ddlTipoContratoColab, .ddlDepartamentoColab
        {
            width: 130px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul class="ulDados">
        <asp:UpdatePanel ID="updTopo" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <li>
                    <label title="Selecione a Unidade para pesquisa">
                        Unidade</label>
                    <asp:DropDownList runat="server" ID="ddlUnidade" Width="220px" ToolTip="Selecione a Unidade para pesquisa"
                        OnSelectedIndexChanged="ddlUnidade_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Selecione o Departamento para pesquisa">
                        Local</label>
                    <asp:DropDownList runat="server" ID="ddlDept" Width="170px" ToolTip="Selecione o Departamento para pesquisa">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Selecione a Especialidade para pesquisa">
                        Especialidade</label>
                    <asp:DropDownList runat="server" ID="ddlEspec" Width="150px" ToolTip="Selecione a Especialidade para pesquisa"
                        OnSelectedIndexChanged="ddlEspec_OnSelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li style="clear:both">
                    <label>
                        Tipo de Contrato</label>
                    <asp:DropDownList ID="ddlTipoContratoColab" ToolTip="Selecione o Tipo de Contrato do Funcionário"
                        CssClass="ddlTipoContratoColab" runat="server" OnSelectedIndexChanged="ddlTipoContratoColab_OnSelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Selecione o Colaborador Plantonista para pesquisa">
                        Nome do Colaborador</label>
                    <asp:DropDownList runat="server" ID="ddlColaborador" Width="220px" ToolTip="Selecione o Colaborador Plantonista para pesquisa"
                        ClientIDMode="Static">
                    </asp:DropDownList>
                </li>
                <li style="clear: both">
                    <label title="Seleciione a Situação para Pesquisa">
                        Situação</label>
                    <asp:DropDownList runat="server" ID="ddlSituacao" Width="80px" ToolTip="Seleciione a Situação para Pesquisa">
                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Aberto" Value="A"></asp:ListItem>
                        <asp:ListItem Text="Cancelado" Value="C"></asp:ListItem>
                        <asp:ListItem Text="Realizado" Value="R"></asp:ListItem>
                        <asp:ListItem Text="Planejado" Value="P"></asp:ListItem>
                        <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Selecione a ordenação desejada">
                        Ordenado por:</label>
                    <asp:DropDownList runat="server" ID="ddlClassificacao" ToolTip="Selecione a ordenação desejada"
                        Width="130px" ClientIDMode="Static">
                        <asp:ListItem Value="1" Text="Nome"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Unidade/Especialidade"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Unidade/Local"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Especialidade"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li>
                    <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio" ToolTip="Selecione o período dentro do qual deseja pesquisar">Período</asp:Label><br />
                    <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                        ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
                    <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
                    <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                        ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            carregaCss();
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            carregaCss();
        });

        function carregaCss() {
            $(".campoData").datepicker();
            $(".campoData").mask("99/99/9999");

            $("#ddlColaborador").change(function (evento) {
                var e = document.getElementById("ddlColaborador");
                var itSelec = e.options[e.selectedIndex].value;
                if (itSelec != "0") {
                    $("#ddlClassificacao").attr('disabled', true);
                    $("#ddlClassificacao").css("background-color", "#F5F5F5");
                    $("#ddlClassificacao").val("1");
                }
                else {
                    $("#ddlClassificacao").removeAttr('disabled');
                    $("#ddlClassificacao").css("background-color", "White");
                }
            });
        }

    </script>
</asp:Content>
