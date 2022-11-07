<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormularioEndereco.ascx.cs" Inherits="C2BR.GestorEducacao.UI.Library.Componentes.FormEndereco" %>
<ul>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <li id="liCEP" class="liCEP" runat="server">
        <label for="txtCEP" title="CEP">CEP</label>
        <asp:TextBox ID="txtCEP" ToolTip="Informe o CEP" CssClass="txtCEP" runat="server"></asp:TextBox>
    </li>
    <li id="liPesquisarCep" class="liPesquisarCep" runat="server">
        <asp:ImageButton ID="btnPesquisarCep" runat="server" onclick="btnPesquisarCep_Click"
            ImageUrl="/Library/IMG/Gestor_BtnPesquisa.png" CssClass="btnPesquisarCep" 
            CausesValidation="false"/>
    </li>
    <li id="liCorreios" class="liCorreios" runat="server">
        <asp:ImageButton ID="btnCorreios" runat="server" ImageUrl="/Library/IMG/Gestor_Correios.gif" CssClass="btnCorreios" OnClientClick="javascript:window.open('http://www.buscacep.correios.com.br/servicos/dnec/index.do');" CausesValidation="false"/>
    </li>
    <li id="liLogradouro" class="liLogradouro" runat="server">
        <label for="txtLogradouro" class="lblObrigatorio" title="Endereço">Endere&ccedil;o</label>
        <asp:TextBox ID="txtLogradouro" ToolTip="Informe o Endereço" Enabled="false" CssClass="txtLogradouro" MaxLength="60" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLogradouro" ErrorMessage="Logradouro deve ser informado" Display="None"></asp:RequiredFieldValidator>
    </li>
    <li id="liNumero" class="liNumero" runat="server">
        <label for="txtNumero" title="Número">N°</label>
        <asp:TextBox ID="txtNumero" ToolTip="Informe o Número" CssClass="txtNumero campoNumerico" MaxLength="5" runat="server"></asp:TextBox>
    </li>
    <li id="liComplemento" class="liComplemento" runat="server">
        <label for="txtComplemento" title="Complemento">Complemento</label>
        <asp:TextBox ID="txtComplemento" ToolTip="Informe o Complemento" CssClass="txtComplemento" MaxLength="30" runat="server"></asp:TextBox>
    </li>
    <li id="liBairro" class="liBairro" runat="server">
        <label for="ddlBairro" class="lblObrigatorio" title="Bairro">Bairro</label>
        <asp:DropDownList ID="ddlBairro" ToolTip="Selecione o Bairro" CssClass="ddlBairro" Enabled="false" runat="server"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBairro" ErrorMessage="Bairro deve ser informado" Display="None"></asp:RequiredFieldValidator>
    </li>
    <li id="liCidade" class="liCidade" runat="server">
        <label for="ddlCidade" class="lblObrigatorio" title="Cidade">Cidade</label>
        <asp:DropDownList ID="ddlCidade" ToolTip="Selecione a Cidade" CssClass="ddlCidade" Enabled="false"
            AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCidade" ErrorMessage="Cidade deve ser informada" Display="None"></asp:RequiredFieldValidator>
    </li>
    <li id="liUF" class="liUF" runat="server">
        <label for="ddlUF" class="lblObrigatorio" title="UF">UF</label>
        <asp:DropDownList ID="ddlUF" ToolTip="Selecione a UF" CssClass="ddlUF" Enabled="false"
            AutoPostBack="true" OnSelectedIndexChanged="ddlUf_SelectedIndexChanged" runat="server"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlUF" ErrorMessage="UF deve ser informada" Display="None"></asp:RequiredFieldValidator>
    </li>
    </ContentTemplate>
    </asp:UpdatePanel>
</ul>
<script type="text/javascript">
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        $(".txtNumero").mask("?99999");
    }); 
    $(document).ready(function() {
        $(".txtNumero").mask("?99999");
    });
</script>