<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1109_CtrlRestrAtend.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 270px;
            margin-top: 20px;
        }
        .ulDados li
        {
            margin-bottom: -5px;
        }
        .ulDados li label
        {
            margin-bottom: 2px;
        }
        .top
        {
            margin-top: 6px;
        }
        input
        {
            height: 13px;
        }
        .ddlReg
        {
            width: 150px;
            clear: both;
        }
        .txtNomeFeriado
        {
            width: 260px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidSituacao" />
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtCodigo" class="lblObrigatorio" title="Código ">
                Código
            </label>
            <asp:TextBox ID="txtCodigo" ToolTip="Código"  style=" width:43px" MaxLength="6" runat="server"></asp:TextBox>
        </li>
        <li class="liOcorrencia">
            <label for="NomeFeriado" title="Descrição  do feriado ">
                Nome
            </label>
            <asp:TextBox ID="txtNome" runat="server" MaxLength="100" ToolTip="Nome" CssClass="txtNomeFeriado"></asp:TextBox>
        </li>
        <li>
            <label>
                Descrição
            </label>
            <asp:TextBox ID="txtDescricao"  style="width:400px;" ToolTip="Descrição" MaxLength="300" CssClass="" runat="server" TextMode="MultiLine"></asp:TextBox>
        </li>
        <li class="top">
            <label title="Status">
                Situação
            </label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="70px" ToolTip="Situação de cadastro">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
