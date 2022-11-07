<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cadastro.aspx.cs" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    Title="Cadastro" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1112_CadastramentoSubGrupoCtaContabil.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 240px; }
        
        /*--> CSS LIs */
        .liDescricao
        {
            clear: both;
            margin-top: 5px;
        }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom: 1px; }
        .txtDescricao { width: 200px; }
        .txtNumSubGrupo { width: 30px; text-align: right; }
        
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
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTipoConta" 
            ErrorMessage="Tipo de Conta deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liDescricao">
            <label for="ddlGrupo" title="Grupo" class="lblObrigatorio labelPixel">Grupo</label>
            <asp:DropDownList ID="ddlGrupo" ToolTip="Selecione o Grupo" Width="240px" runat="server">
            </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlGrupo"
                ErrorMessage="Grupo deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>    
        <li class="liDescricao">
            <label for="txtNumSubGrupo" title="Número" class="lblObrigatorio labelPixel">Número</label>
            <asp:TextBox ID="txtNumSubGrupo" ToolTip="Informe o Número do SubGrupo" MaxLength="3" runat="server" CssClass="txtNumSubGrupo"></asp:TextBox>           
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNumSubGrupo" 
            ErrorMessage="Número do SubGrupo deve ser informado" Display="None">
            </asp:RequiredFieldValidator>
        </li>
        <li class="liDescricao" style="margin-top: -5px;">
            <label for="txtDescricaoSubGrupo" title="Descrição" class="lblObrigatorio labelPixel">Descrição</label>
            <asp:TextBox ID="txtDescricaoSubGrupo" ToolTip="Informe a Descrição" runat="server" CssClass="txtDescricao" MaxLength="50"></asp:TextBox>
            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDescricaoSubGrupo"
                ErrorMessage="Descrição do SubGrupo deve ter no máximo 50 caracteres"
                ValidationExpression="^(.|\s){1,50}$">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescricaoSubGrupo"
                ErrorMessage="Descrição deve ser informada" Display="None">
            </asp:RequiredFieldValidator>
        </li>
    </ul>
    <script type="text/javascript">
        jQuery(function($) {
            $(".txtNumSubGrupo").mask("?9999");
        };
    </script>
</asp:Content>
