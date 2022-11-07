<%@ Page Title="" Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1900_TabelasGenerCtrlAdmEscolar.F1903_CursosFormacaoEspecializacao.Cadastro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .ulDados { width: 225px; }
    label { margin-bottom:1px; }
    
    /*--> CSS LIs */
    .liClear { clear: both; }
    .liPromocao{ margin-left: 5px;}                            
    .liTipo { clear: both; margin-bottom:10px; }
    
    /*--> CSS DADOS */ 
    .txtSigla { width: 80px; }
    .txtDescricao { width: 225px; }
    .txtPontuacao{ width: 55px;}
    .ddlPromocao{ width: 55px;}
    
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
<ul id="ulDados" class="ulDados">
    <li>
        <label for="txtCodigo" title="Código">Código</label>
        <asp:TextBox ID="txtCodigo" Enabled="false" runat="server" MaxLength="10" CssClass="txtCod" Text="0"
            ToolTip="Código">
        </asp:TextBox>
    </li>
    <li class="liClear">
        <label for="txtDescricao" class="lblObrigatorio" title="Descrição">Descrição</label>
        <asp:TextBox ID="txtDescricao" runat="server" CssClass="txtDescricao" MaxLength="60"
            ToolTip="Informe a Descrição"></asp:TextBox>
        <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtDescricao" ErrorMessage="Descrição deve ter no máximo 60 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$" SetFocusOnError="true" CssClass="validatorField"></asp:RegularExpressionValidator>   
        <asp:RequiredFieldValidator ID="rfvDescricao" runat="server" ControlToValidate="txtDescricao" ErrorMessage="Descrição deve ser informada" Text="*" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>       
    </li> 
    <li class="liClear">
        <label for="txtSigla" class="lblObrigatorio" title="Sigla">Sigla</label>
        <asp:TextBox ID="txtSigla" runat="server" CssClass="txtSigla" MaxLength="12"
            ToolTip="Informe a Sigla"></asp:TextBox>
        <asp:RegularExpressionValidator ID="revSigla" runat="server" ControlToValidate="txtSigla" ErrorMessage="Sigla deve ter no máximo 12 caracteres" Text="*" ValidationExpression="^(.|\s){1,12}$" SetFocusOnError="true" CssClass="validatorField"></asp:RegularExpressionValidator>  
        <asp:RequiredFieldValidator ID="rfvSigla" runat="server" ControlToValidate="txtSigla" ErrorMessage="Sigla deve ser informada" Text="*" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>        
    </li>
    <li class="liTipo">
        <label for="ddlTipo" class="lblObrigatorio" title="Tipo">Tipo</label>
        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="ddlTipo"
            ToolTip="Selecione o Tipo">
            <asp:ListItem Value="">Selecione</asp:ListItem>
            <asp:ListItem Value="EP">Específico</asp:ListItem>
            <asp:ListItem Value="TE">Técnico</asp:ListItem>
            <asp:ListItem Value="GR">Graduação</asp:ListItem>
            <asp:ListItem Value="ES">Especialização</asp:ListItem>
            <asp:ListItem Value="MB">MBA</asp:ListItem>
            <asp:ListItem Value="PG">Pós-Graduação</asp:ListItem>
            <asp:ListItem Value="ME">Mestrado</asp:ListItem>
            <asp:ListItem Value="DO">Doutorado</asp:ListItem>
            <asp:ListItem Value="PD">Pós-Doutorado</asp:ListItem>                
            <asp:ListItem Value="OU">Outros</asp:ListItem>                    
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTipo" ErrorMessage="Tipo deve ser informado" Text="*" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>
    </li>  
    <li class="liClear">
        <label for="txtPontuacao" class="lblObrigatorio" title="Pontuação">Pontuação</label>
        <asp:TextBox ID="txtPontuacao" runat="server" CssClass="txtPontuacao" MaxLength="9"
            ToolTip="Informe a Pontuação"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPontuacao" ErrorMessage="Pontuação deve ser informada" Text="*" SetFocusOnError="true" CssClass="validatorField"></asp:RequiredFieldValidator>        
    </li>
    <li class="liPromocao">
        <label for="ddlPromocao" class="lblObrigatorio" title="Promoção">Promoção</label>
        <asp:DropDownList ID="ddlPromocao" runat="server" CssClass="ddlPromocao"
            ToolTip="Informe se é Promoção">
            <asp:ListItem Value="N">Não</asp:ListItem>
            <asp:ListItem Value="S">Sim</asp:ListItem>                   
        </asp:DropDownList>
    </li>
</ul>
<script type="text/javascript">
    jQuery(function($) {
    $(".txtPontuacao").mask("?999999999");
    });
</script>
</asp:Content>