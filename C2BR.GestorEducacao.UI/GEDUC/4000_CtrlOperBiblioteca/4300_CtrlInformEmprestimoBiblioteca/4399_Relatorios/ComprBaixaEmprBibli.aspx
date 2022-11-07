<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="ComprBaixaEmprBibli.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F4000_CtrlOperBiblioteca.F4300_CtrlInformEmprestimoBiblioteca.F4399_Relatorios.ComprBaixaEmprBibli" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 230px; }
        .liNumEmpres,.liUnidade
        {
            margin-top: 5px;
            width: 200px;
        }
        .ddlEmprestimo { width:115px; }      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <ul id="ulDados" class="ulDados">         
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <li class="liUnidade">
            <label class="lblObrigatorio" for="txtUnidade" title="Unidade/Escola">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" 
                CssClass="ddlUnidadeEscolar" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlUnidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlUnidade" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>               
        <li class="liNumEmpres">
            <label class="lblObrigatorio" for="ddlEmprestimo" title="Nº Empréstimo">
                Nº Empréstimo</label>               
            <asp:DropDownList ID="ddlEmprestimo" ToolTip="Selecione um Tipo de Usuário" CssClass="ddlEmprestimo" runat="server">            
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlTpUsuario" runat="server" CssClass="validatorField"
            ControlToValidate="ddlEmprestimo" Text="*" 
            ErrorMessage="Campo Nº Empréstimo é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>   
        </ContentTemplate>
        </asp:UpdatePanel>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
    <script type="text/javascript">        
    </script>
</asp:Content>
