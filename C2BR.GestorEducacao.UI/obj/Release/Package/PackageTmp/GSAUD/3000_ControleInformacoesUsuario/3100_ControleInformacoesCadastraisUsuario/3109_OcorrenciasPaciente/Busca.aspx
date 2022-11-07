<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3109_OcorrenciasPaciente.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label>Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnidade" Width="200px" ToolTip="Selecine a unidade desejada" />
        </li>
        <li style="clear: both;">
            <label title="Nome do Paciente">
                Paciente</label>
            <asp:TextBox ID="txtPaciente" Width="200px" ToolTip="Informe o nome do paciente" runat="server"></asp:TextBox>
        </li>
        <li style="clear: both;">
            <label>Profissional</label>
            <asp:DropDownList runat="server" ID="ddlProfissional" Width="200px" ToolTip="Selecione o profissional responsável" />
        </li>
        <li style="clear: both;">
            <label title="Tipo da Ocorrência">
                Tipo</label>
            <asp:DropDownList ID="ddlTipo" Width="100px" ToolTip="Informe o tipo da ocorrência" runat="server" />
        </li>
        <li style="clear: both; margin-top:5px;">
            <asp:Label runat="server" class="lblObrigatorio" ID="lblPeriodo">Período</asp:Label><br />
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
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
