<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelatorioDeChequesDeMatricula.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2100_CtrlServSecretariaEscolar._2199_Relatorios.RelatorioDeChequesDeMatricula" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style type="text/css">
        .ulDados
        {
            width: 860px;
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
<ul class = "ulDados">
        <li class="liClear">
            <label for="ddlAno" title="Selecione o ano de referência desejado">Ano</label>
            <asp:DropDownList ID="ddlAno" runat="server" Width="47px" ToolTip="Selecione o ano de referência desejado">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlEmp" title="Selecione a unidade desejada">Unidade</label>
            <asp:DropDownList ID="ddlEmp" runat="server" Width="180px" ToolTip="Selecione a unidade desejada">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlCol" title="Selecione o colaborador desejado">Colaborador</label>
            <asp:DropDownList ID="ddlCol" runat="server" Width="180px" ToolTip="Selecione o colaborador desejado">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlMod" title="Selecione a modalidade desejada">Modalidade</label>
            <asp:DropDownList ID="ddlMod" Width="160px" OnSelectedIndexChanged="ddlMod_SelectedIndexChanged" AutoPostBack="true" runat="server" ToolTip="Selecione a modalidade desejada">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlSer" title="Selecione a série desejada">Série/Curso</label>
            <asp:DropDownList ID="ddlSer" Width="190px" OnSelectedIndexChanged="ddlSer_SelectedIndexChanged" AutoPostBack="true" runat="server" ToolTip="Selecione a série desejada">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlTur" title="Selecione a turma desejada">Turma</label>
            <asp:DropDownList ID="ddlTur" Width="170px" runat="server" ToolTip="Selecione a turma desejada">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label>Período</label>
            <asp:TextBox ID="txtDtIni" runat="server" CssClass="campoData"></asp:TextBox>
            &nbsp;à&nbsp;
            <asp:TextBox ID="txtDtFim" runat="server" CssClass="campoData"></asp:TextBox>
        </li>
</ul>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
