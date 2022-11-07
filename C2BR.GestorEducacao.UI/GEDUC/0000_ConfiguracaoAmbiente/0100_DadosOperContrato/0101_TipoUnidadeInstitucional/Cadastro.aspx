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
            <label for="txtDescricao" class="lblObrigatorio" title="Descrição do Tipo de Unidade">Descrição</label>
            <asp:TextBox ID="txtDescricaoTpUnid" ToolTip="Informe a Descrição do Tipo de Unidade" CssClass="txtDescricaoTpUnid" runat="server" MaxLength="60"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricaoTpUnid"
                CssClass="validatorField" ErrorMessage="Descrição deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlClassificacaoTpUnid" title="Classificação" class="lblObrigatorio">Classificação</label>
            <asp:DropDownList ID="ddlClassificacaoTpUnid" ToolTip="Selecione uma Classificação" runat="server" CssClass="ddlClassificacaoTpUnid">
                <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem> 
                <asp:ListItem Text="Clientes" Value="C"></asp:ListItem>
                <asp:ListItem Text="Escola" Value="E" ></asp:ListItem>
                <asp:ListItem Text="Fornecedores" Value="F"></asp:ListItem>                
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlClassificacaoTpUnid"
                CssClass="validatorField" ErrorMessage="Classificação deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
