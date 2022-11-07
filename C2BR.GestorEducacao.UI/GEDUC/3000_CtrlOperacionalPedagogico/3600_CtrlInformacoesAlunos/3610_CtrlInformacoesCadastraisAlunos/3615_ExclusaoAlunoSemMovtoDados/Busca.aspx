<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3600_CtrlInformacoesAlunos.F3610_CtrlInformacoesCadastraisAlunos.F3615_ExclusaoAlunoSemMovtoDados.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtNire" title="Número de Indentificação do Aluno na Rede de Ensino">NIRE</label>
        <asp:TextBox ID="txtNire" ToolTip="Informe um Número de Indentificação do Aluno na Rede de Ensino" runat="server" MaxLength="10" CssClass="txtNire"></asp:TextBox>
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
            <asp:ListItem Value=""></asp:ListItem>
            <asp:ListItem Value="T">Todas</asp:ListItem>
            <asp:ListItem Value="N">Nenhuma</asp:ListItem>
            <asp:ListItem Value="A">Auditiva</asp:ListItem>   
            <asp:ListItem Value="V">Visual</asp:ListItem>
            <asp:ListItem Value="F">Física</asp:ListItem>
            <asp:ListItem Value="M">Mental</asp:ListItem>
            <asp:ListItem Value="P">Múltiplas</asp:ListItem>
            <asp:ListItem Value="O">Outras</asp:ListItem> 
        </asp:DropDownList>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">    
    $(document).ready(function() {
        $(".txtNire").mask("?9999999999");
        $(".campoCpf").mask("999.999.999-99");
    });
    </script>
</asp:Content>
