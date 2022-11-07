<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7300_CtrlCadastralInforFunc._7310_CadastroColabor.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <style type="text/css">
        /*--> CSS DADOS */
        .txtCpf { width: 82px; }
        .txtNome { width: 210px; }
        .ddlCategoriaFuncional{width:76px;}
        .ddlDeficiencia{width:70px;}
        .ddlSituacao{width:125px;}
        
    </style>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtCpf">CPF</label>
            <asp:TextBox ID="txtCpf" CssClass="txtCpf" runat="server" ToolTip="Informe o CPF"></asp:TextBox>
    </li>
    <li>
        <label for="txtNome">Nome</label>
            <asp:TextBox ID="txtNome" CssClass="txtNome" runat="server" MaxLength="40" ToolTip="Informe o Nome do Colaborador"></asp:TextBox>
    </li>
    <li>
        <label for="ddlCategoriaFuncional" title="Categoria Funcional">Categoria Funcional</label>
        <asp:DropDownList ID="ddlCategoriaFuncional" 
            ToolTip="Selecione a Categoria Funcional do Funcionário"
            CssClass="ddlCategoriaFuncional" Width="120px" runat="server">
            <asp:ListItem Value="T">Todos</asp:ListItem>
            <asp:ListItem Value="N">Funcionário</asp:ListItem>
            <asp:ListItem Value="S">Profissional Saúde</asp:ListItem>
            <asp:ListItem Value="P">Prestador Serviços</asp:ListItem>
            <asp:ListItem Value="E">Estagiário(a)</asp:ListItem>
            <asp:ListItem Value="O">Outro</asp:ListItem>
        </asp:DropDownList>
    </li>
    <li>
      <label for="txtProfissional">Classificação Profissional</label>
        <asp:DropDownList ID="ddlClassificacaoProficiona" Width="120px" runat="server">
        </asp:DropDownList>
    </li>
     <li>
       <label for="txtFuncao">Função</label>
        <asp:TextBox ID="txtFuncao" CssClass="txtNome" runat="server" MaxLength="40" ToolTip="Função"></asp:TextBox>
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
        <label for="ddlSituacao" title="Situação Atual">Situação Atual</label>
        <asp:DropDownList ID="ddlSituacao" 
            ToolTip="Selecione a Situação Atual do Funcionário"
            CssClass="ddlSituacao" runat="server">
            <asp:ListItem Value="T">Todas</asp:ListItem>
            <asp:ListItem Value="ATI">Atividade Interna</asp:ListItem>
            <asp:ListItem Value="ATE">Atividade Externa</asp:ListItem>
            <asp:ListItem Value="FCE">Cedido</asp:ListItem>
            <asp:ListItem Value="DEM">Demitido</asp:ListItem>
            <asp:ListItem Value="FES">Estagiário</asp:ListItem>
            <asp:ListItem Value="INA">Inativo</asp:ListItem>
            <asp:ListItem Value="LFR">Licença Funcional</asp:ListItem>
            <asp:ListItem Value="LME">Licença Médica</asp:ListItem>
            <asp:ListItem Value="LMA">Licença Maternidade</asp:ListItem>
            <asp:ListItem Value="SUS">Suspenso</asp:ListItem>
            <asp:ListItem Value="TRE">Treinamento</asp:ListItem>
            <asp:ListItem Value="FER">Férias</asp:ListItem>
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
