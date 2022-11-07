<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7150_CadastroPlantoes.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .lblsub
        {
            color: #436EEE;
        }
        .ulDados
        {
            width: 300px !important;
            margin-left:380px !important;
        }
        .ulDados li
        {
            margin-top: 5px;
            margin-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="lblsub" style="clear: both; margin-top:30px;">
            <label for="ddlAgrupador" title="Contexto de Atividades Funcional">
                Dados do Plantão</label>
        </li>
        <li style="clear: both">
            <label class="lblObrigatorio" title="Unidade do Plantão">
                Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnid" ToolTip="Unidade do Plantão" Width="220px"
                OnSelectedIndexChanged="ddlUnid_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfvddlUnid" ControlToValidate="ddlUnid"></asp:RequiredFieldValidator>
        </li>
        <li style="clear:both; margin-bottom:-10px;">
            <label title="Nome(descrição) do Plantão">
                Nome</label>
            <asp:TextBox ID="txtNome" Width="210px" runat="server" onkeyup="javascript:MaxLength(this, 200);"
                ToolTip="Nome(descrição) do Plantão"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNome" runat="server" ControlToValidate="txtNome"
                ErrorMessage="O Nome deve ser Inserido" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="clear: both">
            <label title="Nome reduzido do Plantão">
                Sigla</label>
            <asp:TextBox ID="txtSig" Width="70px" runat="server" onkeyup="javascript:MaxLength(this, 12);"
                ToolTip="Nome reduzido do Plantão"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSigla" runat="server" ControlToValidate="txtSig"
                ErrorMessage="A Sigla deve ser Informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Hora de início do Plantão">
                QRI</label>
            <asp:TextBox ID="txtHoraIni" CssClass="txtHora" Width="30px" runat="server" ToolTip="Hora de início do Plantão"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvHoraIni" runat="server" ControlToValidate="txtHoraIni"
                ErrorMessage="A Hora Inicial deve ser Informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="Quantidade de horas do Plantão">
                QTH</label>
            <asp:TextBox ID="txtQtHoras" Width="25px" runat="server" ToolTip="Quantidade de horas do Plantão" CssClass="txtQtHr"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvQtHoras" runat="server" ControlToValidate="txtQtHoras"
                ErrorMessage="A Quantidade de Horas deve ser Informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="lblsub" style="clear: both;">
            <label for="ddlAgrupador" title="Dados de onde será realizado o Plantão (opcional)">
                Local do Plantão</label>
        </li>
        <li style="clear: both">
            <label title="Local onde o Plantão será realizado (opcional)">
                Local</label>
            <asp:DropDownList runat="server" ID="ddlLocal" ToolTip="Local onde o Plantão será realizado (opcional)"
                Width="170px">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Especialidade específica do Plantão (opcional)">
                Especialidade</label>
            <asp:DropDownList runat="server" ID="ddlEspec" ToolTip="Especialidade específica do Plantão (opcional)"
                Width="200px">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Situação cadatral do plantão">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSitu" Width="70px" ToolTip="Situação cadatral do plantão">
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="rfvddlSitu" ControlToValidate="ddlSitu"></asp:RequiredFieldValidator>
        </li>
        <%--<li style="clear:both; margin-top:20px">
            <label>Legenda: <br /> QRI(Hora de início do Plantão) <br /> QTH(Quantidade de Horas do Plantão)</label>
        </li>--%>
        <li style="display: none;">
            <asp:TextBox runat="server" ID="txtIsEd"></asp:TextBox>
        </li>
        <li style="display: none;">
            <asp:TextBox runat="server" ID="txtSituAlter"></asp:TextBox>
        </li>
    </ul>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".txtHora").mask("99:99");
            $(".txtQtHr").mask("?999");
        });

    </script>
</asp:Content>
