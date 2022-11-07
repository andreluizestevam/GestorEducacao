<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoRelatorios.Master" AutoEventWireup="true" CodeBehind="CarteiraIdentificacaoBeneficiario.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GSAUD._3000_ControleInformacoesUsuario._3100_ControleInformacoesCadastraisUsuario._3199_Relatorios.CarteiraIdentificacaoBeneficiario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados 
        {
            width: 1200px;
        }
        
        .ulDados li 
        {
            margin-top: 5px;
            margin-left: 5px;
        }
        
        .liBoth 
        {
            clear: both;
            margin-left: 390px !important;
        }
        .chkInfos label
        {
            display: inline;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul class="ulDados">
        <li class="liBoth">
            <asp:Label ID="lblUnidade" runat="server" ToolTip="Selecione a unidade">
            Unidade:
            </asp:Label><br />
            <asp:DropDownList ID="ddlUnidade" style="width: 200px" runat="server" OnSelectedIndexChanged="ddlUnidade_SelectedIndexChanged" AutoPostBack="true" ToolTip="Selecione a unidade">
            </asp:DropDownList>
        </li>      
        <li class="liBoth" style="margin-bottom:4px">
            <asp:Label ID="lblAluno" runat="server" ToolTip="Selecione o aluno desejado">
            Aluno:
            </asp:Label><br />
            <asp:DropDownList ID="ddlAluno" style="width: 240px;" runat="server" ToolTip="Selecione o aluno desejado">
            </asp:DropDownList>
        </li>
      
        <li class="liBoth">
            <asp:Label ID="Label1" runat="server" CssClass="lblObrigatorio" ToolTip="Informe a validade da carteira">
            Validade:
            </asp:Label><br />
            <asp:TextBox ID="txtValidade" runat="server" CssClass="campoData" ToolTip="Informe a validade da carteira">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvValidade" runat="server" CssClass="validatorField"
                ControlToValidate="txtValidade" Text="*" 
                ErrorMessage="Campo Data de Validade da Carteirinha é requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </li>
    </ul>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentScript" runat="server">
</asp:Content>
