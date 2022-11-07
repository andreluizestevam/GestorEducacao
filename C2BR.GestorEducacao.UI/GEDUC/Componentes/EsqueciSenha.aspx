<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EsqueciSenha.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.EsqueciSenha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <style type="text/css">
        #divEsqueciSenhaContainer #ulEsqueciSenha { margin-top: 10px; }
        #divEsqueciSenhaContainer #ulEsqueciSenha #liEspacejamento { margin: 0 5px; }
        #divEsqueciSenhaContainer #ulEsqueciSenha li { float: left; }
        #divEsqueciSenhaContent span, label, p { font-size: 12px;}
        .ui-widget input, .ui-widget select, .ui-widget textarea, .ui-widget button 
        {
            font-family: Arial,sans-serif;
            font-size: 0.7em;
            margin-top: 5px;
        }
        #divEsqueciSenhaContainer #divHelpTxt
        {
            float: left;
            margin-top: 10px;
            width: 195px;
            color: #DF6B0D;
            font-weight: bold;
        }
        #divEsqueciSenhaContainer #divContentRodape { clear: both; text-align: center; }
        #divEsqueciSenhaContainer #divRodape
        {
            margin-top: 10px;
            float: right;
        }
        #divEsqueciSenhaContainer #imgLogoGestor
        {
            width: 127px;
            height: 30px;
        }
        .vsEsqueciSenha { margin-top: 5px }
    </style>
</head>
<body>
    <div id="divEsqueciSenhaContainer">
        <form id="frmEsqueciSenha" runat="server">
        <div id="divEsqueciSenhaContent">
            <span>Informe seu Nome de Usuário de acesso ao sistema.</span>
            <ul id="ulEsqueciSenha">
                <li>
                    <label for="txtNomeUsuario">
                        Nome Usuário</label>
                    <asp:TextBox runat="server" ID="txtNomeUsuario" ValidationGroup="vgEsqueciSenha" MaxLength="25"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTxtNomeUsuario" ValidationGroup="vgEsqueciSenha" ControlToValidate="txtNomeUsuario" runat="server" Display="None" ErrorMessage="Informe seu nome de usuário."></asp:RequiredFieldValidator>
                </li>
            </ul>
            <div id="divContentRodape">
                <asp:Button Text="Recuperar Senha" runat="server" ID="btnEnviar" ValidationGroup="vgEsqueciSenha" OnClick="btnEnviar_Click" />
                <asp:ValidationSummary ID="vsEsqueciSenha" ValidationGroup="vgEsqueciSenha" runat="server" CssClass="vsEsqueciSenha" />
            </div>
        </div>
        <div id="divHelpTxt">
            <p>Sua Senha será enviada para o Email cadastrado em sua conta.</p>
        </div>
        <div id="divRodape">
            <img id="imgLogoGestor" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>
        </form>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#divEsqueciSenhaContainer #frmEsqueciSenha').ajaxForm({ target: '#divLoadEsqueciSenha', url: '/Componentes/EsqueciSenha.aspx' });
        });
    </script>
</body>
</html>