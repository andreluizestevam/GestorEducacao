<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9303_ControleGrupos.Busca"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label>Contratação</label>
            <asp:DropDownList ID="ddlOperadora" runat="server" style="width: 225px;" OnSelectedIndexChanged="ddlOperadora_OnSelectedIndexChanged" AutoPostBack="true" />
        </li>
        <li style="clear:both;">
            <label>Procedimento</label>
            <asp:DropDownList ID="ddlProcedimento" runat="server" style="width: 225px;" />
        </li>
        <li style="clear:both;">
            <label title="Código do Grupo">
               Código
            </label>
            <asp:TextBox ID="txtCodGrupo" runat="server" MaxLength="10" Width="70px" ToolTip="Informe o Código do Grupo" />
        </li>
        <li style="clear:both;">
            <label title="Grupo de Procedimento">
               Grupo
            </label>
            <asp:TextBox ID="txtGrupo" runat="server" CssClass="campoDescricao" ToolTip="Informe o Grupo" />
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
