<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1600_ProgramasSociais.F1602_TipoProgrSociais.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados{ width: 302px; }        
    .ulDados input, .ulDados select{ margin-bottom: 0;}
    
    /*--> CSS LIs */
    .ulDados li{ margin-bottom: 10px; margin-right: 10px;}
        
    /*--> CSS DADOS */
    .campoUnidadeEscolar {width:282px !important;}
    .txtTipo {width:210px;}
    .txtSigla {width:60px;}
    .ddlSituacao {width:80px;}
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulDados" class="ulDados">
    <li>
        <label for="txtInstituicao" title="Institui��o">Institui��o</label>
        <asp:TextBox ID="txtInstituicao" CssClass="campoUnidadeEscolar" runat="server" Enabled="false"
            ToolTip="Institui��o" />
    </li>
    <li style="clear:both;">
        <label for="txtTipo" class="lblObrigatorio" title="Tipo do Programa/Conv�nio S�cio-Educacional">Tipo</label>
        <asp:TextBox ID="txtTipo" CssClass="txtTipo" runat="server" MaxLength="60"
            ToolTip="Informe o Tipo do Programa/Conv�nio S�cio-Educacional" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTipo" CssClass="validatorField"
            ErrorMessage="Tipo do Programa/Conv�nio deve ser informado.">
        </asp:RequiredFieldValidator>
    </li>
    <li>
        <label for="txtSigla" class="lblObrigatorio" title="Sigla do Programa/Conv�nio S�cio-Educacional">Sigla</label>
        <asp:TextBox ID="txtSigla" CssClass="txtSigla" runat="server" MaxLength="12"
            ToolTip="Informe a Sigla do Programa/Conv�nio S�cio-Educacional" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSigla" CssClass="validatorField"
            ErrorMessage="Sigla deve ser informada.">
        </asp:RequiredFieldValidator>
    </li>
    <li style="clear:both;">
        <label for="ddlSituacao" class="lblObrigatorio" title="Situa��o do Programa/Conv�nio S�cio-Educacional">Situa��o</label>
        <asp:DropDownList ID="ddlSituacao" CssClass="ddlSituacao" runat="server" 
            ToolTip="Informe a Situa��o do Programa/Conv�nio S�cio-Educacional" >
            <asp:ListItem Selected="True" Value="A">Ativo</asp:ListItem>
            <asp:ListItem Value="I">Inativo</asp:ListItem>
            <asp:ListItem Value="S">Suspenso</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSituacao" CssClass="validatorField"
            ErrorMessage="Situa��o deve ser informada.">
        </asp:RequiredFieldValidator>
    </li>
    <li>
        <label for="txtDataSituacao" class="lblObrigatorio" title="Data da Situa��o">Data da Situa��o</label>
        <asp:TextBox ID="txtDataSituacao" Enabled="False" CssClass="campoData" runat="server" 
            ToolTip="Informe a Data da Situa��o do Programa/Conv�nio S�cio-Educacional" />             
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDataSituacao" CssClass="validatorField"
            ErrorMessage="Data de Situa��o deve ser informada.">
        </asp:RequiredFieldValidator>
    </li>
    <li style="clear:both;">
        <label for="txtDataCadastro" title="Data de Cadastro">Data de Cadastro</label>
        <asp:TextBox ID="txtDataCadastro" CssClass="campoData" Enabled ="false" runat="server" 
            ToolTip="Data de Cadastro do Programa/Conv�nio S�cio-Educacional" />
    </li>
    </ul>
<script type="text/javascript">
    jQuery(function ($) {
    });
    </script>
</asp:Content>