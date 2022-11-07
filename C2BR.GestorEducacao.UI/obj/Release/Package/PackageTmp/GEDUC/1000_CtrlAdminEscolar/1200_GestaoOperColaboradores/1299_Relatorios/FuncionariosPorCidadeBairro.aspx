<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="FuncionariosPorCidadeBairro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1299_Relatorios.FuncionariosPorCidadeBairro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 480px;
        }
        .liUnidade, .liSituacao, .liUF
        {
            clear: both;
            margin-top: 5px;
        }
        .liCidade
        {
            margin-top: 5px;
            margin-left: 10px;
            width: 220px;
        }
        .liBairro
        {
            margin-top: 5px;
            margin-left: 10px;
            width: 180px;
        }
        .ddlSituacao
        {
            width: 125px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label class="lblObrigatorio" title="Unidade/Escola de Ensino">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola de Ensino"
                CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
        </li>
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade">
                Unidade Lotação/Contrato</label>
            <asp:DropDownList ID="ddlUnidContrato" CssClass="ddlUnidadeEscolar" runat="server"
                ToolTip="Selecione a Unidade">
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liUF">
                    <label class="lblObrigatorio" title="UF">
                        UF</label>
                    <asp:DropDownList ID="ddlUF" ToolTip="Selecione uma UF" CssClass="ddlUF" runat="server"
                        OnSelectedIndexChanged="ddlUF_SelectedIndexChanged1" AutoPostBack="True" style=" width:60px">
                    </asp:DropDownList>
                </li>
                <li class="liCidade">
                    <label class="lblObrigatorio" title="Cidade">
                        Cidade</label>
                    <asp:DropDownList ID="ddlCidade" ToolTip="Selecione uma Cidade" CssClass="ddlCidade"
                        runat="server" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </li>
                <li class="liBairro" title="Bairro">
                    <label class="lblObrigatorio">
                        Bairro</label>
                    <asp:DropDownList ID="ddlBairro" ToolTip="Selecione um Bairro" CssClass="ddlBairro"
                        runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
