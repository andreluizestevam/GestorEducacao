<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="FaltasConsecutivas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios.FaltasConsecutivas" %>
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
        .ddlPadrao
        {
            width: 285px;
            clear: both;
        }
        .chk
        {
            margin-left: -5px;
        }
        .chk label
        {
            display: inline;
            margin-left: -4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados" style="margin-left: 355px">
        <li class="liboth">
            <asp:Label runat="server" ID="lblUnidadeCadastro">Unidade de Cadastro</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="ddlUnidadeCadastro" OnSelectedIndexChanged="dllUnidadeCadastro_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecine a unidade desejada" />
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblUnidadeLotacao">Unidade Lotação/Contrato</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="ddlUnidadeContrato" OnSelectedIndexChanged="ddlUnidadeContrato_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecine a unidade desejada" />
        </li>
        <li class="liboth">
            <label style="">Operadora</label>
            <asp:DropDownList runat="server" ID="ddlOperadora" Width="195px" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged" AutoPostBack="true" />
        </li>
        <li class="liboth">
            <label>Plano</label>
            <asp:DropDownList runat="server" ID="ddlPlano" Width="85px">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <label>Classificação funcional</label>
            <asp:DropDownList runat="server" ID="ddlClassFuncional" Width="170px" OnSelectedIndexChanged="ddlClassFuncional_SelectedIndexChanged" AutoPostBack="True" />
        </li>
        <li style="clear: both">
            <label>Local</label>
            <asp:DropDownList runat="server" ID="ddlLocal" class="ddlPadrao">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>Profissional</label>
            <asp:DropDownList runat="server" ID="ddlProfissional" class="ddlPadrao" OnSelectedIndexChanged="ddlProfissional_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="0">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label for="ddlNomeUsuAteMed" title="Nome do usuário selecionado">Paciente</label>
            <asp:DropDownList ID="ddlPaciente" runat="server" class="ddlPadrao" style="margin-bottom:10px;" Visible="false"></asp:DropDownList>
            <asp:TextBox ID="txtNomePacPesq" ToolTip="Digite o nome ou parte do nome do paciente" runat="server" style="width: 283px;"/>
        </li>
        <li style="margin-left: 290px;margin-top: -30px;">
            <asp:ImageButton ID="imgbPesqPacNome" ImageUrl="~/Library/IMG/Gestor_BtnPesquisa.png"
                OnClick="imgbPesqPacNome_OnClick" runat="server" style="margin-top: 3px;"/>
            <asp:ImageButton ID="imgbVoltarPesq" ImageUrl="~/Library/IMG/PGS_Desfazeer.ico"
                OnClick="imgbVoltarPesq_OnClick" Visible="false" runat="server" style="margin-top: 4px;"/>
        </li>
        <li style="clear: both; margin-top: -10px;">
            <label for="ddlConsiderar" title="Tipo de relatório que deve retornar">Considerar por</label>
            <asp:DropDownList ID="ddlTipoRelatorio" runat="server" style="width: 80px;">
                <asp:ListItem Value="A">Agendamento</asp:ListItem>
                <asp:ListItem Value="D">Dias</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <asp:CheckBox ID="chkFaltas" CssClass="chk" Text="Somente com Faltas" runat="server" />
        </li>
        <li style="clear: both">
            <asp:Label runat="server" class="lblObrigatorio" ID="lblPeriodo">Dt Agendamento</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data do Agendamento"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>