<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1905_Idiomas.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtNO_IDIOM" title="Idioma">
                Idioma</label>
            <asp:TextBox ID="txtNO_IDIOM" runat="server" CssClass="txtDescricao" MaxLength="40"
                ToolTip="Informe o Idioma">    
            </asp:TextBox>
        </li>
    </ul>
</asp:Content>
