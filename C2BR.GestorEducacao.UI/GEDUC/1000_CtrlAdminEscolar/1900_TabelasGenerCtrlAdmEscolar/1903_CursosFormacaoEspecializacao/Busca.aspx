<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1903_CursosFormacaoEspecializacao.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .txtDescricaoCurso { width: 265px; }
        .txtSiglaCurso
        {
            width: 80px;
            text-transform: uppercase;
            clear: both;
            margin-top: 1px;
        }
        .fldDados tr td { border: none; }
        .fldDados label { display: inline; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca" style="border-style: none">
        <li>
            <label for="txtDE_ESPEC" title="Curso">
                Curso</label>
            <asp:TextBox ID="txtDE_ESPEC" class="txtDescricaoCurso" runat="server" MaxLength="40"
                ToolTip="Informe o Curso">
            </asp:TextBox>
        </li>
        <li>
            <label for="txtNO_SIGLA_ESPEC" title="Sigla">
                Sigla</label>
            <asp:TextBox ID="txtNO_SIGLA_ESPEC" class="txtSiglaCurso" runat="server" MaxLength="12"
                ToolTip="Informe a Sigla">
            </asp:TextBox>
        </li>
        <li>
            <fieldset class="fldDados">
                <legend title="Tipos de Curso">Tipos de Curso</legend>
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
