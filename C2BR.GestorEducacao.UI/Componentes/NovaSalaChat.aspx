<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NovaSalaChat.aspx.cs" Inherits="C2BR.GestorEducacao.UI.Componentes.NovaSalaChat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="frmCadSala" runat="server">
        <div id="divCadSala">
            <ul>
                <li>
                    <label>Nome da Sala</label>
                    <asp:TextBox ID="txtNomSala" runat="server" OnTextChanged="txtNomSala_TextChange" Width="50px" ToolTip="Informe o nome da Sala.">
                    </asp:TextBox>
                </li>

                <li class="liBtn" style="margin-left: 0px !important; width: 50px !important; height: 20px !important;">
                    <asp:LinkButton ID="lnkIncSala" OnClick="lnkIncSala_Click" ValidationGroup="EnvMsg"
                        runat="server" ToolTip="Clique para Gravar a Nova Sala">
                        <asp:Label runat="server" ID="Label3" Text="ENVIAR"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
    </form>
</body>
</html>
