<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master"
    AutoEventWireup="true" CodeBehind="ExtratoPlantao.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._7000_ControleOperRH._7100_CtrlPontoFuncional._7199_Relatorios.ExtratoPantao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            width: 250px;
        }
        
        .ulDados li
        {
            margin-left: 5px;
            margin-top: 5px;
        }
        .ulDados label
        {
            margin-bottom: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li>
            <label title="Selecione a unidade">
                Unidade Plantão</label>
            <asp:DropDownList ID="ddlUnidade" runat="server" ToolTip="Selecione a unidade do Plantão" Width="200px">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Local onde o Plantão será realizado">
                Local Plantão</label>
            <asp:DropDownList runat="server" ID="ddlLocal" ToolTip="Local onde o Plantão será realizado"
                Width="170px">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Especialidade específica do Plantão">
                Especialidade Plantão</label>
            <asp:DropDownList runat="server" ID="ddlEspec" ToolTip="Especialidade específica do Plantão"
                Width="170px">
            </asp:DropDownList>
        </li>
        <li>
            <label title="Selecione a ordenação desejada">
                Ordenado por</label>
            <asp:DropDownList runat="server" ID="ddlOrdenacao" ToolTip="Selecione a ordenação desejada"
                Width="130px">
                <asp:ListItem Value="1" Text="Nome"></asp:ListItem>
                <asp:ListItem Value="2" Text="Unidade/Local"></asp:ListItem>
                <asp:ListItem Value="3" Text="Especialidade"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label title="Selecione o tipo de plantão">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao" ToolTip="Selecione o tipo de plantão">
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
