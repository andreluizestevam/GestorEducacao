<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loginCadastroPreMatriculaOnLine.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.loginCadastroPreMatriculaOnLine" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    #corpoPreOnline
    {
        text-align:center; 
    }
    #txtErro
    {
        margin-top:20px;
    }
    .linhaNova
    {
        clear:both;
        height:10px;
    }
    </style>
</head>
<body>
    <form id="frmEsqueciSenha" runat="server">
<div id="corpoPreOnline">
    <div id="divCpf" runat="server">
        <asp:Label ID="lblCadastrado0" runat="server" Font-Bold="False" Font-Size="11pt" 
            ForeColor="Black" Text="Para fazer a Matrícula On-Line é necessário estar cadastrado.&lt;br /&gt;&lt;br /&gt;
            Informe os dados abaixo e depois faça o Login (Faça o seu acesso) &lt;br /&gt;e efetue a Pré-Matrícula.&lt;br /&gt;&lt;br /&gt;
            " AssociatedControlID="txtCpf" Width="420px"></asp:Label>
        <br />
    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtCpf" 
            Text="CPF: " Font-Size="10pt"></asp:Label>
    <asp:TextBox ID="txtCpf" CssClass="txtCpf" runat="server" 
            MaxLength="11" Columns="12" Font-Size="10pt"></asp:TextBox>
    <asp:Button ID="btnContinuar" runat="server" Text="Continuar" 
        onclick="btnContinuar_Click" />
    </div>
    <div id="divSenha" runat="server">
        <asp:Label ID="lblCadastrado" runat="server" Font-Bold="True" Font-Size="12pt" 
            ForeColor="Green" Text="" 
            Visible="False">Usuário cadastrado!<br /><br /> Faça seu Login no box "FAÇA O SEU CADASTRO".</asp:Label>
    </div>
    <div id="divCadastro" runat="server">
    
        <table style="width:450px;" align="center">
            <tr style="padding:5px;">
                <td align="right" height="50%" style="padding-right:5px;">
    
        <asp:Label ID="Label5" runat="server" Text="Escola:" AssociatedControlID="dpUnidades" 
                        Font-Size="10pt"></asp:Label>
                </td>
                <td align="left" height="50%" style="padding:5px;">
        <asp:DropDownList ID="dpUnidades" runat="server" Width="300px" Font-Size="10pt">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvUnid" runat="server" 
            ControlToValidate="dpUnidades" 
            ErrorMessage="Por favor informe a unidade escolar" Font-Size="8pt">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="padding:5px; ">
                <td align="right" height="50%" style="padding-right:5px;">
    
        <asp:Label ID="Label3" runat="server" AssociatedControlID="txtSenhaNova" 
            Text="Senha:" Font-Size="10pt"></asp:Label><br /><br />
                </td>
                <td align="left" height="50%" style="padding:5px;">
        <asp:TextBox ID="txtSenhaNova" runat="server" TextMode="Password" Columns="8" 
                        Font-Size="10pt"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvSenha" runat="server" 
            ControlToValidate="txtSenhaNova" 
            ErrorMessage="Informe uma senha para acesso a pré-matrícula" Font-Size="8pt">*</asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="Label6" runat="server" 
                        Text="(Senha de no máximo 8 caracteres alfanumericos.)" Font-Size="7pt"></asp:Label>
                </td>
            </tr>
            <tr style="padding:5px;">
                <td align="right" height="50%" style="padding-right:5px;">
        <asp:Label ID="Label4" runat="server" 
            AssociatedControlID="txtSenhaNovaConfirma" Text="Repita a senha: " Font-Size="10pt"></asp:Label><br /><br />
                </td>
                <td align="left" height="50%" style="padding:5px;">
        <asp:TextBox ID="txtSenhaNovaConfirma" runat="server" TextMode="Password" Columns="9" 
                        MaxLength="8" Font-Size="10pt"></asp:TextBox>
        <asp:CompareValidator ID="cvRepita" runat="server" 
            ControlToCompare="txtSenhaNova" ControlToValidate="txtSenhaNovaConfirma" 
            ErrorMessage="As senhas digitadas devem ser idênticas" Font-Size="8pt">*</asp:CompareValidator>
        <asp:RequiredFieldValidator ID="rfvRepita" runat="server" 
            ControlToValidate="txtSenhaNovaConfirma" 
            ErrorMessage="Informe novamente a senha nova" Font-Size="8pt">*</asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="Label7" runat="server" 
                        Text="(Repita novamente a senha digitada anteriormente.)" Font-Size="7pt"></asp:Label>
                </td>
            </tr>
            <tr style="padding:5px;">
                <td align="center" height="50%" colspan="2" style="padding:5px;">
                <asp:Button ID="btnCadastrar" runat="server" onclick="btnCadastrar_Click" 
            Text="Cadastrar" />
                    </td>
            </tr>
        </table>
        
    
    </div>
    <div class="linhaNova"></div>
    <asp:Label ID="txtErro" CssClass="txtErro" runat="server" Font-Bold="True" Font-Size="9pt" 
        ForeColor="Red" Visible="False"></asp:Label>
        <div class="linhaNova"></div>
    </div>
    
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#divLoadLoginCadastroPreOnline #frmEsqueciSenha').ajaxForm({ target: '#divLoadLoginCadastroPreOnline', url: '/Componentes/loginCadastroPreMatriculaOnLine.aspx' });

        });
        
    </script>
</body>
</html>
