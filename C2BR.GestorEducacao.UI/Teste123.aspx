<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Teste123.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Teste123" %>

<%@ Register Src="~/Library/Componentes/ManterEnderecoSN.ascx"   TagName="ManterEnderecoSN" TagPrefix="ac" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
    <div>   
   <ac:ManterEnderecoSN id="ucEnderecoSN" runat="server" />
    </div>

    </form>
</body>
</html>
