<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6300_CtrlOperPatrimonio.F6390_TabelasGenerCtrlOperPatrimonio.F6391_ClassifPatrimonio.Cadastro"
    Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 300px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        .txtDescricao { width: 300px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">       
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio" title="Descrição da Classificação do Patrimônio">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" CssClass="txtDescricao" ToolTip="Informe a Descrição da Classificação do Patrimônio" runat="server" MaxLength="100"></asp:TextBox>    
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>    
    </ul>
</asp:Content>
