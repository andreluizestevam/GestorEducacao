<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD.Cadastro" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox runat="server" ID="plsql" TextMode="MultiLine" Height="168px" Width="672px"></asp:TextBox>
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            <br />
            <asp:Button runat="server" ID="btnplsql" Text="Executar" OnClick="btnplsql_Click" />
        </div>
    </form>
</body>
</html>
