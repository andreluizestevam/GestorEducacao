<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3105_CtrlInformacoesResponsaveis.CtrlInformacoesResponsaveis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        /*--> CSS DADOS */
        .txtCpf { width: 82px; }
        .txtNome { width: 210px; }
        .ddlCategoriaFuncional{width:95px;}
        .ddlDeficiencia,.ddlSituacao{width:70px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

 <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtCpf" title="CPF">
               CPF</label>
            <asp:TextBox ID="txtCpf" ToolTip="Informe o CPF" class="txtCpf" runat="server" MaxLength="15"></asp:TextBox>
        </li>
        <li>
            <label for="txtNome" title="Nome do Responsável">
                Nome</label>
            <asp:TextBox ID="txtNome" ToolTip="Informe o Nome do Responsável" class="txtNome" runat="server" MaxLength="60"></asp:TextBox>
        </li>
        <li>
            <label for="ddlCategoriaFuncional" title="Categoria Funcional">Categoria</label>
            <asp:DropDownList ID="ddlCategoriaFuncional" 
                ToolTip="Selecione a Categoria Funcional do Funcionário"
                CssClass="ddlCategoriaFuncional" runat="server">
                <asp:ListItem Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="S">Funcionário</asp:ListItem>
                <asp:ListItem Value="N">Não Funcionário</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlDeficiencia" title="Deficiência">Deficiência</label>
            <asp:DropDownList ID="ddlDeficiencia" 
                ToolTip="Informe se o Funcionário possui Deficiências"
                CssClass="ddlDeficiencia" runat="server">
                <asp:ListItem Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="N">Nenhuma</asp:ListItem>
                <asp:ListItem Value="A">Auditivo</asp:ListItem>
                <asp:ListItem Value="V">Visual</asp:ListItem>
                <asp:ListItem Value="F">Físico</asp:ListItem>
                <asp:ListItem Value="M">Mental</asp:ListItem>
                <asp:ListItem Value="I">Múltiplas</asp:ListItem>
                <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
        </li> 
        <li>
            <label for="ddlRenda" title="Renda Familiar">Renda Familiar</label>
            <asp:DropDownList ID="ddlRenda" ToolTip="Selecione a Renda Familiar" CssClass="ddlRenda" runat="server">
                <asp:ListItem Value="T">Todas</asp:ListItem>
                <asp:ListItem Value="1">1 a 3 SM</asp:ListItem>
                <asp:ListItem Value="2">3 a 5 SM</asp:ListItem>
                <asp:ListItem Value="3">+5 SM</asp:ListItem>
                <asp:ListItem Value="4">Sem Renda</asp:ListItem>
            </asp:DropDownList>
        </li>
        <li>
        <label for="ddlSituacao" title="Situação Atual">Situação Atual</label>
        <asp:DropDownList ID="ddlSituacao" 
            ToolTip="Selecione a Situação Atual do Responsável"
            CssClass="ddlSituacao" runat="server">
            <asp:ListItem Value="T">Todas</asp:ListItem>
            <asp:ListItem Value="A">Ativo</asp:ListItem>
            <asp:ListItem Value="I">Inativo</asp:ListItem>
        </asp:DropDownList>
    </li>
    </ul>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">

 <script type="text/javascript">
     jQuery(function ($) {
         $(".txtCpf").mask("999.999.999-99");
     });
    </script>

</asp:Content>
