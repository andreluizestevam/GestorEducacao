<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9306_ControleValoresreferencia.Busca"
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
            <asp:DropDownList ID="ddlProcedimento" runat="server" style="width: 225px;" OnSelectedIndexChanged="ddlProcedimento_OnSelectedIndexChanged" AutoPostBack="true" />
        </li>
        <li style="clear:both;">
            <label>Grupo</label>
            <asp:DropDownList ID="ddlGrupo" runat="server" style="width: 140px;" ToolTip="Selecione o Grupo" OnSelectedIndexChanged="ddlGrupo_OnSelectedIndexChanged" AutoPostBack="true" />
        </li>
        <li style="clear:both;">
            <label>Subgrupo</label>
            <asp:DropDownList ID="ddlSubgrupo" runat="server" style="width: 140px;" ToolTip="Selecione o Subgrupo" OnSelectedIndexChanged="ddlSubgrupo_OnSelectedIndexChanged" AutoPostBack="true" />
        </li>
        <li style="clear:both;">
            <label>Item de Avaliação</label>
            <asp:DropDownList ID="ddlItemAvaliacao" runat="server" style="width: 180px;" ToolTip="Selecione o Subgrupo" />
        </li>
        <li style="clear:both;">
            <label>Referência</label>
            <asp:TextBox ID="txtReferencia" runat="server" MaxLength="200" ToolTip="Informe o nome da Referência" />
        </li>      
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
