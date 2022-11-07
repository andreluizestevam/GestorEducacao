<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AjudaPorPalavraChave.ascx.cs"
    Inherits="C2BR.GestorEducacao.UI.Componentes.PossoAjudar.AjudaPorPalavraChave" %>
<div id="divPalavraChaveContainer">
    <div id="divPalavraChaveHeader" class="divCabecalho">
        <img alt="Icone Cabecalho" src="" />
        <h3>
            Encontre por <b>Palavra-Chave</b></h3>
        <asp:TextBox ID="txtQueryDePesquisa" runat="server" />
        <asp:Button ID="btnEncontrarAjuda" Text="Encontrar Ajuda" runat="server" 
            onclick="btnEncontrarAjuda_Click" />
    </div>
    <div id="divPalavraChaveContent" class="divContent">
        <asp:Repeater ID="rptResultadosPorPalavraChave" runat="server">
            <ItemTemplate>
                <h4>
                    <%# Eval("Titulo") %>
                </h4>
                <p>
                    <%# Eval("Descricao") %>
                </p>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
