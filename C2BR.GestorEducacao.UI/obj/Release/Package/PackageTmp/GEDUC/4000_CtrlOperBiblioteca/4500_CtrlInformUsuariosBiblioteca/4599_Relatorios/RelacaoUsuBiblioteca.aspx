<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="RelacaoUsuBiblioteca.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4500_CtrlInformUsuariosBiblioteca.F4599_Relatorios.RelacaoUsuBiblioteca" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 230px; }
        .liTpUsuario, .liUnidade
        {
            margin-top: 5px;
            width: 200px;
        }        
        .ddlTpUsuario { width:120px; }      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">         
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>               
        <li class="liTpUsuario">
            <label class="lblObrigatorio" for="ddlTpUsuario" title="Tipo de Usuário">
                Tipo Usuário</label>               
            <asp:DropDownList ID="ddlTpUsuario" ToolTip="Selecione um Tipo de Usuário" CssClass="ddlTpUsuario" runat="server">
            <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
            <asp:ListItem Value="A">Alunos</asp:ListItem>
            <asp:ListItem Value="F">Funcionário/Professor</asp:ListItem>
            <asp:ListItem Value="O">Outros</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTpUsuario" runat="server" CssClass="validatorField"
            ControlToValidate="ddlTpUsuario" Text="*" 
            ErrorMessage="Campo Tipo Usuário é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>   
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">        
    </script>
</asp:Content>
