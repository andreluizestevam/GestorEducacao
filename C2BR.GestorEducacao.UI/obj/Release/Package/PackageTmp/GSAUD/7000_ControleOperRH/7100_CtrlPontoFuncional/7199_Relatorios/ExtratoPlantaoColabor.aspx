<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ExtratoPlantaoColabor.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7199_Relatorios.ExtratoPlantaoColabor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 250px;
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
        .ulDados label
        {
            margin-bottom:1px;
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
                Local Plantão</label>
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
            <label title="Selecione o Profissional para pesquisa">
                Nome Profissional</label>
            <asp:DropDownList runat="server" ID="ddlNomeProfissional" Width="220px" ToolTip="Selecione o Profissional para pesquisa">
            </asp:DropDownList>
        </li>
        <li style="clear: both; margin: 7px 0 0 0">
            <asp:CheckBox runat="server" ID="chkComValor" class="chk" Text="Imprime colunas de dados Financeiros"
                Checked="true" ToolTip="Selecione caso deseje visualizar as colunas de dados financeiros" />
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
</asp:Content>
