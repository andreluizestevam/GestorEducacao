<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1200_GestaoOperColaboradores.F1208_AgendaContato.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlTipoContato { width: 80px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlTipoContato" class="lblObrigatorio" title="Tipo de Contato">Tipo de Contato</label>
            <asp:DropDownList ID="ddlTipoContato" ToolTip="Selecione o Tipo de Contato" CssClass="ddlTipoContato" runat="server">
                <asp:ListItem Text="Todos" Value=""></asp:ListItem>
                <asp:ListItem Text="Funcionário" Value="F"></asp:ListItem>
                <asp:ListItem Text="Professor" Value="P"></asp:ListItem>
                <asp:ListItem Text="Aluno" Value="A"></asp:ListItem>
                <asp:ListItem Text="Responsável" Value="R"></asp:ListItem>
                <asp:ListItem Text="Outros" Value="O"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
