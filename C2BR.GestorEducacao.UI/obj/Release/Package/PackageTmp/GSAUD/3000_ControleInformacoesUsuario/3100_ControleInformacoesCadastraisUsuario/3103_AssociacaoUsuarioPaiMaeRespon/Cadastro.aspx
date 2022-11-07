<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3100_ControleInformacoesCadastraisUsuario._3103_AssociacaoUsuarioPaiMaeRespon.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">             
        .ulDados { width: 350px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        .txtCodigo { width: 60px; text-align: right; } 
        .ddlAluno { width: 237px; }
        .txtResponsavelAtual { width: 300px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
 <ul class="ulDados">
        <li>
            <label for="txtCodigo" title="NIRE">Código</label>
            <asp:TextBox ID="txtCodigo" CssClass="txtCodigo" Enabled="false" runat="server"></asp:TextBox>
        </li>
        <li>
            <label for="ddlAluno" title="Aluno">Usuário de Saúde</label>
            <asp:DropDownList ID="ddlAluno" ToolTip="Selecione o Aluno" runat="server" CssClass="ddlAluno" AutoPostBack="true" Enabled="false"
                OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged"></asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="txtResponsavelAtual" title="Responsável Atual">Responsável Atual</label>
            <asp:TextBox ID="txtResponsavelAtual" ToolTip="Responsável Atual" CssClass="txtResponsavelAtual" Enabled="false" runat="server"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="ddlResponsavel" title="Novo Responsável">Novo Responsável</label>
            <asp:DropDownList ID="ddlResponsavel" ToolTip="Selecione o Novo Responsável" CssClass="txtResponsavelAtual" runat="server"></asp:DropDownList>
        </li>
        <li class="liClear" style="margin-top: 10px;">
            <label for="ddlFamilia" title="Seleciona a nova Família">Família</label>
            <asp:DropDownList ID="ddlFamilia" ToolTip="Selecione a nova Família" CssClass="txtResponsavelAtual" runat="server"></asp:DropDownList>
        </li>
        <li class="liClear" id="liCheck" runat="server" visible="false">
            <asp:CheckBox ID="chkFinanceiro" Text="Substituir Responsável Financeiro nos Títulos de Receita" runat="server" />
        </li>
    </ul>
</asp:Content>
