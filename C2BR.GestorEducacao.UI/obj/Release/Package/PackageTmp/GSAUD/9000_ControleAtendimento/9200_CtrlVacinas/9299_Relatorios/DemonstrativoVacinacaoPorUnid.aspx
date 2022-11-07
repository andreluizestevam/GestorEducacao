<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="DemonstrativoVacinacaoPorUnid.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9200_CtrlVacinas._9299_Relatorios.DemonstrativoVacinacaoPorUnid" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados" style="margin-left: 360px">
        <li class="liboth">
            <asp:Label runat="server" ID="Label2">Campanha de Vacinação</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="drpCampanhaVacinacao" ToolTip="Selecine a campanha desejada" />
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblUnidadeCadastro">Unidade de Vacinação</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="drpUnidadeVacinacao" ToolTip="Selecine a unidade desejada" />
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblGrupo">Grupo de Risco</asp:Label><br />
            <asp:DropDownList runat="server" Width="155px" ID="drpGrupoRisco" ToolTip="Selecine o grupo de risco desejado">
                <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                <asp:ListItem Text="Idosos" Value="ID"></asp:ListItem>
                <asp:ListItem Text="Gestantes" Value="GS"></asp:ListItem>
                <asp:ListItem Text="Doenças Crônicas" Value="DC"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-top: 0px;">
            <label>Faixa Etária</label>
            <asp:DropDownList runat="server" Width="124px" ID="drpFaixaEtaria">
                <asp:ListItem Text="Todas" Value="0"></asp:ListItem>
                <asp:ListItem Text="0 a 2 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="3 a 6 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="7 a 9 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="10 a 13 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="14 a 18 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="19 a 23 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="24 a 28 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="29 a 31 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="32 a 35 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="36 a 39 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="40 a 42 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="43 a 36 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="47 a 50 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="51 a 53 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="54 a 57 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="58 a 61 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="62 a 65 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="66 a 69 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="70 a 73 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="74 a 77 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="78 a 80 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="81 a 84 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="85 a 88 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="89 a 91 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="92 a 93 anos" Value="DC"></asp:ListItem>
                <asp:ListItem Text="Acima de 93 anos" Value="DC"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>Usuários Código e-SUS</label>
            <asp:DropDownList Width="80px" runat="server" ID="drpUsuariosEsus">
                <asp:ListItem Text="Ambos" Value=""></asp:ListItem>
                <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                <asp:ListItem Text="Não" Value="N"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <asp:Label runat="server" class="lblObrigatorio" ID="lblPeriodo">Período de Registro de Vacina</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
