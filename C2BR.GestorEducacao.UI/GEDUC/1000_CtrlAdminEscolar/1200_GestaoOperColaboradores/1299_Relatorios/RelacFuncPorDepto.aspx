<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="RelacFuncPorDepto.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1299_Relatorios.RelacFuncPorDepto"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 230px;
        }
        .liUnidade, .liSituacao, .liDepto
        {
            clear: both;
            margin-top: 5px;
        }
        .ddlDepto
        {
            width: 230px;
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <li class="liUnidade">
                    <label class="lblObrigatorio" title="Unidade/Escola de Ensino">
                        Unidade</label>
                    <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola de Ensino"
                        CssClass="ddlUnidadeEscolar" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liUnidade">
                    <label class="lblObrigatorio" for="txtUnidade">
                        Unidade Lotação/Contrato</label>
                    <asp:DropDownList ID="ddlUnidContrato" CssClass="ddlUnidadeEscolar" runat="server"
                        ToolTip="Selecione a Unidade">
                    </asp:DropDownList>
                </li>
                <li class="liDepto">
                    <label class="lblObrigatorio">
                        Departamento local</label>
                    <asp:DropDownList ID="ddlDepto" ToolTip="Selecione um Departamento" CssClass="ddlDepto"
                        runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDepto"
                        ErrorMessage="Departamento deve ser informado" Text="*" Display="None"></asp:RequiredFieldValidator>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
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
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
