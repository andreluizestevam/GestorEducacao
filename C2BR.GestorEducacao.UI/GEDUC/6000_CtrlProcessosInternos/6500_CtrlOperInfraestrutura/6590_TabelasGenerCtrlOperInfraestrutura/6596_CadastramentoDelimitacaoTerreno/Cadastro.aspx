<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F6000_CtrlProcessosInternos.F6500_CtrlOperInfraestrutura.F6590_TabelasGenerCtrlOperInfraestrutura.F6596_CadastramentoDelimitacaoTerreno.Cadastro"
    Title="Untitled Page" %>
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
        <li class="liClear">
            <label for="txtDescricao" class="lblObrigatorio labelPixel" title="Descrição do Delimitador de Terreno">
                Descrição</label>
            <asp:TextBox ID="txtDescricao" CssClass="txtDescricao" ToolTip="Informe a Descrição do Delimitador do Terreno" runat="server" MaxLength="30"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ter no máximo 30 caracteres" ValidationExpression="^(.|\s){1,60}$"
                CssClass="validatorField">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
         <li class="liClear">
            <label for="txtSigla" title="Sigla do Delimitador de Terreno" class="lblObrigatorio labelPixel">
                Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Informe a Sigla do Delimitador do Terreno" runat="server" CssClass="txtSigla" MaxLength="3"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ser informada" CssClass="validatorField">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>
