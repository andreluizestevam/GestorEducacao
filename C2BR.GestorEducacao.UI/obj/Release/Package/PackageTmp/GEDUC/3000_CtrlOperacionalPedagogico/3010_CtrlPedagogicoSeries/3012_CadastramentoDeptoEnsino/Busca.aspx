<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.F3012_CadastramentoDeptoEnsino.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtNome" title="Departamento">Departamento</label>
        <asp:TextBox ID="txtNome" class="txtDescricao" runat="server" MaxLength="40"
            ToolTip="Informe o Departamento">
        </asp:TextBox>
    </li>
    <li>
        <label for="txtSigla" title="Sigla">Sigla</label>
        <asp:TextBox ID="txtSigla" class="txtSigla" runat="server" MaxLength="40"
            ToolTip="Informe a Sigla">
        </asp:TextBox>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
