<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1901_TipoOcorrencia.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 150px;}      
        
        /*--> CSS LIs */  
        .liClear{ clear: both;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio" title="Descrição">Descrição</label>
            <asp:TextBox ID="txtDescricao" runat="server" MaxLength="30" CssClass="campoDescricao"
                ToolTip="Informe a Descrição"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ter no máximo 30 caracteres" CssClass="validatorField"
                ValidationExpression="^(.|\s){1,30}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear">
            <label for="txtSigla" class="lblObrigatorio" title="Sigla">Sigla</label>
            <asp:TextBox ID="txtSigla" runat="server" MaxLength="3" CssClass="campoSigla" Enabled="false"
                ToolTip="Informe a Sigla"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ter até 3 caracteres" CssClass="validatorField" ValidationExpression="^(.|\s){1,3}$">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ser informada" CssClass="validatorField"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlTipo" class="lblObrigatorio" title="Tipo">Tipo</label>
            <asp:DropDownList ID="ddlTipo" runat="server"
                ToolTip="Selecione o Tipo">
                <asp:ListItem Text="Selecione" Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Aluno" Value="A"></asp:ListItem>
                <asp:ListItem Text="Funcionário" Value="F"></asp:ListItem>
                <asp:ListItem Text="Responsável" Value="R"></asp:ListItem>
                <asp:ListItem Text="Outros" Value="O"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTipo"
                ErrorMessage="Sigla deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
