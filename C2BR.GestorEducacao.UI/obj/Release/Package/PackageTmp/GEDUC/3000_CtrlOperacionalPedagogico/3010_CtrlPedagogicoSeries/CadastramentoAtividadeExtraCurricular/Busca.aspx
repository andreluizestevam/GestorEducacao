<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true"
    CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3010_CtrlPedagogicoSeries.CadastramentoAtividadeExtraCurricular.Busca"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css" rel="stylesheet">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulParamsFormBusca" class="ulParamsFormBusca">
        <li>
            <label for="txtDescricao" title="Descrição">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" runat="server" CssClass="txtDescricao" MaxLength="40"
                ToolTip="Informe a Descrição">
            </asp:TextBox>
        </li>
    </ul>
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
   <script type="text/javascript">
    </script>
</asp:Content>
