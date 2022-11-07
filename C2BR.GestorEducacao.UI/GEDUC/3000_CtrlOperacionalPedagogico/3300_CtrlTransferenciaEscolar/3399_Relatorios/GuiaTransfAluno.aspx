﻿<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="GuiaTransfAluno.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3300_CtrlTransferenciaEscolar.F3399_Relatorios.GuiaTransfAluno"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
        }
        .liUnidade, .liAnoRefer, .liAluno
        {
            margin-top: 5px;
            width: 300px;
        }
        .liModalidade
        {
            clear: both;
            width: 140px;
            margin-top: 5px;
        }
        .liSerie
        {
            clear: both;
            margin-top: 5px;
        }
        .liTurma
        {
            margin-left: 5px;
            margin-top: 5px;
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
                    <label id="Label1" class="lblObrigatorio" runat="server">
                        Unidade/Escola</label>
                    <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlUnidade" Text="*" ErrorMessage="Campo Unidade/Escola é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liAnoRefer">
                    <label class="lblObrigatorio">
                        Ano Referência</label>
                    <asp:DropDownList ID="ddlAnoRefer" CssClass="ddlAno" runat="server" ToolTip="Selecione o Ano de Referência"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlAnoRefer_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li class="liModalidade">
                    <label class="lblObrigatorio" for="ddlModalidade">
                        Modalidade</label>
                    <asp:DropDownList ID="ddlModalidade" CssClass="ddlModalidade" runat="server" AutoPostBack="true"
                        ToolTip="Selecione a Modalidade" OnSelectedIndexChanged="ddlModalidade_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlModalidade" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlModalidade" Text="*" ErrorMessage="Campo Modalidade é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liSerie">
                    <label class="lblObrigatorio" for="ddlSerieCurso">
                        Série/Curso</label>
                    <asp:DropDownList ID="ddlSerieCurso" CssClass="ddlSerieCurso" runat="server" AutoPostBack="true"
                        ToolTip="Selecione a Série/Curso" OnSelectedIndexChanged="ddlSerieCurso_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlSerieCurso" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlSerieCurso" Text="*" ErrorMessage="Campo Série/Curso é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li class="liTurma">
                    <label id="lblTurma" class="lblObrigatorio" for="ddlTurma">
                        Turma</label>
                    <asp:DropDownList ID="ddlTurma" CssClass="ddlTurma" runat="server" ToolTip="Selecione a Turma"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlTurma_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvddlTurma" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlTurma" Text="*" ErrorMessage="Campo Turma é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
                <li style="clear: both">
                    <label>
                        Tipo</label>
                    <asp:DropDownList runat="server" ID="ddlTipoTrans" Width="90px" ToolTip="tipo da transferência"
                        OnSelectedIndexChanged="ddlTipoTrans_OnSelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="0" Selected="True">Todas</asp:ListItem>
                        <asp:ListItem Value="T">Interna</asp:ListItem>
                        <asp:ListItem Value="X">Externa</asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="liAluno">
                    <label class="lblObrigatorio">
                        Aluno</label>
                    <asp:DropDownList ID="ddlAlunos" CssClass="ddlNomePessoa" runat="server" ToolTip="Selecione o Aluno">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
                        ControlToValidate="ddlAlunos" Text="*" ErrorMessage="Campo Aluno é requerido"
                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
