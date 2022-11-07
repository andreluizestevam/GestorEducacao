<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4900_TabelasGenerCtrlOperBiblioteca.F4902_InformacaoClassificacaoObra.Cadastro"
    Title="Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 200px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; margin-top: 10px; }
        
        /*--> CSS DADOS */
        .txtDescricao, .ddlAreaConhe { width: 200px; }
        
    </style>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados"> 
        <li>
            <label for="ddlAreaConhe" class="lblObrigatorio" title="Área de Conhecimento">Área de Conhecimento</label>
            <asp:DropDownList ID="ddlAreaConhe" runat="server" CssClass="ddlAreaConhe"
                ToolTip="Selecione a Área de Conhecimento">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAreaConhe"
                ErrorMessage="Área de Conhecimento deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li> 
        <li class="liClear">
            <label for="txtDescricao" title="Descrição" class="lblObrigatorio">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe a Descrição" runat="server" MaxLength="60" CssClass="txtDescricao"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ter no máximo 60 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"
                CssClass="validatorField">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
