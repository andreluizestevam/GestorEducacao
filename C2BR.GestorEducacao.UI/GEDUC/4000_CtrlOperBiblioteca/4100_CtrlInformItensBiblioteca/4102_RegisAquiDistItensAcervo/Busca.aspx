<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoBuscas.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4100_CtrlInformItensBiblioteca.F4102_RegisAquiDistItensAcervo.Busca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*--> CSS DADOS */
        .ddlUnidadeEscolar{width:230px;}
        .ddlFornecedor{width:230px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulParamsFormBusca" class="ulParamsFormBusca">
    <li>
        <label class="lblObrigatorio" for="ddlFornecedor">
            Fornecedor</label>
        <asp:DropDownList ID="ddlFornecedor" CssClass="ddlFornecedor" runat="server" ToolTip="Selecione um Fornecedor">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validatorField"
        ControlToValidate="ddlFornecedor" Text="*" 
        ErrorMessage="Campo Fornecedor é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
    </li>
    <li>
        <label class="lblObrigatorio" for="txtUnidade">Unidade/Escola</label>
        <asp:DropDownList ID="ddlUnidade" CssClass="ddlUnidadeEscolar" runat="server" ToolTip="Selecione a Unidade/Escola"
            AutoPostBack="True">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="validatorField"
        ControlToValidate="ddlUnidade" Text="*" 
        ErrorMessage="Campo Unidade é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
    </li>
    <li>
        <label for="ddlTipo" title="Tipo de Acervo">Tipo</label>
        <asp:DropDownList ID="ddlTipo" ToolTip="Selecione o Tipo" CssClass="ddlTipo" runat="server">
            <asp:ListItem Value="">Selecione</asp:ListItem>
            <asp:ListItem Value="C">Compra</asp:ListItem>
            <asp:ListItem Value="D">Doação</asp:ListItem>
            <asp:ListItem Value="T">Transferência</asp:ListItem>
            <asp:ListItem Value="O">Outros</asp:ListItem>
        </asp:DropDownList>
    </li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
 <script type="text/javascript">
</script>
</asp:Content>
