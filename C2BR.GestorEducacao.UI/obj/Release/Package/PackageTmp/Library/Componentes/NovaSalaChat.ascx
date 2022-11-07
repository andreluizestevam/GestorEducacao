<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NovaSalaChat.ascx.cs" Inherits="C2BR.GestorEducacao.UI.Library.Componentes.NovaSalaChat" %>


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
