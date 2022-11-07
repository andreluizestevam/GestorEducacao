<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0100_DadosOperContrato.F0105_TipoDepartamentoInstitucional.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtNomeDepto" class="labelPixel" title="Nome Departamento/Local">
                Nome Departamento/Local</label>
            <asp:TextBox ID="txtNomeDepto" ToolTip="Informe o nome completo ou parte do nome de Departamento/Local" runat="server" CssClass="txtDescricao" MaxLength="50"></asp:TextBox>
        </li>
        <li class="clear">
            <label for="ddlClass" title="Classificação do Departamento/Local" class="lblObrigatorio">
                Classificação</label>
            <asp:DropDownList CssClass="ddlClass" ID="ddlClass" runat="server" ToolTip="Selecione a classificação atual para o tipo de Departamento/Local">
                <asp:ListItem Value="" Text="Todos" Selected="true"></asp:ListItem>
                <asp:ListItem Value="ACO" Text="Acomodação"></asp:ListItem>
                <asp:ListItem Value="ADM" Text="Administrativo"></asp:ListItem>
                <asp:ListItem Value="ATE" Text="Atendimento"></asp:ListItem>
                <asp:ListItem Value="FIN" Text="Financeiro"></asp:ListItem>
                <asp:ListItem Value="OPE" Text="Operacional"></asp:ListItem>
                <asp:ListItem Value="TEC" Text="Técnico"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li style="clear:both; margin-top: 5px;">
            <label for="ddlSitua" title="Situação do tipo de Departamento/Local" class="lblObrigatorio">
                Situação</label>
            <asp:DropDownList ID="ddlSitua" runat="server" ToolTip="Selecione a situação atual para o tipo de Departamento/Local">
                <asp:ListItem Value="" Text="Todos" Selected="True"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
