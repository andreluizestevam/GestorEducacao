<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="DemonstrativoProced.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8200_CtrlClinicas._8299_Relatorios.DemonstrativoProced" %>

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
            <asp:Label runat="server" ID="lblUnidadeCadastro">Unidade de Cadastro</asp:Label><br />
            <asp:DropDownList runat="server" class="ddlPadrao" ID="ddlUnidadeCadastro" ToolTip="Selecine a unidade desejada" />
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblGrupo">Grupo</asp:Label><br />
            <asp:DropDownList runat="server" Width="115px" ID="ddlGrupo" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged"
                AutoPostBack="true" ToolTip="Selecine o grupo de procedimento desejado" />
        </li>
        <li style="margin-top: 0px;">
            <label>
                SubGrupo</label>
            <asp:DropDownList runat="server" Width="164px" ID="ddlSubGrupo">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label>
                Ordenar</label>
            <asp:DropDownList Width="150px" runat="server" ID="ddlOrdem" class="ddlPadrao">
                <asp:ListItem Text="Nenhuma" Value=""></asp:ListItem>
                <asp:ListItem Text="Por Quantidade -12 Meses" Value="QA"></asp:ListItem>
                <asp:ListItem Text="Por Valor -12 Meses" Value="VA"></asp:ListItem>
                <asp:ListItem Text="Por Quantidade de Planejados" Value="QP"></asp:ListItem>
                <asp:ListItem Text="Por Valor de Planejados" Value="VP"></asp:ListItem>
                <asp:ListItem Text="Por Quantidade de Executos" Value="QE"></asp:ListItem>
                <asp:ListItem Text="Por Valor de Executos" Value="VE"></asp:ListItem>
                <asp:ListItem Text="Por Quantidade de Cancelados" Value="QC"></asp:ListItem>
                <asp:ListItem Text="Por Valor de Cancelados" Value="VC"></asp:ListItem>
                <asp:ListItem Text="Por Quantidade de Abertos" Value="QB"></asp:ListItem>
                <asp:ListItem Text="Por Valor de Abertos" Value="VB"></asp:ListItem>
            </asp:DropDownList>
            <asp:CheckBox runat="server" ID="chkCrescente" ToolTip="Marque para que seja ordenado de forma crescente" />
        </li>
        <li class="liboth">
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
