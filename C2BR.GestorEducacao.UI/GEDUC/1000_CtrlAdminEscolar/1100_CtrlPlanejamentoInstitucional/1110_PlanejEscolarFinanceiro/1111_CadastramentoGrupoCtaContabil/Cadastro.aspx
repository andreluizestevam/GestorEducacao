<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1111_CadastramentoGrupoCtaContabil.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 216px; }
        
        /*--> CSS LIs */
        .liDescricao
        {
            clear: both;
            margin-top: 5px;
        }
        
        /*--> CSS DADOS */        
        .labelPixel { margin-bottom: 1px; }        
        .txtDescricao { width: 200px; }
        .txtNumGrupo { width: 30px; text-align: right; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="ddlTipoConta" title="Tipo de Conta" class="lblObrigatorio labelPixel">Tipo de Conta</label>
            <asp:DropDownList ID="ddlTipoConta" ToolTip="Selecione o Tipo de Conta" Width="190px" runat="server">
                <asp:ListItem Value="">Selecione</asp:ListItem>
                <asp:ListItem Value="A">1 - Ativo</asp:ListItem>
                <asp:ListItem Value="B">2 - Passivo</asp:ListItem>
                <asp:ListItem Value="C">3 - Entradas/Saídas e Compensações</asp:ListItem>
                <asp:ListItem Value="D">4 - Entradas e Custos</asp:ListItem>
                <asp:ListItem Value="E">5 - Despesas</asp:ListItem>
                <asp:ListItem Value="F">6 - Receita</asp:ListItem>                
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTipoConta" 
            ErrorMessage="Tipo de Conta deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liDescricao">
            <label for="txtNumGrupo" title="Número" class="lblObrigatorio labelPixel">Número</label>
            <asp:TextBox ID="txtNumGrupo" ToolTip="Informe o Número do Grupo" runat="server" MaxLength="2" CssClass="txtNumGrupo"></asp:TextBox>           
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNumGrupo" 
            ErrorMessage="Número do Grupo deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liDescricao" style="margin-top: -5px;">
            <label for="txtDescricaoGrupo" title="Descrição" class="lblObrigatorio labelPixel">Descrição</label>
            <asp:TextBox ID="txtDescricaoGrupo" ToolTip="Informe a Descrição" runat="server" CssClass="txtDescricao" MaxLength="60"></asp:TextBox>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDescricaoGrupo"
                ErrorMessage="Descrição do Grupo deve ter no máximo 60 caracteres"
                ValidationExpression="^(.|\s){1,60}$">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescricaoGrupo" 
                ErrorMessage="Descrição deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function($) {
            $(".txtNumGrupo").mask("?999");
        };
    </script>
</asp:Content>
