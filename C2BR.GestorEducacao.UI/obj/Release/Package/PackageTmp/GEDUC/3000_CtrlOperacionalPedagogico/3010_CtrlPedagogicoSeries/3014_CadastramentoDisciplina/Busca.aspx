<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3014_CadastramentoDisciplina.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtNome" title="Disciplina">Disciplina</label>
        <asp:TextBox ID="txtNome" CssClass="txtMateria" runat="server" MaxLength="40"
            ToolTip="Informe o nome da Disciplina">
        </asp:TextBox>
    </li>
    <li>
        <label for="txtSigla" title="Sigla">Sigla</label>
        <asp:TextBox ID="txtSigla" CssClass="txtSigla" runat="server" MaxLength="40"
            ToolTip="Informe a Sigla da Disciplina">
        </asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
