<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="FichaCadastIndivUsuario.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3199_Relatorios.FichaCadastIndivUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 270px; }
        .liUnidade,.liResponsavel, .liUsuario
        {
            margin-top: 10px;
            width: 270px;            
        }             
        .lblCampo 
        {
            margin-bottom: 2px;
        }   
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio lblCampo" title="Unidade/Escola" runat="server">
                Unidade</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é obrigatório" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>
        <li class="liResponsavel">
            <label class="lblObrigatorio lblCampo" for="ddlResponsaveis"  title="Responsável">
                Responsável (CPF/Nome) </label>               
            <asp:DropDownList ID="ddlResponsaveis" AutoPostBack="true" OnSelectedIndexChanged="ddlResponsaveis_SelectedIndexChanged" ToolTip="Selecione um Responsável" CssClass="ddlNomePessoa" runat="server">
            </asp:DropDownList>                      
            <asp:RequiredFieldValidator ID="rfvddlResponsaveis" runat="server" CssClass="validatorField"
            ControlToValidate="ddlResponsaveis" Text="*" 
            ErrorMessage="Campo Responsável é obrigatório" SetFocusOnError="true"></asp:RequiredFieldValidator> 
        </li>    
        <li class="liUsuario">
            <label class="lblObrigatorio lblCampo" for="ddlUsuario"  title="Selecione um Usuário">
                Usuário (NIRS/Nome) </label>               
            <asp:DropDownList ID="ddlUsuario" ToolTip="Selecione um Usuiário" CssClass="ddlNomePessoa" runat="server">
            </asp:DropDownList>                      
            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUsuario" Text="*" 
            ErrorMessage="Campo Usuário é obrigatório" SetFocusOnError="true"></asp:RequiredFieldValidator> 
        </li>    
     </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
