<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="DemonTempoAtendPorProfissional.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios.DemonTempoAtendPorProfissional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
        }
        .ulDados li
        {
            margin: 3px;
        }
        input
        {
            height: 13px;
        }
        label
        {
            margin-bottom: 2px;
        }
        .ddlUnidade
        {
            width: 280px;
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados" style="margin-left: 360px">
        <li class="liboth">
            <asp:Label runat="server" ID="lblUnidade">Unidade</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlUnidade" ID="ddlUnidade" OnSelectedIndexChanged="ddlUnidade_OnSelectedIndexChanged"
                AutoPostBack="true" ToolTip="Selecine a Unidade Solicitante">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>
                Local
            </label>
            <asp:DropDownList AutoPostBack="true" runat="server" ID="ddlLocal" Style="width: auto;">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>
                Classificação Funcional
            </label>
            <asp:DropDownList AutoPostBack="true" runat="server" ID="ddlEspec" Width="280px" OnSelectedIndexChanged="ddlEspec_SelectedIndexChanged">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>
                Profissional
            </label>
            <asp:DropDownList runat="server" ID="ddlProfissional" Width="280px" OnSelectedIndexChanged="ddlProfissional_SelectedIndexChanged">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" class="lblObrigatorio" ID="lblPeriodo" ToolTip="Período entre datas">Período</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" ValidationGroup="ValidacaoData"
                CssClass="validatorField" ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" ValidationGroup="ValidacaoData"
                CssClass="validatorField" ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-left: 30px;">
            <asp:Label runat="server" class="lblObrigatorio" ID="lblHora" ToolTip="Período entre horas"
                Text="Hora"></asp:Label><br />
            <asp:TextBox runat="server" Style="width: 30px;" class="campoHora" ID="horaInicial"
                ToolTip="Informe a Hora Inical do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniHora" ValidationGroup="ValidacaoData"
                CssClass="validatorField" ErrorMessage="O campo hora Inicial é requerido" ControlToValidate="horaInicial"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label3" Style="margin-left: -5px; margin-right: -5px;"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" Style="width: 30px;" class="campoHora" ID="horaFinal"
                ToolTip="Informe a Hora Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimHora" ValidationGroup="ValidacaoData"
                CssClass="validatorField" ErrorMessage="O campo hora Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">
        $('.campoHora').mask('99:99');
    </script>
</asp:Content>
