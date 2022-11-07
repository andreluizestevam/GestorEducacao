<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0308_ManutencaoTipoPerfilAcesso.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .campoNomePerfilAcessoTPA{width: 168px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
             <li>
                <label title="Nome do Perfil">Nome do Perfil</label>
                <asp:TextBox ID="txtNomePerfilTPA" ToolTip="Informe o Nome do Perfil"  MaxLength="40" CssClass="campoNomePerfilAcessoTPA" runat="server"></asp:TextBox>
            </li>
            <li>
                <label for="ddlStatusPerfilTPA">Status</label>
                <asp:DropDownList ID="ddlStatusPerfilTPA" runat="server" ToolTip="Selecione o Status do Perfil">
                    <asp:ListItem Text="Todos" Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Ativo" Value="A"></asp:ListItem>
                    <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                </asp:DropDownList>
            </li>
            <li>
                <label title="Unidade/Escola">Unidade</label>
                <asp:DropDownList ID="ddlUnidadeTPA" runat="server"  ToolTip="Selecione a Unidade" CssClass="campoUnidadeEscolar"></asp:DropDownList>
            </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
