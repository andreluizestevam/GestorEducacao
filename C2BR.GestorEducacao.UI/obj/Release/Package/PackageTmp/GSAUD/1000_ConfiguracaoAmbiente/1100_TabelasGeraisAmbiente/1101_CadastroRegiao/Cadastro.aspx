<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._1000_ConfiguracaoAmbiente._1100_TabelasGeraisAmbiente._1101_CadastroRegiao.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 270px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liEspaco { margin-left: 10px; }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom: 1px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
  <ul id="ulDados" class="ulDados">
        <li id="liClear">
            <label for="txtRegiao" title="Região" class="lblObrigatorio labelPixel">
                Região</label>
            <asp:TextBox ID="txtRegiao" ToolTip="Informe a Região" runat="server" MaxLength="60" CssClass="campoRegiao"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" CssClass="validatorField"  runat="server" ControlToValidate="txtRegiao"
                ErrorMessage="Região deve ter no máximo 60 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" CssClass="validatorField"  runat="server" ControlToValidate="txtRegiao"
                ErrorMessage="Região deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>

                <li id="liClear2">
            <label for="txtSigla" title="Sigla" class="lblObrigatorio labelPixel">
                Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Informe a Sigla" runat="server" MaxLength="12" CssClass="campoSigla"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revSigla" CssClass="validatorField"  runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ter no máximo 12 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvSigla" CssClass="validatorField"  runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ser informada" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        
    </ul>
</asp:Content>

