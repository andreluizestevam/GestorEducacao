<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    Inherits="C2BR.GestorEducacao.UI.GSAUD._2000_ControleInformacoesFamilia._2100_ControleInformacoesCadastraisFamilia._2102_CadastroBeneficiarioFamilia.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtCodigo" title="Número de Indentificação da Família">
                Código</label>
            <asp:TextBox ID="txtCodigo" CssClass="txtCodigo" ToolTip="Informe um Número de Indentificação da Família"
                runat="server" MaxLength="15"></asp:TextBox>
        </li>
        <li>
            <label for="txtNome" title="Nome do Responsável pela Família">
                Nome</label>
            <asp:TextBox ID="txtNome" ToolTip="Informe o Nome do Responsável pela Família" runat="server"
                MaxLength="80" CssClass="campoNomePessoa"></asp:TextBox>
        </li>
        <li>
            <label for="txtCep" title="CEP">
                CEP</label>
            <asp:TextBox ID="txtCep" ToolTip="Informe um CEP" runat="server" CssClass="campoCpf"></asp:TextBox>
        </li>
        <li>
            <label for="ddlUf" title="UF">
                UF</label>
            <asp:DropDownList ID="ddlUf" runat="server" CssClass="campoUf" ToolTip="Informe a UF"
                AutoPostBack="true" OnSelectedIndexChanged="ddlUf_SelectedIndexChanged">
            </asp:DropDownList>
        </li>
        <li>
            <label for="ddlCidade" title="Cidade">
                Cidade</label>
            <asp:DropDownList ID="ddlCidade" Width="96px" ToolTip="Selecione a Cidade" CssClass="ddlCidadeResp"
                runat="server" AutoPostBack="true">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlRegiao" title="Região" class="lblObrigatorio">
                Região</label>
            <asp:DropDownList ID="ddlRegiao" AutoPostBack="true" Width="120px" OnSelectedIndexChanged="ddlRegiao_SelectedIndexChanged"
                ToolTip="Selecione a Região" CssClass="ddlRegiao" runat="server">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlArea" title="Área">
                Área</label>
            <asp:DropDownList ID="ddlArea" AutoPostBack="true" Width="120px" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"
                ToolTip="Selecione a Área" CssClass="ddlArea" runat="server">
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <label for="ddlSubArea" title="Subárea">
                Subárea</label>
            <asp:DropDownList ID="ddlSubArea" AutoPostBack="true" Width="120px" ToolTip="Selecione a Subárea"
                CssClass="ddlSubArea" runat="server">
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".campoCpf").mask("999.999.999-99");
        });
    </script>
</asp:Content>
