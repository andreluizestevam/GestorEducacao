<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true"
    CodeBehind="RelacCursosFormacao.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F9299_Relatorios.RelacCursosFormacao" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; } /* Usado para definir o formulário ao centro */
        .fldDados tr td
        {
            border: none;
        }
        .fldDados label
        {
            display: inline;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <fieldset class="fldDados">
                <legend title="Tipos de Curso*">Tipos de Curso</legend>
                <asp:CheckBoxList ID="rblTipo" runat="server" RepeatColumns="2" RepeatDirection="Vertical"
                    CssClass="rblTipo" Width="290px" BorderStyle="None">
                    <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                    <asp:ListItem Value="EP">Específico</asp:ListItem>
                    <asp:ListItem Value="TE">Técnico</asp:ListItem>
                    <asp:ListItem Value="GR">Graduação</asp:ListItem>
                    <asp:ListItem Value="ES">Especialização</asp:ListItem>
                    <asp:ListItem Value="MB">MBA</asp:ListItem>
                    <asp:ListItem Value="PG">Pós-Graduação</asp:ListItem>
                    <asp:ListItem Value="ME">Mestrado</asp:ListItem>
                    <asp:ListItem Value="DO">Doutorado</asp:ListItem>
                    <asp:ListItem Value="PD">Pós-Doutorado</asp:ListItem>
                    <asp:ListItem Value="OU">Outros</asp:ListItem>
                </asp:CheckBoxList>
            </fieldset>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
