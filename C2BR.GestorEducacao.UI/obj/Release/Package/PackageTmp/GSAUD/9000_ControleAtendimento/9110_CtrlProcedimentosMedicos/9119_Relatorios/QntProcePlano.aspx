<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="QntProcePlano.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9110_CtrlProcedimentosMedicos._9119_Relatorios.QntProcePlano" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 300px;
        }
        .ulDados li
        {
            margin: 3px;
        }
        input
        {
            height: 13px;
        }
        label
        {
            margin-bottom: 2px;
        }
        .ddlPadrao
        {
            width: 285px;
        }
        .chk
        {
            margin-left: -5px;
        }
        .chk label
        {
            display: inline;
            margin-left: -4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDadosPesquisa" runat="server" class="ulDados">
        <li style="clear: both">
            <label style="">Unidade</label>
            <asp:DropDownList runat="server" ID="ddlUnidade" Width="200px" />
        </li>
        <li style="clear: both">
            <label style="">Operadora</label>
            <asp:DropDownList runat="server" ID="ddlOperadora" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="OnSelectedIndexChanged_Operadora"/>
        </li>  
        <li style="clear: both">
            <label style="">Plano</label>
            <asp:DropDownList runat="server" ID="ddlPlano" Width="200px" />
        </li>               
        <li style="clear: both">
            <label style="">Procedimento</label>
            <asp:DropDownList runat="server" ID="ddlProcedimento" Width="200px" />
        </li>        
        <li style="clear: both">
            <label>Período</label>
            <asp:TextBox runat="server" class="campoData" ID="txtIniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvIniPeri" CssClass="validatorField"
                ErrorMessage="O campo data Inicial é requerido" ControlToValidate="txtIniPeri"></asp:RequiredFieldValidator>
            <asp:Label runat="server" ID="Label1"> &nbsp à &nbsp </asp:Label>
            <asp:TextBox runat="server" class="campoData" ID="txtFimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ID="rfvFimPeri" CssClass="validatorField"
                ErrorMessage="O campo data Final é requerido" ControlToValidate="txtFimPeri"></asp:RequiredFieldValidator>
        </li>        
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
