<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1113_CadastramentoCtaContabil.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 248px; }
        
        /*--> CSS LIs */
        .liDescricaoConta, .liSubgrupo { clear: both; margin-top: 5px; }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom: 1px; }
        .txtDescricao { width: 200px; }
        .txtNumConta { width: 30px; text-align: right; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlTipoConta" title="Tipo de Conta" class="lblObrigatorio labelPixel">Tipo de Conta</label>
            <asp:DropDownList ID="ddlTipoConta" ToolTip="Selecione o Tipo de Conta" Width="110px" runat="server"
            onselectedindexchanged="ddlTipoConta_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                <asp:ListItem Value="B">2 - Passivo</asp:ListItem>
                <asp:ListItem Value="C">3 - Entradas/Saidas e compensações</asp:ListItem>
                <asp:ListItem Value="D">4 - Entradas e custos</asp:ListItem>
                <asp:ListItem Value="E">5 - Despesas</asp:ListItem>
                <asp:ListItem Value="F">6 - Receita</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTipoConta" 
            ErrorMessage="Tipo de Conta deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liSubgrupo">
            <label for="ddlGrupo" title="Grupo" class="lblObrigatorio labelPixel">Grupo</label>
            <asp:DropDownList ID="ddlGrupo" ToolTip="Selecione o Grupo" Width="240px" runat="server" 
                onselectedindexchanged="ddlGrupo_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGrupo" 
                ErrorMessage="Grupo deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>        
        <li class="liSubgrupo">
            <label for="ddlSubgrupo" class="lblObrigatorio labelPixel" title="Subgrupo">Subgrupo</label>
            <asp:DropDownList ID="ddlSubgrupo" Width="240px" runat="server" ToolTip="Selecione o Subgrupo"
            onselectedindexchanged="ddlSubgrupo_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSubgrupo" 
                ErrorMessage="Subgrupo deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>    
        <li class="liSubgrupo">
            <label for="ddlSubgrupo2" class="lblObrigatorio labelPixel" title="Subgrupo2">Subgrupo2</label>
            <asp:DropDownList ID="ddlSubGrupo2" Width="240px" runat="server" ToolTip="Selecione o Subgrupo2">
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSubGrupo2" 
                ErrorMessage="Subgrupo2 deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liDescricaoConta">
            <label for="txtNumConta" title="Número" class="lblObrigatorio labelPixel">Número</label>
            <asp:TextBox ID="txtNumConta" ToolTip="Informe o Número da Conta" runat="server" MaxLength="4" CssClass="txtNumConta"></asp:TextBox>           
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNumConta" 
            ErrorMessage="Número da Conta deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liDescricaoConta" style="margin-top: -5px;">
            <label for="txtDescricaoConta" title="Conta" class="lblObrigatorio labelPixel">Conta</label>
            <asp:TextBox ID="txtDescricaoConta" ToolTip="Informe a Conta" runat="server" CssClass="txtDescricao" MaxLength="50"></asp:TextBox>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDescricaoConta"
                ErrorMessage="Descrição da Conta deve ter no máximo 50 caracteres"
                ValidationExpression="^(.|\s){1,50}$">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescricaoConta" 
                ErrorMessage="Conta deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function($) {
            $(".txtNumConta").mask("?99999");
        };
    </script>
</asp:Content>
