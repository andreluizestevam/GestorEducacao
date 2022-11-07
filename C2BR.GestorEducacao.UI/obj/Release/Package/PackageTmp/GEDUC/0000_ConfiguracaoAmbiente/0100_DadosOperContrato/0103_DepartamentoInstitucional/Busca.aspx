<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0100_DadosOperContrato.F0103_DepartamentoInstitucional.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .clear
        {
            clear: both;
            margin-top: 5px;
        }
        .ddlSitua
        {
            width: auto;
        }
        .ddlTipo
        {
            width: auto;
        }
        .ddlRisco
        {
            width: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="ddlTipo" title="Tipo do Departamento/Local" class="lblObrigatorio">
                Tipo Departamento/Local</label>
            <asp:DropDownList CssClass="ddlTipo" ID="ddlTipo" runat="server" ToolTip="Selecione o tipo do Departamento/Local">
            </asp:DropDownList>
        </li>
        <li class="clear">
            <label for="txtNomeDepto" class="labelPixel" title="Departamento ou Sigla">
                Departamento / Sigla</label>
            <asp:TextBox ID="txtNomeDepto" ToolTip="Informe um Departamento ou uma Sigla" runat="server" CssClass="txtDescricao" MaxLength="40"></asp:TextBox>
        </li>
        <%--<li style="margin-top: 0px;">
            <label title="Tipo Classificação de Risco">
                Protocolo</label>
            <asp:DropDownList ID="ddlRisco" runat="server" ToolTip="Pesquise pelo tipo de classificação de risco">
                <asp:ListItem Text="Todos" Value="0" />
                <asp:ListItem Text="Australiano" Value="1" />
                <asp:ListItem Text="Canadense" Value="2" />
                <asp:ListItem Text="Manchester" Value="3" />
                <asp:ListItem Text="Americano" Value="4" />
                <asp:ListItem Text="Pediatria" Value="5" />
                <asp:ListItem Text="Obstetrícia" Value="6" />
                <asp:ListItem Text="Instituição" Value="7" />
            </asp:DropDownList>
        </li>--%>
        <li class="clear">
            <label for="ddlSitua" title="Situação do Departamento/Local" class="lblObrigatorio">
                Situação</label>
            <asp:DropDownList CssClass="ddlSitua" ID="ddlSitua" runat="server" ToolTip="Selecione a situação atual do Departamento/Local">
                <asp:ListItem Value="" Text="Todos" Selected="True"></asp:ListItem>
                <asp:ListItem Value="A" Text="Ativo"></asp:ListItem>
                <asp:ListItem Value="I" Text="Inativo"></asp:ListItem>
                <asp:ListItem Value="M" Text="Manutenção"></asp:ListItem>
                <asp:ListItem Value="X" Text="Interditado"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
