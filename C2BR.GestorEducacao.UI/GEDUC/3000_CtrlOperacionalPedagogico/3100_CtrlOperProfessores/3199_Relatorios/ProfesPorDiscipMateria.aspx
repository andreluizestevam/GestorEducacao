<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="ProfesPorDiscipMateria.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3100_CtrlOperProfessores.F3199_Relatorios.ProfesPorDiscipMateria"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
        }
        .ulDados li
        {
            margin-left:5px;
            margin-top:5px;
        }
        .liUnidade
        {
            margin-top: 5px;
            width: 300px;
        }
        .liTipo
        {
            margin-top: 5px;
        }
        .liMateria
        {
            clear: both;
            margin-top: 5px;
        }
        .ddlTipo
        {
            width: 90px;
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
                    <label id="Label1" title="Unidade/Escola" class="lblObrigatorio" runat="server">
                        Unidade/Escola</label>
                    <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar"
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li style="clear: both;">
                    <label title="Ano letivo" for="ddlAno">
                        Ano Letivo</label>
                    <asp:DropDownList ID="ddlAno" ToolTip="Selecione um ano letivo de referência" runat="server"
                        AutoPostBack="True" Style="width: 60px !important;" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Modalidade" for="ddlModalidade">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" ToolTip="Selecione uma modalidade para referência"
                        runat="server" Width="210px" AutoPostBack="True" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li style="clear: both">
                    <label>
                        Curso</label>
                    <asp:DropDownList ID="ddlSerie" Style="width: 120px !important;" ToolTip="Selecione uma série/turma de referência"
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSerie_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li>
                    <label title="Turma" for="ddlTurma">
                        Turma</label>
                    <asp:DropDownList ID="ddlTurma" ToolTip="Selecione uma turma de referência" Style="width: 140px !important;"
                        runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </li>
                <li id="liMateria" class="liMateria" runat="server">
                    <label id="lblMateria" title="Matéria" class="lblObrigatorio">
                        Matéria</label>
                    <asp:DropDownList ID="ddlMateria" ToolTip="Selecione a Matéria" CssClass="ddlMateria"
                        runat="server">
                    </asp:DropDownList>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li class="liTipo">
            <label class="lblObrigatorio" title="Classificação">
                Classificação</label>
            <asp:DropDownList ID="ddlTipo" ToolTip="Selecione a Classificação" CssClass="ddlTipo"
                runat="server">
                <asp:ListItem Selected="True" Value="P">Por Professor</asp:ListItem>
                <asp:ListItem Value="M">Por Matéria</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
