<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="MovFinacConsultas.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._8000_GestaoAtendimento._8100_CtrlAgendaMedica._8199_Relatorios.MovFinacConsultas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ulDados
        {
            width: 860px;
            margin-left: 30px;
        }
        
        .ulDados li
        {
            clear: none;
            margin-left: 335px;
            margin-top: 5px;
        }
        
        .liClear
        {
            clear: both !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="liClear">
            <label for="ddlEmp" title="Selecione a unidade desejada">
                Unidade de Consulta
            </label>
            <asp:DropDownList ID="ddlUnidadeDeConsulta" runat="server" Width="270px" ToolTip="Selecione">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlEmp" title="Selecione a unidade desejada">
                Unidade de Cadastro</label>
            <asp:DropDownList ID="dllUnidadeCadastro" OnSelectedIndexChanged="dllUnidadeCadastro_SelectedIndexChanged"
                AutoPostBack="true" runat="server" Width="270px" ToolTip="Selecione">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlEmp" title="Selecione a unidade desejada">
                Unidade Lotação/Contrato</label>
            <asp:DropDownList ID="ddlUnidadeContrato" runat="server" Width="270px" ToolTip="Selecione" OnSelectedIndexChanged="ddlUnidadeContrato_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlCol" title="Selecione ">
                Especialidade</label>
            <asp:DropDownList ID="ddlEspecialidade" runat="server" Width="185px" ToolTip="Selecione " OnSelectedIndexChanged="ddlEspecialidade_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlClassificacoesProfissional" title="Selecione a ">
                Classificação Profissional</label>
            <asp:DropDownList ID="ddlProgramacoaProfissional" Width="90px" runat="server" ToolTip="Selecione"  OnSelectedIndexChanged="ddlProgramacoaProfissional_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlSer" title="">
                Profissional Saúde</label>
            <asp:DropDownList ID="ddlProfissionalSaude" Width="270px" runat="server" ToolTip="Selecione">
                <asp:ListItem Value="">Todos</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear">
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
