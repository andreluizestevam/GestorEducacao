<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9307_ControleGruposExameFisico.Busca"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label>Especialiade</label>
            <asp:DropDownList ID="ddlEspec" runat="server" style="width: 225px;"/>
        </li>
        <li style="clear:both;">
            <label title="Grupo de Exame Fisico">
               Grupo
            </label>
            <asp:TextBox ID="txtGrupo" runat="server" CssClass="campoDescricao" ToolTip="Informe o Grupo de Exame Fisico" />
        </li>
        <li style="clear:both;">
            <label for="ddlSitu" title="Situação do grupo">
                Situação
            </label>
            <asp:DropDownList ID="ddlSitu" runat="server" ToolTip="Selecione a situação inicial do grupo">
                <asp:ListItem Text="Todos" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Ativo" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I" ></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
