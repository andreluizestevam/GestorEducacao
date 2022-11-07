<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BreadCrumb.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Components.BreadCrumb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bread Crumb</title>
    <style type="text/css">
        #imgBreadCrumb
        {
            border: none !important;
            height: 17px;
            width: 17px;
            margin-right: -4px !important;
            margin-top: -7px;
            padding-right: 1px !important;
        }
        #divBreadCrumb { margin-top: 2px !important; }
        #lblTextBreadCrumb
        {
        	font-family: Arial;
        }
        #divBreadCrumb a:hover { text-decoration:none; }
    </style>
</head>
<body>
    <form id="frmBreadCrumb" runat="server">
        <div id="divBreadCrumb">
            <a href="javascript:parent.showHomePage()"><img id="imgBreadCrumb" title="Clique para voltar a tela inicial." src="../Library/IMG/Gestor_AlteraUnidade.png" alt="Icone voltar a tela inicial" />&nbsp;</a>
            <asp:Label runat="server" ID="lblTextBreadCrumb" />
        </div>
    </form>
    <script type="text/javascript">
        
    </script>
</body>
</html>
