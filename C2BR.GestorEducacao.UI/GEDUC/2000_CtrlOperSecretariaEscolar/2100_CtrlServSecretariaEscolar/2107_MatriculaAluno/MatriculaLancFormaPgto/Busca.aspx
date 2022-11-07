<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2107_MatriculaAluno.MatriculaLancFormaPgto.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            margin-left:25px;
            width:150px;
            margin-top:20px;
        }    
        .ulDados li
        {
            margin-top:7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label>Ano</label>
            <asp:TextBox runat="server" ID="txtAno" Width="30px"></asp:TextBox>
        </li>
        <li>
            <label for="ddlModalidade" title="Modalidade" class="lblObrigatorio">
                Modalidade</label>
            <asp:DropDownList ID="ddlModalidade" Style="width: 115px !important;" ToolTip="Selecione a Modalidade"
                CssClass="campoModalidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlModalidade_OnSelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlSerieCurso" title="Série/Curso" class="lblObrigatorio">
                Série/Curso</label>
            <asp:DropDownList ID="ddlSerieCurso" Style="width: 130px !important;" ToolTip="Selecione a Série/Curso"
                CssClass="campoSerieCurso" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSerieCurso_OnSelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlTurma" title="Turma" class="lblObrigatorio">
                Turma</label>
            <asp:DropDownList ID="ddlTurma" Style="width: 120px !important;" ToolTip="Selecione a Turma"
                CssClass="campoTurma" runat="server">
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
