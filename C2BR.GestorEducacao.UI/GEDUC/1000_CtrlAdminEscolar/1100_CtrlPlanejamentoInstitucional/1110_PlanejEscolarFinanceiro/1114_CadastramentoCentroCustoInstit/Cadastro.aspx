<%@ Page Language="C#" MasterPageFile="~/App_Masters/PadraoCadastros.Master" AutoEventWireup="true"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F1000_CtrlAdminEscolar.F1100_CtrlPlanejamentoInstitucional.F1110_PlanejEscolarFinanceiro.F1114_CadastramentoCentroCustoInstit.Cadastro"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 230px; }
        
        /*--> CSS LIs */
        .liSigla
        {
            clear: both;
            margin-top: -5px !important;
        }
        .liGrupo
        {
            clear: both;
            margin-top: -5px !important;
        }
        .liSituacao
        {
            clear: both;
            margin-top: 5px !important;
        }
        .liDescricao
        {
            clear: both;
            margin-top: -5px !important;
        }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom: 1px; }        
        .txtSigla { width: 50px; }
        .txtDescricao { width: 200px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li>
            <label for="txtNumConta" title="Número da Conta" class="lblObrigatorio">
                Num. Conta</label>
            <asp:TextBox ID="txtNumConta" ToolTip="Informe o Número da Conta" runat="server" CssClass="tamCampo" Width="70px" MaxLength="12"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumConta"
                ErrorMessage="Num. Conta deve ter no máximo 12 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNumConta"
                ErrorMessage="Num. Conta deve ser informada" CssClass="validatorField" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liDescricao">
            <label for="txtDescricao" title="Descrição do Centro de Custo" class="lblObrigatorio labelPixel">
                Descrição do Centro de Custo</label>
            <asp:TextBox ID="txtDescricao" ToolTip="Informe a Descrição do Centro de Custo" runat="server" CssClass="txtDescricao" MaxLength="60"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revDescricao" runat="server" ControlToValidate="txtDescricao"
                ErrorMessage="Descrição do Centro de Custo deve ter no máximo 60 caracteres"
                Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvDescricao" CssClass="validatorField" runat="server"
                ControlToValidate="txtDescricao" ErrorMessage="Descrição do Centro de Custo deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liSigla">
            <label for="txtSigla" title="Sigla" class="lblObrigatorio labelPixel">
                Sigla</label>
            <asp:TextBox ID="txtSigla" ToolTip="Informe a Sigla" runat="server" CssClass="txtSigla" MaxLength="6"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ter no máximo 6 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"
                Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSigla"
                ErrorMessage="Sigla deve ser informada" CssClass="validatorField" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liGrupo">
            <label for="txtCorrecaoParcela" title="Departamento" class="lblObrigatorio labelPixel">
                Departamento</label>
            <asp:DropDownList ID="ddlDepartamento" ToolTip="Selecione o Departamento" Width="220px" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDepartamento"
                ErrorMessage="Departamento deve ser informado" CssClass="validatorField" Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liSituacao">
            <label for="txtReceberValorEntrada" title="Situação" class="lblObrigatorio labelPixel">
                Situação</label>
            <asp:DropDownList ID="ddlSituacao" ToolTip="Selecione a Situação" runat="server" Width="60px">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>

    <script type="text/javascript">
        jQuery(function($) {
            $(".tamCampo").mask("9.99.99.9999");
        });
    </script>

</asp:Content>
