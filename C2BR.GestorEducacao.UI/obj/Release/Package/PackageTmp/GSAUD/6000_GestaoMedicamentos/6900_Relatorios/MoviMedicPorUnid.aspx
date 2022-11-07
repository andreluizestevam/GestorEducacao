<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="MoviMedicPorUnid.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._6000_GestaoMedicamentos._6900_Relatorios.MoviMedicPorUnid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ulDados 
        {
            width: 275px;
            margin-top: 50px;
        }
        
        .ulDados li
        {
            margin-top: 6px;
            margin-left: 60px;
        }
        
        .liboth
        {
            clear:both;
        }
                
        .ddltop
        {
            width:150px;
            clear:both;
        }
        .ddlReg
        {
             width:90px;
            clear:both;         
        }
        .ddlArea
        {
            width:130px;
            clear:both;                     
        }
        .ddlSubArea
        {
            width:150px;
            clear:both;                     
        }
        
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">

    <li class="liboth">
        <asp:Label runat="server" ID="lblUnidSolic">Unidade</asp:Label><br />
        <asp:DropDownList runat="server" class="ddltop" ID="ddlUnidSolic" ToolTip="Selecine a Unidade Solicitante"></asp:DropDownList>
    </li>

    <li class="liboth">
        <asp:Label runat="server" ID="lblMedic">Medicamento</asp:Label><br />
        <asp:DropDownList runat="server" class="ddltop" ID="ddlMedic" ToolTip="Selecine o Medicamento"></asp:DropDownList>
    </li>

    <li class="liboth">
        <br /><asp:Label runat="server" ID="lblLocal" class="liboth">Localidade</asp:Label>
    </li<br />
    
    <li class="liboth">
        <asp:Label runat="server" ID="lblReg">Região</asp:Label><br />
        <asp:DropDownList runat="server" class="ddlReg" ID="ddlReg" OnSelectedIndexChanged="ddlReg_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    </li>

    <li class="liboth">
        <asp:Label runat="server" ID="lblArea">Área</asp:Label><br />
        <asp:DropDownList runat="server" class="ddlArea" ID="ddlArea" AutoPostBack="true"></asp:DropDownList>
    </li>

    <li class="liboth">
        <asp:Label runat="server" ID="lblSubArea">Subárea</asp:Label><br />
        <asp:DropDownList runat="server" class="ddlSubArea" ID="ddlSubArea"></asp:DropDownList>
    </li>

        <li class="liboth">
        <asp:Label runat="server" class="lblObrigatorio" ID="lblPeriodo" >Período</asp:Label><br />

        <asp:TextBox runat="server" class="campoData" ID="IniPeri" ToolTip="Informe a Data Inicial do Período"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" id="rfvIniPeri" CssClass="validatorField" ErrorMessage="O campo data Inicial é requerido"
        ControlToValidate="IniPeri"></asp:RequiredFieldValidator>

        <asp:Label runat="server" ID="Label1" > &nbsp à &nbsp </asp:Label>

        <asp:TextBox runat="server" class="campoData" ID="FimPeri" ToolTip="Informe a Data Final do Período"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" id="rfvFimPeri" CssClass="validatorField" ErrorMessage="O campo data Final é requerido"
        ControlToValidate="FimPeri"></asp:RequiredFieldValidator>
    </li>

    </ul>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
