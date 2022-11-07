<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0901_Bairro.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 300px; }
        .ulDados input{ margin-bottom: 0;}
        
        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px; margin-right: 10px;}              
        .liClear{ clear: both;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlUfCB" title="UF" class="lblObrigatorio">UF</label>
            <asp:DropDownList ID="ddlUfCB" ToolTip="Selecione uma UF" runat="server" CssClass="campoUf" AutoPostBack="true" Enabled="false"
                onselectedindexchanged="ddlUfCB_SelectedIndexChanged"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"  runat="server" ControlToValidate="ddlUfCB"
                ErrorMessage="UF deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlCidadeCB" title="Cidade" class="lblObrigatorio">Cidade</label>
            <asp:DropDownList ID="ddlCidadeCB" ToolTip="Selecione uma Cidade" runat="server" CssClass="campoCidade" Enabled="false"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvDescricao" CssClass="validatorField"  runat="server" ControlToValidate="ddlCidadeCB"
                ErrorMessage="Cidade deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtBairroCB" title="Bairro" class="lblObrigatorio">Bairro</label>
            <asp:TextBox ID="txtBairroCB" ToolTip="Informe um Bairro" runat="server" MaxLength="80" CssClass="campoBairro"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validatorField"  runat="server" ControlToValidate="txtBairroCB"
                ErrorMessage="Bairro deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
