<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="FichaCadasResponAluno.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3700_CtrlInformacoesResponsaveis.F3799_Relatorios.FichaCadasResponAluno" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 270px; }
        .liUnidade,.liResponsavel
        {
            margin-top: 5px;
            width: 270px;            
        }                
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liUnidade">
            <label id="Label1" class="lblObrigatorio" title="Unidade/Escola" runat="server">
                Unidade/Escola</label>
            <asp:DropDownList ID="ddlUnidade" ToolTip="Selecione uma Unidade/Escola" CssClass="ddlUnidadeEscolar" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvddlDescOpRelatorio" runat="server" CssClass="validatorField"
            ControlToValidate="ddlUnidade" Text="*" 
            ErrorMessage="Campo Unidade/Escola é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>  
        </li>
        <li class="liResponsavel">
            <label class="lblObrigatorio"  title="Responsável">
                Responsável</label>               
            <asp:DropDownList ID="ddlResponsaveis" ToolTip="Selecione um Responsável" CssClass="ddlNomePessoa" runat="server">
            </asp:DropDownList>                      
            <asp:RequiredFieldValidator ID="rfvddlResponsaveis" runat="server" CssClass="validatorField"
            ControlToValidate="ddlResponsaveis" Text="*" 
            ErrorMessage="Campo Responsável é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator> 
        </li>    
     </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
