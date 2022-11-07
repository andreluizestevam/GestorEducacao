<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3618_RegistroOcorrenciaSaudeAluno.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlUnidade">Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" CssClass="campoUnidadeEscolar" AutoPostBack="true "
                OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged" ToolTip="Selecione a Unidade/Escola"></asp:DropDownList>
        </li>
        <li>
            <label for="ddlAluno">Aluno</label>
            <asp:DropDownList ID="ddlAluno" runat="server" CssClass="campoNomePessoa" ToolTip="Selecione o Aluno desejado"></asp:DropDownList>
        </li>
        <li>
            <label for="txtDataConsulta" title="Data da Consulta">Data Consulta</label>
            <asp:TextBox ID="txtDataConsulta" runat="server" CssClass="campoData" ToolTip="Data de Consulta"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
