<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._2000_CtrlOperSecretariaEscolar._2900_TabelasGenerCtrlOperSecretaria._2910_GrupoItemSolicitacao.Cadsatro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.ulDados
{
 margin: 0 auto;
 width:380px;   
}
.liClear
{
    clear:both;
}

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
 <ul class="ulDados">
    <li class="liClear">
        <label class="lblObrigatorio">Codigo</label>
        <asp:TextBox ID="txtCodigo" runat="server"  MaxLength="12"  Width="85px"/>
     </li>
    <li  class="liClear">
        <label class="lblObrigatorio" >Nome</label>
        <asp:TextBox ID="txtNome" runat="server" Width="208px" MaxLength="30"  />
     </li>
    <li  class="liClear">
        <label class="lblObrigatorio" >Data</label>
        <asp:TextBox ID="txtData" runat="server"  CssClass="campoData" />
     </li>
    <li >
        <label  class="lblObrigatorio" >Situação</label>
        <asp:DropDownList ID="ddlSituacao" runat="server" Width="80px" >
            <asp:ListItem Value="A" Text="Ativo" />
            <asp:ListItem Value="I" Text="Inativo" />
        </asp:DropDownList>
     </li> 
 </ul>
</asp:Content>
