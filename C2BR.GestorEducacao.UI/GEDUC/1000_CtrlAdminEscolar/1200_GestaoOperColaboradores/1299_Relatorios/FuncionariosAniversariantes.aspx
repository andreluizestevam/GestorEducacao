<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="FuncionariosAniversariantes.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1299_Relatorios.FuncionariosAniversariantes"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 230px;
        }
        .liUnidade, .liMesReferencia, .liTipoCol
        {
            margin-top: 5px;
            width: 200px;
            clear: both;
        }
        .ddlMesReferencia
        {
            width: 75px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola">
            </asp:DropDownList>
        </li>
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade Lotação/Contrato</label>
            <asp:DropDownList ID="ddlUnidContrato" CssClass="ddlUnidadeEscolar" runat="server"
                ToolTip="Selecione a Unidade">
            </asp:DropDownList>
        </li>
        <li class="liTipoCol">
            <label class="lblObrigatorio" for="ddlTipoColaborador">
                Tipo do Colaborador</label>
            <asp:DropDownList ID="ddlTipoColaborador" CssClass="ddlTipoCol" runat="server" ToolTip="Selecione o Tipo do Colaborador"
                AutoPostBack="True" Width="120px">
              <%--  <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
                <asp:ListItem Value="N">Funcionários</asp:ListItem>
                <asp:ListItem Value="S">Professores</asp:ListItem>--%>
            </asp:DropDownList>
        </li>
        <li class="liMesReferencia">
            <label class="lblObrigatorio" for="txtMesReferencia">
                Mês de Referência</label>
            <asp:DropDownList ID="ddlMesReferencia" CssClass="ddlMesReferencia" runat="server"
                ToolTip="Selecione o Mês de Referência">
                <asp:ListItem Value="0">Todos</asp:ListItem>
                <asp:ListItem Value="1">Janeiro</asp:ListItem>
                <asp:ListItem Value="2">Fevereiro</asp:ListItem>
                <asp:ListItem Value="3">Março</asp:ListItem>
                <asp:ListItem Value="4">Abril</asp:ListItem>
                <asp:ListItem Value="5">Maio</asp:ListItem>
                <asp:ListItem Value="6">Junho</asp:ListItem>
                <asp:ListItem Value="7">Julho</asp:ListItem>
                <asp:ListItem Value="8">Agosto</asp:ListItem>
                <asp:ListItem Value="9">Setembro</asp:ListItem>
                <asp:ListItem Value="10">Outubro</asp:ListItem>
                <asp:ListItem Value="11">Novembro</asp:ListItem>
                <asp:ListItem Value="12">Dezembro</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
