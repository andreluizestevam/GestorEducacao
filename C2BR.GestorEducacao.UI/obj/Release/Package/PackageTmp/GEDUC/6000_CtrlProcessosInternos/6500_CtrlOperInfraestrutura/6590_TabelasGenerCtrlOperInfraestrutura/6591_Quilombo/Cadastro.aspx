<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6500_CtrlOperInfraestrutura.F6590_TabelasGenerCtrlOperInfraestrutura.F6591_Quilombo.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 160px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom: 1px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear" >
            <label for="txtNO_QUI" title="Quilombo" class="lblObrigatorio labelPixel">
                Quilombo</label>
            <asp:TextBox ID="txtNO_QUI" ToolTip="Informe o Quilombo" runat="server" MaxLength="50" CssClass="campoDescricao"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revTxtNO_QUI" runat="server"
                ControlToValidate="txtNO_QUI" ValidationExpression="^(.|\s){1,50}$" CssClass="validatorField"
                ErrorMessage="Campo Quilombo não pode ser maior que 50 caracteres" Text="*" Display="Dynamic"
                SetFocusOnError="true">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvTxtNO_QUI" runat="server"  ControlToValidate="txtNO_QUI"
                CssClass="validatorField" ErrorMessage="Campo Quilombo é requerido" Text="*"
                Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
