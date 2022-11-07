<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0902_Cidade.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 270px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liEspaco { margin-left: 10px; }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom: 1px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li id="liClear">
            <label for="txtDescricaoCC" title="Cidade" class="lblObrigatorio labelPixel">
                Cidade</label>
            <asp:TextBox ID="txtDescricaoCC" ToolTip="Informe a Cidade" runat="server" MaxLength="60" CssClass="campoCidade"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" CssClass="validatorField"  runat="server" ControlToValidate="txtDescricaoCC"
                ErrorMessage="Descrição deve ter no máximo 60 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" CssClass="validatorField"  runat="server" ControlToValidate="txtDescricaoCC"
                ErrorMessage="Descrição deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liEspaco">
            <label for="ddlUfCC" title="UF" class="lblObrigatorio labelPixel">
                UF</label>
            <asp:DropDownList ID="ddlUfCC" ToolTip="Selecione a UF" runat="server" CssClass="campoUf">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"  runat="server" ControlToValidate="ddlUfCC"
                ErrorMessage="UF deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
