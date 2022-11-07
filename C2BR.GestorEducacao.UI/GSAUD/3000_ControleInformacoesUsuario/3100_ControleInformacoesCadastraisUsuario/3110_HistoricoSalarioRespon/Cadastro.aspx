<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3110_HistoricoSalarioRespon.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 800px;
        }
        
        /*--> CSS LIs */
        .liClear
        {
            clear: both;
        }
        /*--> CSS DADOS */
        .divGrid
        {
            height: 267px;
            width: 800px;
            overflow-y: scroll;
            margin-top: 10px;
        }
        .liAlinhaEsquerda
        {
            margin-left: 100px;
        }
        .liRendi
        {
            margin-top: 35px;
        }
        .txtRendi
        {
            width: 152px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liAlinhaEsquerda">
            <label for="ddlUnidade" title="Unidade" class="lblObrigatorio">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione a Unidade" runat="server" AutoPostBack="true"
                Width="200px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvUnidade" runat="server" ControlToValidate="ddlUnidade"
                CssClass="validatorField" ErrorMessage="A Unidade deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlAssociado" title="Associado" class="lblObrigatorio">
                Associado</label>
            <asp:DropDownList ID="ddlAssociado" ToolTip="Selecione o Associado" CssClass="campoNomePessoa"
                runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvAssociado" runat="server" ControlToValidate="ddlAssociado"
                CssClass="validatorField" ErrorMessage="O Associado deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li style="margin-right: 55px;">
            <label for="txtAnoMes" class="lblObrigatorio" title="Ano/Mês de Referência">
                Ano/Mês</label>
            <asp:TextBox ID="txtAnoMes" CssClass="txtAnoMes" runat="server" ToolTip="Informe o Ano/Mês de Referência" Width="45px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvAnoMes" runat="server" ControlToValidate="txtAnoMes"
                CssClass="validatorField" ErrorMessage="A Unidade deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>        
        <li class="liAlinhaEsquerda" style="margin-top: 10px;">
            <label title="Renda Principal">
                Renda Principal</label>
        </li>
        <li class="liRendi" style="margin-left: -73px">
            <label for="txtPrincipalRendimento" title="R$ Rendimento">
                R$ Rendimento</label>
            <asp:TextBox ID="txtPrincipalRendimento" CssClass="txtPrincipalRendimento txtRendi"
                runat="server" ToolTip="Informe o Rendimento Principal"></asp:TextBox>
        </li>
        <li class="liRendi">
            <label for="txtPrincipalDescontos" title="R$ Descontos">
                R$ Descontos</label>
            <asp:TextBox ID="txtPrincipalDescontos" CssClass="txtPrincipalDescontos txtRendi"
                runat="server" ToolTip="Informe os Descontos Principais"></asp:TextBox>
        </li>
        <li class="liRendi" style="margin-right: 80px;">
            <label for="txtPrincipalLiquido" title="R$ Liquido">
                R$ Liquido</label>
            <asp:TextBox ID="txtPrincipalLiquido" CssClass="txtPrincipalLiquido txtRendi" runat="server"
                ToolTip="Informe o Liquido Principal"></asp:TextBox>
        </li>
        <li class="liAlinhaEsquerda" style="margin-top: 10px;">
            <label title="Renda Extra">
                Renda Extra</label>
        </li>
        <li class="liRendi" style="margin-left: -58px;">
            <label for="txtExtraRendimento" title="R$ Rendimento">
                R$ Rendimento</label>
            <asp:TextBox ID="txtExtraRendimento" CssClass="txtExtraRendimento txtRendi" runat="server"
                ToolTip="Informe o Rendimento Extra"></asp:TextBox>
        </li>
        <li class="liRendi">
            <label for="txtExtraDescontos" title="R$ Descontos">
                R$ Descontos</label>
            <asp:TextBox ID="txtExtraDescontos" CssClass="txtExtraDescontos txtRendi" runat="server"
                ToolTip="Informe os Descontos Extras"></asp:TextBox>
        </li>
        <li class="liRendi" style="margin-right:160px;">
            <label for="txtExtraLiquido" title="R$ Liquido">
                R$ Liquido</label>
            <asp:TextBox ID="txtExtraLiquido" CssClass="txtExtraLiquido txtRendi" runat="server"
                ToolTip="Informe o Liquido Extra"></asp:TextBox>
        </li>
        <li id="liSituacao" runat="server" visible="false" style="margin-top:10px;margin-left:100px;">
            <label for="ddlSituacao" title="Situação" class="lblObrigatorio">
                Situação</label>
            <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione a Situação" runat="server">
                <asp:ListItem Value="A">Ativo</asp:ListItem>
                <asp:ListItem Value="I">Inativo</asp:ListItem>
                <asp:ListItem Value="V">Validação</asp:ListItem>
                <asp:ListItem Value="S">Suspenso</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
    <asp:HiddenField ID="hdnIdHistoSalar" runat="server" />
    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(".txtAnoMes").mask("9999/99");
            $(".txtPrincipalRendimento").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtPrincipalDescontos").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtPrincipalLiquido").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtExtraRendimento").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtExtraDescontos").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtExtraLiquido").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });

        $(document).ready(function () {
            $(".txtAnoMes").mask("9999/99");
            $(".txtPrincipalRendimento").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtPrincipalDescontos").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtPrincipalLiquido").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtExtraRendimento").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtExtraDescontos").maskMoney({ symbol: "", decimal: ",", thousands: "." });
            $(".txtExtraLiquido").maskMoney({ symbol: "", decimal: ",", thousands: "." });
        });
    </script>
</asp:Content>
