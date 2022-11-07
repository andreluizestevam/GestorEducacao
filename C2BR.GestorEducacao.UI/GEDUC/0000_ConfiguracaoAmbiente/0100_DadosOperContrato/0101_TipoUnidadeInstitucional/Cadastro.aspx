<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" 
MasterPageFile="~/App_Masters/PadraoCadastros.Master" Title="Cadastro"
Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0100_DadosOperContrato.F0101_TipoUnidadeInstitucional.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .ulDados{ width: 260px; }        
        
        /*--> CSS DADOS */
        .txtDescricaoTpUnid{ width: 220px; }
        .ddlClassificacaoTpUnid{ width: 95px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados"> 
        <li>
            <label for="txtDescricao" class="lblObrigatorio" title="Descri��o do Tipo de Unidade">Descri��o</label>
            <asp:TextBox ID="txtDescricaoTpUnid" ToolTip="Informe a Descri��o do Tipo de Unidade" CssClass="txtDescricaoTpUnid" runat="server" MaxLength="60"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricaoTpUnid"
                CssClass="validatorField" ErrorMessage="Descri��o deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlClassificacaoTpUnid" title="Classifica��o" class="lblObrigatorio">Classifica��o</label>
            <asp:DropDownList ID="ddlClassificacaoTpUnid" ToolTip="Selecione uma Classifica��o" runat="server" CssClass="ddlClassificacaoTpUnid">
                <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem> 
                <asp:ListItem Text="Clientes" Value="C"></asp:ListItem>
                <asp:ListItem Text="Escola" Value="E" ></asp:ListItem>
                <asp:ListItem Text="Fornecedores" Value="F"></asp:ListItem>                
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlClassificacaoTpUnid"
                CssClass="validatorField" ErrorMessage="Classifica��o deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
