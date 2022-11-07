<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9300_CtrlExames._9308_ControleSubGruposExameFisico.Busca"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li style="clear: both;">
            <label>
                Grupo</label>
            <asp:DropDownList ID="ddlGrupo" runat="server" Style="width: 125px;" ToolTip="Selecione o Grupo" />
        </li>
        <li style="clear: both;">
            <label>
                Subgrupo</label>
            <asp:TextBox ID="txtSubgrupo" runat="server" MaxLength="60" ToolTip="Informe o nome do Subgrupo" />
        </li>
        <li style="clear: both;">
            <label>
                Situação</label>
            <asp:DropDownList ID="ddlSitu" runat="server" Style="width: 125px;" ToolTip="Selecione a situação do SubGrupo">
                <asp:ListItem Text="Todos" Value="" Selected="True">
                </asp:ListItem>
                <asp:ListItem Text="Ativo" Value="A">
                </asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I">
                </asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
