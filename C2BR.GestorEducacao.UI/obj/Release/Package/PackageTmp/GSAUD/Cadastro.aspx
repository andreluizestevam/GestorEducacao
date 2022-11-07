<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD.Cadastro" %>

<%@ Register Src="~/Componentes/CadastroRespUsuario.ascx" TagName="InfosCadasRespUsuario"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul>
        <li class="liEndereco">
            <uc2:InfosCadasRespUsuario ID="FormularioInfosCadastrais" runat="server" />
        </li>
    </ul>
</asp:Content>
