<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC._3000_CtrlOperacionalPedagogico._3600_CtrlInformacoesAlunos._3640_CtrlOcorrenciasAlunos._3643_CadasOcorrSalvas.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados
    {
     margin:40px 0 0 10px;
        width:270px;
    }
    .ulDados li
    {
        margin-left:5px;
    }
    input 
    {
        height:13px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul class="ulDados">
    <li>
        <label>Categoria</label>
        <asp:DropDownList runat="server" ID="ddlTipo" Width="80px"></asp:DropDownList>
    </li>
    <li>
        <label>Descrição</label>
        <asp:TextBox runat="server" ID="txtDescricao" Width="160px"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
