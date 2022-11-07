﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ExtAgendaMonitoria.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3030_CtrlMonitoria._3099_ExtratoMonitoria.ExtAgendaMonitoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ulDados
        {
            width: 300px;
            margin-top: 40px;
            margin-left: 40%;
        }
        
        .ulDados li
        {
            margin-top: 5px;
            margin-left: 8px;
            margin-right: 0px !important;
        }
        
        .liboth
        {
            clear: both;
        }
        
        .ddlMat
        {
            width: 140px;
            clear: both;
        }
        .liUnidade, .liTipoRelatorio
        {
            margin-top: 5px;
            width: 240px;
        }
        
        .ddlModalidade
        {
            width: 140px;
            clear: both;
        }
        
        .ddlSerie
        {
            width: 100px;
            clear: both;
        }
        
        .ddlTurma
        {
            width: 120px;
            clear: both;
        }
        
        .ddlAluno
        {
            width: 205px;
            clear: both;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li style="width: 160px;">
            <asp:Label runat="server" ID="lbltpP">Tipo de Relatório</asp:Label>
            <asp:DropDownList runat="server" Width="160px" ID="tpP" ToolTip="Selecione o Tipo de Relatório">
                <asp:ListItem Text="Por Professor" Selected="True" Value="P"></asp:ListItem>
                <asp:ListItem Text="Por Disciplina" Value="D"></asp:ListItem>
                <asp:ListItem Text="Por Modalidade/Curso/Turma" Value="M"></asp:ListItem>
                <asp:ListItem Text="Professor X Disciplina" Value="PD"></asp:ListItem>
                <asp:ListItem Text="Por Agendamento de Monitoria" Value="A"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblModalidade" class="lblObrigatorio">Modalidade</asp:Label><br />
            <asp:DropDownList ID="ddlModalidade" class="ddlModalidade" runat="server" ToolTip="Selecione a Modalidade"
                OnSelectedIndexChanged="ddlModalidade_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlModalidade"
                ErrorMessage="Modalidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liboth" style="width: 100px;">
            <asp:Label runat="server" ID="lblSerieCurso" class="lblObrigatorio">Série/Curso</asp:Label><br />
            <asp:DropDownList ID="ddlSerieCurso" class="ddlSerie" runat="server" ToolTip="Selecione a Série/Curso"
                OnSelectedIndexChanged="ddlSerieCurso_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSerieCurso"
                ErrorMessage="Série/Curso deve ser informada" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li>
            <asp:Label runat="server" ID="lblTurma" class="lblObrigatorio">Turma</asp:Label><br />
            <asp:DropDownList ID="ddlTurma" class="ddlTurma" runat="server" ToolTip="Selecione a Turma"
                OnSelectedIndexChanged="ddlTurma_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTurma"
                ErrorMessage="Turma deve ser informado" Display="None"></asp:RequiredFieldValidator>
        </li>
        <li class="liboth" style="width: 90px;">
            <asp:Label runat="server" ID="lblMat" class="lblObrigatorio">Matéria</asp:Label>
            <asp:DropDownList ID="ddlMateria" runat="server" Width="135px" OnSelectedIndexChanged="ddlMateria_OnSelectedIndexChanged"
                AutoPostBack="true" ToolTip="Selecione a Matéria">
            </asp:DropDownList>
        </li>
        <li class="liboth" style="width: 270px;">
            <label for="ddlProfessor" class="lblObrigatorio">
                Professor</label>
            <asp:DropDownList ID="ddlProfessor" runat="server" Width="270px" ToolTip="Selecione o Professor">
            </asp:DropDownList>
        </li>
        <li class="liboth">
            <asp:Label runat="server" ID="lblPeriodo" class="lblObrigatorio">Período</asp:Label><br />
            <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="IniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> à </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="FimPeri"></asp:RequiredFieldValidator><br />
        </li>
        <li style="width: 70px; margin-left:10px;">
            <asp:Label ID="lblSitu" runat="server">Situação</asp:Label>
            <asp:DropDownList runat="server" ID="ddlSitu" Width="70px">
                <asp:ListItem Value="0">Todos</asp:ListItem>
                <asp:ListItem Value="R">Realizado</asp:ListItem>
                <asp:ListItem Value="C">Cancelado</asp:ListItem>
                <asp:ListItem Value="S">Suspenso</asp:ListItem>
                <asp:ListItem Value="N">Não Realizado</asp:ListItem>
                <asp:ListItem Value="A">Em Aberto</asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
