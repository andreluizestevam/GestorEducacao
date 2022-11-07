<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master"
    AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._9000_ControleAtendimento._9400_CtrlSUS._9410_Registros._9411_CadastroDomicilio.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados
        {
            margin: 60px 0 0 20px;
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
                Código Documento</label>
            <asp:TextBox runat="server" ID="txtcodDocum" Width="160px" MaxLength="15" ToolTip="Código do Documento de Cadastro de Domicílio"></asp:TextBox>
        </li>
        <li>
            <label>
                MICROAREA</label>
            <asp:TextBox runat="server" ID="txtMicroarea" Width="15px" MaxLength="2" ToolTip="Código da Microarea"></asp:TextBox>
        </li>
        <li>
        <label>
                Data de Cadastro</label>
            <asp:TextBox ID="txtDtIni" runat="server" CssClass="campoData">
            </asp:TextBox>
            &nbsp até &nbsp
            <asp:TextBox ID="txtDtFim" runat="server" CssClass="campoData">
            </asp:TextBox>
        </li>
    </ul>
    <script type="text/javascript">
        $("#txtcodDocum").mask("?999999999999999");
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
