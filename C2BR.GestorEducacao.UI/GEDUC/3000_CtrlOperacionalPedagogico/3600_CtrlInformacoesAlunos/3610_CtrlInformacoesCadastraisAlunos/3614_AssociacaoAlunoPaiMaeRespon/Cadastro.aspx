<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3614_AssociacaoAlunoPaiMaeRespon.Cadastro" %>

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
            <label for="txtCodigo" title="NIRE">NIRE</label>
            <asp:TextBox ID="txtCodigo" CssClass="txtCodigo" Enabled="false" runat="server"></asp:TextBox>
        </li>
        <li>
            <label for="ddlAluno" title="Aluno">Aluno</label>
            <asp:DropDownList ID="ddlAluno" ToolTip="Selecione o Aluno" runat="server" CssClass="ddlAluno" AutoPostBack="true" Enabled="false"
                OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged"></asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="txtResponsavelAtual" title="Respons�vel Atual">Respons�vel Atual</label>
            <asp:TextBox ID="txtResponsavelAtual" ToolTip="Respons�vel Atual" CssClass="txtResponsavelAtual" Enabled="false" runat="server"></asp:TextBox>
        </li>
        <li class="liClear">
            <label for="ddlResponsavel" title="Novo Respons�vel">Novo Respons�vel</label>
            <asp:DropDownList ID="ddlResponsavel" ToolTip="Selecione o Novo Respons�vel" CssClass="txtResponsavelAtual" runat="server"></asp:DropDownList>
        </li>
        <li class="liClear" id="liCheck" runat="server" visible="false">
            <asp:CheckBox ID="chkFinanceiro" Text="Substituir Respons�vel Financeiro nos T�tulos de Receita" runat="server" />
        </li>
    </ul>
</asp:Content>
