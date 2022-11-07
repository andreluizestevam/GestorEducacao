<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1300_ServicosApoioAdministrativo.F1310_CtrlAgendaAtividadesFuncional.F1312_RepasseAgendaAtividade.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .txtCpf { width: 82px; }
        .txtChaveUnica { width: 66px; }
        .ddlPrioridade { width: 72px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtCpf">CPF</label>
            <asp:TextBox ID="txtCpf" CssClass="txtCpf" runat="server" ToolTip="Informe o CPF"></asp:TextBox>
    </li>
    <li>
        <label for="txtChaveUnica">Chave &Uacute;nica</label>
            <asp:TextBox ID="txtChaveUnica" CssClass="txtChaveUnica" runat="server" ToolTip="Informe a Chave Única"></asp:TextBox>
    </li>
    <li>
        <label for="ddlPrioridade">Prioridade</label>
            <asp:DropDownList ID="ddlPrioridade" CssClass="ddlPrioridade" runat="server" ToolTip="Informe a Prioridade da Tarefa">
                <asp:ListItem Value="">Todas</asp:ListItem>
                <asp:ListItem Value="NEN">Nenhuma</asp:ListItem>
                <asp:ListItem Value="NOR">Normal</asp:ListItem>
                <asp:ListItem Value="MED">M&eacute;dia</asp:ListItem>
                <asp:ListItem Value="CRI">Cr&iacute;tica</asp:ListItem>
                <asp:ListItem Value="URG">Urgente</asp:ListItem>
            </asp:DropDownList>
    </li>    
    <li>
        <label for="txtDtInicial">Data</label>
        <asp:TextBox ID="txtDtInicial" CssClass="txtDtInicial campoData" runat="server" ToolTip="Data Início"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        jQuery(function($) {
            $(".txtCpf").mask("999.999.999-99");
            $(".txtChaveUnica").mask("?9999999999");
        });
    </script>
</asp:Content>