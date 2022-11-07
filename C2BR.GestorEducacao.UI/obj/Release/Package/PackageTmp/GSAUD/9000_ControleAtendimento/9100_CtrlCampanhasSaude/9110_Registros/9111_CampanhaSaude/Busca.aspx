<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9100_CtrlCampanhasSaude._9110_Registros._9111_CampanhaSaude.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            margin:60px 0 0 20px;
        }
        .ulDados li
        {
            margin: 5px 0 0 5px;
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
            <label>
                Nome Campanha</label>
            <asp:TextBox runat="server" ID="txtNomeCampanha" Width="160px" MaxLength="100" ToolTip="Nome da Campanha de Saúde"></asp:TextBox>
        </li>
        <li style="clear: both">
            <label class="lblObrigatorio" title="Tipo da Campanha de Saúde">
                Tipo</label>
            <asp:DropDownList runat="server" ID="ddlTipoCamp" ToolTip="Tipo da Campanha de Saúde"
                Width="110px">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label class="lblObrigatorio" title="Competência da Campanha de Saúde">
                Competência</label>
            <asp:DropDownList runat="server" ID="ddlCompetencia" Width="120px" ToolTip="Competência da Campanha de Saúde">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label class="lblObrigatorio" title="Classificação da Campanha de Saúde">
                Classificação</label>
            <asp:DropDownList runat="server" ID="ddlClassCamp" Width="100px" ToolTip="Classificação da Campanha de Saúde">
            </asp:DropDownList>
        </li>
        <li style="clear: both">
            <label class="lblObrigatorio" title="Situação da Campanha de Saúde">
                Situação</label>
            <asp:DropDownList runat="server" ID="ddlSituacao" ToolTip="Situação da Campanha de Saúde"
                Width="105px">
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
