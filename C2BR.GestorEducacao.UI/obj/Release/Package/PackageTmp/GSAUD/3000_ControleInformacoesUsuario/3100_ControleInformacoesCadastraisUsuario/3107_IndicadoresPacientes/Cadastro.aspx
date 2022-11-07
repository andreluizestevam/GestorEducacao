<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3107_IndicadoresPacientes.Cadastro" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:HiddenField runat="server" ID="hidSituacao" />
    <ul id="ulDados" class="ulDados">
        <li>
            <label>
                CPF</label>
            <asp:TextBox runat="server" ID="txtCPF" CssClass="campoCpf" Width="75px"></asp:TextBox>
        </li>
        <li>
            <label class="lblObrigatorio">
                Nome</label>
            <asp:TextBox runat="server" MaxLength="200" ID="txtnome" ToolTip="Nome" Width="250px"></asp:TextBox>
        </li>
        <li>
            <label title="Classificação Funcional">
                Classificação Funcional
            </label>
            <asp:DropDownList ID="ddlClassificacoesFuncionais" CssClass="ddlReg" runat="server">
            </asp:DropDownList>
        </li>
        <li></li>
        <li>
            <label>
                Sigla</label>
            <asp:DropDownList runat="server" ID="ddlSiglaEntidProfi" style=" width: 60px" CssClass="ddlSiglaEntd"
                ToolTip="Sigla da Entidade">
                <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                <asp:ListItem Value="CRM" Text="CRM"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label class="top">
                Nº</label>
            <asp:TextBox runat="server" MaxLength="20" ID="txtNrEntidProfi" CssClass="txtNrEntidProfi"
                ToolTip="Número da Entidade Profissional"></asp:TextBox>
        </li>
        <li>
            <label class="top">
                UF</label>
            <asp:DropDownList runat="server" ID="ddlUfEntidProfi" CssClass="ddlUF" ToolTip="UF de emissão da Entidade Profissional">
            </asp:DropDownList>
        </li>
        <li>
            <label class="top">
                Data</label>
            <asp:TextBox runat="server" ID="txtDtEmissEntidProfi" CssClass="campoData" ToolTip="Data de Emissão da Entidade Profissional"></asp:TextBox>
        </li>
        <li>
            <label title="Situação da Operadora">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSitu" Width="70px" ToolTip="Situação da Operadora">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoCpf").mask("999.999.999-99");
        });
    </script>
</asp:Content>
