<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3200_ControleAtendimentoUsuario._3207_DocumentacaoPaciente._AssociacaoDocOperadora.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li class="liboth">
        <label style="">Operadora</label>
        <asp:DropDownList runat="server" ID="ddlOperadora" CssClass="lblObrigatorio" ToolTip="Selecione a operadora" Width="195px" />
        <asp:RequiredFieldValidator ID="rfvOperadora" runat="server" ControlToValidate="ddlOperadora"
            CssClass="validatorField" ErrorMessage="Operadora deve ser informada"></asp:RequiredFieldValidator>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
