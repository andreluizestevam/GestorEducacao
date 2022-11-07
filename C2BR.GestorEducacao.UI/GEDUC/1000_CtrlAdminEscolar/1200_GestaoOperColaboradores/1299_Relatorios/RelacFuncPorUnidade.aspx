<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="RelacFuncPorUnidade.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1299_Relatorios.RelacFuncPorUnidade"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 430px;
            margin-left:365px;
        }
        .ulDados li
        {
            margin-left:5px;
        }
        .liUnidade, .liSituacao
        {
            clear: both;
            margin-top: 5px;
        }
        .ddlSituacao
        {
            width: 125px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label class="lblObrigatorio" title="Unidade/Escola de Ensino">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola de Ensino"
                CssClass="ddlUnidadeEscolar" runat="server" Width="260px">
            </asp:DropDownList>
        </li>
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade Lotação/Contrato</label>
            <asp:DropDownList ID="ddlUnidContrato" CssClass="ddlUnidadeEscolar" runat="server"
                ToolTip="Selecione a Unidade" Width="260px">
            </asp:DropDownList>
        </li>
        <li class="liSituacao">
            <label for="ddlSituacao" class="lblObrigatorio" title="Situação Atual">
                Situação</label>
            <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione a Situação do Funcionário"
                CssClass="ddlSituacao" runat="server">
                <asp:ListItem Value="T">Todas</asp:ListItem>
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
            <asp:RequiredFieldValidator ID="rfvSituacao" runat="server" ControlToValidate="ddlSituacao"
                ErrorMessage="Situação deve ser informada" Text="*" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liSituacao" style="clear:none">
            <label title="Selecione pelo que o relatório será agrupado">
                Agrupado por</label>
            <asp:DropDownList runat="server" ID="ddlAgrupadoPor" Width="140px" ToolTip="Selecione pelo que o relatório será agrupado">
            <asp:ListItem Text="Unidade Cadastro" Value="UC"></asp:ListItem>
            <asp:ListItem Text="Unidade Lotação/Contrato" Value="UL" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <%-- Este controle só é apresentado quando a unidade logada for do tipo SAÚDE --%>
        <li class="liUnidade" id="liClassFunc" runat="server">
            <label title="Selecione a Classificação Funcional">
                Classificação Funcional</label>
            <asp:DropDownList ID="ddlClassFunc" Width="90px" runat="server" ToolTip="Selecione a Classificação Funcional">
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
