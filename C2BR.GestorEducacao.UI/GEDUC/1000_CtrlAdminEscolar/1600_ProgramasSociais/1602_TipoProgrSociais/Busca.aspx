<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1600_ProgramasSociais.F1602_TipoProgrSociais.Busca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .txtTipoPrograma {width:210px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label for="txtTipoPrograma" title="Tipo do Programa/Convênio Sócio-Educacional">Tipo do Programa/Convênio</label>
        <asp:TextBox ID="txtTipoPrograma" CssClass="txtTipoPrograma" runat="server" MaxLength="60"
            ToolTip="Informe o Tipo do Programa/Convênio Sócio-Educacional" />
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script type="text/javascript">
        jQuery(function ($) {
        });
    </script>
</asp:Content>