<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App_Masters/PadraoCadastros.Master"
    CodeBehind="Cadastro.aspx.cs" Inherits="C2BR.GestorEducacao.UI.GEDUC.F3000_CtrlOperacionalPedagogico.F3800_CtrlEncerramentoLetivo.F3810_CtrlRegioesEnsino.F3812_CadastramentoRegiaoEquipeEnsino.Cadastro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .ulDados { width: 265px; }
        
        /*--> CSS LIs */
        .liClear { clear: both; }
        .liClear2 
        {
        	margin-top: 5px !important;
            clear: both;
        }
        .liDataI, .liDataS
        {
            margin-top: -5px !important;
            clear: both;
        }
        .liDataF
        {
            margin-top: -5px !important;
            margin-left: 10px;
        }
        .liDataC
        {        	
            clear: both;
            margin-top: -5px !important;
        }
        
        /*--> CSS DADOS */
        .labelPixel { margin-bottom: 1px; }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <ul id="ulDados" class="ulDados">
        <li class="liClear">
            <label for="ddlNucleo" class="lblObrigatorio" title="Núcleo">
                Núcleo</label>
            <asp:DropDownList ID="ddlNucleo" ToolTip="Selecione um Núcleo" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlNucleo"
                CssClass="validatorField" ErrorMessage="Núcleo deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear2">
            <label for="ddlFuncionario" class="lblObrigatorio" title="Funcionário">
                Funcionário</label>
            <asp:DropDownList ID="ddlFuncionario" ToolTip="Selecione um Funcionário" runat="server" CssClass="campoNomePessoa">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlFuncionario"
                CssClass="validatorField" ErrorMessage="Funcionário deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear2">
            <label for="txtFuncao" class="lblObrigatorio labelPixel" title="Função">Função</label>
            <asp:DropDownList ID="ddlFuncao" ToolTip="Selecione uma Função" runat="server">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFuncao"
                CssClass="validatorField" ErrorMessage="Função deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liClear2">
            <label for="txtTelefone" class="lblObrigatorio" title="Telefone">
                Telefone</label>
            <asp:TextBox ID="txtTelefone" ToolTip="Informe o Telefone" runat="server" MaxLength="10" 
                CssClass="campoTelefone" ></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTelefone"
                ErrorMessage="Telefone deve ter no máximo 10 caracteres" Text="*" ValidationExpression="^(.|\s){1,60}$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTelefone"
                CssClass="validatorField" ErrorMessage="Telefone deve ser informado" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liDataI">
            <label for="txtDataI" class="lblObrigatorio" title="Data de Inicio da Atividade">
                Inicio Atividade</label>
            <asp:TextBox ID="txtDataI" ToolTip="Informe a Data de Inicio da Atividade" runat="server" MaxLength="8" CssClass="campoData"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDataI"
                CssClass="validatorField" ErrorMessage="Data de Inicio deve ser informada"
                Text="*" Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liDataF">
            <label for="txtDataF" title="Data Final da Atividade" >
                Fim Atividade</label>
            <asp:TextBox ID="txtDataF" ToolTip="Informe a Data Final da Atividade" runat="server" MaxLength="8" CssClass="campoData"></asp:TextBox>
            
        </li>
        <li class="liDataC">
            <label for="txtDataCadastro" title="Data de Cadastro">
                Data de Cadastro</label>
            <asp:TextBox ID="txtDataCadastro" ToolTip="Informe a Data de Cadastro" runat="server" CssClass="campoData" Enabled="false" MaxLength="8"></asp:TextBox>
        </li>
        <li class="liDataS">
            <label for="txtDataS" class="lblObrigatorio" title="Data Situação">
                Data Situação</label>
            <asp:TextBox ID="txtDataS" Enabled="False" ToolTip="Informe a Data da Situação" runat="server" MaxLength="8" CssClass="campoData"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDataS"
                CssClass="validatorField" ErrorMessage="Data Status deve ser informada" Text="*"
                Display="Static"></asp:RequiredFieldValidator>
        </li>
        <li class="liDataF">
            <label for="txtSituacao" class="lblObrigatorio" title="Status">
                Status</label>
            <asp:DropDownList ID="ddlSituacao" runat="server" ToolTip="Selecione o Status">
                <asp:ListItem Text="Ativo" Value="A" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
            </asp:DropDownList>
        </li>
    </ul>

    <script type="text/javascript">
        jQuery(function($) {
            $(".campoTelefone").mask("(99)9999-9999");
        });
    </script>

</asp:Content>
