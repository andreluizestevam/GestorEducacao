<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._4000_ControleOperacionalEstoque._4100_ControleOperacionalMateriais._4110_ControleOperacionalItensEstoque._4111_CadastroItemEstoque.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <li>
                    <label for="ddlGrupo" class="labelPixel" title="Grupo de Produtos">
                        Grupo</label>
                    <asp:DropDownList ID="ddlGrupo" CssClass="campoDescricao" ToolTip="Selecione o Grupo de Produtos"
                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
                <li>
                    <label for="ddlSubGrupo" class="labelPixel" title="SubGrupo de Produtos">
                        SubGrupo</label>
                    <asp:DropDownList ID="ddlSubGrupo" Width="150px" ToolTip="Selecione o SubGrupo de Produtos"
                        runat="server">
                    </asp:DropDownList>
                </li>
            </ContentTemplate>
        </asp:UpdatePanel>
        <li>
            <label for="txtCodRef" class="labelPixel" title="Código de Referência">
                Cód. Referência</label>
            <asp:TextBox ID="txtCodRef" Width="120px" ToolTip="Informe o Código de Referência"
                runat="server" MaxLength="9"></asp:TextBox>
        </li>
        <li>
            <label for="txtDescricao" title="Produto">
                Produto</label>
            <asp:TextBox ID="txtDescricao" runat="server" ToolTip="Pesquise pela Descrição do Produto"
                CssClass="campoDescricao" MaxLength="40"></asp:TextBox>
        </li>
        <li>
            <label for="ddlTipoProduto" class="labelPixel" title="Tipo de Produto">
                Tipo de Produto</label>
            <asp:DropDownList ID="ddlTipoProduto" CssClass="campoDescricao" ToolTip="Selecione o Tipo de Produto"
                runat="server">
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
