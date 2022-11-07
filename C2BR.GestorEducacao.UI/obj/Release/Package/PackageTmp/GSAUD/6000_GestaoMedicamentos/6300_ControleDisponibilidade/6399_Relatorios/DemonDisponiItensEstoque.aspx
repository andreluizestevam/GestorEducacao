<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="DemonDisponiItensEstoque.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._6000_GestaoMedicamentos._6300_ControleDisponibilidade._6399_Relatorios.DemonDisponiItensEstoque" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 250px;
        }
        .ulDados li
        {
            margin: 5px 0 0 5px;
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
        .chk label
        {
            display: inline;
        }
        .lblsub
        {
            color: #436EEE;
            clear: both;
            margin-bottom: -3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="lblsub" style="margin-bottom: -5px">
            <label>
                Parâmetros</label>
        </li>
        <li>
            <label title="Unidade do estoque e movimentação do produto">
                Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnidade" OnSelectedIndexChanged="ddlUnidade_OnSelectedIndexChanged"
                AutoPostBack="true" ToolTip="Unidade do estoque e movimentação do produto" Width="200px">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Grupo do Produto">
                Grupo</label>
            <asp:DropDownList runat="server" ID="ddlGrupo" Width="130px" ToolTip="Grupo do Produto"
                OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Subgrupo do Produto">
                Subgrupo</label>
            <asp:DropDownList runat="server" ID="ddlSubGrupo" Width="120px" ToolTip="Subgrupo do Produto">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Selecione o Item, caso deseja fazer a busca de forma específica">
                Item</label>
            <asp:DropDownList runat="server" ID="ddlProduto" ToolTip="Selecione o Item, caso deseja fazer a busca de forma específica"
                Width="150px">
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio">Período</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
        </li>
        <li class="lblsub" style="margin-bottom: -2px">
            <label>
                Visualização</label>
        </li>
        <li style="clear: both; margin-left: 0px !important;">
            <asp:CheckBox runat="server" ID="chkGraficos" Text="Gráficos" CssClass="chk" ToolTip="Selecione caso deseje que seja impresso um Gráfico Demonstrativo junto ao Relatório"
                Checked="true" />
        </li>
        <li>
            <asp:CheckBox runat="server" ID="chkRelatorio" Text="Relatório" CssClass="chk" ToolTip="Quando selecionado, apresenta as informações do Relatório"
                Checked="true" />
        </li>
        <li style="clear: both">
            <label title="Selecione a ordenação desejada">
                Ordenado por</label>
            <asp:DropDownList runat="server" ID="ddlClassificacao" ToolTip="Selecione a ordenação desejada"
                Width="80px" ClientIDMode="Static">
                <asp:ListItem Value="1" Text="Produto"></asp:ListItem>
                <asp:ListItem Value="2" Text="SubGrupo"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="margin-left: 3px">
            <label title="Selecione a Ordenação para o relatório (Crescente ou Decrescente">
                Ordem</label>
            <asp:DropDownList runat="server" ID="ddlTipoOrdem" Width="80px" ToolTip="Selecione a Ordenação para o relatório (Crescente ou Decrescente)"
                ClientIDMode="Static" CssClass="ddlTipoOrdem">
                <asp:ListItem Value="C" Text="Crescente"></asp:ListItem>
                <asp:ListItem Value="D" Text="Decrescente"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
