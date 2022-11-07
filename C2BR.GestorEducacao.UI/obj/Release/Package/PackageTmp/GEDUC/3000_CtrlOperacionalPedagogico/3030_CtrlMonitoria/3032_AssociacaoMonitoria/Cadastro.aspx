<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3032_AssociacaoMonitoria.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            margin-top: 10px;
            margin-left: 31%;
            width: 320px !important;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li style="width: 200px;">
            <label title="Ano">
                Ano</label>
            <asp:TextBox ID="txtAno" Width="40px" Enabled="false" CssClass="txtAno" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAno" runat="server" ControlToValidate="txtAno"
                ErrorMessage="O Ano deve ser Inserido" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="width: 140px;">
            <asp:Label runat="server" ID="lblModalidade" class="lblObrigatorio">Modalidade</asp:Label>
            <asp:DropDownList ID="ddlModalidade" Width="140px" runat="server" ToolTip="Selecione a Modalidade"
                OnSelectedIndexChanged="ddlModalidade_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="width: 100px; margin: 0 0 0 5px;">
            <asp:Label runat="server" ID="lblSerieCurso" class="lblObrigatorio">Série/Curso</asp:Label>
            <asp:DropDownList ID="ddlSerieCurso" Width="100px" runat="server" ToolTip="Selecione a Série/Curso"
                OnSelectedIndexChanged="ddlSerieCurso_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="width: 110px; margin: 5px 0 0 0;">
            <asp:Label runat="server" ID="lblTurma" class="lblObrigatorio">Turma</asp:Label>
            <asp:DropDownList ID="ddlTurma" Width="110px" runat="server" ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="width: 185px; margin: 5px 0 0 11px;">
            <asp:Label runat="server" ID="lblMateria" class="lblObrigatorio">Matéria</asp:Label>
            <asp:DropDownList ID="ddlMateria" Width="185px" runat="server" ToolTip="Selecione a Turma">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlMateria" runat="server" ControlToValidate="ddlMateria"
                ErrorMessage="A Matéria deve ser selecionada." Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="width: 200px; margin: 5px 0 0 0;">
            <asp:Label runat="server" ID="lblPro" class="lblObrigatorio">Professor Monitor</asp:Label>
            <asp:DropDownList ID="ddlProfessor" Width="200px" runat="server" ToolTip="Selecione o Professor">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlProfessor"
                ErrorMessage="O Professor deve ser Selecionado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li style="width: 40px; margin-top: 7px; margin-left:10px;">
            <label title="R$ Hora">
                R$ Hora</label>
            <asp:TextBox ID="txtVlHr" Width="40px" runat="server" CssClass="maskHrVL"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvVlHr" runat="server" ControlToValidate="txtVlHr"
                ErrorMessage="O Valor da Hora de Monitoria deve ser Inserido" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liboth" style="width:200px;">
            <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio">Período de Disponibilidade</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp&nbsp à &nbsp&nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
        </li>
        <li style="width: 70px; margin: 0 0 0 0;">
            <asp:Label ID="lblSitu" runat="server" class="lblObrigatorio">Situação</asp:Label>
            <asp:DropDownList runat="server" ID="ddlSitu" Width="70px">
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="display: none;">
            <asp:TextBox runat="server" ID="txtIsEd"></asp:TextBox>
        </li>
        <li style="display: none;">
            <asp:TextBox runat="server" ID="txtSituAlter"></asp:TextBox>
        </li>
    </ul>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".maskHrVL").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtAno").mask("9999");
        });
    </script>
</asp:Content>
