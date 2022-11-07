<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="RedirecionaMensagem.aspx.cs"
    Inherits="C2BR.GestorEducacao.UI.RedirecionaMensagem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" dir="ltr">
<head runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>Redirecionando...</title>
    <style type="text/css">
        *
        {
            margin: 0;
            padding: 0;
            font-family: Tahoma, Arial, Verdana;
        }
        a:link, a:visited, a:active
        {
            text-decoration: none;
            color: #FFA200;
        }
        a:hover
        {
            text-decoration: underline;
        }
        .SucessMessage
        {
            background: #F1FFEF url(/Library/IMG/Gestor_ImgGood.png) no-repeat scroll center 10px;                 
            border: 1px solid #D2DFD1;
            font-size: 15px;
            font-weight: bold;
            margin: 10% auto auto;
            padding: 35px 10px 10px;
            text-align: center;
            width: 330px;
        }
        .ErrorMessage
        {
            background: #FFEDDF url(/Library/IMG/Gestor_ImgError.png) no-repeat scroll center 10px;
            border: 1px solid #D2DFD1;
            font-size: 15px;
            font-weight: bold;
            margin: 10% auto auto;
            padding: 35px 10px 10px;
            text-align: center;
            width: 330px;
        }
        .divDescription
        {
            color: #BBBBBB;
            font-size: 0.6em;
            font-weight: normal;
        }
    </style>
</head>
<body id="bdyRedirecionaMensagem">
    <div id="pageContainer">
        <form id="frmMain" runat="server">
        <div id="divMessage" class='<%= this.MensagemTipo + "Message" %>'>
            <asp:Label runat="server" ID="lblMessage"></asp:Label>
            <div id="divDescription" class="divDescription">
                Você está sendo redirecionado, caso não aconteça nos próximos 5 segundos, clique
                <a href="#" onclick="Ok_Click()">aqui.</a>
            </div>
        </div>
        </form>
    </div>

    <script type="text/javascript">
        if (location.href.indexOf("0503_ExportacaoDadosPortal") == -1) {
            setTimeout(function () { document.getElementById("frmMain").submit(); }, 3000);
        }
        else {
            setTimeout(function () { document.getElementById("frmMain").submit(); }, 30000);
        }

        function Ok_Click() {
            document.getElementById("frmMain").submit();
        }
    </script>

</body>
</html>
