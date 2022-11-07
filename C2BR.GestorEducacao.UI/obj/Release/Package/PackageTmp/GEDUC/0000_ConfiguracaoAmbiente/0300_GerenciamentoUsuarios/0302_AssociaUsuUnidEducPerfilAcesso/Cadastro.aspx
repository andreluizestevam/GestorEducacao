<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0300_GerenciamentoUsuarios.F0302_AssociaUsuUnidEducPerfilAcesso.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados {width: 270px;}
        
        /*--> CSS LIs */
        .ulDados li {margin-top: 10px;}
        .liClear {clear: both;}
        .liStatusAssoc {margin-left:10px;}
        
        /*--> CSS DADOS */
        .labelPixel {margin-bottom: 1px;}
        .chkLocaisAssoc label { display: inline !important; margin-left:-4px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label class="lblObrigatorio">
                Tipo de Usuário</label>
            <asp:DropDownList ID="drpTipoUsu" runat="server" AutoPostBack="true" ToolTip="Selecione o Tipo de Usuário"
                OnSelectedIndexChanged="drpTipoUsu_SelectedIndexChanged" Width="93px" />
        </li>
        <li class="liClear">
            <label for="ddlUsuarioAssoc" class="lblObrigatorio labelPixel" title="Nome do Usuário">
                Usuário</label>
            <asp:DropDownList ID="ddlUsuarioAssoc" ToolTip="Informe o Usuário" CssClass="campoNomePessoa"
                runat="server">
                <asp:ListItem Text="Selecione" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="ddlUsuarioAssoc"
                ErrorMessage="Usuário deve ser informado" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlUnidadeAssoc" class="lblObrigatorio labelPixel" title="Unidade">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidadeAssoc"  Width="257px" ToolTip="Informe a Unidade/Escola" CssClass="campoUnidade"
                runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlUnidadeAssoc_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUnidadeAssoc"
                ErrorMessage="Unidade/Escola deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="ddlPerfilAssoc" class="lblObrigatorio labelPixel" title="Perfil de Acesso">
                Perfil de Acesso</label>
            <asp:DropDownList ID="ddlPerfilAssoc" Width="170px" ToolTip="Informe o Perfil de Acesso"
                CssClass="campoDescricao" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPerfilAssoc"
                ErrorMessage="Perfil de Acesso deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liStatusAssoc">
            <label for="ddlStatusAssoc" class="lblObrigatorio labelPixel" title="Status">
                Status</label>
            <asp:DropDownList ID="ddlStatusAssoc" Width="60px" ToolTip="Informe o Status" runat="server">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
        <li class="liClear">
            <asp:CheckBox ID="cbOrigem" runat="server" CssClass="checkboxLabel" 
                Text="Unidade Principal" />
        </li>
    </ul>
</asp:Content>

