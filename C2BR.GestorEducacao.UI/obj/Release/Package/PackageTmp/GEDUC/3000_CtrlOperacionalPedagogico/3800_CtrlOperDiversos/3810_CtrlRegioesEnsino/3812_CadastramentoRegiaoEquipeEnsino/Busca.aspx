<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3800_CtrlEncerramentoLetivo.F3810_CtrlRegioesEnsino.F3812_CadastramentoRegiaoEquipeEnsino.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtDE_NUCLEO" class="labelPixel" title="Núcleo / Sigla do Núcleo">Núcleo / Sigla do Núcleo</label>
            <asp:TextBox ID="txtDE_NUCLEO" ToolTip="Informe um Núcleo ou uma Sigla do Núcleo" CssClass="campoNucleo" runat="server" MaxLength="40"></asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
