<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0500_SuporteOperacionalGE.F0505_AssociaUsuarioGrafico.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados{ width: 300px; }
        .ulDados input{ margin-bottom: 0;}
        
        /*--> CSS LIs */
        .ulDados li{ margin-bottom: 10px; margin-right: 10px;}              
        .liClear{ clear: both;}
        
        /*--> CSS Dados */
        .ddlUsuario { width: 300px; }
        .ddlStatus { width: 60px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label title="Usu�rio do Gr�fico" class="lblObrigatorio">
                Usu�rio</label>
            <asp:DropDownList ID="ddlUsuario" ToolTip="Selecione o Usu�rio do Gr�fico" CssClass="ddlUsuario" runat="server">                  
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validatorField"  runat="server" ControlToValidate="ddlUsuario"
                ErrorMessage="Usu�rio do Gr�fico deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label title="T�tulo do Gr�fico" class="lblObrigatorio">
                T�tulo do Gr�fico</label>
            <asp:DropDownList ID="ddlTitulGrafi" ToolTip="Selecione o T�tulo do Gr�fico" CssClass="ddlUsuario" runat="server">                              
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validatorField"  runat="server" ControlToValidate="ddlTitulGrafi"
                ErrorMessage="T�tulo do Gr�fico deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li>
            <label for="ddlStatus" title="Status" class="lblObrigatorio">Status</label>
            <asp:DropDownList ID="ddlStatus" ToolTip="Selecione o status" runat="server" CssClass="ddlStatus">
                <asp:ListItem Text="Ativa" Value="A"></asp:ListItem>
                <asp:ListItem Text="Inativa" Value="I"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="validatorField"  runat="server" ControlToValidate="ddlStatus"
                ErrorMessage="Status deve ser informado" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
