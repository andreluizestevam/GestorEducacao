<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManterEnderecoSN.ascx.cs" Inherits="C2BR.GestorEducacao.UI.Library.Componentes.ManterEnderecoSN" %>
<div id="divActionBar" class="bar" >
    <label for="txtCEP" title="CEP">
        CEP</label>
    <asp:TextBox ID="txtCep" runat="server" MaxLength="9"></asp:TextBox>
    <asp:Button ID="btnConsultar" runat="server" Text="Consultar Cep" OnClick="btnConsultar_Click" />
    <label for="txtLogradouro" title="Logradouro">
        Logradouro</label>
    <asp:TextBox ID="txtLogradouro" runat="server"></asp:TextBox>
    <label for="txtEstado" title="Estado">
        Estado</label>
    <asp:DropDownList ID="ddlUf" runat="server"  AutoPostBack="true" 
        OnSelectedIndexChanged="ddlUf_SelectedIndexChanged">
    </asp:DropDownList>
    <br /><br />
    <label for="txtCidade" title="Cidade">
        Cidade</label>
    <asp:DropDownList ID="ddlCidade" runat="server" AutoPostBack="true" Width="200px">
    </asp:DropDownList>
    <br /><br />
    <label for="txtBairro" title="Bairro">
        Bairro</label>
    <asp:TextBox ID="txtBairro" runat="server"></asp:TextBox><br /><br />
</div>
