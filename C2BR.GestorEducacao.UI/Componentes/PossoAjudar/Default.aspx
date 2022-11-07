<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.PossoAjudar.Default" %>

<%@ Register Src="AjudaPorPalavraChave.ascx" TagName="AjudaPorPalavraChave" TagPrefix="uc1" %>
<%@ Register Src="AjudaPorModulo.ascx" TagName="AjudaPorModulo" TagPrefix="uc2" %>
<%@ Register Src="SuporteOnLine.ascx" TagName="SuporteOnLine" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Posso Ajudar?</title>
    <style type="text/css">
        img
        {
            float: left;
        }
    </style>
</head>
<body>
    <div id="divPossoAjudarContainer">
        <form id="frmPossoAjudar" runat="server">
        <div id="divPossoAjudarHeader">
            <img src="#" alt="Icone Portal Educação" /><h1>
                Central de Ajuda e Suporte</h1>
            <h4>
                Em que <b>Posso Ajudar?</b>
            </h4>
        </div>
        <div id="divPossoAjudarContent">
            <uc1:AjudaPorPalavraChave ID="AjudaPorPalavraChave1" runat="server" />
            <uc2:AjudaPorModulo ID="AjudaPorModulo1" runat="server" />
            <uc3:SuporteOnLine ID="SuporteOnLine1" runat="server" />
        </div>
        </form>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
</body>
</html>
