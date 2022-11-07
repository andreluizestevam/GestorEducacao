<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F0000_ConfiguracaoAmbiente.F0900_TabelasGeraisAmbienteGE.F0910_TipoLogradouro.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 184px; } 
        
        /*--> CSS LIs */
        .liCodigoTL { width: 80px; clear: both;}
        .liDescricaoTL { width: 250px; clear: both; }  
        
        /*--> CSS DADOS */
        .txtCodigoTL {width: 90px; text-transform:uppercase;}        
        .txtDescricaoTL {width: 180px;}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="liCodigoTL">
            <label for="txtCodigoTL" class="lblObrigatorio" title="Código">Código</label>
            <asp:TextBox ID="txtCodigoTL" runat="server" MaxLength="12" ToolTip="Código" CssClass="txtCodigoTL">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="txtCodigoTL" 
                ErrorMessage="Campo Código é requerido">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liDescricaoTL">
            <label for="txtDescricaoTL" class="lblObrigatorio" title="Descrição">Descrição</label>
            <asp:TextBox ID="txtDescricaoTL" runat="server" MaxLength="60" CssClass="txtDescricaoTL"
                ToolTip="Informe a Descrição">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" CssClass="validatorField" ControlToValidate="txtDescricaoTL"
                ErrorMessage="Campo Descrição é requerida">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>