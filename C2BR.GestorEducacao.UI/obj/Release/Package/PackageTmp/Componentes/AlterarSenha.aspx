<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlterarSenha.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.AlterarSenha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Alterar Senha</title>
    <style type="text/css">
        #divAlterarSenhaContent  input[type='password']
        {
            width: 150px;
        }
        #divAlterarSenhaContent  
        {
            margin:auto;
            text-align:center;
            width:210px;
        }
        #divAlterarSenhaContainer #divHelpTxt
        {
            float: left;
            margin-top: 10px;
            width: 195px;
            color: #DF6B0D;
            font-weight: bold;
        }
        #divAlterarSenhaContainer #divAlterarSenhaFormButtons
        {
            margin-top: 5px;
            clear: both;
        }
        #divAlterarSenhaContainer #divRodape
        {
            margin-top: 10px;
            float: right;
        }
        #divAlterarSenhaContainer #imgLogoGestor
        {
            width: 127px;
            height: 30px;
        }
        .vsAlterarSenha { margin-top: 5px }
        .successMessage 
        { 
            background: #F1FFEF url(/Library/IMG/Gestor_ImgGood.png) no-repeat scroll center 10px;                 
            border: 1px solid #D2DFD1;
            font-size: 15px;
            font-weight: bold;
            margin: 5% auto auto;
            padding: 35px 10px 10px;
            text-align: center;
            width: 260px;
            display: none;
        }
    </style>
</head>
<body>
    <div id="divAlterarSenhaContainer">
        <form id="frmAlterarSenha" runat="server">
        <div id="divAlterarSenhaContent">
            <ul id="ulAlterarSenhaForm">
                <li>
                    <label for="txtSenhaAtual">Senha Atual</label>
                    <asp:TextBox runat="server" ID="txtSenhaAtual" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rvfTxtSenhaAtual" ErrorMessage="Por Favor preencha o campo Senha Atual" ControlToValidate="txtSenhaAtual" runat="server" Display="None" />
                </li>
                <li>
                    <label for="txtNovaSenha">Nova Senha</label>
                    <asp:TextBox runat="server" ID="txtNovaSenha" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rfvTxtNovaSenha" ErrorMessage="Por Favor preencha o campo Nova Senha" ControlToValidate="txtNovaSenha" runat="server" Display="None" />
                </li>
                <li>
                    <label for="txtConfirmaNovaSenha">Confirmar Nova Senha</label>
                    <asp:TextBox runat="server" ID="txtConfirmaNovaSenha" TextMode="Password" />
                    <asp:CompareValidator ID="cvNovaSenha" ControlToValidate="txtConfirmaNovaSenha" ControlToCompare="txtNovaSenha" runat="server" ErrorMessage="Os campos Nova Senha e Confirmar Nova Senha não coincidem." Display="None"></asp:CompareValidator>

                </li>
            </ul>
            <div id="divAlterarSenhaFormButtons">
                <asp:Button Text="Salvar" runat="server" ID="btnSalvar" onclick="btnSalvar_Click" />
                <asp:ValidationSummary ID="vsAlterarSenha" runat="server" CssClass="vsAlterarSenha" />
            </div>
        </div>
        <div class="successMessage">
            <asp:Label ID="lblMensagem" runat="server" Visible="false" />
        </div>
        <div id="divHelpTxt">
            <p>
                Informe a sua nova senha no formuário acima.</p>
        </div>
        <div id="divRodape">
            <img id="imgLogoGestor" src="/Library/IMG/Logo_EscolaW.png" alt="Portal Educação" />
        </div>
        </form>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#divAlterarSenhaContainer #frmAlterarSenha').ajaxForm({ target: '#divLoadAlterarSenha', url: '/Componentes/AlterarSenha.aspx' });

            if ($('#divAlterarSenhaContainer .successMessage span').length > 0) {
                $('#divAlterarSenhaContainer .successMessage').show();
                $('#divAlterarSenhaContainer #divAlterarSenhaContent').hide();
                $('#divAlterarSenhaContainer #divHelpTxt p').hide();
                $('#divAlterarSenhaContainer #divHelpTxt').html("<p>Clique no botão \"Fechar\" para voltar a tela inicial.</p>");
            }
        });
    </script>
</body>
</html>
