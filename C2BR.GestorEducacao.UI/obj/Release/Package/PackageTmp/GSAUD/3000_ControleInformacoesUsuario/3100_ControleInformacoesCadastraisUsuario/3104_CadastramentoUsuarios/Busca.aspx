<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3104_CadastramentoUsuarios.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

 <style type="text/css">
        /*--> CSS LIs */
        .liPeriodoAte { clear: none !important; display:inline; margin-left: 0px; margin-top:13px;} 
        .liAux { margin-right: 5px; margin-left: 0px !important; clear:none !important; display:inline;}
        .liClear { clear: both; }
        
        /*--> CSS Dados*/
        .labelAux { margin-top: 16px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtNire" title="Número de Indentificação Social do Usuario">NIS</label>
        <asp:TextBox ID="txtNire" 
            ToolTip="Informe um Número de Indentificação Social do Usuario" 
            runat="server" MaxLength="10"></asp:TextBox>
    </li>
    <li>
        <label for="txtNome" title="Nome do Aluno">Nome</label>
        <asp:TextBox ID="txtNome" ToolTip="Informe o Nome de Aluno" runat="server" MaxLength="80" CssClass="campoNomePessoa"></asp:TextBox>
    </li>
    <li>
        <label for="txtCpf" title="CPF">CPF</label>
        <asp:TextBox ID="txtCpf" ToolTip="Informe um CPF" runat="server" CssClass="campoCpf"></asp:TextBox>
    </li> 
    <li>
        <label for="ddlDeficiencia" title="Deficiência">Deficiência</label>
        <asp:DropDownList ID="ddlDeficiencia" ToolTip="Informe uma Deficiência" runat="server" CssClass="ddlDeficiencia">
        </asp:DropDownList>
    </li>
    <li class="liClear">
        <label for="txtPeriodoDe" title="Intervalo de Nascimento">Intervalo de Nascto</label>
        <asp:TextBox ID="txtPeriodoDe" CssClass="campoData" runat="server" ToolTip="Informe a Data Inicial"></asp:TextBox>
    </li>
    <li class="liAux">
        <label class="labelAux">até</label>
    </li>
    <li class="liPeriodoAte">
        <asp:TextBox ID="txtPeriodoAte" CssClass="campoData" runat="server" ToolTip="Informe a Data Final"></asp:TextBox>
    </li>
    <li>
        <label for="ddlAnoOri" title="Deficiência">Ano de Origem</label>
        <asp:DropDownList ID="ddlAnoOri" ToolTip="Informe o ano de origem" runat="server" >
        </asp:DropDownList>
    </li>
</ul>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">

<script type="text/javascript">
    $(document).ready(function () {
        $(".txtNire").mask("?999999999");
        $(".campoCpf").mask("999.999.999-99");
    });
    </script>

</asp:Content>
