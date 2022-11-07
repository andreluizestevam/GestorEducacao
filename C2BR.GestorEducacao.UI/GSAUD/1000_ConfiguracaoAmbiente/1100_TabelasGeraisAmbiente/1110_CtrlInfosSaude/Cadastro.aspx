<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1110_CtrlInfosSaude.Cadastro" %>

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
        .Nome
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
                Sigla
            </label>
            <asp:TextBox ID="txtSigla" ToolTip="Sigla" Style="width: 43px" MaxLength="6" runat="server"></asp:TextBox>
        </li>
        <li class="liOcorrencia">
            <label for="NomeFeriado" title="Descrição  do feriado ">
                Nome
            </label>
            <asp:TextBox ID="txtNome" runat="server" Style="width: 260px;" MaxLength="100" ToolTip="Nome"></asp:TextBox>
        </li>
        <li>
            <label>
                Descrição
            </label>
            <asp:TextBox ID="txtDescricao" Style="width: 260px; height:60px" ToolTip="Descrição" MaxLength="400"
                CssClass="" runat="server" TextMode="MultiLine"></asp:TextBox>
        </li>
        <li>
            <br />
            <label title="Status">
                Situação
            </label>
            <asp:DropDownList runat="server" ID="ddlSituacao" Width="70px" ToolTip="Situação de cadastro">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
