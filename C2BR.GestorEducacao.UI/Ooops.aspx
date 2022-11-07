<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoGenericas.Master" AutoEventWireup="true" CodeBehind="Ooops.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Ooops" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .style1
    {
        text-align: center;
        font-size: x-large;
    }
        .style2
        {
            width: 391px;
            height: 267px;
        }
        .style3
        {
            font-size: small;
            color: #CCCCCC;
        }
        .style4
        {
            font-size: small;
            color: #C0C0C0;
            text-align: center;
        }
        .style5
        {
            color: #C0C0C0;
            text-align: center;
        font-size: medium;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<p class="style1">
        <img alt="" class="style2" src="BarrasFerramentas/Icones/Oops.jpg" /></p>
        <p class="codCol"><span class="style5">Desculpe ocorreu um erro, Serviço indisponível</span><br 
         class="style4" /> </p>
    <p class="codCol"><span class="style4">Entre em contato com o Apoio Personalizado ao Cliente</span><br 
        class="style4" />
    <span class="style4">Email: </span><a class="style4" 
        href="mailto:suporte@gestoreducacao.com.br">suporte@gestoreducacao.com.br</a><span class="style4"> 
    /Skype:suporte.sistema</span><br class="style3" /></p>
</asp:Content>
