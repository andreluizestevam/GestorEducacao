<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="RelacOcorrencias.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3640_CtrlOcorrenciasAlunos._3642_RelacOcorrAluno.RelacOcorrencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 420px;
            margin:0 0 0 350px !important;
        }
        .ulDados li
        {
            margin: 10px 0 0 10px;
        }
        .input
        {
            height: 13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label>
                Ano</label>
            <asp:DropDownList runat="server" ID="ddlAno" Width="50px" OnSelectedIndexChanged="ddlAno_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li style="clear:both">
            <label>
                Modalidade</label>
            <asp:DropDownList runat="server" ID="ddlModalidade" Width="160px" OnSelectedIndexChanged="ddlModalidade_OnSelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Curso</label>
            <asp:DropDownList runat="server" ID="ddlCurso" Width="100px" OnSelectedIndexChanged="ddlCurso_OnSelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li>
            <label>
                Turma</label>
            <asp:DropDownList runat="server" ID="ddlTurma" Width="140px" OnSelectedIndexChanged="ddlTurma_OnSelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
        </li>
                <li class="liTipoOcorrencia">
            <label for="ddlTipoOcorrencia" class="lblObrigatorio" title="Tipo Ocorrência">
                Tipo de Ocorrência</label>
            <asp:DropDownList ID="ddlTipoOcorrencia" ToolTip="Selecione o Tipo de Ocorrência"
                CssClass="ddlTipoOcorrencia" runat="server">
            </asp:DropDownList>
        </li>
        <li style="clear:both;">
            <label for="ddlAluno" class="lblObrigatorio" title="Aluno">
                Aluno</label>
            <asp:DropDownList ID="ddlAluno" ToolTip="Selecione o Aluno" CssClass="ddlAluno" runat="server" Width="240px">
            </asp:DropDownList>
        </li>
        <li style="clear:both">
            <label>
                Período</label>
            <asp:TextBox ID="txtDtIni" runat="server" CssClass="campoData"></asp:TextBox>
            &nbsp;à&nbsp;
            <asp:TextBox ID="txtDtFim" runat="server" CssClass="campoData"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
