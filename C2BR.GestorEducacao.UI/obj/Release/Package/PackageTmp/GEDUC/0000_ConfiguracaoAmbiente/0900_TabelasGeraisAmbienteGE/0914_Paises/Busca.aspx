<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0914_Paises.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .txtNomePais { width: 300px; }
        .txtCodISO { width: 20px; }
        .txtCodISO3 { width: 30px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtCodISO"  title="Código do ISO">
                ISO</label>
            <asp:TextBox ID="txtCodISO" ToolTip="Informe o código ISO do País" runat="server" CssClass="txtCodISO"  MaxLength="2"></asp:TextBox>
        </li>
        <li>
            <label for="txtCodISO3"  title="Código do ISO 3">
                ISO3</label>
            <asp:TextBox ID="txtCodISO3" ToolTip="Informe o código ISO 3 do País" runat="server" CssClass="txtCodISO3"  MaxLength="3"></asp:TextBox>
        </li>
        <li>
            <label for="txtNomePais"  title="Nome do País">
                Nome do País</label>
            <asp:TextBox ID="txtNomePais" ToolTip="Pesquise pelo Nome do País" runat="server" CssClass="txtNomePais"  MaxLength="255"></asp:TextBox>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
