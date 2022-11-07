<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1107_CtrlCategoria.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .ulDados
        {
            width: 250px;
            margin:40px 0 0 40px !important;
        }
        .ulDados li
        {
            margin-top: 5px;
        }
         .ddlReg
        {
            width: 190px;
            clear: both;
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul class="ulDados">
        <li>
            <label title="Operadora">
                Operadora
            </label>
            <asp:DropDownList ID="ddlOperadora"  CssClass="ddlReg" OnSelectedIndexChanged="ddOperadora_OnSelectedIndexChanged" AutoPostBack="true" runat="server">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Operadora">
                Plano
            </label>
            <asp:DropDownList ID="ddlPlano" CssClass="ddlReg"  runat="server">
            <asp:ListItem Value="0">Selecione</asp:ListItem>
            </asp:DropDownList>
        </li>
          <li>
            <label title="Sigla" class="lblObrigatorio">
                Sigla</label>
            <asp:TextBox runat="server" ID="txtSigla" Width="40px" ToolTip="Sigla" MaxLength="6"></asp:TextBox>
        </li>
        <li>
            <label title="Nome da Categoria" class="lblObrigatorio">
                Nome</label>
            <asp:TextBox runat="server" ID="txtNomeCategoria" Width="200px" ToolTip="Nome da Categoria"
                MaxLength="60"></asp:TextBox>
        </li>
      
        <li>
            <label title="Situação da categoria">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSitu" Width="70px" ToolTip="Situação da Operadora">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                <asp:ListItem Text="Suspenso" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
